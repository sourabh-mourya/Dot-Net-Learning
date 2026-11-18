# Background Jobs in ASP.NET Core (Hangfire)

## 1. Background Job kya hai?

Background job aisa kaam hai jo **user ki request ke dauran nahi, request ke baad (ya alag se)** execute hota hai.

Agar slow kaam request ke beech me hi karenge, to request complete hone me bahut time lagega aur user ko wait karna padega.

**Example:** Admin ne employee create kiya. Ab us employee ki salary slip generate karni hai aur welcome email bhejna hai. Ye kaam API response return hone ke baad background me ho sakta hai.

---

## 2. Bina background job ke problem

User "Register" dabata hai, aur API ye sab karti hai:

1. User ko DB me save karna (50 ms)
2. Welcome email bhejna (3 sec)
3. Admin ko notification bhejna (2 sec)
4. Response return karna

- User ko response **5+ sec baad** milta hai.
- Email server down ho to request fail ho sakti hai, jabki user DB me save ho chuka hota hai.

## 3. Background job ke saath solution

1. User ko DB me save karna (50 ms)
2. Email aur notification ko **queue me daal dena** (sirf ek chhoti entry)
3. Turant response return karna (~100 ms)

---

## 4. Teen hisse

```
Producer ──► Queue (Storage) ──► Worker
```

| Hissa | Kaam |
|---|---|
| **Producer** | API/controller, jo bolta hai "ye kaam karna hai" |
| **Queue / Storage** | Kaam ki list, jo DB (MySQL, Redis, SQL Server) me save hoti hai |
| **Worker** | Background thread jo list se kaam uthakar execute karta hai |

---

## 5. ASP.NET Core me approaches

| Approach | Kab use karte hain |
|---|---|
| **IHostedService** | App ke start/stop ke saath chalne wale background tasks ka lifecycle manage karne ke liye |
| **BackgroundService** | Long-running ya continuous workers ke liye (ye `IHostedService` ka ready-made base class hai) |
| **Hangfire** | Fire-and-forget, delayed, recurring aur persistent jobs, dashboard aur retry ke saath |
| **Quartz.NET** | Advanced scheduling, cron expressions aur triggers ke liye |

> `Task.Run`, `IHostedService` ya `BackgroundService` me jobs memory me hote hain. App restart ya crash par pending kaam gayab ho jata hai. Hangfire jobs ko DB me save karta hai, isliye wo restart ke baad bhi bachte hain.

---

## 6. Hangfire ke job types

| Type | Method | Matlab |
|---|---|---|
| Fire-and-forget | `Enqueue` | Abhi queue me daalo, free worker turant chala dega |
| Delayed | `Schedule` | Itni der baad chalao (jaise 10 min baad reminder) |
| Recurring | `AddOrUpdateRecurring` | Cron ke hisaab se baar baar chalao (jaise roz 12 baje) |

---

## 7. Existing project me Hangfire kaise implement karein?

### Step 1: NuGet packages install karo

```powershell
dotnet add package Hangfire.AspNetCore
dotnet add package Hangfire.MySqlStorage
```

(SQL Server use kar rahe ho to `Hangfire.SqlServer` install karo.)

### Step 2: Database banao

Hangfire apni **tables** khud bana leta hai, lekin **database** khud nahi banata.

```sql
CREATE DATABASE hangfire_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

Tum chaaho to apna hi existing project database use kar sakte ho, tab alag DB banane ki zarurat nahi.

### Step 3: Connection string (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "HangfireConnection": "Server=localhost;Port=3306;Database=hangfire_db;User ID=root;Password=YOUR_PASSWORD;Allow User Variables=True;"
  }
}
```

`Allow User Variables=True` MySQL ke liye zaroori hai.

### Step 4: `Program.cs` me Hangfire register karo

```csharp
using Hangfire;
using Hangfire.MySql;

var connectionString = builder.Configuration.GetConnectionString("HangfireConnection")
    ?? throw new InvalidOperationException("HangfireConnection missing hai.");

// Hangfire configuration
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseStorage(new MySqlStorage(connectionString, new MySqlStorageOptions
    {
        TablesPrefix = "Hangfire",
        PrepareSchemaIfNecessary = true
    })));

// Background server (jobs yahi execute karta hai)
builder.Services.AddHangfireServer(options =>
{
    options.WorkerCount = Math.Max(1, Environment.ProcessorCount * 2);
    options.Queues = new[] { "default" };
});
```

Pipeline me dashboard add karo (`var app = builder.Build();` ke baad):

```csharp
app.UseHangfireDashboard("/hangfire");
```

### Step 5: Wrapper service banao (optional, par clean hai)

Controller seedha Hangfire par depend na kare, isliye ek interface banao:

```csharp
public interface IBackroundService
{
    string Enqueue<T>(Expression<Func<T, Task>> job);
    string Schedule<T>(Expression<Func<T, Task>> job, TimeSpan delay);
    void AddOrUpdateRecurring<T>(string jobId, Expression<Func<T, Task>> job, string cron);
}
```

Implementation me:

- `IBackgroundJobClient` -> `Enqueue` aur `Schedule` ke liye
- `IRecurringJobManager` -> recurring jobs ke liye

Dono Hangfire khud DI me register karta hai. Apni service register karo:

```csharp
builder.Services.AddScoped<IBackroundService, HanfireBackgroundJobService>();
```

### Step 6: Job class banao

```csharp
public class EmployeeJobs
{
    private readonly ILogger<EmployeeJobs> _logger;

    // Job class DI se banti hai, isliye DbContext / services yahan inject kar sakte ho
    public EmployeeJobs(ILogger<EmployeeJobs> logger)
    {
        _logger = logger;
    }

    public async Task SendWelcomeEmailAsync(int employeeId)
    {
        _logger.LogInformation("Welcome email start, EmployeeId: {Id}", employeeId);
        // 1. DB se employee load karo
        // 2. Email bhejo
        await Task.Delay(2000);
        _logger.LogInformation("Welcome email done, EmployeeId: {Id}", employeeId);
    }

    public async Task GenerateSalarySlipAsync(int employeeId)
    {
        // salary slip generate karne ka logic
        await Task.Delay(2000);
    }
}
```

Register karo:

```csharp
builder.Services.AddTransient<EmployeeJobs>();
```

### Step 7: Controller se job queue karo

```csharp
[HttpPost]
public async Task<IActionResult> CreateEmployee(EmployeeDto dto)
{
    var employee = await _employeeService.CreateAsync(dto);   // 1. DB me save

    // 2. Background jobs queue me daalo (turant return ho jata hai)
    _jobs.Enqueue<EmployeeJobs>(j => j.SendWelcomeEmailAsync(employee.Id));
    _jobs.Enqueue<EmployeeJobs>(j => j.GenerateSalarySlipAsync(employee.Id));

    return Ok(employee);                                       // 3. Fast response
}
```

Delayed aur recurring ke examples:

```csharp
// 10 minute baad
_jobs.Schedule<EmployeeJobs>(j => j.SendWelcomeEmailAsync(employee.Id), TimeSpan.FromMinutes(10));

// Roz midnight UTC (Program.cs me app.Build() ke baad)
using (var scope = app.Services.CreateScope())
{
    var bg = scope.ServiceProvider.GetRequiredService<IBackroundService>();
    bg.AddOrUpdateRecurring<EmployeeJobs>("daily-report", j => j.GenerateSalarySlipAsync(0), Cron.Daily());
}
```

### Step 8: Run karke check karo

```powershell
dotnet run
```

- Console me job ke logs dikhenge (`Information` level).
- Dashboard: `http://localhost:5100/hangfire` (port apne project ka dekhna).
- Pehli run par `Hangfire_*` tables khud ban jayengi.

---

## 8. Hangfire andar se kaise kaam karta hai

```
Controller ──► Enqueue(...)
                   │  expression ko parse karke {Type, Method, Args} JSON me save
                   ▼
            MySQL (Hangfire_* tables)
                   ▲
                   │ har ~5 sec poll
         Hangfire Server (worker threads)
                   │
                   ▼
   DI se job class ka object banata hai → method call → Succeeded / Failed
```

Job states: **Enqueued → Processing → Succeeded**. Fail hone par **Failed**, phir retry me **Scheduled**.

---

## 9. Important rules

1. **Arguments simple rakho.** `string`, `int`, `Guid` ya chhota DTO pass karo. `DbContext`, `HttpContext` ya bade objects kabhi pass mat karo, kyunki arguments JSON me save hote hain. Employee ke case me `employeeId` pass karo aur job ke andar DB se data load karo.
2. **Job idempotent likho.** Retry ya crash ki wajah se job do baar chal sakta hai, to double email ya double payment jaisa galat result na ho.
3. **Retry control karo:**
   ```csharp
   [AutomaticRetry(Attempts = 3)]
   public class EmployeeJobs { ... }
   ```
4. **Recurring job ID fixed rakho.** `AddOrUpdate` same ID par update karta hai, isliye restart par duplicate nahi banta.
5. **Dashboard ko protect karo.** Default me sirf localhost se khulta hai. Production me authorization filter lagao:
   ```csharp
   public class HangfireAuthFilter : IDashboardAuthorizationFilter
   {
       public bool Authorize(DashboardContext context)
       {
           var http = context.GetHttpContext();
           return http.User.Identity?.IsAuthenticated == true && http.User.IsInRole("Admin");
       }
   }

   app.UseHangfireDashboard("/hangfire", new DashboardOptions
   {
       Authorization = new[] { new HangfireAuthFilter() }
   });
   ```
6. **Same database/prefix.** Agar API aur worker alag apps me chalaoge, to dono ka connection string aur `TablesPrefix` same rakho.

---