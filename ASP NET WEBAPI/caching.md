# Caching in ASP.NET Core

## What is Caching?

Baar-baar same data DB se laane ki jagah, use temporarily fast storage me rakhna.
Isse response time kam hota hai aur DB pe load kam padta hai.

## 1. In-Memory Caching (IMemoryCache)

- Data app ki apni RAM me store hota hai (same process).
- `IMemoryCache` interface se implement hota hai.
- Sabse fast, kyunki network call nahi lagti.
- App restart hone par data chala jata hai.
- Multiple servers ho to har server ka apna alag cache hota hai (data inconsistent ho sakta hai).

## 2. Distributed Caching (IDistributedCache)

- Cache app ke bahar ek alag server pe store hota hai.
- Saare app instances same cache share karte hain.
- App restart par bhi data safe rehta hai.
- Network call lagti hai, isliye in-memory se thoda slow.
- Redis ke through implement hota hai (SQL Server, NCache bhi options hain).

## 3. Redis kya hai?

- Open source, in-memory, NoSQL (key-value) data store.
- Bahut fast hai, kyunki data RAM me rehta hai.
- Caching ke alawa session store, rate limiting, pub/sub, queues, leaderboards me bhi use hota hai.
- Replication aur clustering support karta hai (high availability).
- Data types: String, List, Set, Hash, Sorted Set.

## 4. Hybrid Cache (.NET 9+)

- In-memory (L1) + Redis (L2) ka combination.
- Pehle local memory check, phir Redis, phir DB.
- Cache stampede protection built-in: same key ke liye 100 requests aayein to DB pe sirf 1 jaati hai.

---

# 3. In-Memory Caching (`IMemoryCache`)

Koi extra package install nahi karna padta, ASP.NET Core ke saath built-in aata hai.

### Step 1: `Program.cs` me register karo

```csharp
builder.Services.AddMemoryCache();
```

### Step 2: Service/Controller me inject karo

```csharp
public class EmployeeService
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _cache;

    public EmployeeService(ApplicationDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }
}
```

### Step 3: `_cache` se use karo

```csharp
// SET: "employees" key hai (double quotes me likha naam), 5 min baad expire
_cache.Set("employees", employees, TimeSpan.FromMinutes(5));

// GET
if (_cache.TryGetValue("employees", out List<Employee>? cached))
{
    return cached;
}

// REMOVE
_cache.Remove("employees");
```

### Short tarika: `GetOrCreateAsync`

```csharp
var employees = await _cache.GetOrCreateAsync("employees", async entry =>
{
    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
    return await _context.Employees.ToListAsync();
});
```

> **Absolute vs Sliding expiry:** Absolute = fixed time baad expire. Sliding = jab tak use hota rahe tab tak zinda, unused rehne par expire.

### In-Memory ki limitations

- App restart hone par data chala jata hai
- Multiple servers me har server ka apna alag cache hota hai (data inconsistent ho sakta hai)

---

# 4. Redis Cache (Distributed)

## 4.1 Redis kya hai?

- Open source, in-memory, NoSQL **key-value** data store
- Data RAM me rehta hai, isliye bahut fast
- Caching ke alawa: session store, rate limiting, pub/sub, queues, leaderboards
- Replication aur clustering support (high availability)
- Data types: String, List, Set, Hash, Sorted Set

**Redis local chalane ke liye (Docker):**

```bash
docker run -d --name redis -p 6379:6379 redis
```

## 4.2 Key design ke 2 tarike

| Tarika | Matlab | Example |
| ------ | ------ | ------- |
| **Project based** | 1 key me bahut saare records (poori list) | `Employees` -> saare employees |
| **User/Record based** | 1 key me sirf 1 record | `Employee_5` -> sirf id 5 |

Dono saath use hote hain: list ke liye `Employees`, single record ke liye `Employee_{id}`.

## 4.3 Redis se connect karne ke 3 tarike

| # | Tarika | Package | Kab use karein |
| - | ------ | ------- | -------------- |
| 1 | `IDistributedCache` | `Microsoft.Extensions.Caching.StackExchangeRedis` | Simple get/set cache, sabse kam code. HybridCache ka L2 bhi yahi hai |
| 2 | Direct connection (`IConnectionMultiplexer`) | `StackExchange.Redis` | Poora Redis control chahiye: Hash, List, Set, Pub/Sub, `INCR` (rate limiting) |
| 3 | Static `ConnectionHelper` | `StackExchange.Redis` | Purana tarika, DI nahi hai. Naye project me tarika 2 behtar |

> **Important:** `IDistributedCache` (tarika 1) Redis me data **Hash** ki tarah store karta hai (`absexp`, `sldexp`, `data` fields ke saath). Direct `StringGet` se is key ko padhoge to `WRONGTYPE` error aayega. Isliye ek hi key ke liye dono tarike mix mat karo.

---

## 4.4 Tarika 1: `IDistributedCache`

Direct connection nahi banana ho to ye sabse aasan hai.

### Step 1: Package install

```bash
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
```

### Step 2: `appsettings.json`

```json
{
  "ConnectionStrings": {
    "RedisConnection": "localhost:6379"
  }
}
```

### Step 3: `Program.cs` me register

```csharp
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration
        .GetConnectionString("RedisConnection");

    options.InstanceName = "HybridCaching_";
});
```

- `Configuration` Redis ka address hai.
- `InstanceName` har key ke aage prefix laga deta hai. `Employees` key Redis me `HybridCaching_Employees` ban jati hai. Isse ek hi Redis me kai projects ki keys mix nahi hoti.

### Step 4: Service me inject

```csharp
private readonly IMemoryCache _memoryCache;
private readonly IDistributedCache _distributedCache;
private readonly ApplicationDbContext _dbContext;

public EmployeeService(
    IMemoryCache memoryCache,
    IDistributedCache distributedCache,
    ApplicationDbContext dbContext)
{
    _memoryCache = memoryCache;
    _distributedCache = distributedCache;
    _dbContext = dbContext;
}
```

### Step 5: Redis operations

```csharp
// SET
await _distributedCache.SetStringAsync(
    "Employees",
    JsonSerializer.Serialize(employees),
    new DistributedCacheEntryOptions
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
    });

// GET
var json = await _distributedCache.GetStringAsync("Employees");

if (!string.IsNullOrEmpty(json))
{
    var employees = JsonSerializer.Deserialize<List<Employee>>(json);
}

// REMOVE
await _distributedCache.RemoveAsync("Employees");
```

`IDistributedCache` sirf string/byte data ke saath kaam karta hai, isliye object ko pehle `JsonSerializer.Serialize` se string banate hain aur wapas `Deserialize` karte hain.

---

## 4.5 Tarika 2: Direct Redis connection (`IConnectionMultiplexer`)

### Step 1: Package install

```bash
dotnet add package StackExchange.Redis
```

### Step 2: `appsettings.json`

```json
{
  "ConnectionStrings": {
    "RedisConnection": "localhost:6379"
  }
}
```

### Step 3: `Program.cs` me connection register

```csharp
using StackExchange.Redis;

var redisConnection = builder.Configuration
    .GetConnectionString("RedisConnection");

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(redisConnection!)
);
```

`IConnectionMultiplexer` Redis ke connection ko manage karta hai. Isko **Singleton** register karte hain taaki har request par naya connection na bane (connection banana costly hota hai).

> **Tip:** Redis band ho to app start hote hi crash ho sakta hai. Isse bachne ke liye connection string me `abortConnect=false` lagao: `"localhost:6379,abortConnect=false"`.

### Step 4: Service me inject

```csharp
using StackExchange.Redis;

private readonly IMemoryCache _memoryCache;
private readonly ApplicationDbContext _dbContext;
private readonly IDatabase _redis;

public EmployeeService(
    IMemoryCache memoryCache,
    ApplicationDbContext dbContext,
    IConnectionMultiplexer redis)
{
    _memoryCache = memoryCache;
    _dbContext = dbContext;

    _redis = redis.GetDatabase();   // IDatabase se Redis commands chalte hain
}
```

### Step 5: Redis operations

```csharp
// SET
await _redis.StringSetAsync(
    "Employees",
    JsonSerializer.Serialize(employees),
    TimeSpan.FromMinutes(5)
);

// GET
var cachedData = await _redis.StringGetAsync("Employees");

if (cachedData.HasValue)
{
    var employees =
        JsonSerializer.Deserialize<List<Employee>>(cachedData.ToString());

    return employees ?? new List<Employee>();
}

// REMOVE
await _redis.KeyDeleteAsync("Employees");
```

### Tarika 1 vs Tarika 2

| Point | `IDistributedCache` | `IConnectionMultiplexer` |
| ----- | ------------------- | ------------------------ |
| Code | Kam, simple | Thoda zyada |
| Features | Sirf get/set/remove + expiry | Poora Redis (Hash, List, Set, Pub/Sub, `INCR`, etc.) |
| Key prefix | `InstanceName` se automatic | Khud likhna padta hai |
| HybridCache ka L2 | Haan, yahi use hota hai | Nahi |
| Best for | Normal caching | Rate limiting, queues, leaderboards, advanced use |

---

## 4.6 Tarika 3: Static `ConnectionHelper` (purana tarika)

Ye `Distrubute_Caching_Redis_` project me use hua tha. DI ke bina static class se connection banta hai.

### `ConfigurationManager.cs` (settings load karne ke liye)

```csharp
using Microsoft.Extensions.Configuration;

namespace Distrubute_Caching_Redis_
{
    public class ConfigurationManager
    {
        public static IConfiguration AppSettings { get; }

        static ConfigurationManager()
        {
            AppSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}
```

`appsettings.json`:

```json
{
  "RedisURL": "127.0.0.1:6379"
}
```

> **Dhyan rakho:** `Microsoft.Extensions.Configuration` me bhi ek `ConfigurationManager` class hai. Naam clash ho to class ka naam `AppConfig` jaisa kuch rakh lo.

### `ConnectionHelper.cs`

```csharp
using StackExchange.Redis;

namespace Distrubute_Caching_Redis_.Cache
{
    public class ConnectionHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection =
            new Lazy<ConnectionMultiplexer>(() =>
            {
                string redisUrl =
                    ConfigurationManager.AppSettings["RedisURL"]
                    ?? throw new InvalidOperationException("RedisURL not found.");

                return ConnectionMultiplexer.Connect(redisUrl);
            });

        public static ConnectionMultiplexer Connection => lazyConnection.Value;
    }
}
```

`Lazy` se connection sirf pehli baar bante hai aur poori app me ek hi connection reuse hota hai.

### `ICacheService.cs`

```csharp
namespace Distrubute_Caching_Redis_.Cache
{
    public interface ICacheService
    {
        Task<T?> GetData<T>(string key);
        Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime);
        Task<bool> RemoveData(string key);
    }
}
```

### `CacheService.cs`

```csharp
using StackExchange.Redis;
using System.Text.Json;

namespace Distrubute_Caching_Redis_.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase redisDb;

        public CacheService()
        {
            redisDb = ConnectionHelper.Connection.GetDatabase();
        }

        public async Task<T?> GetData<T>(string key)
        {
            var value = await redisDb.StringGetAsync(key);

            if (value.IsNullOrEmpty)
            {
                Console.WriteLine($"[REDIS MISS] Key: {key}");
                return default;
            }

            Console.WriteLine($"[REDIS HIT] Key: {key}");
            return JsonSerializer.Deserialize<T>(value.ToString());
        }

        public async Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var serializedData = JsonSerializer.Serialize(value);
            var expiration = expirationTime - DateTimeOffset.UtcNow;

            if (expiration <= TimeSpan.Zero)
            {
                Console.WriteLine("[REDIS] Expiration time must be in the future.");
                return false;
            }

            bool result = await redisDb.StringSetAsync(key, serializedData, expiration);
            Console.WriteLine($"[REDIS SET] Key: {key}, Result: {result}");
            return result;
        }

        public async Task<bool> RemoveData(string key)
        {
            bool result = await redisDb.KeyDeleteAsync(key);
            Console.WriteLine($"[REDIS DELETE] Key: {key}, Result: {result}");
            return result;
        }
    }
}
```

### `Program.cs`

```csharp
builder.Services.AddScoped<ICacheService, CacheService>();
```

### Controller (sirf Redis, Memory nahi)

```csharp
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly ICacheService _cacheService;
    private readonly ApplicationDbContext _dbContext;

    public EmployeeController(ICacheService cacheService, ApplicationDbContext dbContext)
    {
        _cacheService = cacheService;
        _dbContext = dbContext;
    }

    // GET api/Employee/Employees
    [HttpGet("Employees")]
    public async Task<IActionResult> GetAll()
    {
        var cachedData = await _cacheService.GetData<IEnumerable<Employee>>("Employees");

        if (cachedData != null)                                  // Cache HIT
            return Ok(cachedData);

        cachedData = await _dbContext.Employees.ToListAsync();   // Cache MISS -> DB

        await _cacheService.SetData("Employees", cachedData, DateTimeOffset.UtcNow.AddMinutes(5));
        return Ok(cachedData);
    }

    // GET api/Employee/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        string cacheKey = $"Employee_{id}";

        var employee = await _cacheService.GetData<Employee>(cacheKey);
        if (employee != null)
            return Ok(employee);

        employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null)
            return NotFound("Employee not found.");

        await _cacheService.SetData(cacheKey, employee, DateTimeOffset.UtcNow.AddMinutes(5));
        return Ok(employee);
    }

    // POST api/Employee
    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();

        await _cacheService.RemoveData("Employees");   // list purani ho gayi

        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    // PUT api/Employee/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Employee employee)
    {
        if (id != employee.Id)
            return BadRequest("Employee ID mismatch.");

        var existing = await _dbContext.Employees.FindAsync(id);
        if (existing == null)
            return NotFound("Employee not found.");

        _dbContext.Entry(existing).CurrentValues.SetValues(employee);
        await _dbContext.SaveChangesAsync();

        await _cacheService.RemoveData("Employees");
        await _cacheService.RemoveData($"Employee_{id}");

        return NoContent();
    }

    // DELETE api/Employee/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _dbContext.Employees.FindAsync(id);
        if (employee == null)
            return NotFound("Employee not found.");

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();

        await _cacheService.RemoveData("Employees");
        await _cacheService.RemoveData($"Employee_{id}");

        return NoContent();
    }
}
```

| Operation | Cache ka kaam |
| --------- | ------------- |
| GET (all / by id) | Pehle Redis check, miss par DB se lao aur 5 min ke liye Redis me save |
| POST | DB me add, `Employees` list cache **invalidate** |
| PUT | DB update, `Employees` + `Employee_{id}` dono invalidate |
| DELETE | DB se delete, `Employees` + `Employee_{id}` dono invalidate |

---

# 5. Manual Hybrid Cache (Memory L1 + Redis L2, khud ka code)

Yahan hum **khud** Memory aur Redis ko jodte hain. Ye `Hybrid_Caching` project ka pehla version hai.

## 5.1 Idea

```
Request
  |
  |-- L1: IMemoryCache me hai?   -> HIT, seedha return (sabse fast)
  |
  |-- L2: Redis me hai?          -> HIT, L1 me bhi copy karo, return
  |
  |-- Dono me nahi (MISS)        -> MySQL se lao
                                    -> Redis me save + Memory me save -> return
```

## 5.2 Common code (teeno implementations ke liye same)

`IEmployeeService`, `EmployeeController` aur `Program.cs` ke baaki hisse **same** rehte hain. Sirf `EmployeeService` ke andar ka code badalta hai (manual Memory+Redis ya built-in HybridCache).

### `IEmployeeService.cs`

```csharp
using Hybrid_Caching.Models;

namespace Hybrid_Caching.Service
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetData(string key);
        Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime);
        Task RemoveData(string key);
    }
}
```

### `EmployeeController.cs`

```csharp
using Hybrid_Caching.Data;
using Hybrid_Caching.Models;
using Hybrid_Caching.Service;
using Microsoft.AspNetCore.Mvc;

namespace Hybrid_Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Saare employees ki list is ek key me cache hoti hai
        private const string EmployeesKey = "Employees";

        private readonly IEmployeeService _employeeService;
        private readonly ApplicationDbContext _dbContext;

        public EmployeeController(
            IEmployeeService employeeService,
            ApplicationDbContext dbContext)
        {
            _employeeService = employeeService;
            _dbContext = dbContext;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetData(EmployeesKey);
            return Ok(employees);
        }

        // GET: api/Employee/1  (cached list se filter hota hai, alag DB query nahi)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employees = await _employeeService.GetData(EmployeesKey);
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound("Employee not found.");

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

            await _employeeService.RemoveData(EmployeesKey);   // cache invalidate

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        // PUT: api/Employee/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest("Employee ID mismatch.");

            var existing = await _dbContext.Employees.FindAsync(id);
            if (existing == null)
                return NotFound("Employee not found.");

            _dbContext.Entry(existing).CurrentValues.SetValues(employee);
            await _dbContext.SaveChangesAsync();

            await _employeeService.RemoveData(EmployeesKey);

            return NoContent();
        }

        // DELETE: api/Employee/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
                return NotFound("Employee not found.");

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            await _employeeService.RemoveData(EmployeesKey);

            return NoContent();
        }

        // DELETE: api/Employee/cache  (testing ke liye)
        [HttpDelete("cache")]
        public async Task<IActionResult> ClearCache()
        {
            await _employeeService.RemoveData(EmployeesKey);
            return NoContent();
        }
    }
}
```

## 5.3 Service Version A: `IMemoryCache` + `IDistributedCache`

```csharp
using Hybrid_Caching.Data;
using Hybrid_Caching.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Hybrid_Caching.Service
{
    public class EmployeeService : IEmployeeService
    {
        // L1 chhota, L2 bada rakho (neeche "Important Points" dekho)
        private static readonly TimeSpan MemoryExpiry = TimeSpan.FromMinutes(2);
        private static readonly TimeSpan RedisExpiry = TimeSpan.FromMinutes(5);

        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(
            IMemoryCache memoryCache,
            IDistributedCache distributedCache,
            ApplicationDbContext dbContext)
        {
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
            _dbContext = dbContext;
        }

        // GET: Memory -> Redis -> Database
        public async Task<List<Employee>> GetData(string key)
        {
            // 1. L1: In-Memory
            if (_memoryCache.TryGetValue(key, out List<Employee>? employees)
                && employees != null)
            {
                Console.WriteLine("[MEMORY HIT] Data found in memory cache");
                return employees;
            }

            Console.WriteLine("[MEMORY MISS] Checking Redis...");

            // 2. L2: Redis
            var cachedValue = await _distributedCache.GetStringAsync(key);

            if (!string.IsNullOrEmpty(cachedValue))
            {
                Console.WriteLine("[REDIS HIT] Data found in Redis");

                employees = JsonSerializer.Deserialize<List<Employee>>(cachedValue);

                if (employees != null)
                {
                    // Redis se L1 me bhi copy kar do (agli baar memory se milega)
                    _memoryCache.Set(key, employees, MemoryExpiry);
                    Console.WriteLine("[MEMORY SET] Data copied from Redis to memory");

                    return employees;
                }
            }

            // 3. MISS: MySQL se lao
            Console.WriteLine("[REDIS MISS] Fetching data from MySQL...");

            employees = await _dbContext.Employees.AsNoTracking().ToListAsync();

            // Redis me save
            await _distributedCache.SetStringAsync(
                key,
                JsonSerializer.Serialize(employees),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = RedisExpiry
                });
            Console.WriteLine("[REDIS SET] Data stored in Redis");

            // Memory me save
            _memoryCache.Set(key, employees, MemoryExpiry);
            Console.WriteLine("[MEMORY SET] Data stored in memory");

            return employees;
        }

        // SET: Memory aur Redis dono me
        public async Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiration = expirationTime - DateTimeOffset.UtcNow;

            if (expiration <= TimeSpan.Zero)
                return false;

            _memoryCache.Set(key, value, expiration);

            await _distributedCache.SetStringAsync(
                key,
                JsonSerializer.Serialize(value),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                });

            Console.WriteLine("[CACHE SET] Data stored in Memory and Redis");
            return true;
        }

        // REMOVE: Memory aur Redis dono se
        public async Task RemoveData(string key)
        {
            _memoryCache.Remove(key);
            await _distributedCache.RemoveAsync(key);

            Console.WriteLine($"[CACHE REMOVE] Key: {key} removed from Memory and Redis");
        }
    }
}
```

**`Program.cs` (Version A):**

```csharp
builder.Services.AddMemoryCache();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "HybridCaching_";
});

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
```

## 5.4 Service Version B: `IMemoryCache` + `IConnectionMultiplexer` (direct Redis)

Sirf Redis wale hisse badalte hain, baaki logic same hai.

```csharp
using Hybrid_Caching.Data;
using Hybrid_Caching.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System.Text.Json;

namespace Hybrid_Caching.Service
{
    public class EmployeeService : IEmployeeService
    {
        private static readonly TimeSpan MemoryExpiry = TimeSpan.FromMinutes(2);
        private static readonly TimeSpan RedisExpiry = TimeSpan.FromMinutes(5);

        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationDbContext _dbContext;
        private readonly IDatabase _redis;

        public EmployeeService(
            IMemoryCache memoryCache,
            ApplicationDbContext dbContext,
            IConnectionMultiplexer redis)
        {
            _memoryCache = memoryCache;
            _dbContext = dbContext;
            _redis = redis.GetDatabase();
        }

        public async Task<List<Employee>> GetData(string key)
        {
            // 1. L1: Memory
            if (_memoryCache.TryGetValue(key, out List<Employee>? employees)
                && employees != null)
            {
                Console.WriteLine("[MEMORY HIT]");
                return employees;
            }

            Console.WriteLine("[MEMORY MISS] Checking Redis...");

            // 2. L2: Redis
            var cachedData = await _redis.StringGetAsync(key);

            if (cachedData.HasValue)
            {
                Console.WriteLine("[REDIS HIT]");

                employees = JsonSerializer.Deserialize<List<Employee>>(cachedData.ToString());

                if (employees != null)
                {
                    _memoryCache.Set(key, employees, MemoryExpiry);
                    return employees;
                }
            }

            // 3. MISS: MySQL
            Console.WriteLine("[REDIS MISS] Fetching data from MySQL...");

            employees = await _dbContext.Employees.AsNoTracking().ToListAsync();

            await _redis.StringSetAsync(key, JsonSerializer.Serialize(employees), RedisExpiry);
            _memoryCache.Set(key, employees, MemoryExpiry);

            return employees;
        }

        public async Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiration = expirationTime - DateTimeOffset.UtcNow;

            if (expiration <= TimeSpan.Zero)
                return false;

            _memoryCache.Set(key, value, expiration);
            await _redis.StringSetAsync(key, JsonSerializer.Serialize(value), expiration);

            return true;
        }

        public async Task RemoveData(string key)
        {
            _memoryCache.Remove(key);
            await _redis.KeyDeleteAsync(key);

            Console.WriteLine($"[CACHE REMOVE] Key: {key}");
        }
    }
}
```

**`Program.cs` (Version B):**

```csharp
using StackExchange.Redis;

builder.Services.AddMemoryCache();

var redisConnection = builder.Configuration.GetConnectionString("RedisConnection");

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(redisConnection!)
);

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
```

## 5.5 Console me kya dikhega

| Situation | Console output |
| --------- | -------------- |
| Pehli request | `[MEMORY MISS]` -> `[REDIS MISS]` -> `[REDIS SET]` -> `[MEMORY SET]` |
| Dobara request | `[MEMORY HIT]` |
| App restart ke baad | `[MEMORY MISS]` -> `[REDIS HIT]` -> `[MEMORY SET]` |
| POST / PUT / DELETE ke baad | `[CACHE REMOVE]`, phir agli GET me `[REDIS MISS]` |

## 5.6 Manual hybrid ki limitations

- **Cache stampede protection nahi hai.** Cache khali ho aur 100 requests ek saath aayein to 100 DB queries chalengi.
- **Boilerplate zyada hai.** Har method me L1, L2, DB ka code khud likhna padta hai.
- **Multi-server me L1 inconsistent ho sakta hai.** `RemoveData` sirf usi server ka Memory aur shared Redis clear karta hai. Dusre servers ka L1 expiry tak purana data de sakta hai.

Inhi problems ko built-in HybridCache solve karta hai (section 6).

---

# 6. Built-in HybridCache (.NET 9+)

## 6.1 Kya hai?

HybridCache ASP.NET ka built-in feature hai jo in-memory caching ko optional distributed caching (Redis) ke saath jodta hai, taaki API performance badhe.

- Baar-baar hone wali DB queries kam hoti hain
- Response fast hota hai
- Ek hi data ke liye kai concurrent requests aayein to sirf **1 request** DB tak jati hai (stampede protection)

Section 5 me jo L1/L2 ka code khud likha tha, wo sab yahan **ek method** (`GetOrCreateAsync`) me ho jata hai.

## 6.2 Step 1: Package install

```bash
dotnet add package Microsoft.Extensions.Caching.Hybrid
```

## 6.3 Step 2: `Program.cs` me register

**Sirf L1 (Memory) ke saath:**

```csharp
builder.Services.AddHybridCache();
```

> **Dhyan rakho:** Sirf `AddHybridCache()` likhne par cache **sirf memory (L1)** me chalega. Redis (L2) tabhi use hoga jab `IDistributedCache` registered ho. Isliye Redis bhi register karo.

**L1 + L2 (Redis) ke saath (recommended):**

```csharp
// L2: Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "HybridCaching_";
});

// L1 + L2: HybridCache
builder.Services.AddHybridCache(options =>
{
    options.MaximumPayloadBytes = 1024 * 1024;   // max 1 MB per entry

    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(5),            // Redis (L2) me kitni der
        LocalCacheExpiration = TimeSpan.FromMinutes(2)   // Memory (L1) me kitni der
    };
});

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
```

## 6.4 Step 3: `EmployeeService`

```csharp
using Hybrid_Caching.Data;
using Hybrid_Caching.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace Hybrid_Caching.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HybridCache _hybridCache;
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(
            HybridCache hybridCache,
            ApplicationDbContext dbContext)
        {
            _hybridCache = hybridCache;
            _dbContext = dbContext;
        }

        // GET: L1 -> L2 -> DB (sab ek method me)
        public async Task<List<Employee>> GetData(string key)
        {
            return await _hybridCache.GetOrCreateAsync(
                key,
                async cancellationToken =>
                {
                    // Ye sirf tab chalta hai jab L1 aur L2 dono me data nahi mila
                    Console.WriteLine("[CACHE MISS] Fetching from MySQL");

                    return await _dbContext.Employees
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                },
                new HybridCacheEntryOptions
                {
                    Expiration = TimeSpan.FromMinutes(5),
                    LocalCacheExpiration = TimeSpan.FromMinutes(2)
                }
            );
        }

        // SET: L1 aur L2 dono me
        public async Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiration = expirationTime - DateTimeOffset.UtcNow;

            if (expiration <= TimeSpan.Zero)
                return false;

            await _hybridCache.SetAsync(
                key,
                value,
                new HybridCacheEntryOptions
                {
                    Expiration = expiration,
                    LocalCacheExpiration = TimeSpan.FromMinutes(2)
                }
            );

            return true;
        }

        // REMOVE: L1 aur L2 dono se
        public async Task RemoveData(string key)
        {
            await _hybridCache.RemoveAsync(key);

            Console.WriteLine($"[HYBRID CACHE REMOVE] {key}");
        }
    }
}
```

## 6.5 Controller

`IEmployeeService` same hai, isliye **`EmployeeController` me koi change nahi** (section 5.2 wala hi use hoga).

## 6.6 `GetOrCreateAsync` ko samjho

```csharp
await _hybridCache.GetOrCreateAsync(
    key,                      // 1. cache key
    async ct => { ... },      // 2. factory: DB se data lane ka logic (sirf MISS par chalta hai)
    options                   // 3. expiry settings
);
```

**Step 1: DB logic alag se likho (factory)**

```csharp
Func<CancellationToken, ValueTask<List<Employee>>> getFromDb =
    async token => await _dbContext.Employees.AsNoTracking().ToListAsync(token);
```

Ye sirf ek function hai, abhi chala nahi.

**Step 2: Cache ko bolo "pehle cache dekho, nahi mile to ye function chalao"**

```csharp
var employees = await _hybridCache.GetOrCreateAsync(key, getFromDb, options);
```

### `CancellationToken` kya hai?

Ye ek **"ruk jao" ka signal** hai. User ne request bheji, DB query chal rahi hai, aur beech me user ne browser band kar diya. Token ke bina query phir bhi poori chalti hai (resources waste). Token ke saath query beech me hi ruk jati hai.

```csharp
// Controller me (ASP.NET Core khud token bhar deta hai)
public async Task<IActionResult> GetAll(CancellationToken ct) { ... }
```

- **`ct`** controller ka token hai. Isko `GetOrCreateAsync(..., cancellationToken: ct)` me do.
- **`token` / `cancellationToken`** factory (lambda) ka apna token hai, jo HybridCache khud deta hai. Isko DB query me do: `ToListAsync(token)`.

## 6.7 Hybrid kaise kaam karta hai

```
Request
  |
  |-- L1 (local memory) me hai?  -> HIT, seedha return
  |
  |-- L2 (Redis) me hai?         -> HIT, L1 me bhi copy, return
  |
  |-- Dono me nahi (MISS)        -> factory chalao (DB query)
                                    -> L1 + L2 dono me save -> return
```

**Stampede protection:** ek key ke liye cache khali ho aur 100 requests ek saath aayein, to sirf 1 request DB tak jati hai. Baaki 99 uska result ka wait karti hain. Manual hybrid me 100 ki 100 DB pe pahunch jati hain.

## 6.8 Manual vs Built-in HybridCache

| Point | Manual (Memory + Redis) | Built-in HybridCache |
| ----- | ----------------------- | -------------------- |
| Code | Zyada (L1, L2, DB sab khud) | Kam (`GetOrCreateAsync`) |
| Stampede protection | Nahi | Haan |
| Serialization | Khud (`JsonSerializer`) | Automatic |
| .NET version | Koi bhi | .NET 9+ (package se .NET 8 me bhi) |
| Control | Poora control | Kam (framework sambhalta hai) |
| Console me L1 vs L2 dikhana | Aasan (khud log likhte ho) | Mushkil, HybridCache khud nahi batata |

## 6.9 Dhyan dene wali baatein

- **Multi-server me L1 inconsistent ho sakta hai.** `RemoveAsync` Redis se delete karta hai aur sirf usi server ka L1 clear hota hai. Dusre servers ka L1 `LocalCacheExpiration` tak purana data de sakta hai. Isliye `LocalCacheExpiration` chhota rakho.
- **Null result bhi cache hota hai.** Factory `null` return kare to wo bhi cache me chala jata hai (expiry tak).
- **Serialization:** by default `System.Text.Json` use hota hai. Entity me circular navigation properties ho to error aayega. Aise me DTO cache karo.
- **Sensitive data:** key me user id rakho (`Orders_{userId}`), warna ek user ka data dusre ko mil sakta hai.

---

# 7. Rate Limiting

Rate limiting = ek client ko ek time window me limited requests dene ki limit. Isse API abuse, brute-force login aur DDoS jaise attacks se bachav hota hai. Limit cross hone par **HTTP 429 Too Many Requests** milta hai.

## 7.1 Built-in Rate Limiter (.NET 7+)

Koi extra package nahi chahiye.

**`Program.cs`:**

```csharp
using System.Threading.RateLimiting;

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // Policy: 10 second me max 5 requests
    options.AddFixedWindowLimiter("fixed", o =>
    {
        o.PermitLimit = 5;
        o.Window = TimeSpan.FromSeconds(10);
        o.QueueLimit = 0;
    });
});

var app = builder.Build();

app.UseRateLimiter();   // middleware add karna zaruri hai
```

**Controller / endpoint par lagao:**

```csharp
[EnableRateLimiting("fixed")]
[HttpGet("Employees")]
public async Task<IActionResult> GetAll() { ... }

// Kisi action par band karna ho
[DisableRateLimiting]
```

## 7.2 Algorithms

| Algorithm | Kaise kaam karta hai | Kab use karein |
| --------- | -------------------- | -------------- |
| **Fixed Window** | Fixed time window me N requests | Simple cases |
| **Sliding Window** | Window slide hoti hai, burst kam hota hai | Zyada smooth limiting |
| **Token Bucket** | Tokens refill hote hain, burst allowed | Burst traffic allow karna ho |
| **Concurrency** | Ek saath N requests hi chal sakti hain | Heavy/slow endpoints |

## 7.3 Per-IP limiting

```csharp
options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
        factory: _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromMinutes(1)
        }));
```

## 7.4 Redis ke saath rate limiting (multi-server)

Built-in limiter **har server ki apni memory** me count rakhta hai. 3 servers ho to user ko 3 guna limit mil jayegi. Isliye multi-server me Redis use hota hai: counter sabke liye shared rehta hai. Iske liye **direct connection (`IConnectionMultiplexer`, tarika 2)** chahiye, kyunki `INCR` jaisi commands `IDistributedCache` me nahi hoti.

Idea: har request par `INCR` + pehli baar `EXPIRE`.

```csharp
public class RedisRateLimiter
{
    private readonly IDatabase _db;

    public RedisRateLimiter(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task<bool> IsAllowed(string clientId, int limit, TimeSpan window)
    {
        string key = $"ratelimit:{clientId}";

        long count = await _db.StringIncrementAsync(key);

        if (count == 1)                       // pehli request -> window start
            await _db.KeyExpireAsync(key, window);

        return count <= limit;                // false ho to 429 return karo
    }
}
```

> `INCR` aur `EXPIRE` alag commands hain, isliye strictly atomic nahi hain. Production me Lua script ya Redis 7+ ke `ExpireWhen.HasNoExpiry` option se ise atomic banaya jata hai.

---
