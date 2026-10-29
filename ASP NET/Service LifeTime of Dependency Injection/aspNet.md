# Difference Between .NET Framework and .NET Core

## 1. What is a Framework?

A **framework** is a collection of pre-written code, libraries, and tools that developers can use to build applications more easily.

Instead of writing everything from scratch, developers can use the functionality already provided by the framework.

For example, a framework may provide ready-made functionality for:

- Connecting to a database.
- Handling HTTP requests.
- Reading and writing files.
- Managing security and authentication.
- Handling errors and exceptions.

**In simple words:** Framework ek ready-made structure aur code provide karta hai, jisse developer ko har functionality khud se implement nahi karni padti.

## 2. What is .NET Framework?

**.NET Framework** is Microsoft's older development platform used to build and run applications, primarily on Windows.

It provides:

- **CLR (Common Language Runtime):** Executes managed code and provides services such as garbage collection and exception handling.
- **Class Libraries:** Pre-written code for collections, file handling, networking, database access, etc.
- **Application frameworks:** Technologies such as ASP.NET, Windows Forms and WPF.

### Example

Suppose we develop an application using .NET Framework 4.8.

To run this application, the target computer generally needs a compatible .NET Framework runtime installed. If the required version is missing, the application may not run until the framework is installed or an appropriate compatible version is available.

**Important:** .NET Framework is Windows-focused and is mainly used today for maintaining existing applications.

## 3. What is .NET Core?

**.NET Core** was introduced by Microsoft as an open-source, cross-platform development platform.

It allows developers to build applications that can run on Windows, Linux and macOS.

It provides:

- CLR-based runtime functionality.
- Built-in dependency injection support in ASP.NET Core.
- Libraries for networking, collections, file handling and other operations.
- Support for modern web APIs, cloud applications and containerized services.

### Example

Suppose we develop a REST API using ASP.NET Core.

We can develop the application on Windows and deploy it to a Linux server using Docker, provided the application and its dependencies support the target environment.

**Important:** .NET Core is the earlier name of the modern .NET platform. Starting with .NET 5, Microsoft dropped the word "Core" from the platform name.

For example:

- .NET Core 3.1
- .NET 5
- .NET 6
- .NET 8
- .NET 9
- .NET 10

ASP.NET Core continues to be the name of Microsoft's modern web framework.

## 4. Difference Between .NET Framework and .NET Core

| Feature              | .NET Framework                                                   | .NET Core / Modern .NET                                    |
| -------------------- | ---------------------------------------------------------------- | ---------------------------------------------------------- |
| Introduction         | Older Microsoft platform                                         | Modern platform introduced as .NET Core                    |
| Platform support     | Primarily Windows                                                | Windows, Linux and macOS                                   |
| Open source          | Mostly proprietary historically; source code is available        | Open source                                                |
| Performance          | Depends on application and workload                              | Often offers improved performance and scalability          |
| Dependency Injection | No built-in general-purpose DI container in the framework itself | Built-in DI container available in ASP.NET Core            |
| Web development      | ASP.NET                                                          | ASP.NET Core                                               |
| Deployment           | Generally requires a compatible installed framework              | Supports framework-dependent and self-contained deployment |
| Current usage        | Existing and legacy applications                                 | Recommended for new applications                           |

## 5. Runtime Dependency: .NET Framework vs Modern .NET

### .NET Framework

Suppose an application targets .NET Framework 4.8.

The target machine generally needs a compatible .NET Framework installation. .NET Framework versions have specific compatibility and in-place update rules, so installing a higher version does not universally solve every version mismatch.

### Modern .NET

Modern .NET supports two common deployment approaches.

**1. Framework-dependent deployment**

The application uses the .NET runtime installed on the target machine. The required compatible runtime must be available.

**2. Self-contained deployment**

The application is published with the required .NET runtime files for a particular operating system and architecture.

This means the target machine generally does not need a separately installed .NET runtime for that application.

**Important:** Self-contained deployment does not mean the application has no dependencies. It can still depend on operating-system libraries, native components and other external resources.

## 6. Dependency Injection (DI)

### What is Dependency Injection?

Dependency Injection is a technique in which an object receives the services it needs from outside instead of creating those dependencies itself.

### .NET Framework

The .NET Framework does not provide a built-in general-purpose Dependency Injection container in the same way ASP.NET Core does.

However, DI is still possible in .NET Framework applications using manual injection or third-party libraries such as Autofac and Unity.

### ASP.NET Core

dotnet new mvc -n MyMvcApp --- >> isse mvc folder automatic create ho jyege
dotnet new webapi -n myApp --- >> iss mvc folder automatic create nhi hogi.

ASP.NET Core provides a built-in DI container. We can register services and inject them into controllers or other classes.

Example:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
```

Here:

- `IMessageService` is an interface.
- `MessageService` is its implementation.
- `AddScoped()` registers the service with a scoped lifetime.
- ASP.NET Core creates and supplies the required service instances through DI.

This helps us write loosely coupled, testable and maintainable applications.

## 7. Important Naming Difference

Do not confuse these terms:

- **.NET Framework:** The older Windows-focused platform.
- **.NET Core:** The original name of Microsoft's modern, cross-platform platform.
- **.NET:** The name used for the modern platform from .NET 5 onward.
- **ASP.NET Core:** The web framework used to build modern web applications and REST APIs on .NET.

For new backend projects, modern .NET with ASP.NET Core is generally the recommended choice.

# MVC in asp net core

There are three basic components in mvc

1. Model (Buisness entines)
2. View(Presentation logic)
3. Controller (Buiness logic)

Controller ke ander hi hm saaaara kaam krte hi ki kaise model se apne data lena hi and user ko kiss trh dikhana hi ye biew ke ander aata hi...

Model view Controller

It is a architectural design pattern that means this design pattern is used at the architecture level of an application.

Mvc koi bhi programming lang. nhi hi na koi frame work hi ek design pattern hi ..

Hr kaam appne ke speration files ke ander krte hi ...

All works on individual pieces of the system in isolation.

Facilitates resuability
Ensures the maintability of the system.

Agar user request krta hi to sbse phele request controller ke pass jati hi

then wo controller model ko check krta hi ki koi data hi ki nhi then wo view ke pass jati hi and and whi view user ko dikh jata hi...

user srif ur srif view ko dekhega na to controller ko dekhega na to model ko dekheega..

File folder structure of asp net core

In the Properties folder is an asp net core project you can find launchsetting.json file which contains settings that control how your web app is started on your development.

means launchSetting.json me ye sb likha hota jo ki apni project ko chalne ke liye jauri hi..

The setting that are present with in the file are going to be used when we run .net core application either from visual studio or by using .net core cli.

the most important point that you need to keep in mind is this launchSetting.json file is used only in the local development machine.

that means this file is not required when we plublishing our asp.net core application to the production server.

Config se related jinta bhi kaam apne appsettings.json mr krte hi

like ye similar hi apne env file jaise kyuki ussme bhi apne apni config file likhte then to evn is the appsetting.json in the C#.

asp net core ke jo previous version the use ander apne webconfig.file ke ander store krke rkhte the database connection string and many moreeeeeee...........

appsetting.json jo file hi wo automatically create ho jaaati hi jb
apne empty project template ko create kr krte hi or razor pages or mvc template ke saath create krte hi to vs code automatically appsetting,json file ko create kr deta....

# ------------------

# ASP.NET Core: Middleware Notes

## 1. Middleware kya hota hai?

Middleware ek chhota software component hai jo **HTTP request aur response ke beech me** kaam karta hai.

ASP.NET Core me jab bhi client request bhejta hai, wo seedha tumhare code (route/controller) tak nahi pahunchti. Pehle wo **middlewares ki ek chain** se guzarti hai. Is chain ko **HTTP Pipeline** (Request Pipeline) kehte hain.

Middleware ye kaam karte hain:

- Request ko **check ya modify** karna (jaise token verify karna)
- Request ko **aage bhejna** ya **wahin rok dena**
- Response ko **modify** karna (jaise header add karna)
- **Error handle** karna (app crash hone par proper error page/response dena)
- **Authentication** (tum kaun ho?) aur **Authorization** (tumhe ye karne ki permission hai?)
- Logging, CORS, compression, caching, HTTPS redirect, etc.

> **Express se compare:** Express ka `app.use((req, res, next) => {...})` bilkul yahi cheez hai.

---

## 2. HTTP Pipeline ka flow

Ek middleware ho ya kai, **sabhi middleware isi pipeline ke andar** define hote hain.

```
Client
  │  (1) Request bhejta hai
  ▼
┌──────────────────────────────────────────┐
│            HTTP PIPELINE                 │
│                                          │
│   Middleware 1  ──►  Middleware 2  ──►  Middleware 3  ──► Endpoint / Server logic
│                                                                  │
│   Middleware 1  ◄──  Middleware 2  ◄──  Middleware 3  ◄──────────┘
│                                          │
└──────────────────────────────────────────┘
  │  (2) Response wapas aata hai
  ▼
Client
```

Simple words me:

1. Client request bhejta hai.
2. Request pehle pipeline me jaati hai, wahan har middleware apna logic chalata hai.
3. Phir request server logic (endpoint) tak pahunchti hai.
4. Server jab response banata hai, wo response **wapas usi pipeline se ulta guzarta hai** (last middleware se pehle middleware tak).
5. Tab jaake response client ko milta hai.

**Important:** Response wapas aate waqt bhi middlewares ko kaam karne ka mauka milta hai. Isi liye `await next(context)` ke **baad** wala code response ke raste me chalta hai.

---

## 3. Middleware ke types

| Type                    | Matlab                                  | Example                                                                                                               |
| ----------------------- | --------------------------------------- | --------------------------------------------------------------------------------------------------------------------- |
| **Built-in middleware** | ASP.NET Core ke saath already aate hain | `UseHttpsRedirection()`, `UseAuthentication()`, `UseAuthorization()`, `UseStaticFiles()`, `UseCors()`, `UseRouting()` |
| **Custom middleware**   | Tum khud apna banate ho                 | `app.Use(...)`, `app.Run(...)`, ya alag class                                                                         |

---

## 4. Custom middleware: `app.Run()` (terminal middleware)

Custom middleware banane ka sabse simple tarika `app.Run()` hai. Isme ek lambda pass karte hain:

```csharp
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Welcome to ASP.NET Core 6....");
});
```

Kya hoga:

- Jo bhi request aayegi, **har ek request** par ye method chalega.
- Response me text likha jayega.

**Lekin ek badi baat:** `app.Run()` me **`next` hota hi nahi**. Isliye ye **kabhi bhi agle middleware ko call nahi karta**. Request yahin khatam ho jati hai. Isko **terminal middleware** kehte hain.

> Middleware defined using `app.Run` will never call the subsequent middleware.

---

## 5. Custom middleware: `app.Use()` (aage bhejne wala)

Agar tumhe middleware ko aage chalne dena hai to `app.Use()` use karte hain. Ye **2 parameters** leta hai: `context` aur `next`.

```csharp
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Welcome to ASP.NET Core 6....\n");
    await next(context);   // agle middleware ko chalao
});
```

- `context` = request + response ki saari info
- `next` = pipeline ka agla middleware
- `await next(context)` = "mera kaam ho gaya, ab agle ko do"

> The `Use()` method places a middleware in the pipeline and allows that middleware to pass control to the next item in the pipeline.

---

## 6. `context` kya hai?

`context` ek **HttpContext** object hai. Isme ek request ki **saari info** hoti hai: request bhi aur response bhi.

Express me ye do alag objects hote hain (`req`, `res`). ASP.NET Core me dono ek hi `context` ke andar hote hain:

```csharp
app.Use(async (context, next) =>
{
    context.Request   // aane wali request (Express ka req)
    context.Response  // bhejne wala response (Express ka res)
});
```

| ASP.NET Core                        | Express           |
| ----------------------------------- | ----------------- |
| `context.Request`                   | `req`             |
| `context.Response`                  | `res`             |
| `context.Request.Path`              | `req.path`        |
| `context.Request.Method`            | `req.method`      |
| `context.Request.Headers`           | `req.headers`     |
| `context.Response.StatusCode = 404` | `res.status(404)` |

---

## 7. `WriteAsync` kya hai?

```csharp
await context.Response.WriteAsync("text");
```

Ye response ki **body me text likh deta hai**, jo browser ko jayega. Express ke `res.write("text")` jaisa.

- **Write** = response me likho
- **Async** = ye kaam asynchronous hai (network pe data bhejna time leta hai), isliye `await` lagana padta hai

---

## 8. `next` kya hai?

`next` **agla middleware** hai.

`await next(context)` ka matlab: "mera kaam ho gaya, ab agle middleware ko do."

Express me `next()` hota hai, yahan bas `context` bhi pass karna padta hai taaki agle middleware ko pata rahe ki kaunsi request chal rahi hai.

---

## 9. Middleware ka ORDER bahut important hai

Middleware **usi order me chalte hain jis order me tum unhe likhte ho**. Order galat hua to app galat behave karegi.

Example:

```csharp
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("1st\n");
    await next(context);
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("2nd\n");
});

app.MapGet("/", () => "Hello World!");   // KABHI NAHI CHALEGA
```

Output: `1st` aur `2nd`. `MapGet` kabhi nahi chalega kyunki `app.Run(...)` ne request wahin rok di.

**Common sahi order (real app me):**

```
1. Exception handling      (error pakadne ke liye, sabse upar)
2. HTTPS redirection
3. Static files
4. Routing
5. CORS
6. Authentication          (tum kaun ho?)
7. Authorization           (tumhe permission hai?)
8. Endpoints               (MapGet / MapControllers)
```

Authentication hamesha Authorization se **pehle** aana chahiye.

---

## 10. Sabse upar ka sawal: end me `app.Run()` ka kya matlab hai?

Tumhare code me do alag tarah ke `Run` hote hain. Naam same hai, kaam alag:

### (a) `app.Run(async (context) => {...})`: **Middleware**

- Argument me lambda hota hai
- Ye ek **terminal middleware** hai
- Request ko yahin khatam kar deta hai, aage nahi bhejta

### (b) `app.Run()`: **Server start karna**

- **Bina kisi argument ke**
- Ye app ko **start** karta hai (Kestrel web server chalu hota hai)
- Ye **blocking call** hai, matlab app yahin pe chalti rehti hai, band nahi hoti
- Ye hamesha **Program.cs ki sabse last line** hoti hai
- Express ka `app.listen(PORT)` jaisa

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();   // <-- ye server start karta hai. Isse pehle jo bhi likha, wo sirf setup tha.
```

Agar ye last line na likho to app start hote hi turant band ho jayegi, kyunki kuch run hi nahi kar raha.

|                   | `app.Run(async ctx => ...)` | `app.Run()`     |
| ----------------- | --------------------------- | --------------- |
| Arguments         | Lambda deta hai             | Koi nahi        |
| Kaam              | Terminal middleware         | Server start    |
| `next` hota hai?  | Nahi                        | Lagu nahi hota  |
| Kahan likhte hain | Pipeline ke andar           | Sabse last line |

---

## 11. Poora example (sab kuch ek saath)

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware 1: Use -> kaam karke aage bhejta hai
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("First middleware\n");
    await next(context);
});

// Middleware 2: Use -> ye bhi aage bhejta hai
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Second middleware\n");
    await next(context);
});

// Middleware 3: Run -> terminal, yahin khatam
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Third (terminal) middleware\n");
});

// Server start
app.Run();
```

Browser me output:

```
First middleware
Second middleware
Third (terminal) middleware
```

Agar tum chahte ho ki `MapGet("/")` bhi chale, to **terminal middleware (Run lambda) hata do** ya sabse last me rakho jahan uske baad kuch na ho.

---

## 12. Bonus: `next` ke baad ka code (response ke raste me)

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("Request aa rahi hai");     // request ke raste me

    await next(context);                           // aage jao

    Console.WriteLine("Response wapas ja raha hai"); // response ke raste me
});
```

Isse tum request ka time napna, logging karna, ya response me header add karna jaise kaam kar sakte ho.

---

# ASP.NET Core MVC: Routing (Complete Notes)

## 1. Routing kya hoti hai?

Routing ek mechanism hai jo browser se aane wali **request (URL + HTTP method)** ko check karta hai aur use sahi **Controller ke Action method** se connect kar deta hai.

```
Browser Request  →  Routing Middleware  →  Controller  →  Action  →  View / Response
```

**Flow step by step:**

1. User URL daalta hai, jaise `localhost:5000/Home/About`
2. Request sabse pehle **routing** ke paas jaati hai
3. Routing dekhti hai ki kaun sa controller aur action is URL se match karta hai
4. Wo action method execute hota hai
5. Action `View()` ya koi aur result return karta hai

### Routing = URL + HTTP Method

| HTTP Method | Kaam                     |
| ----------- | ------------------------ |
| GET         | Data lena / page dikhana |
| POST        | Naya data bhejna         |
| PUT         | Data update karna        |
| DELETE      | Data delete karna        |

Same URL alag HTTP method ke saath alag action ko call kar sakta hai. Jaise `/Products/Create` par GET se form dikhta hai aur POST se form save hota hai.

### Routing ko set kaise karte hain?

`Program.cs` me routing middleware pipeline me add karna padta hai. Routing 2 tarike se define hoti hai:

1. **Convention-based routing**
2. **Attribute-based routing**

---

## 2. Convention-Based Routing

Isme ek **common pattern** `Program.cs` me ek baar define karte hain, aur wo sabhi controllers par lagu hota hai.

### Pattern

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

### Pattern ke parts

| Part                | Matlab                                                 |
| ------------------- | ------------------------------------------------------ |
| `{controller=Home}` | URL me controller na ho to **Home** use karo           |
| `{action=Index}`    | URL me action na ho to **Index** use karo              |
| `{id?}`             | `?` matlab **optional**. id ho bhi sakti hai, nahi bhi |

### Example URLs

| URL                  | Controller | Action  | id   |
| -------------------- | ---------- | ------- | ---- |
| `/`                  | Home       | Index   | null |
| `/Home`              | Home       | Index   | null |
| `/Home/Privacy`      | Home       | Privacy | null |
| `/Home/Details/10`   | Home       | Details | 10   |
| `/Student`           | Student    | Index   | null |
| `/Student/Hello`     | Student    | Hello   | null |
| `/Employee/GetAll`   | Employee   | GetAll  | null |
| `/Product/Details/5` | Product    | Details | 5    |

### Sabse important baat

**Ek hi route se 50 controllers bhi access ho sakte hain.** Har action ke liye alag `MapControllerRoute()` likhne ki zaroorat nahi hai.

```csharp
// GALAT tarika: har action ke liye alag route
app.MapControllerRoute(pattern: "{controller=Home}/{action=Privacy}");
app.MapControllerRoute(pattern: "{controller=Home}/{action=Details}/{id?}");
app.MapControllerRoute(pattern: "{controller=Home}/{action=Hello}");

// SAHI tarika: ek route sab ke liye
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

Routing URL se `{controller}` aur `{action}` ki values padhti hai aur matching controller/action select kar leti hai.

### `Home` aur `Index` sirf default values hain

Ye fixed nahi hain. Ye tab use hoti hain jab URL me value **na ho**.

| URL              | Kya hoga                                                |
| ---------------- | ------------------------------------------------------- |
| `/`              | Dono default: Home + Index                              |
| `/Student`       | Controller = Student (URL se), Action = Index (default) |
| `/Student/Hello` | Dono URL se aaye, defaults ignore                       |

### `name` optional hai?

Haan. `name` na do to bhi routing chalegi. `pattern` zaroori hai (URL match ke liye). `name` sirf route ko pehchanne ke liye hai (jaise URL generate karte waqt).

### Interview point: Default value vs Literal segment

```csharp
// Home = default value. Koi bhi controller chalega
pattern: "{controller=Home}/{action=Index}/{id?}"

// Home = literal. Sirf "Home/..." wale URLs match honge
pattern: "Home/{action=Index}/{id?}"
```

Doosre pattern me `/Student` match **nahi** hoga, kyunki pehla segment fix `Home` hai.

### Custom route (specific pehle, general baad me)

```csharp
app.MapControllerRoute(
    name: "blog",
    pattern: "blog/{year}/{month}",
    defaults: new { controller = "Blog", action = "Archive" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

Routes **upar se neeche** check hote hain, isliye specific route pehle aur general route baad me likho.

---

## 3. Bina MVC template ke setup (manual)

`dotnet new mvc` ye sab khud bana deta hai. Manual karna ho to:

### Step 1: Folders

```
Controllers/
Models/
Views/
    Home/
    Shared/
```

### Step 2: `Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);

// Build karne se PEHLE services register karo
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

> Newer templates (.NET 9) me `app.MapStaticAssets()` aur route ke end me `.WithStaticAssets()` bhi hota hai. Ye static files ko fast serve karne ke liye hai, routing ke concept me koi farak nahi.

### Step 3: Controller

`Controllers/HomeController.cs`

```csharp
using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();   // Views/Home/Index.cshtml
        }

        public IActionResult Privacy()
        {
            return Content("Privacy action called");
        }

        public IActionResult Details(int id)
        {
            return Content($"Id = {id}");
        }

        public string Hello()
        {
            return "Hello action called. Return type string hai.";
        }
    }
}
```

**Rules:**

- Class ka naam `___Controller` hona chahiye aur `Controller` se inherit kare
- Har `public` method ek **action** hai
- Action ka naam hi URL me use hota hai (convention routing me)
- `View()` return karna ho to return type `IActionResult` ya `ViewResult` rakho
- `int`, `string` return karna bhi allowed hai, lekin real projects me `IActionResult` use hota hai

### Step 4: View

`Views/Home/Index.cshtml`

```html
@{ ViewData["Title"] = "Home Page"; }

<div class="text-center">
  <h1>Welcome</h1>
</div>
```

### View kaise dhoondhi jaati hai?

`return View();` par MVC ye order me search karta hai:

1. `Views/{ControllerName}/{ActionName}.cshtml`
2. `Views/Shared/{ActionName}.cshtml`

Isliye `HomeController` ke `Index` action ke liye file `Views/Home/Index.cshtml` honi chahiye. **View ka naam = Action ka naam**, folder ka naam = Controller ka naam (bina `Controller` suffix ke).

Agar view ka naam alag rakhna ho:

```csharp
return View("About");                          // Views/Home/About.cshtml
return View("~/Views/Home/Index.cshtml");      // full path
```

### Common naming mistakes

| Galat                          | Sahi                              |
| ------------------------------ | --------------------------------- |
| `AddControllerWithViews()`     | `AddControllersWithViews()`       |
| `builder.build()`              | `builder.Build()`                 |
| `app.defaultControllerRoute()` | `app.MapDefaultControllerRoute()` |
| `viewResult`                   | `ViewResult`                      |
| `return view();`               | `return View();`                  |

`MapDefaultControllerRoute()` wahi hai jo upar wala default pattern, bas short form.

---

## 4. Attribute-Based Routing

Isme route **directly controller ya action ke upar** `[Route]` attribute se likhte hain.

### Setup

`Program.cs` me `MapControllers()` zaroori hai:

```csharp
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();
app.MapControllers();   // attribute routing ke liye zaroori
```

`MapControllers()` ke bina attribute routing kaam **nahi** karegi.

### Basic example

```csharp
public class HomeController : Controller
{
    [Route("")]                  // localhost:5000/
    public IActionResult Index()
    {
        return View();
    }

    [Route("home/privacy")]      // /home/privacy
    public IActionResult Privacy()
    {
        return View();
    }

    [Route("home/details/{id?}")]   // /home/details  ya  /home/details/5
    public IActionResult Details(int id)
    {
        return Content($"Id = {id}");
    }
}
```

Dhyan do: optional id ka sahi syntax `{id?}` hai (curly brackets ke saath). `id?` bina brackets ke sirf ek literal text hota hai.

### Multiple routes ek action par

```csharp
[Route("/")]
[Route("/home")]
[Route("/home/index")]
public IActionResult Index()
{
    return View();
}
```

Ye action teeno URLs par chalega: empty, `/home`, aur `/home/index`.

### Important rules

- **Same route path do actions par nahi ho sakta.** Aisa kiya to `AmbiguousMatchException` aayega
- Attribute routing me **action ke naam se farak nahi padta**, sirf route ka path matter karta hai
- Jis action par `[Route]` laga hai, us par conventional route **apply nahi hota**

### Controller level par `[Route]`

Har action ke upar `home/` likhne ke bajay controller par ek baar likh do:

```csharp
[Route("Home")]
public class HomeController : Controller
{
    [Route("Index")]          // /Home/Index
    public IActionResult Index() => View();

    [Route("Privacy")]        // /Home/Privacy
    public IActionResult Privacy() => View();

    [Route("Details/{id?}")]  // /Home/Details/5
    public IActionResult Details(int id) => Content($"Id = {id}");
}
```

Controller ka route aur action ka route **jud jaate hain**.

### Leading slash (`/` ya `~/`) ka effect

| Action route                               | Result                                    |
| ------------------------------------------ | ----------------------------------------- |
| `[Route("Index")]` (controller par `Home`) | `/Home/Index` (combine hota hai)          |
| `[Route("/Index")]`                        | `/Index` (controller route **override**)  |
| `[Route("~/")]`                            | `/` (root URL, controller route override) |

Isliye agar controller par `[Route("Home")]` hai aur action par `[Route("/index")]` likhoge to URL `/index` banega, `/Home/index` **nahi**. Combine karwana ho to action ke route me leading `/` mat lagao.

### Root URL par error se bachne ke liye

Agar controller par `[Route("Home")]` laga hai to sirf `localhost:5000/` kholne par 404 aayega. Isse bachne ke liye ek action par ye lagao:

```csharp
[Route("~/")]
public IActionResult Index() => View();
```

`~/` ka matlab hai root, aur ye controller ke route ko ignore karta hai.

### Token replacement: `[controller]` aur `[action]`

Naam khud likhne ke bajay token use kar sakte ho (square brackets me):

```csharp
[Route("[controller]")]
public class HomeController : Controller
{
    [Route("[action]")]
    public IActionResult Privacy() => View();   // /Home/Privacy
}
```

| Token          | Replace hota hai                                    |
| -------------- | --------------------------------------------------- |
| `[controller]` | Controller ke naam se (bina `Controller` suffix ke) |
| `[action]`     | Action method ke naam se                            |

Controller par ek baar likh do, to sab actions ko ek saath route mil jaata hai:

```csharp
[Route("[controller]/[action]")]
public class HomeController : Controller
{
    public IActionResult Index() => View();     // /Home/Index
    public IActionResult Privacy() => View();   // /Home/Privacy
    public IActionResult Hello() => Content("Hello");   // /Home/Hello
}
```

## Method for Routing...

Map,MapGet,MapPost,MapPut, & MapDelete

ye saare Http ke methodh jo ki routing ke liye used hote hi....

Map srif url ka path dekhta hi wo http method nhi dekhta hi ki wo konsi http method hi like get, post, put etc ......
wo sb pr chelge mtlb apne post, put, get, ye sb Map ket rhoguh chala skte hi

The MapGet method is used to defined an endpoint.

An endpoint is something that can be : Selected by matching url and HTTP Methods........

Excuted by running a delegates jo wo excute krta hi wo ek delegates ke through jakar run hota hi .......

apne api do tarike se create krte hi

Minimal api
Controller ke ander api

dono ka kaam same bus krane ka tarika thoda different hi

minimal api ke ander

apne program.cs file ke ander sb saare endpoints defined kr dete hi

abd controller ke ander apne hr ek controller file ke ander krte hi

like program.cs me file me apne iss trh krtehi ...

// Minimal API
app.MapGet("/products", () => products);
app.MapPost("/products", (Product p) => Results.Created($"/products/{p.Id}", p));

and controller file ke ander apne iss trh se karte hi ..

// Controller
[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
[HttpGet]
public IActionResult GetAll() => Ok(products);

    [HttpPost]
    public IActionResult Create(Product p) => Created($"/products/{p.Id}", p);

}

hr action ke upar method ko defined kr dete hiiiiii

jb apne chhota project ho ya microservice ho to wha pr apne ko minimal pai ko create krkna

and jaha pr bade mvc and enterprise ke proejct ho to wha pr apne ko controller wle api create krna

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
// GET
endpoints.MapGet("/", () =>
{
return "Welcome to Student API! - GET";
});

    // GET + HttpContext + async/await
    endpoints.MapGet("/hello", async (HttpContext context) =>
    {
        context.Response.ContentType = "text/plain";

        await context.Response.WriteAsync(
            "Hello! Response sent using HttpContext. - GET"
        );
    });

    // PUT
    endpoints.MapPut("/students/{id:int}",
        async (int id, HttpContext context) =>
    {
        await context.Response.WriteAsync(
            $"Student {id} updated using PUT."
        );
    });

    // POST
    endpoints.MapPost("/students", async (HttpContext context) =>
    {
        await context.Response.WriteAsync(
            "Student created using POST."
        );
    });

});

app.Run();

like ye minimal apis ki trh apne ne create kri hi ...

UseEndpoints() mein MVC Controllers kaise map karte hain?
Agar tum MVC Controller use kar rahe ho, to pehle controllers ki services register karo.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
endpoints.MapControllers();
});
app.Run();
Yahan:
AddControllers() controller-related services register karta hai.
UseRouting() request ke liye matching route select karta hai.
MapControllers() attribute-routed controllers ke endpoints register karta hai.
Example controller:
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
[HttpGet]
public IActionResult GetStudents()
{
return Ok("All students");
}
[HttpGet("{id:int}")]
public IActionResult GetStudent(int id)
{
return Ok($"Student ID: {id}");
}
}
Is controller ke routes honge:
GET /api/students
GET /api/students/5
Note: Is controller example ke liye using Microsoft.AspNetCore.Mvc; bhi chahiye.

MVC Controller ke ander apne same class ke ander
same methdo/action define nhi kr skte hi
chahe unke upar tuhme HttpGet likha ho ya HttpPost
alg http method likhne se bhi kaam nhi chlega

tum same class ke ander same method ko defined nhi kr skte ho....

## ---------------------------------------------

View ur model ke beech me koi bhi communication nhi hota hi

view controller contact krta hi and model controller se contanct dono srif controller se contact

agar model na to user ki request controller ke pass jyegi after that view ke pass chle jyegi

agar yesa view hota hi to controller phle model ke pass jata hi after that wo view ke pass jata hi ...

basically apne ye bol skte hi

controller apn view ur model ke beech communication krwa ke deta hi....

url ke ander apne jb bhi enter krte hi broswer se to

to wo sbsse phele routing ke pass jata hi

then routing ke baad controller ke pass jata hi uske pass action method wo excute hota hi then wo view ke pass chala jata hi.....

url ----> routing ----> controller ----> action method ---> view.

ye flow hota hi isko yaad rkhna chiye ....

Controller manages the flow of application.

A controller is used to define and group set of actions.

Is responsible for intercepting incoming requests and excuting the appropriate application code.

Controller communicates with the model of the application and selects the required view to be rendered for the requests.

view ke ande jo bhi data aa rha hota hi wo model ke ander se aa rha hota hi....

Allow sperating the buisness logic of the applications from the presentation logic.

Incoming requests are mapped to actions through routing.

Controller are basically C# classes that inherit from inherit from
Microsoft.AspNetCore.MVC.Controller

jb apne file create uske suffic me Controller name hona hi chiye usko nhi hatan hi
like
HomeController.cs

AdminController.cs

Home , Admin ki naam change ho skta hi but Controller naam hona hi chiye ...

Controller jo ki action methods hoti hi incoming request ke according required information reterives krti hi model se and provide krti hi suitable responses.

It is recommend that class name of controller ends with suffix 'Controller'.

jo bhi apne request ke ander pass krte hi to controller hi usko action method tk transfer krta hi..

Exceptions ko jo bhi handle krne ka kaam hi wo bhi controller hi krta hi...

## ----------------------------------------------------------------------------------------------------------------

## Action Methods

Controller ke ander apne ek ur ek se jdya action methods bana skte hi...

Action methods public method hoti hi kyuki apni requests controller ke pass jaati hi isliye....

Action method agar me private kr dunga to wo kya return krege isliye me usko public rkhta hu........

Action method may can return multiple types of data

By default it generate response in the form of IActionResult interface or ActionResult Abstract class.

IActionResult return type apne tb rkhte hi jb apne ko pata hota hi ki ye multiple responses ko generate kr skte hi

IActionResult or ActionResult work as a container for other action results...
basically ye ek container ka kaam krte hi ur wo container kon hote wo hote apne dusre class........

ActionMethod Konse konse Return type hote hi...

IActionResult

# ASP.NET Core — Controller Action Return Types

## 1. Controller Action Return Type Kya Hota Hai?

Controller action ek method hota hai jo HTTP request handle karta hai. Uska return type decide karta hai ki client ko response mein kya milega.

```csharp
[HttpGet]
public int GetAge()
{
    return 21;
}
```

Real APIs mein different situations ho sakti hain:

- Student mil gaya → `200 OK`
- Student nahi mila → `404 Not Found`
- Request invalid hai → `400 Bad Request`

In situations ko handle karne ke liye ASP.NET Core mein different return types available hain.

## 2. Main Return Types

| Return Type               | Meaning                                     |
| ------------------------- | ------------------------------------------- |
| `IActionResult`           | Different HTTP responses return karna       |
| `ActionResult<T>`         | Typed data ya HTTP result                   |
| `Task<IActionResult>`     | Async operation + different HTTP responses  |
| `Task<ActionResult<T>>`   | Async operation + typed data ya HTTP result |
| `T`, `Student`, `List<T>` | Direct data return karna                    |
| `string`, `int`           | Simple values return karna                  |
| `void`, `Task`            | Explicit response body return nahi karna    |
| `IResult`                 | Minimal APIs ka result abstraction          |

---

## 3. IActionResult

### Definition

`IActionResult` ASP.NET Core MVC ka interface hai jo HTTP response represent karne wale result objects ke liye use hota hai.

**Simple meaning:** Ek hi controller action se different HTTP responses return kar sakte hain.

### Example

```csharp
[HttpGet("{id}")]
public IActionResult GetStudent(int id)
{
    if (id <= 0)
    {
        return BadRequest("Invalid student ID");
    }

    if (id != 1)
    {
        return NotFound("Student not found");
    }

    return Ok(new
    {
        Id = 1,
        Name = "Sourabh"
    });
}
```

### Possible Responses

| Condition         | Method         | Status Code |
| ----------------- | -------------- | ----------- |
| ID invalid hai    | `BadRequest()` | 400         |
| Student nahi mila | `NotFound()`   | 404         |
| Student mil gaya  | `Ok(data)`     | 200         |

### Common IActionResult Helper Methods

| Method              | Status Code | Purpose                                         |
| ------------------- | ----------: | ----------------------------------------------- |
| `Ok(data)`          |         200 | Successful response with data                   |
| `Created()`         |         201 | Resource create hua                             |
| `CreatedAtAction()` |         201 | Resource create hua, location bhi provide karna |
| `NoContent()`       |         204 | Success without response body                   |
| `BadRequest()`      |         400 | Invalid request                                 |
| `Unauthorized()`    |         401 | Authentication required ya failed               |
| `Forbid()`          |         403 | Permission nahi hai                             |
| `NotFound()`        |         404 | Resource nahi mila                              |
| `Conflict()`        |         409 | Resource state se conflict                      |
| `StatusCode(500)`   |         500 | Specific status code return karna               |

### 401 vs 403

- **401 Unauthorized:** User authenticated nahi hai ya valid authentication credentials provide nahi hue.
- **403 Forbidden:** User authenticated hai, lekin uske paas required permission nahi hai.

JWT/RBAC example:

- Missing ya invalid token → `401`
- Valid token, lekin doctor-only endpoint ko patient access kar raha hai → `403`

### IActionResult Kab Use Karein?

Jab controller action ko different responses return karne hon, jaise:

- `200 OK`
- `400 Bad Request`
- `404 Not Found`
- `204 No Content`
- Redirect ya file response

---

## 4. ActionResult<T>

### Definition

`ActionResult<T>` controller action ke expected response data type ko specify karta hai aur HTTP result bhi return karne deta hai.

Yahan `T` kisi bhi appropriate data type ko represent karta hai, jaise `Student`, `List<Student>` ya DTO.

### Example

```csharp
[HttpGet("{id}")]
public ActionResult<Student> GetStudent(int id)
{
    if (id != 1)
    {
        return NotFound();
    }

    var student = new Student
    {
        Id = 1,
        Name = "Sourabh"
    };

    return student;
}

public class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
```

### Expected Responses

- `GET /api/students/1` → `200 OK` + Student JSON
- `GET /api/students/2` → `404 Not Found`

### IActionResult vs ActionResult<T>

| Feature                                     | IActionResult               | ActionResult<T>     |
| ------------------------------------------- | --------------------------- | ------------------- |
| Different HTTP responses                    | Yes                         | Yes                 |
| Expected data type explicitly specify karna | No                          | Yes                 |
| Direct typed object return                  | Usually `Ok(data)`          | `return student;`   |
| Typed API documentation                     | Less explicit               | Generally clearer   |
| Typical use                                 | Multiple kinds of responses | Typed data + errors |

### Practical Rule

Agar API ka main response `Student`, `List<Student>` ya DTO hai aur errors bhi handle karne hain, toh `ActionResult<T>` usually convenient choice hai.

Agar endpoint views, redirects, files ya different result types return karta hai, toh `IActionResult` useful hai.

---

## 5. Direct Data Types

Har controller action mein `IActionResult` use karna compulsory nahi hai.

### A. Student Object Return Karna

```csharp
[HttpGet]
public Student GetStudent()
{
    return new Student
    {
        Id = 1,
        Name = "Sourabh"
    };
}
```

Typical Web API configuration mein ASP.NET Core object ko JSON mein serialize karta hai.

Response:

```json
{
  "id": 1,
  "name": "Sourabh"
}
```

### B. List Return Karna

```csharp
[HttpGet]
public List<Student> GetStudents()
{
    return new List<Student>
    {
        new Student { Id = 1, Name = "Sourabh" },
        new Student { Id = 2, Name = "Rahul" }
    };
}
```

Response:

```json
[
  {
    "id": 1,
    "name": "Sourabh"
  },
  {
    "id": 2,
    "name": "Rahul"
  }
]
```

### C. Integer aur String Return Karna

```csharp
[HttpGet("count")]
public int GetCount()
{
    return 100;
}

[HttpGet("message")]
public string GetMessage()
{
    return "Hello from API";
}
```

**Important:** Agar status code explicitly control karna hai, toh `IActionResult` ya `ActionResult<T>` use karna zyada flexible hai.

---

## 6. Task<IActionResult>

### Definition

`Task<IActionResult>` ka matlab hai ki method asynchronous operation perform karke ek `IActionResult` provide karega.

Jab database, external API ya kisi I/O operation ka wait karna ho, tab asynchronous programming useful hoti hai.

### Example

```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetStudent(int id)
{
    var student = await _dbContext.Students.FindAsync(id);

    if (student == null)
    {
        return NotFound();
    }

    return Ok(student);
}
```

### Explanation

- `Task<IActionResult>` → Asynchronous method ka return type.
- `async` → Method ke andar `await` use kar sakte hain.
- `FindAsync(id)` → Database se student find karta hai.
- `await` → Operation complete hone ka asynchronously wait karta hai.
- `NotFound()` → Student nahi mila.
- `Ok(student)` → Student mil gaya.

`_dbContext` ko controller mein dependency injection ke through receive karna hoga.

### Async/Await Ka Benefit

Database operation ka wait karte waqt request handle karne wala thread unnecessarily block nahi hota. Isse server busy hone par scalability improve ho sakti hai.

Node.js mein `await` se asynchronous operation wait karne ka concept similar hai, lekin C# ka `Task<T>` type system ka part hai.

---

## 7. Task<ActionResult<T>>

Jab asynchronous database operation ke saath typed data aur error responses dono chahiye, tab `Task<ActionResult<T>>` use kar sakte hain.

### Example

```csharp
[HttpGet("{id}")]
public async Task<ActionResult<Student>> GetStudent(int id)
{
    var student = await _dbContext.Students.FindAsync(id);

    if (student == null)
    {
        return NotFound();
    }

    return student;
}
```

### Difference

| Return Type                   | Meaning                                       |
| ----------------------------- | --------------------------------------------- |
| `IActionResult`               | Different HTTP results                        |
| `ActionResult<Student>`       | Student data ya HTTP result                   |
| `Task<IActionResult>`         | Async operation + different HTTP results      |
| `Task<ActionResult<Student>>` | Async operation + Student data ya HTTP result |

**Remember:** `async` sirf tab use karo jab actual asynchronous operation ho, jaise EF Core ka `FindAsync()` ya `ToListAsync()`.

---

## 8. Specific Action Result Types

`IActionResult` ek general interface hai. `ViewResult`, `JsonResult`, `ContentResult` aur doosri classes specific response types represent karti hain.

### A. ViewResult

HTML page render karne ke liye.

```csharp
public IActionResult Index()
{
    return View();
}
```

Example view: `Views/Home/Index.cshtml`

**Use:** MVC web pages, dashboards aur server-rendered forms.

### B. JsonResult

Explicitly JSON response return karne ke liye.

```csharp
public IActionResult GetStudent()
{
    return Json(new
    {
        Id = 1,
        Name = "Sourabh"
    });
}
```

Typical Web API mein `Ok(student)` bhi object ko JSON response ke roop mein return kar sakta hai.

### C. ContentResult

Plain text ya custom content return karne ke liye.

```csharp
public IActionResult Message()
{
    return Content("Hello, Student!");
}
```

### D. RedirectToActionResult

Client ko doosre controller action par redirect karta hai.

```csharp
public IActionResult GoHome()
{
    return RedirectToAction("Index", "Home");
}
```

**Use:** MVC forms submit hone ke baad doosre page par redirect karna.

### E. NoContentResult

Successful response without a response body.

```csharp
public IActionResult EmptyResponse()
{
    return NoContent();
}
```

HTTP status: `204 No Content`

### F. FileResult

File download ya file response ke liye.

```csharp
public IActionResult Download()
{
    var bytes = System.Text.Encoding.UTF8.GetBytes("Sample file");

    return File(bytes, "text/plain", "sample.txt");
}
```

Yeh `sample.txt` file response return karta hai.

### Quick Summary

| Method               | Purpose              |
| -------------------- | -------------------- |
| `View()`             | HTML view            |
| `Json()`             | JSON response        |
| `Content()`          | Plain/custom content |
| `RedirectToAction()` | Action par redirect  |
| `File()`             | File response        |
| `NoContent()`        | 204 response         |

---

## 9. void vs Task

### A. void

```csharp
[HttpPost]
public void LogActivity()
{
    // Activity log karne ka example
}
```

`void` method koi typed return value nahi deta.

Controller actions mein explicit response status aur body control karne ke liye `void` usually ideal choice nahi hai.

### B. Task

```csharp
[HttpPost]
public async Task LogActivity()
{
    await Task.Delay(10);
}
```

`Task` asynchronous operation represent karta hai, lekin typed result return nahi karta.

### Difference

| Feature                                        | void                            | Task                            |
| ---------------------------------------------- | ------------------------------- | ------------------------------- |
| Typed result                                   | No                              | No                              |
| Asynchronous operation represent kar sakta hai | No                              | Yes                             |
| Await kiya ja sakta hai                        | No                              | Yes                             |
| Explicit HTTP response                         | Automatically specify nahi hota | Automatically specify nahi hota |

**Production APIs:** Meaningful status codes aur response bodies ke liye `IActionResult`, `ActionResult<T>` ya suitable typed return types choose karo.

---

## 10. IActionResult vs IResult

Dono HTTP responses return karne mein use hote hain, lekin different ASP.NET Core programming models se associated hain.

| IActionResult                             | IResult                                            |
| ----------------------------------------- | -------------------------------------------------- |
| MVC Controllers                           | Minimal APIs                                       |
| `return NotFound();`                      | `return Results.NotFound();`                       |
| `Ok()`, `View()` jaise controller helpers | `Results.Ok()`, `Results.NotFound()` jaise helpers |

### Controller Example

```csharp
[HttpGet]
public IActionResult Get()
{
    return NotFound();
}
```

### Minimal API Example

```csharp
app.MapGet("/students", () =>
{
    return Results.NotFound();
});
```

Express.js mein `res.status(404).json(...)` se HTTP response set karne ka concept similar hai, lekin ASP.NET Core ke result abstractions aur execution flow alag hain.

---

## 11. Kaunsa Return Type Choose Karein?

| Requirement                               | Recommended Return Type                         |
| ----------------------------------------- | ----------------------------------------------- |
| Student data ya 404                       | `ActionResult<Student>`                         |
| Students list ya error                    | `ActionResult<List<Student>>`                   |
| Multiple different HTTP responses         | `IActionResult`                                 |
| Async database query + flexible responses | `Task<IActionResult>`                           |
| Async database query + typed data/errors  | `Task<ActionResult<Student>>`                   |
| Simple data                               | `Student`, `List<Student>`, etc.                |
| MVC HTML page                             | `IActionResult` + `View()`                      |
| Minimal API                               | `Results.*`, `IResult` ya suitable typed return |

---

## 12. Full Practical Example — StudentsController

Yeh example typed data, HTTP errors, async database queries aur CRUD operations combine karta hai.

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    // Dependency Injection
    public StudentsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET api/students
    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetAll()
    {
        var students = await _dbContext.Students.ToListAsync();

        return students;
    }

    // GET api/students/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid ID");
        }

        var student = await _dbContext.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        return student;
    }

    // POST api/students
    [HttpPost]
    public async Task<ActionResult<Student>> Create(Student student)
    {
        _dbContext.Students.Add(student);

        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = student.Id },
            student
        );
    }

    // DELETE api/students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _dbContext.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        _dbContext.Students.Remove(student);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
```

### Is Example Mein Kya Seekha?

- `GetAll()` → `Task<ActionResult<List<Student>>>`
- `GetById()` → `Task<ActionResult<Student>>`
- `Create()` → `201 Created` with resource location
- `Delete()` → `Task<IActionResult>` with `204 No Content`
- `ControllerBase` → Web API controllers ke liye.
- `Controller` → MVC views render karne ke liye bhi functionality provide karta hai.

**Note:** Example assume karta hai ki `AppDbContext`, `Student` entity aur database configuration already set up hain.

---

## 13. Common Mistakes

| Mistake                                                        | Correct Approach                                          |
| -------------------------------------------------------------- | --------------------------------------------------------- |
| Har jagah `async` lagana                                       | Real asynchronous operation ho tab use karo               |
| API mein `void` ko default banana                              | Explicit response types consider karo                     |
| Errors ko bhi `Ok()` mein bhejna                               | Correct HTTP status code return karo                      |
| `IActionResult` aur `IResult` mix karna                        | Controller aur Minimal API model ke according choose karo |
| Status code control chahiye, phir bhi direct data return karna | `IActionResult` / `ActionResult<T>` consider karo         |
| `401` aur `403` ko same samajhna                               | Authentication aur authorization ka difference samjho     |

---

## View - Razor Syntax & Razor View Engine ....

.View kya hai?
View MVC ka presentation layer hai, jo user ko UI dikhata hai. Files .cshtml extension ki hoti hain aur Views/<Controller>/<Action>.cshtml structure mein rakhi jaati hain. Business logic ya DB queries View mein nahi likhni chahiye.

Razor aur Razor View Engine
Razor ek syntax hai jo HTML ke saath C# mix karne deta hai, @ symbol se. Razor View Engine .cshtml ko process karke plain HTML banata hai, aur browser sirf wahi HTML dekhta hai (C# server par hi chalta hai).

Razor ke main constructs

@name: variable ya expression ki value print karta hai. @(age + 1) jaise expression ke liye brackets lagao.
@{ ... }: C# code block, jisme statements chalte hain par output apne aap nahi aata.
@if / @else: condition ke hisaab se HTML render.
@foreach: collection ko loop karke HTML list banata hai.
var compulsory nahi hai; string, int jaise explicit types bhi valid hain, kyunki var C# ka keyword hai, Razor ka nahi.

Strongly typed View

@model ProductViewModel View ka expected type declare karta hai (chhota m).
@Model.Name actual object ka data access karta hai (bada M).
Controller return View(product); se model pass karta hai.

Layout View....

Layout View ek shared template hota hai jisme wo common UI rakhi jaati hai jo saare pages par repeat hoti hai, jaise header, navbar, footer, CSS/JS links. Isse har View mein wahi code baar-baar nahi likhna padta.

Default file hoti hai Views/Shared/\_Layout.cshtml.

<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"] - MyApp</title>
    <link rel="stylesheet" href="~/css/site.css" />
</head>
<body>
    <nav>...navbar...</nav>

    <main>
        @RenderBody()
    </main>

    <footer>...footer...</footer>

    @await RenderSectionAsync("Scripts", required: false)

</body>
</html>

yaha pr @RenderBody() me child page ka content automatically aa jyege
jaha layout me used hogi ye file...

to ye kaise jyega child page (Means Konse Page is Layout ko used krna hi to me iska path dunga...)

jo apna razor syntax hota hi na @ wala uske ander me iss layout ko pin kr dunga

@{
ViewData["Title"] = "Home";
Layout = "~/Views/Shared/ \_Layout.cshtml" //path de dunga me...
}

Layout View Provides Consistent Looks for all the views in a web appplication.

Layout View is as same as Asp.net web forms MASTER PAGE.

Extension of layout view is .cshtml.

Default Name of Layout view is \_Layout.cshtml.

Layout View files is usally located in Views/Shared Folder...

## --------------------------------------------------------------------------------------------

# ASP.NET Core MVC Notes

## Part 1: `_ViewStart.cshtml`

### 1. Why do we need `_ViewStart.cshtml`?

Agar har View ke andar Layout define karna padta hai, to har file mein Layout ka path likhna padta hai.

Suppose we have 100 Views in our application and all 100 Views want to use the same Layout file. Then we need to set the `Layout` property in all 100 Views.

Matlab same line 100 baar repeat karni padegi:

```html
@{ ViewData["Title"] = "Home"; Layout = "~/Views/Shared/_Layout.cshtml"; }
```

> **Note:** Layout file **ek hi** hoti hai. 100 layout files nahi banti, sirf `Layout = ...` wali line 100 Views mein repeat hoti hai.

Isse problems hoti hain:

- Same cheez har file mein repeat karni padti hai.
- Agar layout ka naam ya path change karna ho, to 100 files edit karni padengi.

### 2. Solution: `_ViewStart.cshtml`

`_ViewStart.cshtml` ek special file hai jo **har View render hone se pehle automatically run hoti hai**.

Isse `Views` folder ke andar banate hain: `Views/_ViewStart.cshtml`

```html
@{ Layout = "_Layout"; }
```

Ab har View automatically `_Layout` use karega. Har View mein alag se Layout likhne ki zaroorat nahi.

Complete path likhna zaroori nahi:

- `"~/Views/Shared/_Layout.cshtml"` bhi valid hai.
- Sirf `"_Layout"` bhi kaafi hai, kyunki Razor `Views/Shared` mein khud search kar leta hai.

### 3. Execution order and priority

```
Root _ViewStart  ->  Folder _ViewStart  ->  View ka apna code
```

Jo baad mein aata hai wo jeetta hai.

| Level                              | Effect                                                          |
| ---------------------------------- | --------------------------------------------------------------- |
| `Views/_ViewStart.cshtml`          | Sabhi Views par apply hoti hai                                  |
| `Views/Admin/_ViewStart.cshtml`    | Sirf Admin folder ke Views par, root wale ko override karti hai |
| View ke andar `Layout = "_Other";` | Sirf us View ke liye override                                   |
| `Layout = null;`                   | Layout hata deta hai                                            |

### 4. Multiple layout files hon to?

We can select a layout conditionally in the `_ViewStart` file, kyunki ye normal C# code hai.

**Option A: Role ke basis par**

```html
@{ if (User.IsInRole("Admin")) { Layout = "_AdminLayout"; } else { Layout =
"_Layout"; } }
```

**Option B: Request path ke basis par**

```html
@{ var path = Context.Request.Path.Value ?? ""; Layout =
path.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase) ? "_AdminLayout" :
"_Layout"; }
```

**Option C (simple aur clean): folder-wise alag `_ViewStart.cshtml`**

```
Views/
├── _ViewStart.cshtml          -> Layout = "_Layout";
├── Home/
└── Admin/
    ├── _ViewStart.cshtml      -> Layout = "_AdminLayout";
    └── Dashboard.cshtml
```

Admin folder ke saare Views `_AdminLayout` use karenge, baaki saare `_Layout`. Ye approach tab best hai jab layout folder ke hisaab se alag ho.

### 5. `_ViewStart` vs `_ViewImports`

| File                  | Purpose                                                                                             |
| --------------------- | --------------------------------------------------------------------------------------------------- |
| `_ViewStart.cshtml`   | Har View se pehle code run karti hai (mainly Layout set karne ke liye)                              |
| `_ViewImports.cshtml` | Shared directives (`@using`, `@addTagHelper`, `@inject`) taaki har View mein repeat na karne padein |

---

## Part 2: Passing Data from Controller to View

Data ko controller se View tak pass karne ke multiple tarike hain:

1. `ViewData`
2. `ViewBag`
3. `TempData`
4. Strongly Typed Views (Model / ViewModel)

### 1. ViewData

`ViewData` controller se View tak data pass karne ke liye use hota hai. Data **key-value pair** form mein store hota hai.

```csharp
ViewData["<Key>"] = <Value>;
```

- **Key:** string, jisse object identify hota hai.
- **Value:** koi bhi `object`: string, int, DateTime, array, list, etc.

**Controller:**

```csharp
public IActionResult Hello()
{
    ViewData["data1"] = "Hello world";
    ViewData["data2"] = 23;
    ViewData["data3"] = DateTime.Now.ToLongDateString();
    return View();
}
```

**View (Razor syntax `@` se access karte hain):**

```html
<h2>@ViewData["data1"]</h2>
<p>@ViewData["data2"]</p>
<p>@ViewData["data3"]</p>
```

`@` lagana zaroori hai, warna value Razor se output nahi hogi, plain text dikhega.

**Collection ke saath type casting zaroori hai**

```csharp
string[] arr = { "Hello ", "World", "To", "This" };
ViewData["data4"] = arr;
```

```html
@foreach (var item in (string[])ViewData["data4"]) {
<h2>@item</h2>
}
```

(Ye `@{ ... }` block ke andar likhne ki zaroorat nahi, seedha `@foreach` kaafi hai.)

**ViewData ke important points**

- Lifetime sirf **current request** tak hoti hai. Agar request redirect ho jaye, to ViewData ki value `null` ho jati hai.
- Complex type (list, array, object) ke saath **type casting** karni padti hai.
- Ye `ViewDataDictionary` hai, aur values `object` type mein store hoti hain.
- **Compile-time checking nahi hoti.** Key ki spelling galat ho to error runtime par pata chalta hai (null milta hai).
- Layout mein `ViewData["Title"]` isi se set/read hota hai.

### 2. ViewBag

`ViewBag` bhi controller se View tak data pass karne ke liye hota hai, bas syntax thoda alag hai.

```csharp
ViewBag.<PropertyName> = <value>;
```

```csharp
public IActionResult Hello()
{
    ViewBag.Message = "Hello world";
    ViewBag.Names = new List<string> { "A", "B", "C" };
    return View();
}
```

```html
<h2>@ViewBag.Message</h2>

@foreach (var name in ViewBag.Names) {
<p>@name</p>
}
```

**ViewBag ke points**

- `ViewBag` ek **dynamic** property hai, aur internally **ViewData ko hi use karta hai** (`ViewBag.Message` aur `ViewData["Message"]` same value hain).
- `ViewBag` `Controller` base class mein hoti hai (`ControllerBase` mein nahi).
- Collection ke saath **type casting ki zaroorat nahi** padti (dynamic hone ki wajah se).
- Lifetime ViewData jaisi hi hai: sirf current request, redirect par lost.
- **Compile-time checking ViewBag mein bhi nahi hoti**, kyunki wo dynamic hai. Dono mein errors runtime par aate hain.

### 3. TempData

`TempData` bhi key-value pair mein data store karta hai.

```csharp
TempData["data1"] = "Hello World";
TempData["data2"] = 1234;
```

**Kab use hota hai?** Mainly **redirection** ke case mein, jab data ek request se agli request tak chahiye (jaise form submit ke baad success message).

```csharp
[HttpPost]
public IActionResult Create(CreateProductDto dto)
{
    // save...
    TempData["Success"] = "Product created!";
    return RedirectToAction("Index");
}
```

```html
@if (TempData["Success"] != null) {
<div class="alert alert-success">@TempData["Success"]</div>
}
```

**TempData ke points**

- Data **read hone ke baad delete ho jata hai** (mark for deletion). Matlab ek baar read kar liya, to dobara nahi milega, chahe wo kisi bhi View/action mein ho.
- Redirect ke case mein reliable hai, isliye wahan sabse zyada use hota hai.
- Ye `ITempDataDictionary` hai (`TempDataDictionary` class se implement).
- Default mein data **cookie** (ya configure karne par session) mein store hota hai.
- Sirf simple types (string, int, bool, DateTime, Guid) direct store hote hain. **Complex object / list ko JSON serialize** karke store karna padta hai, aur wapas deserialize karna padta hai.

**Data ko delete hone se bachana**

| Method                 | Kya karta hai                                       |
| ---------------------- | --------------------------------------------------- |
| `TempData.Keep()`      | Saari keys ko next request ke liye retain karta hai |
| `TempData.Keep("key")` | Sirf us key ko retain karta hai                     |
| `TempData.Peek("key")` | Value padhta hai **bina delete mark kiye**          |

```csharp
var msg = TempData["Success"];   // read ho gaya, delete mark hoga
TempData.Keep("Success");        // ab agli request ke liye bhi rahega

var msg2 = TempData.Peek("Success"); // read bhi, aur delete bhi nahi
```

### 4. Strongly Typed Views (Model / ViewModel)

Production mein sabse recommended tarika. Compile-time checking milti hai aur type casting nahi padti.

**ViewModel:**

```csharp
public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

**Controller:**

```csharp
public IActionResult Details()
{
    var product = new ProductViewModel { Id = 1, Name = "Laptop", Price = 50000 };
    return View(product);
}
```

**View:**

```html
@model MyApp.Models.ProductViewModel

<h1>@Model.Name</h1>
<p>Price: @Model.Price</p>
```

- `@model` (chhota m): View ka expected type declare karta hai.
- `@Model` (bada M): actual object access karta hai.

### 5. Comparison

| Feature            | ViewData                | ViewBag            | TempData                             | Strongly Typed Model |
| ------------------ | ----------------------- | ------------------ | ------------------------------------ | -------------------- |
| Type               | Dictionary (`object`)   | `dynamic`          | Dictionary (`object`)                | Actual class         |
| Direction          | Controller -> View      | Controller -> View | Action -> Action (redirect)          | Controller -> View   |
| Lifetime           | Current request         | Current request    | Next request (read hone tak)         | Current request      |
| Redirect par       | Lost                    | Lost               | Survive karta hai                    | Lost                 |
| Type casting       | Zaroori                 | Nahi               | Zaroori                              | Nahi                 |
| Compile-time check | Nahi                    | Nahi               | Nahi                                 | **Haan**             |
| Best for           | Chhota data, page title | Chhota data        | Success/error message after redirect | Main page data       |

---

## Models...............

Model jo hoti hi wo apni c# classes hoti hi ur interact krti hi data source ,
Data source kya ky hoti hi apna Database bhi ho skta hi
Manual List or Collection bhi ho skte hi ...

A Model is a class with .cs (for c#) as an extension haciving both properities ur method.

maan apne ek student class bante hi uske ander kya kya properities hogi like

    rollNo
    Name
    Gender
    Class

Model are basically used to set or get the data.

mtlb apne whi data ko set kr skte hi ur get bhi kr skte hi

wo get ur set properities ke through jo apne padhi thi ...

If your application does not have data then there is no need for a model.

If your application has a data then you need a model..

We can perform operation like insert , update, delete by using models..

Note :- It is not Mandatory but it is good programmmmmmming practice to store all model classess with in the model folder.

namespace ModelsInASPCore.Models
{
public class studentModel
{
public int rollNo { get; set; }

        public string Name { get; set; }
        public string Gender { get; set; }
        public int Standard { get; set; }
    }

}

ye apne ek model ko create kr diya

issme agar apne ko data set krna hi usski best practice hoti hi apne ek repository pattern ka used krte hi

repository pattern apne abhi thode baad me dekhte hi

ab hi me inta data generate ek list ke through krwa dunga.......

    public IActionResult Index()
        {

            var students = new List<studentModel>
            {
              new studentModel{ rollNo = 1,Name = "Hello World",Gender = "M",Standard = 12},
              new studentModel{ rollNo = 1,Name = "Hello World",Gender = "M",Standard = 12},
              new studentModel{ rollNo = 1,Name = "Hello World",Gender = "M",Standard = 12},
              new studentModel{ rollNo = 1,Name = "Hello World",Gender = "M",Standard = 12},
              new studentModel{ rollNo = 1,Name = "Hello World",Gender = "M",Standard = 12}
            };
        }

view me abhi ke liye yesa defined kr diya hi

@using ModelsInASPCore.Models

@{
ViewData["Title"] = "Home Page";
var students = ViewData["myStudentData"] as List<studentModel>;
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>Student Controller Index Action Method is called.</p>
</div>

@if (students != null && students.Count > 0)
{
foreach (var item in students)
{

<div class="card p-3 mb-3">
<h2>Name: @item.Name</h2>
<p>Gender: @item.Gender</p>
<p>Roll No: @item.rollNo</p>
<p>Standard: @item.Standard</p>
</div>
}
}
else
{
<p class="text-center">No students found.</p>
}

abhi to iss thr kr rhe hi but ye shi method nhi hoti hi
wo repository pattern ko follow krke used krne ke liye hoti hi ...

Repository pattern se padhne se phele apne ko depenedency injection bhi aana chiye to phele apne isko isko dekhege...............

# Dependency Injection (DI) in ASP.NET Core

> Ye notes tumhare likhe hue notes par based hain. Concepts ko step-by-step, example ke saath, aur "kyun" ke saath explain kiya gaya hai. Jahan tumhare notes mein chhoti galti thi, wahan **Correction** mein bataya hai.

---

## 1. Dependency kya hoti hai?

Jab ek class ko apna kaam karne ke liye doosri class ki zaroorat padti hai, to wo doosri class uski **dependency** hai.

- `StudentController` ko student ka data chahiye.
- Data `StudentService` deti hai.
- Isliye `StudentController`, `StudentService` par **depend** karta hai.
- Matlab: `StudentService` = **dependency**, `StudentController` = **dependent class**.

```
StudentController  ----needs---->  StudentService
 (dependent)                        (dependency)
```

Real life example: Ek **chef** ko khana banane ke liye **gas, sabzi, masale** chahiye. Chef gas aur sabzi par depend karta hai. Chef khud sabzi ugata nahi, kisi se milti hai.

---

## 2. Dependency Injection kya hai?

> **Dependency Injection = Class apni dependency khud create nahi karti. Wo bahar se (ASP.NET Core ke DI container se) provide ki jati hai.**

Tumhare notes ki line: _"Don't create object yourself, let ASP.NET Core provide it for you."_ Yahi DI ka poora matlab hai.

Teen cheezein yaad rakho:

| Term             | Matlab                                                                 |
| ---------------- | ---------------------------------------------------------------------- |
| **Dependency**   | Wo class jiski zaroorat hai (`StudentService`)                         |
| **Injection**    | Us class ka object bahar se andar "daal" dena (constructor ke through) |
| **DI Container** | ASP.NET Core ka built-in system jo objects banata aur inject karta hai |

### Related terms (interview mein poochhe jaate hain)

| Term                                     | Kya hai                                                                                  |
| ---------------------------------------- | ---------------------------------------------------------------------------------------- |
| **DIP** (Dependency Inversion Principle) | SOLID ka "D". Class concrete class par nahi, **abstraction (interface)** par depend kare |
| **IoC** (Inversion of Control)           | Object banane ka control class ke paas se hatke framework ke paas chala jana             |
| **DI**                                   | IoC ko implement karne ka technique                                                      |

Short mein: **DIP = principle, IoC = idea, DI = technique.**

---

## 3. DI ke bina kya problem hoti hai?

Suppose `StudentController` ko `StudentService` chahiye:

```csharp
public class StudentController : Controller
{
    private StudentService _service;

    public StudentController()
    {
        _service = new StudentService();   // controller KHUD object bana raha hai
    }
}
```

Yahan controller ne `new` keyword se khud object banaya. Iske problems:

### Problem 1: Tight Coupling

Controller hamesha `StudentService` se hi chipka hai. Kal `NewStudentService` use karni ho to controller ka code **badalna** padega. Agar 20 controllers mein `new StudentService()` hai, to 20 jagah edit.

### Problem 2: Testing mushkil

Unit test mein asli `StudentService` chalegi (jo shayad database se baat kare). Fake service lagane ka koi tarika nahi.

### Problem 3: Implementation replace karna mushkil

Maan lo `StudentService` ko `SqlStudentService` ya `MockStudentService` se badalna hai. Code mein jahan-jahan `new` hai, sab badlo.

### Problem 4: Object creation ki tension

Agar `StudentService` ko khud `DbContext`, `ILogger`, `IConfiguration` chahiye, to controller ko wo sab bhi banana padega aur pass karna padega. Controller ka kaam request handle karna hai, objects ki chain banana nahi.

### Problem 5: Lifetime management

Object kab tak zinda rahe, kab dispose ho (jaise database connection), ye sab tumhe manually dekhna padega.

---

## 4. DI ke saath solution

Controller ko bas **bata dena hai ki use kya chahiye** (constructor mein). Baaki kaam container karta hai.

```csharp
public class StudentController : Controller
{
    private readonly StudentService _service;

    public StudentController(StudentService service)   // bahar se aayega
    {
        _service = service;
    }
}
```

Dhyan do: **kahin `new` nahi hai.** Controller sirf bol raha hai "mujhe `StudentService` chahiye", aur ASP.NET Core wo object banakar de deta hai.

Isko **Constructor Injection** kehte hain. Microsoft controllers ke liye yahi pattern recommend karta hai.

### Recommended approach (3 steps)

```
1. Service create karo            (IStudentService + StudentService)
2. Built-in DI container mein register karo   (Program.cs)
3. Constructor ke through controller mein inject karo
```

Service ek baar banao, aur **multiple controllers** mein reuse karo.

---

## 5. Interface kyun banate hain?

Tumhare notes mein likha hai: _"Service banate waqt hamesha interface banate hain."_ Ab iska **reason** samjho.

### Agar sirf concrete class inject karo

```csharp
public StudentController(StudentService service)  // concrete class par depend
```

Ye kaam karta hai, lekin controller ab bhi `StudentService` se **chipka** hua hai. Doosri class lagani ho to controller ka code badalna padega.

### Agar interface inject karo

```csharp
public StudentController(IStudentService service)  // abstraction par depend
```

Ab controller ko sirf itna pata hai: _"mere paas kuch hai jo `GetStudentName()` aur `GetStudentAge()` de sakta hai."_ Wo **kaun** hai, ye controller ko pata hi nahi.

### Interface ke fayde

| Fayda              | Explanation                                                                     |
| ------------------ | ------------------------------------------------------------------------------- |
| **Loose coupling** | Controller interface jaanta hai, implementation nahi                            |
| **Easy swapping**  | `Program.cs` mein ek line badlo, implementation badal gayi                      |
| **Easy testing**   | Test mein `FakeStudentService` inject kar sakte ho                              |
| **Contract**       | Interface ek contract hai: jo bhi isko implement karega, ye methods dena padega |

Real life example: **Mobile charger port.** Tumhara phone sirf "USB-C port" jaanta hai (interface). Charger kisi bhi company ka ho (implementation), phone ko farak nahi padta.

---

## 6. Poora example step by step

### Step 1: Interface banao (contract)

`Services/IStudentService.cs`

```csharp
namespace DependencyInjectionServices.Services
{
    public interface IStudentService
    {
        // Interface mein sirf method ki DECLARATION hoti hai, body nahi.
        string GetStudentName();
        int GetStudentAge();
    }
}
```

### Step 2: Class banao jo interface implement kare

`Services/StudentService.cs`

```csharp
namespace DependencyInjectionServices.Services
{
    public class StudentService : IStudentService
    {
        // Interface ki saari methods ki body yahan deni zaroori hai.
        public int GetStudentAge()
        {
            return 33;
        }

        public string GetStudentName()
        {
            return "Sourabh Mourya";
        }
    }
}
```

> Tumhare notes mein is class ko "child class" likha hai. Sahi term hai **implementing class** (ya concrete class).

### Step 3: Service ko register karo (`Program.cs`)

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registration: "Jab koi IStudentService maange, StudentService ka object do."
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();
app.MapControllers();
app.Run();
```

**Registration ka format:**

```csharp
builder.Services.AddScoped<IStudentService, StudentService>();
//                          ^^^^^^^^^^^^^^   ^^^^^^^^^^^^^
//                          Kya maanga jayega  Kya diya jayega
//                          (interface)        (implementation)
```

Ye "do cheezein register karna" nahi hai. Ye **ek mapping** hai: _interface -> implementation_. Container ke paas ek dictionary jaisa record ban jata hai:

```
IStudentService  ->  StudentService   (Scoped)
```

### Step 4: Controller mein inject karo

(Neeche section 7 mein poora commented code hai.)

---

## 7. Controller ka code (commented)

```csharp
using Microsoft.AspNetCore.Mvc;
using DependencyInjectionServices.Services;

namespace DependencyInjectionServices.Controllers
{
    // URL: GET /api/Student/GetStudent
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // Dependency ka field.
        // - Type INTERFACE hai, class nahi (loose coupling).
        // - private  : sirf is class ke andar use hoga.
        // - readonly : sirf constructor mein assign ho sakta hai.
        private readonly IStudentService _studentService;

        // Constructor Injection.
        // Hum "new" nahi likh rahe. Sirf bata rahe hain ki IStudentService chahiye.
        // Object DI container banakar yahan inject karega.
        public StudentController(IStudentService stdService)
        {
            _studentService = stdService;
        }

        [HttpGet]
        public IActionResult GetStudent()
        {
            // Controller khud data nahi banata, service se maangta hai.
            var name = _studentService.GetStudentName();
            var age = _studentService.GetStudentAge();

            // 200 OK + JSON response
            return Ok(new
            {
                Name = name,
                Age = age
            });
        }
    }
}
```

**Response:**

```json
{ "name": "Sourabh Mourya", "age": 33 }
```

---

## 8. Internally flow kaise chalta hai?

Tumhare notes mein likha tha: _"ASP.NET Core dekhta hai ki StudentController ko StudentService chahiye, DI container mein dhoondhta hai, banata hai aur controller ko pass karta hai."_ Isko detail mein dekho:

```
┌───────────────────────────────────────────────────────────────┐
│ APP START                                                     │
│ Program.cs chalti hai -> registrations container mein save:   │
│     IStudentService  ->  StudentService (Scoped)              │
└───────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌───────────────────────────────────────────────────────────────┐
│ REQUEST: GET /api/Student/GetStudent                          │
└───────────────────────────────────────────────────────────────┘
                              │
                              ▼
   Routing -> StudentController select hota hai
                              │
                              ▼
   Framework ko controller ka object banana hai
   Constructor dekhta hai: StudentController(IStudentService)
                              │
                              ▼
   Container se poochha: "IStudentService ka kya?"
   Registration mila: StudentService
                              │
                              ▼
   Container ne StudentService ka object banaya
                              │
                              ▼
   Controller ke constructor mein inject kiya
   -> _studentService initialize ho gaya
                              │
                              ▼
   GetStudent() chalta hai -> service se data -> JSON response
                              │
                              ▼
   Request khatam -> Scoped object dispose ho jata hai
```

### Dependency chain bhi container sambhalta hai

Maan lo `StudentService` ko khud `ILogger<StudentService>` chahiye:

```csharp
public class StudentService : IStudentService
{
    private readonly ILogger<StudentService> _logger;

    public StudentService(ILogger<StudentService> logger)
    {
        _logger = logger;
    }
    // ...
}
```

Tumhe kuch extra nahi karna. Container pehle `ILogger` banayega, phir `StudentService`, phir `StudentController`. Poori chain automatic. (`ILogger` framework mein pehle se registered hota hai.)

---

## 9. Service Lifetimes

Register karte waqt batana padta hai ki object **kitni der zinda** rahega. Teen options:

| Method         | Lifetime  | Naya object kab?                        |
| -------------- | --------- | --------------------------------------- |
| `AddTransient` | Transient | **Har baar** jab maanga jaye            |
| `AddScoped`    | Scoped    | **Ek HTTP request** mein ek hi object   |
| `AddSingleton` | Singleton | **Poori application** mein ek hi object |

```csharp
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddSingleton<ICacheService, CacheService>();
```

### Example se samjho

Ek request mein `ControllerA` aur `ServiceB` dono `IMyService` maangte hain:

| Lifetime  | Result                                                                                   |
| --------- | ---------------------------------------------------------------------------------------- |
| Transient | A aur B ko **alag** objects milte hain                                                   |
| Scoped    | A aur B ko **same** object milta hai (is request ke liye). Agli request mein naya object |
| Singleton | A, B, aur baaki saari requests ko **same** object milta hai                              |

### Kab kaunsa use karein?

| Lifetime      | Use case                                                    |
| ------------- | ----------------------------------------------------------- |
| **Transient** | Lightweight, stateless services (helpers, mappers)          |
| **Scoped**    | `DbContext`, repositories, business services (sabse common) |
| **Singleton** | Cache, configuration, shared state, thread-safe services    |

### Captive Dependency (sabse common bug)

**Singleton service ke andar Scoped service inject mat karo.**

Singleton poori app life tak zinda rehta hai. Agar usme Scoped service (jaise `DbContext`) inject ho gayi, to wo bhi singleton ke saath **chipak ke** poori life zinda rahegi. Isse stale data, thread-safety bugs aur `DbContext` ke errors aate hain.

**Rule:** chhoti lifetime wali cheez ko badi lifetime wali service mein inject mat karo.

```
Singleton  ->  Scoped inject  ❌  (galat)
Scoped     ->  Singleton inject ✅ (theek)
Transient  ->  kisi mein bhi    ✅
```

---

## 10. Injection ke types

### 1. Constructor Injection (sabse common aur recommended)

```csharp
public StudentController(IStudentService service) { ... }
```

Dependencies clearly dikhti hain, aur object ke bante hi ready hoti hain.

### 2. Method Injection (`[FromServices]`)

Jab dependency sirf **ek action** mein chahiye:

```csharp
[HttpGet]
public IActionResult Test([FromServices] IStudentService service)
{
    return Ok(service.GetStudentName());
}
```

### 3. View mein injection (`@inject`)

```html
@inject IStudentService StudentService

<p>@StudentService.GetStudentName()</p>
```

### 4. Property Injection

ASP.NET Core ka built-in container isse **support nahi karta**. Third-party containers (Autofac) karte hain.

---

## 11. DI ke bina vs DI ke saath

| Point                  | DI ke bina (`new`)    | DI ke saath                     |
| ---------------------- | --------------------- | ------------------------------- |
| Object banata kaun hai | Class khud            | DI container                    |
| Coupling               | **Tight**             | **Loose**                       |
| Implementation badalna | Har jagah code edit   | `Program.cs` mein ek line       |
| Unit testing           | Mushkil               | Aasan (fake inject karo)        |
| Lifetime management    | Manual                | Container sambhalta hai         |
| Dependency chain       | Khud banani padti hai | Automatic                       |
| Code readability       | Hidden dependencies   | Constructor mein sab dikhta hai |

### Code comparison

**Without DI:**

```csharp
public class StudentController : Controller
{
    private StudentService _service;

    public StudentController()
    {
        _service = new StudentService();   // tight coupling
    }
}
```

**With DI:**

```csharp
public class StudentController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentController(IStudentService service)   // loose coupling
    {
        _service = service;
    }
}
```

---
# Service Lifetimes in ASP.NET Core (Singleton, Scoped, Transient)

> Ye notes tumhare likhe hue notes par based hain, aur unhe in-depth explain kiya gaya hai. Ek **important correction** hai: Scoped ka matlab "per user / session" nahi, **"per HTTP request"** hota hai. Detail section 3 aur section 8 mein hai.

---


## 1. Lifetime kya hoti hai aur kyun zaroori hai?

Jab DI container koi service ka object banata hai, to ye sawaal aata hai:

- Ye object **kitni der zinda** rahega?
- Ye object **kis-kis ko** milega? (sabko same, ya sabko alag)

Isi ka jawab **service lifetime** hai. ASP.NET Core teen lifetimes deta hai:

| Lifetime | Register karne ka method |
|---|---|
| **Singleton** | `AddSingleton<IService, Service>()` |
| **Scoped** | `AddScoped<IService, Service>()` |
| **Transient** | `AddTransient<IService, Service>()` |

**Lifetime kyun zaroori hai?**

- **Performance:** Heavy object (cache, config) baar-baar banana wasteful hai.
- **Correctness:** Kuch objects (jaise `DbContext`) ko ek request ke andar share hona chahiye, lekin requests ke beech nahi.
- **Memory:** Galat lifetime se memory leak ya stale data aa sakta hai.
- **Thread safety:** Shared object mein multiple threads aate hain, jisse bugs aa sakte hain.

**Real life analogy (hotel):**

| Lifetime | Hotel analogy |
|---|---|
| **Singleton** | Hotel ka **reception desk**: ek hi hai, saare guests use karte hain |
| **Scoped** | Ek guest ka **room**: jab tak guest ka stay (request) hai, wahi room uske paas hai. Naya guest = naya room |
| **Transient** | **Disposable cup**: jitni baar maango, naya cup milta hai |

---

## 2. Singleton

### Definition

> **Poori application mein service ka sirf ek hi object banta hai, aur wahi object har jagah, har request mein, har user ko milta hai.**

Tumhare notes: *"Service ka yahan sirf ek object banta hai aur poori application mein use hota hai."* Bilkul sahi.

### Register karna

```csharp
builder.Services.AddSingleton<ICacheService, CacheService>();
```

### Kaise kaam karta hai?

```
App start
   │
   ▼
Request 1 (User A)  ──►  Container: "ICacheService pehli baar maanga gaya"
                         ──► CacheService ka object BANAYA (instance #1)
                         ──► User A ko diya
   │
Request 2 (User B)  ──►  Container: "Already bana hua hai"
                         ──► Wahi instance #1 diya
   │
Request 3 (User A)  ──►  Wahi instance #1
   │
   ▼
App band hone par hi object dispose hota hai
```

Dhyan do: Singleton aam taur par **pehli baar maange jaane par** banta hai (lazy). App start par hi chahiye to `AddSingleton(instance)` se khud object bana ke de sakte ho.

### Example

```csharp
public interface ICounterService
{
    int Increment();
}

public class CounterService : ICounterService
{
    private int _count = 0;

    public int Increment()
    {
        _count++;
        return _count;
    }
}
```

```csharp
builder.Services.AddSingleton<ICounterService, CounterService>();
```

Har request par `Increment()` call karo: 1, 2, 3, 4... **Alag users ke liye bhi counter badhta jayega**, kyunki object ek hi hai.

### Fayde

- Object ek hi baar banta hai, to **memory aur CPU bachta hai**.
- Shared state (cache, counters) rakhne ke liye perfect.

### Nuksan / dhyan dene wali baatein

1. **Thread safety zaroori hai.** Ek hi object par ek saath kai requests (threads) kaam karengi. Upar wale `_count++` mein race condition aa sakti hai. Safe version:

   ```csharp
   public int Increment() => Interlocked.Increment(ref _count);
   ```

2. **User-specific data mat rakho.** Agar singleton mein user ka data rakha, to wo doosre users ko leak ho sakta hai.
3. **Scoped service inject mat karo** (captive dependency, section 9).
4. **Memory bhi hold rehti hai** jab tak app chal rahi hai, isliye usme data bahut mat jamao.

### Kab use karein?

- In-memory cache
- Configuration / settings reader
- Logger factory
- `HttpClient` factory jaisi shared, stateless, thread-safe services
- Application-wide counters / shared state (thread-safe tarike se)

---

## 3. Scoped

### Definition

> **Ek scope ke andar service ka ek hi object banta hai. ASP.NET Core mein har HTTP request ka apna ek scope hota hai.** To ek request ke andar jitni baar bhi service maangi jaye, wahi object milta hai. Agli request mein naya object banta hai.

### Register karna

```csharp
builder.Services.AddScoped<IStudentService, StudentService>();
```

### Important correction

Tumhare notes mein likha hai: *"Scoped = one instance per user / session."* Ye **galat** hai.

Scoped ka matlab **per HTTP request** hai, user ya session nahi.

| Situation | Kya hota hai |
|---|---|
| Ek user 3 requests bhejta hai | **3 alag** objects (har request ka apna) |
| 3 alag users ek saath request bhejte hain | **3 alag** objects |
| Ek hi request ke andar service 5 jagah inject hui | **Sabko same** object |

Matlab ek user ke 10 clicks = 10 requests = 10 Scoped objects. User ya browser session se iska koi lena-dena nahi.

### Kaise kaam karta hai?

```
Request 1 aayi
   │  Scope #1 bana
   │  Controller ko IStudentService chahiye  ──► instance A banaya
   │  Controller ne ProductService ko call kiya, usko bhi IStudentService chahiye
   │                                          ──► wahi instance A diya
   │  Response gaya, scope khatam  ──► instance A dispose
   ▼
Request 2 aayi
   │  Scope #2 bana
   │  IStudentService chahiye  ──► NAYA instance B banaya
   ▼
...
```

### Scoped kyun useful hai?

Ek request ke andar kai classes ko **same data/state share** karni hoti hai.

**Sabse bada example: `DbContext`**

```csharp
builder.Services.AddDbContext<AppDbContext>(options => ...);   // default Scoped
```

Maan lo ek request mein `OrderService` aur `PaymentService` dono database use karti hain. Dono ko **same `DbContext`** milna chahiye, taaki:

- Ek hi transaction / change tracker share ho.
- Ek class ke kiye hue changes doosri class ko dikhein.
- Ek hi connection use ho.

Aur request khatam hote hi `DbContext` dispose ho jaye. Isiliye `DbContext` Scoped hota hai.

### Fayde

- Request ke andar **consistent state** milti hai.
- Request ke baad **automatic cleanup**.
- Requests ke beech data **leak nahi** hota (kyunki har request ka naya object).

### Nuksan / dhyan

- Scoped service ko **Singleton mein inject mat karo**.
- Background threads ya hosted services (`BackgroundService`) mein request scope nahi hota. Wahan manually scope banana padta hai (section 10).

### Kab use karein?

- `DbContext`
- Repositories
- Business / application services (`StudentService`, `OrderService`)
- Unit of Work
- Current-request specific data (current user context)

---

## 4. Transient

### Definition

> **Jitni baar service maangi jaye, utni baar naya object banta hai.**

Tumhare notes: *"Jitni baar service ko call karoge, utni baar service ka object banega."* Bilkul sahi.

### Register karna

```csharp
builder.Services.AddTransient<IEmailBuilder, EmailBuilder>();
```

### Kaise kaam karta hai?

Ek hi request ke andar bhi, har **injection point** par naya object milta hai:

```csharp
public class OrderController : ControllerBase
{
    public OrderController(IEmailBuilder a, IEmailBuilder b)
    {
        // a aur b ALAG objects hain, same request ke andar bhi!
    }
}
```

```
Request 1
   │  Controller ko IEmailBuilder chahiye  ──► object #1
   │  OrderService ko IEmailBuilder chahiye ──► object #2 (naya)
   │  PaymentService ko IEmailBuilder chahiye ──► object #3 (naya)
   ▼
Request 2
   │  Controller ko chahiye ──► object #4
   │  ...
```

### Fayde

- **Koi shared state nahi**, to thread-safety ki tension kam.
- Har user ko fresh, clean object milta hai.

### Nuksan / dhyan

- Bahut saare objects ban sakte hain, to **heavy objects** ke liye wasteful.
- Agar object `IDisposable` hai aur bahut bana rahe ho, to memory pressure aa sakta hai.
- Do classes ko same object **share** nahi karwa sakte.

### Kab use karein?

- Lightweight, **stateless** services
- Helpers, mappers, validators, calculators
- Jahan har baar fresh object chahiye

---

## 5. Teeno ka comparison

| Point | Singleton | Scoped | Transient |
|---|---|---|---|
| **Objects ki ginti** | Poori app mein **1** | **Har request** mein 1 | **Har baar** maange par naya |
| **Kab banta hai** | Pehli baar maange jaane par | Request ke scope mein pehli baar maange jaane par | Har injection par |
| **Kab dispose hota hai** | App band hone par | Request khatam hone par | Us scope ke khatam hone par jisme bana tha |
| **Requests ke beech share?** | **Haan** | Nahi | Nahi |
| **Request ke andar share?** | Haan | **Haan** | Nahi |
| **Thread-safety ki zaroorat** | **Zaroori** | Aam taur par nahi | Nahi |
| **Memory use** | Sabse kam | Medium | Sabse zyada ho sakta hai |
| **Typical use** | Cache, config | DbContext, services | Helpers, lightweight logic |

**Ek line mein:**

```
Singleton  = Poori app ke liye ek
Scoped     = Ek request ke liye ek
Transient  = Har baar naya
```

### Example scenario

Ek request mein `ControllerA` aur `ServiceB` dono `IMyService` maangte hain (do requests ke liye):

| | Request 1 | Request 2 |
|---|---|---|
| **Singleton** | A aur B ko **same**, aur Request 2 mein bhi wahi **purana** | wahi purana object |
| **Scoped** | A aur B ko **same** | **Naya** object, A aur B ko phir same |
| **Transient** | A aur B ko **alag** alag | Phir **naye alag** objects |

---

## 6. Practical demo (Guid se dekho)

Concept ko aankhon se dekhne ka sabse accha tarika.

### Service (Guid object ban'te hi ek baar generate hoga)

```csharp
namespace DependencyInjectionServices.Services
{
    public interface ITransientService { Guid ServiceId(); }
    public interface IScopedService    { Guid ServiceId(); }
    public interface ISingletonService { Guid ServiceId(); }

    // Ek hi class teeno interfaces implement karti hai
    public class Service1 : ITransientService, IScopedService, ISingletonService
    {
        private readonly Guid _id = Guid.NewGuid();   // object ki pehchaan

        public Guid ServiceId() => _id;
    }
}
```

> Guid ko method ke andar `Guid.NewGuid()` se mat banana. Tab har call par naya Guid milega, aur lifetime ka difference dikhega hi nahi. Guid ko field mein **ek baar** banao, taaki wo **object ki identity** ban jaye.

### Program.cs

```csharp
builder.Services.AddTransient<ITransientService, Service1>();
builder.Services.AddScoped<IScopedService, Service1>();
builder.Services.AddSingleton<ISingletonService, Service1>();
```

### Controller

```csharp
using Microsoft.AspNetCore.Mvc;
using DependencyInjectionServices.Services;

namespace DependencyInjectionServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LifetimeController : ControllerBase
    {
        private readonly ITransientService _t1;
        private readonly ITransientService _t2;
        private readonly IScopedService _s1;
        private readonly IScopedService _s2;
        private readonly ISingletonService _g1;
        private readonly ISingletonService _g2;

        public LifetimeController(
            ITransientService t1, ITransientService t2,
            IScopedService s1, IScopedService s2,
            ISingletonService g1, ISingletonService g2)
        {
            _t1 = t1; _t2 = t2;
            _s1 = s1; _s2 = s2;
            _g1 = g1; _g2 = g2;
        }

        [HttpGet]
        public IActionResult Get() => Ok(new
        {
            Transient = new { First = _t1.ServiceId(), Second = _t2.ServiceId() },
            Scoped    = new { First = _s1.ServiceId(), Second = _s2.ServiceId() },
            Singleton = new { First = _g1.ServiceId(), Second = _g2.ServiceId() }
        });
    }
}
```

### Do baar request bhejo (`GET /api/Lifetime/Get`) aur compare karo

**Request 1:**

```json
{
  "transient": { "first": "aaaa...", "second": "bbbb..." },
  "scoped":    { "first": "cccc...", "second": "cccc..." },
  "singleton": { "first": "dddd...", "second": "dddd..." }
}
```

**Request 2:**

```json
{
  "transient": { "first": "eeee...", "second": "ffff..." },
  "scoped":    { "first": "gggg...", "second": "gggg..." },
  "singleton": { "first": "dddd...", "second": "dddd..." }
}
```

**Observation:**

| Lifetime | Request ke andar | Requests ke beech |
|---|---|---|
| **Transient** | `first != second` | Phir bhi naye |
| **Scoped** | `first == second` | **Naya** Guid (cccc -> gggg) |
| **Singleton** | `first == second` | **Wahi** Guid (dddd = dddd) |

Yahi teeno lifetimes ka difference hai.

---

## 7. Kaunsi service ke liye kaunsi lifetime?

| Service type | Lifetime | Reason |
|---|---|---|
| `DbContext` | **Scoped** | Request ke andar share, baad mein dispose |
| Repository (`IProductRepository`) | **Scoped** | DbContext use karta hai (Scoped) |
| Business service (`IOrderService`) | **Scoped** | Repository use karta hai |
| In-memory cache (`ICacheService`) | **Singleton** | Sabke liye shared |
| App settings / config reader | **Singleton** | Ek baar load, sab use karein |
| `HttpClient` (via `IHttpClientFactory`) | **Singleton**-style factory | Factory handle karti hai |
| Email builder / mapper / helper (stateless) | **Transient** | Halka, koi state nahi |
| Validator (stateless) | **Transient** ya Singleton | Stateless ho to dono chalte hain |

### Decision flowchart

```
Kya service mein shared state hai jo sabko dikhni chahiye?
   │
   ├── Haan, poori app ke liye ──► Singleton (thread-safe banao)
   │
   └── Nahi
         │
         Kya DbContext ya request-level data use karti hai?
            │
            ├── Haan ──► Scoped
            │
            └── Nahi (stateless, halki) ──► Transient
```

**Agar confuse ho to default `AddScoped` rakho.** Web apps ke zyadatar services ke liye wahi safe hota hai.

---

## 8. Notes mein correction

| Tumhare notes | Sahi kya hai |
|---|---|
| *"Scoped = one instance per user / session"* | **Scoped = one instance per HTTP request** (scope). User ya session se matlab nahi |
| *"service ka object banega only ek user ur ek session ke liye"* | Ek user ki har nayi request par naya object banega |
| *"Singleton: ek hi object"* | Sahi. Isme **thread-safety** ki baat add karni chahiye |
| *"Transient: jitni baar call, utni baar object"* | Sahi. Precisely: jitni baar **inject/resolve** hota hai |
| `AddTransition` (pehle likha tha) | `AddTransient` |

**Sahi version, notes ke liye:**

```
Singleton :- Poori application mein sirf ek instance.
Scoped    :- Har HTTP request (scope) ke liye ek instance.
Transient :- Har baar service maangne par naya instance.
```

---

## 9. Captive Dependency (sabse common bug)

### Rule

> **Badi lifetime wali service ke andar chhoti lifetime wali service inject mat karo.**

Lifetime ka order (badi se chhoti):

```
Singleton  >  Scoped  >  Transient
```

| Kaun kisme inject ho | Safe? |
|---|---|
| Scoped -> Singleton mein | ❌ **Galat** |
| Transient -> Singleton mein | ⚠️ Technically chalta hai, par wo transient bhi singleton jitna zinda reh jata hai |
| Transient -> Scoped mein | ✅ Theek |
| Singleton -> Scoped mein | ✅ Theek |
| Singleton -> Transient mein | ✅ Theek |
| Scoped -> Scoped mein | ✅ Theek |

### Problem kya hai?

```csharp
public class CacheService : ICacheService    // Singleton
{
    private readonly AppDbContext _db;       // Scoped!

    public CacheService(AppDbContext db)
    {
        _db = db;
    }
}
```

```csharp
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddDbContext<AppDbContext>(...);   // Scoped
```

`CacheService` poori app life tak zinda hai. Usne ek **`DbContext` pakad liya (captive)**, to wo `DbContext` bhi poori app life tak zinda rahega. Natije:

- **Stale data** (purane tracked entities).
- `DbContext` **thread-safe nahi** hota, aur singleton se kai threads use karengi, to **random errors**.
- Connection lamba kharab hota hai.

### ASP.NET Core ka protection

**Development environment** mein scope validation default on hota hai. Agar aisa karoge to startup par ya pehli baar resolve karte waqt ye error milega:

```
System.InvalidOperationException: Cannot consume scoped service
'AppDbContext' from singleton 'ICacheService'.
```

Ye error dekh ke samajh jao ki lifetime galat hai.

> Production mein ye validation default on nahi hoti, isliye development mein hi ise pakad lena zaroori hai.

---

## 10. Singleton mein Scoped chahiye to kya karein?

Kabhi-kabhi sach mein Singleton (ya `BackgroundService`) ko Scoped service chahiye hoti hai. Tab **`IServiceScopeFactory`** use karo aur manually scope banao.

```csharp
public class CleanupWorker : BackgroundService     // Singleton-style
{
    private readonly IServiceScopeFactory _scopeFactory;

    public CleanupWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Naya scope banao (request jaisa)
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // db ka kaam karo...
            }   // yahan scope dispose, db bhi dispose

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
```

Isse har cycle mein **fresh DbContext** milta hai aur kaam ke baad dispose bhi ho jata hai.

---

## 11. Dispose kaun karta hai?

Agar service `IDisposable` implement karti hai, to **container dispose karta hai, tum nahi**. Lifetime ke hisaab se timing alag hai:

| Lifetime | Dispose kab |
|---|---|
| Singleton | App shutdown par |
| Scoped | Scope (request) khatam hone par |
| Transient | Us scope ke khatam hone par jisme wo bana tha |

**Rule:** Jo object container ne banaya hai, uspar manually `Dispose()` ya `using` mat lagao. Container sambhal lega.

> Ek exception: agar tum khud object banake `AddSingleton(instance)` se dete ho, to container use dispose **nahi** karta. Wo tumhari zimmedari hai.

---

## 12. Common mistakes

1. **Scoped ko Singleton mein inject karna** (captive dependency).
2. **Singleton mein user-specific data rakhna** (data leak doosre users ko).
3. **Singleton thread-safe nahi banana** (`_count++` jaise race conditions).
4. **Sab kuch Transient bana dena** (zaroorat se zyada objects, aur shared state nahi mil pati).
5. **Ek hi interface ko multiple lifetimes mein register karna.** Last registration jeet jati hai.
6. **Scoped ko background service mein seedha inject karna.** `IServiceScopeFactory` use karo.
7. **Lifetime ko "per user / session" samajhna.** Scoped = per HTTP request.
8. **`new` se object banana** aur phir container se milne wale object ki jagah use karna. Isse DI ka faida khatam.

---

## 13. Interview questions

**Q1. Singleton, Scoped aur Transient mein kya difference hai?**
Singleton poori app mein ek object, Scoped ek HTTP request mein ek object, Transient har baar naya object.

**Q2. Scoped lifetime ka matlab per user hota hai kya?**
Nahi. Scoped per HTTP request (scope) hota hai. Ek user ki har nayi request par naya object banta hai.

**Q3. `DbContext` kis lifetime mein hona chahiye aur kyun?**
Scoped. Ek request ke andar saari classes same `DbContext` share karein, aur request ke baad wo dispose ho jaye. `DbContext` thread-safe nahi hota, isliye Singleton galat hai.

**Q4. Captive dependency kya hai?**
Jab Singleton jaisi lambi lifetime wali service, Scoped jaisi chhoti lifetime wali service ko hold kar leti hai, aur wo zaroorat se zyada der zinda reh jati hai.

**Q5. Singleton ko Scoped service chahiye to kya karein?**
`IServiceScopeFactory` se manually scope banao, us scope se service resolve karo, kaam karo, aur scope dispose karo.

**Q6. Singleton service mein kya dhyan rakhna chahiye?**
Thread-safety, aur user-specific data store nahi karna.

**Q7. Transient kab use karein?**
Lightweight, stateless services ke liye, jahan har baar fresh object chahiye.

**Q8. Ek hi request mein Transient service do jagah inject ho to?**
Do alag objects milenge.

**Q9. Ek hi request mein Scoped service do jagah inject ho to?**
Dono ko same object milega.

**Q10. Container service ko dispose kab karta hai?**
Singleton app shutdown par, Scoped request khatam hone par, Transient us scope ke khatam hone par jisme wo bana tha.

---

## 14. Final summary

```
Singleton  :- Poori application mein ek hi object. Sab users, sab requests ko same.
              Thread-safe hona zaroori. (cache, config)

Scoped     :- Ek HTTP request ke liye ek object. Request ke andar sab ko same,
              agli request mein naya. (DbContext, repositories, services)

Transient  :- Jitni baar maango, utni baar naya object. (helpers, stateless logic)
```

**Yaad rakhne wali baatein:**

- Scoped = **per request**, per user/session **nahi**.
- **Badi lifetime ke andar chhoti lifetime inject mat karo** (Singleton mein Scoped nahi).
- Confuse ho to **default `AddScoped`**.
- Lifetime demo ke liye **Guid ko field mein ek baar** generate karo.
- Singleton mein Scoped chahiye to **`IServiceScopeFactory`**.

**Ek line mein:**

> Lifetime decide karti hai ki DI container ek object ko kitni der zinda rakhega aur kis-kis ke saath share karega: poori app (Singleton), ek request (Scoped), ya kisi ke saath nahi (Transient).