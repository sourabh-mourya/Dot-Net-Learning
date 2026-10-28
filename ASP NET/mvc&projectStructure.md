# MVC in ASP.NET Core + Project Folder Structure

> Hinglish notes, interview-oriented. Tumhare original notes ka har point isme hai, bas proper tarike se organize + code examples ke saath.
> Analogy ke liye **MediSetu** (Patient / Doctor / Appointment) use kiya hai.

---

# PART 1: MVC (Model - View - Controller)

## 1.1 MVC kya hai?

- **MVC = Model View Controller.**
- Ye ek **architectural design pattern** hai, yaani application ke **architecture level** pe use hota hai.
- MVC **koi programming language nahi hai**, aur **koi framework bhi nahi hai**. Ye sirf ek **design pattern (soch / tarika)** hai code ko organize karne ka.
- ASP.NET Core MVC is pattern ko **implement** karta hai. (Express me tum same pattern khud manually follow karte ho: `models/`, `controllers/`, `views/`.)
- **Core idea:** Har kaam ko **alag-alag files me separate** karo. Har piece apna ek hi kaam kare.

### Fayde (Benefits)

| Benefit                    | Matlab                                                                                                           |
| -------------------------- | ---------------------------------------------------------------------------------------------------------------- |
| **Separation of Concerns** | Har component apna kaam _isolation_ me karta hai, ek dusre me ghusta nahi                                        |
| **Reusability**            | Ek hi Model ko multiple Views/Controllers use kar sakte hain (e.g. `Doctor` model web page + mobile API dono me) |
| **Maintainability**        | Bug aaya to pata hai kis layer me dhundhna hai; ek layer badli to baaki nahi tutti                               |
| **Testability**            | Controller ko UI ke bina unit test kar sakte ho                                                                  |
| **Parallel work**          | Frontend dev View pe, backend dev Controller/Model pe, saath me kaam kar sakte hain                              |

---

## 1.2 Teeno Components

### 1) Model (Business Entities / Data)

- Application ka **data aur uske rules** represent karta hai.
- Example: `Doctor`, `Patient`, `Appointment` classes (properties like `Id`, `Name`, `Specialization`).
- DB se related data (EF Core entity) bhi yahi hota hai.
- **Express analogy:** Mongoose schema / Sequelize model.

### 2) View (Presentation Logic)

- User ko **kaise dikhana hai**, ye View decide karta hai: HTML + CSS + Razor syntax.
- Files: `.cshtml` (Razor view).
- View me **business logic nahi** likhte, sirf data display karte hain.
- **Express analogy:** EJS / Pug / Handlebars templates.
- **User sirf View hi dekhta hai.** User ko na Controller dikhta hai, na Model. (Wo sirf browser me final page dekhta hai.)

### 3) Controller (Request handler / Coordinator)

- User ki **request sabse pehle Controller ke paas aati hai.**
- Controller decide karta hai: **Model se data kaise lena hai** aur **kaunsa View user ko dikhana hai**.
- Controller = Model aur View ke beech ka **traffic police / manager**.
- **Express analogy:** `controllers/doctorController.js` + `routes/` ka combination.

> **Important correction (interview + real-world):**
> Textbook definition me bolte hain "Controller me business logic hota hai", par **real projects me controller ko thin (patla) rakhte hain**.
> Controller sirf: request lo -> service/model ko call karo -> result ke saath View/JSON return karo.
> Heavy business logic **Service layer** me jaata hai (jaise tumhare Node projects me `services/` folder hota hai).
> Interview me pehle textbook definition bolo, phir add karo: _"Practice me thin controller + service layer follow karte hain."_

---

## 1.3 Request Flow (Kya, kaise, kisme hota hai)

```
   User (Browser)
        |
        |  1) GET /Doctors/Details/5
        v
  +---------------+
  |  Middleware   |   (Routing, Auth, Static files, etc.)
  |   Pipeline    |
  +-------+-------+
          |  2) Routing match: DoctorsController -> Details(5)
          v
  +---------------+        3) Data mango         +-----------+
  |  CONTROLLER   | ----------------------------> |   MODEL   |
  |               | <---------------------------- | (+ DB)    |
  +-------+-------+        4) Data wapas          +-----------+
          |
          |  5) Model ko View me bhejo: return View(doctor)
          v
  +---------------+
  |     VIEW      |   Razor (.cshtml) -> HTML generate
  +-------+-------+
          |
          |  6) HTML Response
          v
   User (Browser) -> sirf final HTML page dekhta hai
```

**Step-by-step (tumhare notes ke hisaab se):**

1. User request karta hai.
2. Sabse pehle request **Controller** ke paas jaati hai.
3. Controller **Model** se check karta hai / data mangta hai (DB me hai ya nahi).
4. Data milne ke baad Controller **View** ke paas bhejta hai.
5. View HTML banata hai, aur wahi **User ko dikhta hai**.

> **Web API me:** View ki jagah **JSON response** hota hai. Yani "View" = JSON. MVC pattern wahi rehta hai, sirf presentation alag hai.

---

## 1.4 MVC ka Code Example (MediSetu: Doctors list)

### Model: `Models/Doctor.cs`

```csharp
namespace MediSetuMvc.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Specialization { get; set; } = default!;
    public int ExperienceYears { get; set; }
}
```

### Controller: `Controllers/DoctorsController.cs`

```csharp
using Microsoft.AspNetCore.Mvc;
using MediSetuMvc.Models;

namespace MediSetuMvc.Controllers;

public class DoctorsController : Controller          // Controller (View support ke saath) inherit karo
{
    // Abhi fake data (real me DB / service se aayega)
    private static readonly List<Doctor> _doctors = new()
    {
        new Doctor { Id = 1, Name = "Dr. Sharma", Specialization = "Cardiology", ExperienceYears = 12 },
        new Doctor { Id = 2, Name = "Dr. Verma",  Specialization = "Dermatology", ExperienceYears = 8 }
    };

    // GET /Doctors  (ya /Doctors/Index)
    public IActionResult Index()
    {
        return View(_doctors);                        // Model ko View me bhej diya (Views/Doctors/Index.cshtml)
    }

    // GET /Doctors/Details/1
    public IActionResult Details(int id)              // id route se aati hai
    {
        var doctor = _doctors.FirstOrDefault(d => d.Id == id);
        if (doctor is null) return NotFound();        // 404
        return View(doctor);                          // Views/Doctors/Details.cshtml
    }
}
```

### View: `Views/Doctors/Index.cshtml`

```cshtml
@model List<MediSetuMvc.Models.Doctor>      @* View ko batao ki kaunsa Model aa raha hai (strongly typed) *@

@{
    ViewData["Title"] = "Doctors";           @* Layout ke <title> me use hoga *@
}

<h1>Our Doctors</h1>

<table class="table">
    <tr><th>Name</th><th>Specialization</th><th>Experience</th><th></th></tr>

    @foreach (var d in Model)                @* Model = Controller se jo List aayi *@
    {
        <tr>
            <td>@d.Name</td>                 @* @ = Razor syntax: C# value HTML me *@
            <td>@d.Specialization</td>
            <td>@d.ExperienceYears yrs</td>
            <td><a asp-action="Details" asp-route-id="@d.Id">View</a></td>   @* Tag Helper: link auto generate *@
        </tr>
    }
</table>
```

### `Program.cs` (MVC enable karne ke liye)

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();       // MVC (Controllers + Views) register

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");       // Production me error page
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();                             // wwwroot (css/js/images) serve karo
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(                           // Convention based routing
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

### Routing pattern `{controller=Home}/{action=Index}/{id?}` ka matlab

| URL                  | Controller        | Action          | id  |
| -------------------- | ----------------- | --------------- | --- |
| `/`                  | Home (default)    | Index (default) | -   |
| `/Doctors`           | DoctorsController | Index (default) | -   |
| `/Doctors/Details/5` | DoctorsController | Details         | 5   |

`?` = optional, `=Home` = default value.

### Controller se View ko data bhejne ke tareeke

```csharp
return View(doctor);                    // 1) Strongly-typed Model (BEST, recommended)
ViewBag.Message = "Welcome";            // 2) ViewBag (dynamic, typo ka risk)
ViewData["Count"] = 10;                 // 3) ViewData (dictionary, cast chahiye)
TempData["Success"] = "Saved!";         // 4) TempData (agle request tak, redirect ke baad msg dikhane ke liye)
```

---

## 1.5 MVC vs Razor Pages vs Web API

|           | MVC                            | Razor Pages                 | Web API                               |
| --------- | ------------------------------ | --------------------------- | ------------------------------------- |
| Output    | HTML (Views)                   | HTML (Pages)                | JSON                                  |
| Structure | Controller + View + Model alag | Page + PageModel (ek jagah) | Controller + Model (View nahi)        |
| Use       | Bade server-rendered apps      | Simple page-focused apps    | React/mobile frontend ke liye backend |
| Template  | `dotnet new mvc`               | `dotnet new razor`          | `dotnet new webapi`                   |

> Tum React + Node/Express use karte ho -> tumhara ASP.NET equivalent **Web API** hoga. MVC (Views wala) tab aata hai jab server hi HTML render kare (jaise Express + EJS).

---

---

# PART 2: ASP.NET Core Project File / Folder Structure

Do tarah ke template ko samajhte hain:

| Template  | Command                      | Kab                                 |
| --------- | ---------------------------- | ----------------------------------- |
| **Empty** | `dotnet new web -n EmptyApp` | Bilkul minimal, sab khud banao      |
| **MVC**   | `dotnet new mvc -n MvcApp`   | Controllers + Views + wwwroot ready |

(Visual Studio me: _ASP.NET Core Empty_ / _ASP.NET Core Web App (Model-View-Controller)_.)

---

## 2.1 Type 1: EMPTY Project Structure

```
EmptyApp/
├── Properties/
│   └── launchSettings.json          # Local run settings (sirf development machine)
├── bin/                             # Compiled output (auto)
├── obj/                             # Temporary build files (auto)
├── appsettings.json                 # Config (env file jaisa)
├── appsettings.Development.json     # Development-only config override
├── EmptyApp.csproj                  # Project file (package.json jaisa)
└── Program.cs                       # Entry point + middleware + services
```

Default `Program.cs` (Empty template):

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");     // app.get('/', ...) jaisa

app.Run();                                 // app.listen()
```

### File-by-file kaam

| File / Folder                        | Kya kaam karta hai                                                                                                                                                              | Node.js equivalent                                      |
| ------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------- |
| **`Program.cs`**                     | App ka **entry point**. Services register (DI), middleware pipeline, routes yahi set hote hain. (Purane versions me `Startup.cs` bhi hota tha, .NET 6+ me sab `Program.cs` me.) | `app.js` / `server.js`                                  |
| **`appsettings.json`**               | **Configuration**: connection string, JWT keys, logging levels, custom settings.                                                                                                | `.env` / `config.js`                                    |
| **`appsettings.Development.json`**   | Sirf Development environment me `appsettings.json` ki values **override** karta hai.                                                                                            | `.env.development`                                      |
| **`Properties/launchSettings.json`** | App ko **local machine pe kaise start kare** (port, URL, environment, browser launch).                                                                                          | `nodemon.json` + scripts (`"dev": "PORT=5000 nodemon"`) |
| **`*.csproj`**                       | **Project definition**: target framework (`net8.0`), NuGet packages, build settings.                                                                                            | `package.json`                                          |
| **`bin/`**                           | Build ke baad **compiled DLLs / exe**.                                                                                                                                          | `dist/` / `build/`                                      |
| **`obj/`**                           | Build ke **intermediate temp files**.                                                                                                                                           | cache folders                                           |
| **Dependencies** (VS me dikhta hai)  | Installed NuGet packages + framework references.                                                                                                                                | `node_modules` (concept)                                |

> `bin/` aur `obj/` ko **git me push nahi karte** (`.gitignore` me hote hain), bilkul `node_modules` jaise.

---

## 2.2 Type 2: MVC Project Structure

```
MvcApp/
├── Properties/
│   └── launchSettings.json
├── Controllers/
│   └── HomeController.cs            # Request handlers (Actions)
├── Models/
│   └── ErrorViewModel.cs            # Models / ViewModels
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml             # Home page view
│   │   └── Privacy.cshtml
│   ├── Shared/
│   │   ├── _Layout.cshtml           # Master layout (navbar, footer)
│   │   ├── _Layout.cshtml.css       # Layout ka scoped CSS
│   │   ├── _ValidationScriptsPartial.cshtml
│   │   └── Error.cshtml             # Error page
│   ├── _ViewImports.cshtml          # Global @using + tag helpers
│   └── _ViewStart.cshtml            # Default layout set karta hai
├── wwwroot/                         # Static files (public folder)
│   ├── css/site.css
│   ├── js/site.js
│   ├── lib/                         # bootstrap, jquery
│   └── favicon.ico
├── appsettings.json
├── appsettings.Development.json
├── MvcApp.csproj
└── Program.cs
```

### Folder-wise kaam

| Folder / File                 | Kaam                                                                                                                     | Node/Express equivalent                |
| ----------------------------- | ------------------------------------------------------------------------------------------------------------------------ | -------------------------------------- |
| **`Controllers/`**            | Saare controllers. Class name `XxxController`. Har public method ek **Action** (route handler).                          | `routes/` + `controllers/`             |
| **`Models/`**                 | Business entities, ViewModels, DTOs.                                                                                     | `models/`                              |
| **`Views/`**                  | Saare `.cshtml` templates. Folder name = Controller name, file name = Action name.                                       | `views/` (EJS/Pug)                     |
| **`Views/Home/Index.cshtml`** | `HomeController.Index()` ka View. **Convention:** `Views/<Controller>/<Action>.cshtml`                                   | `views/home.ejs`                       |
| **`Views/Shared/`**           | Jo views/partials **multiple controllers** use kare (layout, error, partials).                                           | `views/partials/`                      |
| **`_Layout.cshtml`**          | Master page: `<html>`, `<head>`, navbar, footer. Beech me `@RenderBody()` jaha har page ka content aata hai.             | EJS layout / `express-ejs-layouts`     |
| **`_ViewStart.cshtml`**       | Har View se pehle run hota hai; default `Layout = "_Layout"` set karta hai. Isliye har view me layout nahi likhna padta. | -                                      |
| **`_ViewImports.cshtml`**     | Saare Views ke liye **common `@using`, `@addTagHelper`**. Har view me baar-baar import nahi karna padta.                 | -                                      |
| **`Error.cshtml`**            | Unhandled error pe dikhne wala page.                                                                                     | error.ejs                              |
| **`wwwroot/`**                | **Static files** (CSS, JS, images, fonts). Sirf yahi folder browser ko directly serve hota hai (`UseStaticFiles()` se).  | `public/` + `express.static('public')` |

### Do important files ka content

**`Views/_ViewStart.cshtml`**

```cshtml
@{
    Layout = "_Layout";       @* Har view me ye layout auto lagega *@
}
```

**`Views/_ViewImports.cshtml`**

```cshtml
@using MvcApp
@using MvcApp.Models
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers   @* asp-action, asp-controller wagairah enable *@
```

**`Views/Shared/_Layout.cshtml` (short)**

```cshtml
<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"] - MediSetu</title>
    <link rel="stylesheet" href="~/css/site.css" />        @* ~ = wwwroot root *@
</head>
<body>
    <nav> ...navbar... </nav>

    <main>
        @RenderBody()                                      @* Har page ka content yaha inject hota hai *@
    </main>

    <footer> ...footer... </footer>

    <script src="~/js/site.js"></script>
    @await RenderSectionAsync("Scripts", required: false)  @* Page-specific scripts yaha *@
</body>
</html>
```

---

## 2.3 `launchSettings.json` (Properties folder) — Detail

**Tumhare notes:**

- Ye `Properties` folder ke andar hota hai.
- Isme wo **settings** hoti hain jo control karti hain ki **web app development me kaise start hogi**.
- Ye settings tab use hoti hain jab app ko **Visual Studio se** ya **.NET Core CLI (`dotnet run`)** se chalate ho.
- **Sabse important point:** ye file **sirf local development machine** pe use hoti hai.
- **Production server pe publish** karte waqt ye file **zaruri nahi** hoti (publish output me jaati hi nahi).

**Example:**

```json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project", // Kestrel server pe chalao
      "dotnetRunMessages": true,
      "launchBrowser": true, // run pe browser auto khule
      "applicationUrl": "http://localhost:5123", // port (Node: PORT=5123)
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development" // NODE_ENV=development jaisa
      }
    },
    "https": {
      "commandName": "Project",
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7123;http://localhost:5123",
      "environmentVariables": { "ASPNETCORE_ENVIRONMENT": "Development" }
    }
  }
}
```

| Setting                  | Matlab                                                                                                   |
| ------------------------ | -------------------------------------------------------------------------------------------------------- |
| `applicationUrl`         | Kis port / URL pe app chalegi                                                                            |
| `launchBrowser`          | Run pe browser khule ya nahi                                                                             |
| `ASPNETCORE_ENVIRONMENT` | `Development` / `Staging` / `Production` (ye decide karta hai kaunsi `appsettings.{Env}.json` load hogi) |
| `commandName: Project`   | Kestrel (built-in server) se chalao                                                                      |
| Multiple profiles        | `dotnet run --launch-profile https` se choose kar sakte ho                                               |

**Production me kya hota hai?** Server pe `ASPNETCORE_ENVIRONMENT` aur `ASPNETCORE_URLS` **real environment variables** (Docker / EC2 / systemd) se set karte ho, `launchSettings.json` se nahi.

---

## 2.4 `appsettings.json` — Detail

**Tumhare notes:**

- **Saari configuration related cheezein** `appsettings.json` me hoti hain.
- Ye bilkul **`.env` file jaisa** hai (jaise Node me `.env` me config likhte ho, waise C# me `appsettings.json`).
- **Purane ASP.NET versions** (.NET Framework) me connection string wagairah **`web.config`** me store hote the.
- `appsettings.json` **automatically create** hoti hai jab tum **Empty, MVC ya Razor Pages template** se project banate ho.

**Default content:**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**Tum isme apna config add karte ho:**

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=medisetu;User=root;Password=pass;"
  },
  "Jwt": {
    "Key": "very-long-secret-key-at-least-32-characters",
    "Issuer": "MediSetu",
    "ExpiryMinutes": 60
  }
}
```

**Code me padhna:**

```csharp
var conn = builder.Configuration.GetConnectionString("Default");   // ConnectionStrings:Default
var key  = builder.Configuration["Jwt:Key"];                       // ":" se nested access (process.env.X jaisa)
```

### `web.config` vs `appsettings.json`

|                         | `web.config` (purana, .NET Framework) | `appsettings.json` (ASP.NET Core)                                                   |
| ----------------------- | ------------------------------------- | ----------------------------------------------------------------------------------- |
| Format                  | **XML**                               | **JSON**                                                                            |
| Kaam                    | Config + IIS settings sab ek jagah    | Sirf **application config**                                                         |
| Environment-wise config | Mushkil (transform files)             | Aasan (`appsettings.Development.json`)                                              |
| Secrets                 | Plain text me                         | User Secrets / Env variables / Key Vault                                            |
| Ab kab?                 | Legacy                                | **Standard**. `web.config` ab sirf IIS hosting pe publish me auto-generate hota hai |

### Configuration load order (baad wala pehle wale ko override karta hai)

```
1. appsettings.json                    (base)
2. appsettings.{Environment}.json      (Development / Production)
3. User Secrets                        (sirf Development, local secret keys)
4. Environment Variables               (Docker / EC2 / CI me production secrets)
5. Command-line arguments              (sabse high priority)
```

> Production me **secrets `appsettings.json` me mat rakho** (git me chale jaate hain). Env variable use karo: `Jwt__Key=xxx` (double underscore = nested `:`). Local me: `dotnet user-secrets set "Jwt:Key" "xxx"`.

---

## 2.5 `Program.cs` aur `.csproj` — Short Detail

**`.csproj` (package.json jaisa):**

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>     <!-- Node version jaisa -->
    <Nullable>enable</Nullable>                   <!-- string vs string? check -->
    <ImplicitUsings>enable</ImplicitUsings>       <!-- common using auto -->
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />   <!-- npm install bcrypt jaisa -->
  </ItemGroup>
</Project>
```

**`Program.cs` 2 hisso me soch:**

```csharp
var builder = WebApplication.CreateBuilder(args);

// ---- PART 1: SERVICES (builder.Services...) ----
// Kya-kya features / dependencies chahiye (MVC, DB, Auth, etc.) wo register karo

var app = builder.Build();

// ---- PART 2: MIDDLEWARE PIPELINE (app.Use...) ----
// Request kis order me kya-kya se guzregi

app.Run();
```

---

## 2.6 Empty vs MVC: Side-by-Side

| Cheez                     | Empty                                       | MVC                     |
| ------------------------- | ------------------------------------------- | ----------------------- |
| Controllers folder        | Nahi                                        | Haan                    |
| Models folder             | Nahi                                        | Haan                    |
| Views folder              | Nahi                                        | Haan                    |
| wwwroot                   | Nahi                                        | Haan                    |
| `Program.cs` me MVC setup | Nahi (`AddControllersWithViews` khud likho) | Pehle se hai            |
| `launchSettings.json`     | Haan                                        | Haan                    |
| `appsettings.json`        | Haan                                        | Haan                    |
| Kab use                   | Minimal API / learning / microservice       | Server-rendered web app |

**Empty project ko MVC banana ho to:**

```csharp
builder.Services.AddControllersWithViews();                 // services me
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
// + khud Controllers/, Views/, Models/ folder aur files banao
```

---

## 2.7 Express Project se Poora Mapping

```
Express (Node)                     ASP.NET Core MVC
─────────────────────────────      ─────────────────────────────
app.js / server.js            ->   Program.cs
.env                          ->   appsettings.json (+ user secrets)
nodemon.json / package scripts->   Properties/launchSettings.json
package.json                  ->   *.csproj
node_modules/                 ->   NuGet packages (Dependencies)
dist/                         ->   bin/ + obj/
routes/ + controllers/        ->   Controllers/
models/                       ->   Models/
views/ (ejs)                  ->   Views/ (cshtml)
views/layout.ejs              ->   Views/Shared/_Layout.cshtml
public/                       ->   wwwroot/
middleware/                   ->   custom middleware classes + Program.cs pipeline
```

---

## 2.8 Interview Q&A

**Q1. MVC kya hai? Language hai ya framework?**
Na language, na framework. Ye ek **architectural design pattern** hai jo application ko Model, View aur Controller me baant ke **separation of concerns** achieve karta hai. ASP.NET Core MVC is pattern ka implementation hai.

**Q2. Teeno components ka kaam?**
Model = data + business entities. View = presentation (UI). Controller = request handle karke Model se data le ke View ko deta hai.

**Q3. Request flow batao.**
Request -> Middleware/Routing -> Controller action -> Model/Service se data -> View (Razor render) -> HTML response.

**Q4. MVC ke fayde?**
Separation of concerns, reusability, maintainability, testability, parallel development.

**Q5. `launchSettings.json` kya hai? Production me kyu nahi chahiye?**
Local dev ke liye run profile (port, environment, browser launch). Ye sirf VS / `dotnet run` use karte hain. Publish me nahi jaati; production me environment variables se config hoti hai.

**Q6. `appsettings.json` aur `web.config` me difference?**
`web.config` = purana XML based (Framework), `appsettings.json` = JSON based, environment-wise override, secrets ke better options. Node ka `.env` jaisa.

**Q7. `wwwroot` kyu hai?**
Static files ka public root. Sirf isi folder ke files browser ko directly milte hain (`UseStaticFiles()` se). Baaki project files protected rehti hain.

**Q8. `_ViewStart.cshtml` aur `_ViewImports.cshtml` ka difference?**
`_ViewStart` = har view se pehle chalke layout set karta hai. `_ViewImports` = saare views ke liye common `@using` aur tag helpers.

**Q9. ViewBag, ViewData, TempData?**
ViewBag = dynamic wrapper, ViewData = dictionary (dono sirf current request), TempData = agle request tak (redirect ke baad message dikhane ke liye). Best practice: **strongly-typed Model**.

**Q10. MVC me controller me business logic rakhna chahiye?**
Textbook me "haan", practice me **thin controller**: logic Service layer me, controller sirf coordinate karta hai.

---

## 2.9 Quick Revision (1 minute)

- **MVC** = design pattern (na language, na framework). Model = data, View = UI, Controller = coordinator.
- Request -> **Controller** -> Model -> **View** -> user. User sirf **View** dekhta hai.
- **Empty template:** `Program.cs`, `appsettings.json`, `launchSettings.json`, `.csproj`.
- **MVC template:** upar wale + `Controllers/`, `Models/`, `Views/`, `wwwroot/`.
- **`launchSettings.json`** = sirf local dev, production me nahi.
- **`appsettings.json`** = `.env` jaisa, purane `web.config` ki jagah.
- Convention: `Views/<Controller>/<Action>.cshtml`, default route `{controller=Home}/{action=Index}/{id?}`.
