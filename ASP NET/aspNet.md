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

# ASP.NET Core MVC — Models, Repository Pattern, Strongly Typed View & `_ViewImports`

> Topics covered:
>
> 1. Models
> 2. Repository Pattern (Interface + Class + Controller)
> 3. Strongly Typed View
> 4. `_ViewImports.cshtml` (hierarchy + directives)

---

## 1. Models

### Model kya hota hai?

- Model ek **C# class** hoti hai (file extension `.cs`) jisme **properties** (aur chahe to methods) hote hain.
- Model ka kaam hai **data ko set aur get karna** (get/set properties ke through).
- Model **data source** ke saath interact karta hai. Data source kuch bhi ho sakta hai:
  - Database
  - Manual `List<T>` / Collection
  - File, API, etc.

### Model kab chahiye?

| Situation                           | Model chahiye? |
| ----------------------------------- | -------------- |
| Application me koi data hi nahi hai | ❌ Nahi        |
| Application me data hai             | ✅ Haan        |

Models ke through hum **Insert, Update, Delete** (aur Read) operations perform kar sakte hain.

> **Note:** Saari model classes ko `Models` folder me rakhna **mandatory nahi hai**, lekin ye **good programming practice** hai.

### Example: Student Model

Student ke andar properties: `RollNo`, `Name`, `Gender`, `Standard (Class)`.

```csharp
namespace ModelsInASPCore.Models
{
    public class StudentModel
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Standard { get; set; }
    }
}
```

> 💡 C# convention: class aur property names **PascalCase** me likhte hain (`StudentModel`, `RollNo`). Notes me `studentModel` / `rollNo` likha tha, wo compile to ho jata hai par convention ke hisaab se PascalCase better hai. **Poore project me ek hi naam consistently use karo** (`StudentModel` kahin aur `studentModel` kahin, ye confusion create karta hai).

### Controller me seedha data bana ke bhejna (shuruaati tareeka)

```csharp
public IActionResult Index()
{
    var students = new List<StudentModel>
    {
        new StudentModel { RollNo = 1, Name = "Hello World", Gender = "M", Standard = 12 },
        new StudentModel { RollNo = 2, Name = "Hello World", Gender = "M", Standard = 12 },
        new StudentModel { RollNo = 3, Name = "Hello World", Gender = "M", Standard = 12 }
    };

    ViewData["myStudentData"] = students;   // ⚠ ye line notes me missing thi
    return View();                          // ⚠ return bhi zaruri hai
}
```

### View me data dikhana (ViewData se)

```cshtml
@using ModelsInASPCore.Models

@{
    ViewData["Title"] = "Home Page";
    var students = ViewData["myStudentData"] as List<StudentModel>;
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
            <p>Roll No: @item.RollNo</p>
            <p>Standard: @item.Standard</p>
        </div>
    }
}
else
{
    <p class="text-center">No students found.</p>
}
```

> ⚠ **Important:** Controller me `ViewData["key"]` aur View me `ViewData["key"]` ka **key naam exactly same** hona chahiye. Notes me controller me `"data1"` tha aur view me `"myStudentData"` — is mismatch ki wajah se data `null` aayega.

### Ye tareeka sahi kyu nahi hai?

- Controller ke andar hi data bana rahe hain / data access ka logic controller me aa gaya.
- Isse code **tightly coupled** ho jata hai, test karna mushkil hota hai, aur baad me DB lagana ho to controller badalna padta hai.
- Iska **best practice** hai: **Repository Pattern**.

> Repository Pattern samajhne se pehle **Dependency Injection (DI)** aana chahiye (uska alag `.md` notes already bana hua hai).

---

## 2. Repository Pattern

### Setup

- Database me ek table hai, maan lo `Student`.
- ASP.NET Core app me us table ko represent karne ke liye ek **Model class** banate hain: `Student.cs` / `StudentModel.cs`.
- Ye class database ki `Student` table ko represent karegi.

### Concept

Normally ye layers hoti hain:

- **Controller**
- **Data Access Layer** (DbContext / List / Collection / Database — kuch bhi)

**Pehle** (bina repository ke):

```
Controller ───────────────▶ Data Access Layer
```

Controller seedha data access layer se data leta tha.

**Repository Pattern ke saath:**

```
Controller ───▶ Repository ───▶ Data Access Layer (DbContext)
```

Controller aur Data Access Layer ke **beech me ek abstraction layer** aa jati hai, jise **Repository** kehte hain.

> **Definition:** With the repository pattern, we create an **abstraction layer between the data access layer and the business logic layer** of an application.

### Repository kis se bana hota hai?

Repository = **Interface + Class**

| Part          | Kaam                                                                                                                     |
| ------------- | ------------------------------------------------------------------------------------------------------------------------ |
| **Interface** | Saare methods ko **declare** karta hai (abstract form me, body nahi hoti). Jaise: `GetAllStudents()`, `GetStudentById()` |
| **Class**     | Interface ke methods ko **body (implementation)** deta hai                                                               |

### Flow

```
User Request
     │
     ▼
Controller          (IStudent ko use karta hai)
     │
     ▼
Repository Class    (StudentRepository : IStudent)
     │
     ▼
DbContext           (Data Access Layer; LINQ ke through data nikalta hai)
     │
     ▼
Data return hota hai Model ke form me (StudentModel / List<StudentModel>)
```

- User ki request Controller ke paas aati hai.
- Controller **Repository** ko call karta hai.
- Repository **DbContext** ko call karta hai (hum DbContext ko hi "Data Access Layer" bol rahe hain).
- Repository, data access layer se **LINQ** ke through interact karti hai.
- DbContext jab Repository ko data return karta hai to **Model** ke form me karta hai.

### Step 1: Interface banana

```csharp
using ModelsInASPCore.Models;

namespace ModelsInASPCore.Repository
{
    public interface IStudent
    {
        // Saare students
        List<StudentModel> GetAllStudents();

        // Roll number se ek student
        StudentModel? GetStudentById(int rollNo);
    }
}
```

### Step 2: Repository class banana (interface implement karna)

```csharp
using ModelsInASPCore.Models;

namespace ModelsInASPCore.Repository
{
    public class StudentRepository : IStudent
    {
        public List<StudentModel> GetAllStudents()
        {
            return DataSource();
        }

        public StudentModel? GetStudentById(int id)
        {
            // FirstOrDefault -> match mila to wo row, nahi mila to null
            return DataSource().FirstOrDefault(x => x.RollNo == id);
        }

        // Abhi ke liye data yahin se aa raha hai.
        // Aage chalke database se data lane ka code alag file/DbContext me hoga.
        private List<StudentModel> DataSource()
        {
            return new List<StudentModel>
            {
                new StudentModel { RollNo = 1, Name = "Hello World", Gender = "M", Standard = 12 },
                new StudentModel { RollNo = 2, Name = "Hello World", Gender = "M", Standard = 12 },
                new StudentModel { RollNo = 3, Name = "Hello World", Gender = "M", Standard = 12 },
                new StudentModel { RollNo = 4, Name = "Hello World", Gender = "M", Standard = 12 },
                new StudentModel { RollNo = 5, Name = "Hello World", Gender = "M", Standard = 12 }
            };
        }
    }
}
```

> ⚠ Notes ke code me ye galtiyan thi, inko fix kiya hai:
>
> - `StudnetModel` (typo) → `StudentModel`
> - `studentModel` vs `StudentModel` ka mismatch
> - RollNo **duplicate** the (1, 1, 1) → unique rakhe, warna `GetStudentById(1)` hamesha pehla hi return karega.

### Step 3: DI container me register karna (zaruri!)

Controller me `IStudent` inject hoga, isliye `Program.cs` me batana padega ki `IStudent` maange to kaunsi class dena hai:

```csharp
builder.Services.AddScoped<IStudent, StudentRepository>();
```

Ye line **nahi** likhi to runtime pe error aayega: _"Unable to resolve service for type IStudent..."_

### Step 4: Controller me use karna

```csharp
using Microsoft.AspNetCore.Mvc;
using ModelsInASPCore.Repository;

namespace ModelsInASPCore.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _studentService;

        // Constructor Injection
        public StudentController(IStudent studentService)
        {
            _studentService = studentService;
        }

        // MVC View
        [HttpGet("/Student/Index")]
        public IActionResult Index()
        {
            var students = _studentService.GetAllStudents();
            ViewData["data1"] = students;     // view me bhi same key "data1" use karni hai
            return View();
        }

        // API: Saare students
        [HttpGet("api/Student/GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            var students = _studentService.GetAllStudents();
            return Ok(students);
        }

        // API: ID se student
        [HttpGet("api/Student/GetStudent/{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _studentService.GetStudentById(id);

            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }
    }
}
```

### Repository Pattern ke fayde

| Fayda                          | Matlab                                                            |
| ------------------------------ | ----------------------------------------------------------------- |
| **Loose coupling**             | Controller sirf `IStudent` jaanta hai, concrete class nahi        |
| **Easy to change data source** | List se SQL Server / MongoDB pe jaana ho to sirf repository badlo |
| **Testability**                | Unit test me fake/mock repository inject kar sakte ho             |
| **Clean code**                 | Data access logic ek jagah, controller patla (thin)               |

---

## 3. Strongly Typed View

### Definition

- Jo view kisi **specific model class** ke saath **bind** hota hai, usse **Strongly Typed View** kehte hain.
- Hum **kisi bhi class** ko view ka model bana sakte hain.
- Model class ki saari **properties view me access** ho jati hain (IntelliSense + compile-time checking ke saath).
- Ye controller se view tak **data pass karne** ke liye use hota hai.

> Model ka matlab sirf ek object nahi, **list of objects** bhi ho sakta hai.

### Case 1: Single object pass karna

**Controller:**

```csharp
public IActionResult Index()
{
    Employee obj = new Employee()
    {
        EmpId = 101,
        EmpName = "Umair",
        Designation = "Manager",
        Salary = 25000
    };
    return View(obj);   // object ko View() me pass kiya
}
```

**View:**

```cshtml
@model Employee   @* ab ye Strongly Typed View ban gaya, Employee model se bind ho gaya *@

<h2>@Model.EmpId</h2>
<h2>@Model.EmpName</h2>
<h2>@Model.Designation</h2>
<h2>@Model.Salary</h2>
```

### Case 2: List of objects pass karna

**Controller:**

```csharp
var myEmployees = new List<Employee>
{
    new Employee { EmpId = 101, EmpName = "Sourabh", Designation = "Manager", Salary = 25000 },
    new Employee { EmpId = 102, EmpName = "Rahul",   Designation = "Developer", Salary = 30000 },
    new Employee { EmpId = 103, EmpName = "Priya",   Designation = "Tester", Salary = 28000 }
};
return View(myEmployees);
```

**View:**

```cshtml
@model List<Employee>

@foreach (var item in Model)
{
    <h2>@item.EmpId</h2>
    <h2>@item.EmpName</h2>
    <h2>@item.Designation</h2>
    <h2>@item.Salary</h2>
}
```

### `@model` vs `@Model` (bahut common confusion)

| Syntax            | Matlab                                                                                 |
| ----------------- | -------------------------------------------------------------------------------------- |
| `@model Employee` | **Lowercase m** — directive, view ka **type** declare karta hai (file ke upar ek baar) |
| `@Model.EmpName`  | **Capital M** — actual **object** (data), jo controller ne bheja                       |

### ViewData vs Strongly Typed View

|                    | ViewData                        | Strongly Typed View               |
| ------------------ | ------------------------------- | --------------------------------- |
| Type safety        | ❌ `as` se cast karna padta hai | ✅ Compile-time type safe         |
| Key mismatch error | Runtime pe (null)               | Nahi hota, kyunki key hi nahi hai |
| IntelliSense       | Limited                         | ✅ Full                           |
| Recommended        | Chhota-mota extra data          | ✅ Main data ke liye              |

Repository wale example me bhi better approach ye hai:

```csharp
public IActionResult Index()
{
    var students = _studentService.GetAllStudents();
    return View(students);          // ViewData ki jagah seedha model
}
```

```cshtml
@model List<StudentModel>

@foreach (var s in Model)
{
    <p>@s.RollNo - @s.Name</p>
}
```

---

## 4. `_ViewImports.cshtml`

### Problem kya hai?

Maan lo `Student` model `MyASPCoreApp.Models` namespace me hai, aur mujhe iska data ye views me chahiye:

- `Index.cshtml`
- `About.cshtml`
- `Contact.cshtml`

To har view me upar ye namespace import karna padega:

```cshtml
@using MyASPCoreApp.Models
```

Ya phir har jagah full name likhna padega:

```cshtml
@model List<MyASPCoreApp.Models.Student>
```

Same cheez **baar baar likhna** boring aur error-prone hai.

### Solution: `_ViewImports.cshtml`

Ek baar is file me namespace register kar do:

```cshtml
@using MyASPCoreApp.Models
```

Ab har view me seedha likh sakte ho:

```cshtml
@model List<Student>
```

Teacher model, Admin model, kitne bhi models ho, **agar sab same namespace me hain** to ek `@using` kaafi hai.

> ⚠ **Correction:** Notes me `@model MyASPCoreApp.Models` likha tha. Namespace import karne ke liye **`@using`** use hota hai, `@model` nahi. `@model` view ka **model type** batata hai.

### Hierarchical Order (multiple `_ViewImports.cshtml`)

Ek project me **multiple `_ViewImports.cshtml`** files ho sakti hain:

```
Views
 ├── _ViewImports.cshtml          ← Root level (poore Views folder ke liye)
 ├── Home
 │    ├── _ViewImports.cshtml     ← Sirf Home folder ke views ke liye
 │    ├── Index.cshtml
 │    └── Details.cshtml
 └── Shared
      └── _Layout.cshtml
```

**Rules:**

1. Root `Views/_ViewImports.cshtml` ki directives **saare folders ke views** pe apply hoti hain.
2. `Views/Home/_ViewImports.cshtml` ki directives **sirf Home folder ke views** (`Index`, `Details`) pe apply hoti hain.
3. Runtime view ke folder se **upar root tak** saari `_ViewImports` files dhundta hai aur unko **root se neeche ki taraf combine** karta hai.
4. `@using`, `@inject`, `@addTagHelper` jaisi directives **cumulative (jud jati hain)** hain: root ki bhi lagti hain aur folder ki bhi.
5. Agar kisi folder me `_ViewImports` nahi hai (jaise `Shared`), to us folder ke views ko root wali file hi milegi.
6. Jo directives ek hi cheez set karti hain (jaise `@model`, `@inherits`, `@namespace`), unme **view ke sabse paas wali file** ki value jeetti hai.

> Simple yaad rakhne ka tareeka: **"Root ki settings sab pe lagti hain, folder wali file uske upar extra/override karti hai."**

## Tag Helper Function ek baar dekh lena....................

2. Tag Helper kya hota hai?
   Isko samajhna zaroori hai, kyunki @addTagHelper, @removeTagHelper aur @tagHelperPrefix isi se related hain.

Normally HTML mein link banane ke liye:
<a href="/Home/Index">Home</a>

Yahan tum manually URL likh rahe ho.

Lekin ASP.NET Core MVC mein tum aise bhi likh sakte ho:
<a asp-controller="Home" asp-action="Index">
Home
</a>

Yahan:

- asp-controller="Home" → kaunsa Controller?
- asp-action="Index" → kaunsa Action Method?
  ASP.NET Core ka Tag Helper in attributes ko process karke actual URL generate karta hai.

Output approximately:
<a href="/Home/Index">Home</a>

Tag Helper ka working flow
Razor View

<a asp-controller="Home" asp-action="Index">

Anchor Tag Helper

asp-controller aur asp-action process karta hai

Generated HTML

<a href="/Home/Index">Home</a>

Agar Tag Helper register nahi kiya, toh asp-controller aur asp-action process nahi honge. Browser unhe normal HTML attributes ki tarah dekhega.

Ab isi concept se pehli directive samjho. 3. @addTagHelper
Meaning: Kisi Tag Helper ko apne Razor Views mein enable karna.

Example:
@addTagHelper \*, Microsoft.AspNetCore.Mvc.TagHelpers

Is line ko tod kar samjho:
Part Meaning
@addTagHelper Tag Helpers enable karo

- Saare available Tag Helpers
  Microsoft.AspNetCore.Mvc.TagHelpers Jis assembly mein ye Tag Helpers hain

ASP.NET Core ke built-in Tag Helpers mein:

- AnchorTagHelper
- InputTagHelper
- FormTagHelper
- LabelTagHelper
- SelectTagHelper
- ValidationMessageTagHelper
  Jaise:

1. Anchor Tag Helper
   <a asp-controller="Student"
      asp-action="Index">
   Students
   </a>

2. Input Tag Helper
   @model Student

<input asp-for="Name" />

Agar Student model mein Name property hai, toh asp-for us property ke according input generate karta hai.

3.  Form Tag Helper
    <form asp-controller="Student"
          asp-action="Create"
          method="post">

        <input asp-for="Name" />

        <button type="submit">Save</button>

    </form>

Yahan Form Tag Helper form ka action URL generate karne mein help karta hai.

Real project mein:
@addTagHelper \*, Microsoft.AspNetCore.Mvc.TagHelpers

Ye generally \_ViewImports.cshtml mein ek baar likha hota hai.

Yaad rakho: @addTagHelper Tag Helpers ko register/available karta hai. Ye khud HTML generate nahi karta.

4. @removeTagHelper
   Meaning: Kisi already enabled Tag Helper ko disable karna.

Maan lo tumne root \_ViewImports.cshtml mein saare Tag Helpers enable kar diye:
@addTagHelper \*, Microsoft.AspNetCore.Mvc.TagHelpers

Ab poore Views folder mein Tag Helpers available hain.

Lekin tum chahte ho ki Admin folder mein Anchor Tag Helper kaam na kare.

Toh structure:
Views
│
├── \_ViewImports.cshtml
│
├── Home
│ └── Index.cshtml
│
└── Admin
├── \_ViewImports.cshtml
└── Index.cshtml

Views/\_ViewImports.cshtml
@addTagHelper \*, Microsoft.AspNetCore.Mvc.TagHelpers

Views/Admin/\_ViewImports.cshtml
@removeTagHelper Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper,
Microsoft.AspNetCore.Mvc.TagHelpers

Ab kya hoga?
View Anchor Tag Helper
Home/Index.cshtml Enabled
Admin/Index.cshtml Removed

Admin folder mein baaki Tag Helpers available rahenge, lekin Anchor Tag Helper remove ho jayega.

Important: \_ViewImports.cshtml ki directives folder hierarchy ke through inherit hoti hain. Child folder mein add/remove karke parent configuration ko modify kar sakte ho.

5. @tagHelperPrefix
   Meaning: Tag Helper use karne ke liye HTML tag ke aage ek specific prefix lagana compulsory karna.

Abhi tak tumne aise likha:
<a asp-controller="Home" asp-action="Index">
Home
</a>

Ismein normal HTML ka <a> tag bhi hai aur Tag Helper ke attributes bhi.

Maan lo tum chahte ho ki sirf wahi HTML elements Tag Helper ke through process hon jinke aage th: prefix ho.

\_ViewImports.cshtml:
@addTagHelper \*, Microsoft.AspNetCore.Mvc.TagHelpers

@tagHelperPrefix th:

Ab View mein:

Tag Helper enabled:
<th:a asp-controller="Home"
asp-action="Index">
Home
</th:a>

Tag Helper apply nahi hoga:
<a asp-controller="Home"
   asp-action="Index">
Home
</a>

Difference:

- <th:a> → Tag Helper process karega.
- <a> → Normal HTML tag ki tarah treat hoga.
  Kahan use hota hai?

Jab normal HTML aur Tag Helper elements ko explicitly distinguish karna ho.

Production MVC applications mein iska use comparatively rare hai.

6. @using
   Ab ye bahut important hai. Iska connection C# ke namespaces se hai.

Maan lo tumhare project mein ek Model hai:

Models/Student.cs
namespace MyASPCoreApp.Models
{
public class Student
{
public int Id { get; set; }

        public string Name { get; set; }
    }

}

Ab tum Student class ko View mein use karna chahte ho.

Normally:
@model MyASPCoreApp.Models.Student

<h2>@Model.Name</h2>

Har baar MyASPCoreApp.Models.Student likhna padega.

Is problem ko solve karta hai @using.

\_ViewImports.cshtml
@using MyASPCoreApp.Models

Ab View mein:
@model Student

<h2>@Model.Name</h2>

Dekho kitna simple ho gaya.
Without @using
@model MyASPCoreApp.Models.Student

With @using
@model Student

Less repetition

Tum multiple namespaces bhi import kar sakte ho:
@using MyASPCoreApp.Models
@using MyASPCoreApp.Repository
@using MyASPCoreApp.Services

Real-world use: Jab tumhare project mein multiple Models, Services aur custom classes hain, toh unke namespaces \_ViewImports mein import kar sakte ho.

7. @namespace
   Isko samajhne ke liye pehle C# namespace samjho.

C# mein:
namespace MyASPCoreApp.Controllers
{
public class HomeController
{
}
}

Yahan MyASPCoreApp.Controllers class ka namespace hai.

Namespace classes ko logically organize karta hai.

Ab Razor Views bhi internally C# classes mein compile hote hain.

For example, tumhari file:
Views/Home/Index.cshtml

Razor internally is View ke liye ek generated C# class banata hai.

@namespace se tum us generated View class ka namespace specify kar sakte ho.

\_ViewImports.cshtml
@namespace MyASPCoreApp.Views

Ab Razor View classes ke liye namespace configure ho jayega.

Agar root namespace MyASPCoreApp.Views hai, toh folder hierarchy ke basis par namespaces mein subfolder names bhi include ho sakte hain.

Example:
MyASPCoreApp.Views.Home
MyASPCoreApp.Views.Student

Kahan use hota hai?

- Razor Class Libraries.
- Advanced Razor View organization.
- Custom namespace configuration.
  Normal MVC application mein generally default namespace convention sufficient hoti hai.

8. @inject
   Ab ye important hai kyunki tumne pehle Dependency Injection padha hai.

Tumhe pata hai DI ka meaning hai: kisi class ko uski dependency manually create karne ke bajaye DI container se provide karna.

Ab maan lo tumhare paas ek interface hai:

Repository/IStudent.cs
public interface IStudent
{
string GetStudentName();
}

Repository/StudentRepository.cs
public class StudentRepository : IStudent
{
public string GetStudentName()
{
return "Sourabh";
}
}

Program.cs
builder.Services.AddScoped<IStudent, StudentRepository>();

Ab DI container ko pata hai:
Jab bhi IStudent ki dependency chahiye, StudentRepository ka object provide karo.

Normally Controller mein:
public class StudentController : Controller
{
private readonly IStudent \_student;

    public StudentController(IStudent student)
    {
        _student = student;
    }

    public IActionResult Index()
    {
        var name = _student.GetStudentName();

        return View();
    }

}

Lekin kya View mein bhi directly service inject kar sakte hain?

Yes! Yahan @inject use hota hai.
@inject IStudent StudentService

<h2>@StudentService.GetStudentName()</h2>

Yahan:
Part Meaning
@inject DI container se service inject karo
IStudent Service ka type
StudentService View mein use hone wala property name

Ab View mein StudentService directly use kar sakte ho.
\_ViewImports mein kyun likhte hain?
Maan lo tumhe 10 Views mein same service chahiye.

Har View mein:
@inject IStudent StudentService

likhne ki jagah \_ViewImports.cshtml mein ek baar likh do:
@using MyASPCoreApp.Repository

@inject IStudent StudentService

Ab us scope ke saare Views mein StudentService available rahegi.
Best practice
Normally MVC architecture mein Controller data fetch karta hai aur Model ke through View ko provide karta hai.
Database
↓
Repository
↓
Controller
↓
Model
↓
View

Har View mein repository inject karke database se data fetch karna generally avoid karna chahiye.

@inject configuration, localization aur chhoti view-specific services ke liye useful hai.

9. @model
   Isko tumne MVC mein already dekha hoga.

Meaning: View ko batana ki usko kis type ka data receive hone wala hai.

Maan lo tumhara Student model hai:
public class Student
{
public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

}

Ab Controller se View mein data bhejna hai.

StudentController.cs
public IActionResult Details()
{
Student student = new Student
{
Id = 1,
Name = "Sourabh",
Age = 21
};

    return View(student);

}

Ab View:

Views/Student/Details.cshtml
@model Student

<h2>Student Details</h2>

<p>ID: @Model.Id</p>

<p>Name: @Model.Name</p>

<p>Age: @Model.Age</p>

Yahan @model Student batata hai ki is View ka model type Student hai.

Aur @Model se tum us object ki properties access kar sakte ho.
@model vs @Model
Syntax Meaning
@model Student View ka model type declare karta hai
@Model Controller se received model object access karta hai
@Model.Name Object ki Name property access karta hai

\_ViewImports mein @model kyun nahi likhna chahiye?
Maan lo:

- Home View ko HomeModel chahiye.
- Student View ko Student chahiye.
- Product View ko Product chahiye.
  Har View ka model alag hai.

Isliye normally @model respective .cshtml file mein likhte hain.

Technically \_ViewImports mein @model specify kiya ja sakta hai, lekin ye common model type sab Views ke liye force karega, jo generally desirable nahi hai.

10. @inherits
    Ye thoda advanced concept hai.

Sabse pehle samjho ki Razor Views internally C# classes hoti hain.

Normally Razor View ek framework-provided base class se inherit karti hai, jisse ViewData, Model, Html, Url jaise members available hote hain.

C# mein inheritance:
public class Parent
{
public void ShowMessage()
{
Console.WriteLine("Hello");
}
}

public class Child : Parent
{
}

Ab Child class Parent ka method use kar sakti hai.

Same concept Razor Views mein bhi apply hota hai.

Tum apni custom base class bana sakte ho.

MyBaseView.cs
using Microsoft.AspNetCore.Mvc.Razor;

public class MyBaseView<TModel> : RazorPage<TModel>
{
public string ApplicationName => "My ASP.NET App";
}

Ab \_ViewImports.cshtml mein:
@inherits MyBaseView<TModel>

Ab Views mein inherited property use kar sakte ho:

<h2>@ApplicationName</h2>

Matlab common properties aur helper methods ko base class mein define karke Views mein reuse kar sakte ho.

### ----------------------------------------------------------------------------------------------------------------------------------

# ASP.NET Core MVC — Model Binding & Model Validation

> Ye notes Hinglish mein hain, interview-oriented framing ke saath. Node/Express ke analogies bhi diye hain taaki backend ka concept clear rahe.

---

## 1. Model Binding kya hoti hai?

### Definition

**Model Binding in ASP.NET Core MVC maps data from an HTTP request to action method parameters or model properties.**

### Simple language mein

Jab browser se server par HTTP request aati hai, toh us request ke andar kuch **data** hota hai. Jaise:

| Data kahan hai?           | Example               |
| ------------------------- | --------------------- |
| URL ke andar              | `/Student/Edit/2`     |
| Query String ke andar     | `?id=10`              |
| Form ke andar             | `<input name="Name">` |
| Request Body ke andar     | JSON body             |
| Route Parameters ke andar | `{id}`                |

ASP.NET Core MVC **automatically** ye data read karke Controller ke **Action Method ke parameters** mein assign kar deta hai.

👉 **Isi process ko Model Binding kehte hain.**

### Ek line mein yaad rakho

> HTTP Request ka data → automatically → C# parameters / model properties mein.

---

## 2. Pehle Routing samjho

Maan lo tum browser mein ye URL open karte ho:

```
https://localhost:5068/Student/Edit/2
```

URL ko todte hain:

| URL Part  | Meaning                |
| --------- | ---------------------- |
| `Student` | Controller             |
| `Edit`    | Action Method          |
| `2`       | Route Parameter (`id`) |

Ab tumhara controller:

```csharp
// StudentController.cs
public class StudentController : Controller
{
    public IActionResult Edit(int id)
    {
        return View();
    }
}
```

### Question

> `Edit(int id)` ke andar `id` ki value **2** kaise aayi?

Isko step-by-step samjhte hain 👇

---

## 3. Route Data se Model Binding (step-by-step)

### Step 1: Browser HTTP Request send karta hai

```
GET /Student/Edit/2 HTTP/1.1
Host: localhost:5068
```

### Step 2: Routing Middleware request ko check karta hai

ASP.NET Core ka **Routing system** incoming URL ko registered routes ke saath match karta hai.

Default MVC route generally aisa hota hai (`Program.cs` mein):

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

Is pattern ko samjho:

```
{controller}/{action}/{id?}
```

| Part                | Matlab                                                |
| ------------------- | ----------------------------------------------------- |
| `{controller=Home}` | Controller ka naam; agar nahi diya toh default `Home` |
| `{action=Index}`    | Action ka naam; agar nahi diya toh default `Index`    |
| `{id?}`             | `id` **optional** hai (`?` ka matlab optional)        |

Ab request `/Student/Edit/2` is pattern se match hoti hai:

```
controller = Student
action     = Edit
id         = 2
```

**Routing ka result:**

| Cheez       | Value                             |
| ----------- | --------------------------------- |
| Controller  | `Student` (→ `StudentController`) |
| Action      | `Edit`                            |
| Route value | `id = 2`                          |

### Step 3: Model Binding start hoti hai

Routing ne identify kar liya:

- Controller: `StudentController`
- Action: `Edit`
- Route data: `id = 2`

Ab **Model Binding system** action method ke parameters check karta hai:

```csharp
public IActionResult Edit(int id)
{
    // id = 2
    return View();
}
```

Model Binder dekhta hai:

- Action mein `id` naam ka parameter hai ✅
- Route data mein bhi `id` naam ki value `2` hai ✅

Toh **automatically** `id = 2` assign ho jata hai.

> 💡 Tumhe manually URL parse karke `id` extract nahi karni padti. ASP.NET Core ye kaam khud karta hai.

### Poora flow ek nazar mein

```
Browser Request
     │
     ▼
Routing Middleware  ──►  controller=Student, action=Edit, id=2
     │
     ▼
Model Binding       ──►  Edit(int id)  →  id = 2
     │
     ▼
Action Method execute hota hai
```

> ⚠️ **Important:** Pehle **Routing** hoti hai, uske baad **Model Binding**. Routing hi route values nikalti hai, aur Model Binder unhe parameters se match karta hai.

---

## 4. Query String se Model Binding

Ab maan lo URL aisa hai:

```
https://localhost:5068/Student/Edit?id=10
```

Difference dekho:

```
/Student/Edit?id=10
              ↑
         Query String
```

`?` ke baad jo data aata hai usse **Query String** kehte hain.

Controller:

```csharp
public IActionResult Edit(int id)
{
    return Content($"Student ID: {id}");
}
```

Browser mein open karo: `/Student/Edit?id=10`

**Output:**

```
Student ID: 10
```

### Kaise hua?

1. Routing `/Student/Edit` ko match karta hai.
2. Controller identify hota hai: `StudentController`.
3. Action identify hota hai: `Edit`.
4. Model Binding request ki **Query String** check karta hai.
5. Query String mein `id=10` milta hai.
6. Action parameter `int id` mein `10` assign ho jata hai.

### Route vs Query String — difference

|                | Route Data                     | Query String               |
| -------------- | ------------------------------ | -------------------------- |
| URL            | `/Student/Edit/2`              | `/Student/Edit?id=10`      |
| Data kahan hai | Path ke andar                  | `?` ke baad                |
| Format         | `/value`                       | `?key=value&key2=value2`   |
| Use case       | Resource identify karna (`id`) | Filter, search, pagination |

---

## 5. Binding Sources — data kahan-kahan se aata hai

Model Binder **multiple jagah** se data dhundhta hai. Default order (MVC mein):

1. **Form values** (POST form data)
2. **Route values** (`{id}`)
3. **Query String** (`?id=10`)

Agar kisi specific jagah se hi data chahiye ho, toh **attributes** use karte hain:

| Attribute      | Data kahan se lega  | Express equivalent           |
| -------------- | ------------------- | ---------------------------- |
| `[FromRoute]`  | Route data          | `req.params`                 |
| `[FromQuery]`  | Query String        | `req.query`                  |
| `[FromForm]`   | Form data           | `req.body` (urlencoded/form) |
| `[FromBody]`   | Request Body (JSON) | `req.body` (JSON)            |
| `[FromHeader]` | HTTP Header         | `req.headers`                |

```csharp
// Sirf query string se lo
public IActionResult Search([FromQuery] string keyword) { ... }

// Sirf route se lo
public IActionResult Edit([FromRoute] int id) { ... }

// JSON body se lo (API mein common)
public IActionResult Create([FromBody] Student student) { ... }
```

---

## 6. Complex Type Binding (Student object)

Ab tak sirf `int id` jaisa simple type tha. Model Binding **poore object** ko bhi bind kar sakti hai.

### Model

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}
```

### Form (View)

```html
<form method="post">
  <input name="Name" />
  <input name="Email" />
  <input name="Age" />
  <button type="submit">Save</button>
</form>
```

### Action

```csharp
[HttpPost]
public IActionResult Create(Student student)
{
    // student.Name, student.Email, student.Age
    // automatically fill ho chuke hain
    return View();
}
```

**Rule:** Form field ka `name` attribute aur model ki **property ka naam match** hona chahiye (case-insensitive).

```
<input name="Name">   →  student.Name
<input name="Email">  →  student.Email
<input name="Age">    →  student.Age
```

Model Binder:

1. `Student` ka naya object banata hai.
2. Request ka data property-name se match karta hai.
3. Type conversion karta hai (jaise string `"20"` → int `20`).
4. Properties set karke object action ko de deta hai.

> ⚠️ Agar type convert nahi ho paya (jaise `Age = "abc"`), toh binding fail hoti hai aur **ModelState mein error** add ho jata hai. (Iska use aage `ModelState.IsValid` mein dekhenge.)

---

## 7. Model Validation kya hoti hai?

### Definition

**Model Validation is the process of checking whether the data received from an HTTP request satisfies the validation rules defined in a model.**

### Problem samjho

User jab form bharta hai, toh kuch bhi data daal sakta hai:

```
Name  = ""
Email = "abc123"
Age   = 12
Phone = "123"
```

**Kya ye valid data hai? ❌ Nahi.** Kyunki:

- `Name` empty hai.
- `Email` ka format incorrect hai.
- `Age` allowed range se kam hai.
- `Phone` number valid nahi hai.

Hum nahi chahte ki aisa **invalid data database mein save ho.**

👉 **Isi problem ko solve karne ke liye Model Validation use hoti hai.**

---

## 8. Model Binding vs Model Validation (difference)

Dono ka kaam alag hai, interview mein ye difference zaroor poocha jata hai:

|            | Model Binding                                                  | Model Validation                                       |
| ---------- | -------------------------------------------------------------- | ------------------------------------------------------ |
| Kaam       | HTTP request ke data ko action method / model se **map** karna | Jo data aaya hai usko rules ke against **check** karna |
| Sawaal     | "Data kahan se aaya aur kahan jayega?"                         | "Jo data aaya wo sahi hai ya nahi?"                    |
| Pehle kaun | **Pehle** hoti hai                                             | Binding ke **baad** hoti hai                           |
| Result     | Parameters / model properties fill hoti hain                   | `ModelState` mein valid/invalid status set hota hai    |

### Analogy

Soch: ek **security guard + receptionist** wala setup.

- **Model Binding** = Receptionist, jo courier ka parcel receive karke sahi desk (parameter) tak pahuncha deta hai.
- **Model Validation** = Security guard, jo parcel khol ke check karta hai ki andar sab sahi hai ya nahi.

### Order of execution

```
HTTP Request
    │
    ▼
Routing
    │
    ▼
Model Binding      (data → model/parameters)
    │
    ▼
Model Validation   (rules check hote hain)
    │
    ▼
Action Method      (yahan ModelState.IsValid check karte hain)
```

---

## 9. Data Annotations

ASP.NET Core mein validation rules define karne ke liye hum **Data Annotations** use karte hain.

### Data Annotations kya hoti hain?

Ye **predefined attributes** hain jo hum model ki **properties** par apply karte hain.

### Namespace import karna padta hai

Data Annotations use karne ke liye sabse pehle ye namespace import karna padta hai:

```csharp
using System.ComponentModel.DataAnnotations;
```

### Poora example model (sab annotations ek saath)

```csharp
using System.ComponentModel.DataAnnotations;

public class Student
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name 3 se 50 characters ke beech hona chahiye")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Enter valid email")]
    public string Email { get; set; }

    [Range(18, 60, ErrorMessage = "Age 18 se 60 ke beech honi chahiye")]
    public int Age { get; set; }

    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter valid mobile number")]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; }
}
```

Ab ek-ek annotation detail mein 👇

---

### 1. `[Required]` Attribute

Property **empty / null nahi honi chahiye**.

```csharp
[Required]
public string Name { get; set; }
```

Agar **custom error message** print karna ho:

```csharp
[Required(ErrorMessage = "Name is required")]
public string Name { get; set; }
```

---

### 2. `[StringLength]` Attribute

String ki **minimum aur maximum length** define karta hai.

```csharp
[StringLength(50, MinimumLength = 3)]
public string Name { get; set; }
```

- `50` = maximum length
- `MinimumLength = 3` = minimum length

> 💡 Ye validation **DB column length** se bhi match karni chahiye (jaise `varchar(50)`), warna DB level par error aa sakta hai.

---

### 3. `[Range]` Attribute

Number ki **minimum aur maximum value** define karta hai.

```csharp
[Range(18, 60)]
public int Age { get; set; }
```

Ab `Age = 12` daala toh validation fail ❌ (kyunki 18 se kam hai).

---

### 4. `[EmailAddress]` Attribute

Check karta hai ki value **email format** mein hai ya nahi.

```csharp
[EmailAddress]
public string Email { get; set; }
```

`abc123` → ❌ Invalid
`abc@gmail.com` → ✅ Valid

---

### 5. `[RegularExpression]` Attribute

Custom pattern (Regex) ke against value match karta hai.

```csharp
[RegularExpression(@"^[6-9]\d{9}$",
    ErrorMessage = "Enter valid mobile number")]
public string PhoneNumber { get; set; }
```

Regex ko samjho:

| Part    | Matlab                                                       |
| ------- | ------------------------------------------------------------ |
| `^`     | String ki shuruaat                                           |
| `[6-9]` | Pehla digit 6, 7, 8 ya 9 hona chahiye (Indian mobile number) |
| `\d{9}` | Uske baad exactly 9 digits                                   |
| `$`     | String ka end                                                |

Total = **10 digit** ka number, jo 6-9 se start hota hai.

`123` → ❌ Invalid
`8109663975` → ✅ Valid

---

### 6. `[Compare]` Attribute

Do properties ki value **match** karta hai. Sabse common use: **Password aur Confirm Password**.

```csharp
public string Password { get; set; }

[Compare("Password", ErrorMessage = "Password does not match")]
public string ConfirmPassword { get; set; }
```

**Kaise kaam karta hai:**

- `Password` property ki value pehle se request mein aa chuki hoti hai.
- Fir `ConfirmPassword` ki value `Password` ke saath compare hoti hai.
- Agar **dono match** karte hain → ✅ validation pass.
- Agar **match nahi** karte → ❌ error message generate hota hai.

> 💡 `Compare` hamesha **confirm wali property** par lagta hai, aur andar us property ka naam likhte hain jisse compare karna hai (yahan `"Password"`).

---

### Annotations ki summary table

| Attribute             | Kya check karta hai                | Example                                |
| --------------------- | ---------------------------------- | -------------------------------------- |
| `[Required]`          | Value empty/null nahi honi chahiye | `[Required]`                           |
| `[StringLength]`      | String ki min/max length           | `[StringLength(50, MinimumLength=3)]`  |
| `[Range]`             | Number min–max ke beech ho         | `[Range(18,60)]`                       |
| `[EmailAddress]`      | Email format                       | `[EmailAddress]`                       |
| `[RegularExpression]` | Custom regex pattern               | `[RegularExpression(@"^[6-9]\d{9}$")]` |
| `[Compare]`           | Do properties ki value same ho     | `[Compare("Password")]`                |

---

## 10. `ModelState.IsValid`

Humne model par validation attributes laga diye. **Lekin sirf laga dene se kaam nahi hota** — controller mein ye bhi check karna padta hai ki model valid hai ya nahi.

Isliye hum use karte hain:

```csharp
ModelState.IsValid
```

| Value   | Matlab                                                                               |
| ------- | ------------------------------------------------------------------------------------ |
| `true`  | Model validation mein **koi error nahi** tha. User ne jo data daala wo sab sahi hai. |
| `false` | Model validation mein **error hai**. User ne jo data daala usme kuch galat hai.      |

### Example

```csharp
[HttpPost]
public IActionResult Create(Student student)
{
    if (ModelState.IsValid)
    {
        // Data valid hai → database mein save karo
        // Save student into database

        return Content("Student Created Successfully");
    }

    // Data invalid hai → same form wapas dikhao (errors ke saath)
    return View(student);
}
```

### Flow

```
Form Submit
    │
    ▼
Model Binding  →  Model Validation
    │
    ▼
ModelState.IsValid ?
    │
    ├── true  → Database mein save → Success response
    │
    └── false → return View(student)  (form wapas + error messages)
```

### Dhyan dene wali baatein

- Agar `ModelState.IsValid` check **nahi** kiya, toh invalid data bhi DB mein save ho jayega ⚠️
- `return View(student)` isliye karte hain taaki user ne jo data daala wo form mein **wapas dikhe** aur user ko dubara sab type na karna pade.
- `ModelState` mein **binding errors** (jaise `"abc"` ko int mein convert nahi kar paye) aur **validation errors** dono store hote hain.

---

## 11. View mein Validation Errors dikhana (Tag Helpers)

Controller se `View(student)` return hua. Ab View mein **errors ko user ko dikhana** hai. Iske liye hum **Tag Helpers** use karte hain.

### 3 important Tag Helpers

| Tag Helper               | Purpose                                                   |
| ------------------------ | --------------------------------------------------------- |
| `asp-for`                | Model property ke saath input ko **associate** karta hai  |
| `asp-validation-for`     | **Particular property** ka error display karta hai        |
| `asp-validation-summary` | **Multiple validation errors** ek jagah display karta hai |

### Poora example (Create.cshtml)

```html
@model Student

<form asp-action="Create" method="post">
  <!-- Saare errors ek jagah (top par) -->
  <div asp-validation-summary="All" class="text-danger"></div>

  <div>
    <label asp-for="Name"></label>
    <input asp-for="Name" />
    <span asp-validation-for="Name" class="text-danger"></span>
  </div>

  <div>
    <label asp-for="Email"></label>
    <input asp-for="Email" />
    <span asp-validation-for="Email" class="text-danger"></span>
  </div>

  <div>
    <label asp-for="Age"></label>
    <input asp-for="Age" />
    <span asp-validation-for="Age" class="text-danger"></span>
  </div>

  <button type="submit">Save</button>
</form>
```

### Ek-ek ko samjho

**1. `asp-for`**

```html
<input asp-for="Name" />
```

Ye automatically generate karta hai:

```html
<input type="text" id="Name" name="Name" value="..." />
```

Matlab `name` attribute property ke naam ke hisaab se set ho jata hai — **isi se Model Binding sahi kaam karti hai.**

**2. `asp-validation-for`**

```html
<span asp-validation-for="Name" class="text-danger"></span>
```

Sirf `Name` property ka error yahan dikhega (jaise "Name is required").

**3. `asp-validation-summary`**

```html
<div asp-validation-summary="All" class="text-danger"></div>
```

Saare errors **ek jagah list** mein dikhte hain.

| Value       | Matlab                                   |
| ----------- | ---------------------------------------- |
| `All`       | Property-level + model-level dono errors |
| `ModelOnly` | Sirf model-level errors                  |
| `None`      | Kuch nahi dikhata                        |

### Client-side validation (bonus)

Agar page ko reload kiye bina **browser mein hi** validation chahiye, toh view ke end mein ye add karte hain:

```html
@section Scripts {
<partial name="_ValidationScriptsPartial" />
}
```

> 💡 **Important:** Client-side validation sirf user experience ke liye hai. **Server-side validation (`ModelState.IsValid`) hamesha zaroori hai**, kyunki client-side ko bypass kiya ja sakta hai (jaise Postman se direct request).

---

## 12. MVC Controller vs API Controller

Ye ek **important difference** hai, yaad rakhna 👇

### MVC Controller mein

Yahan hum **manually** `ModelState.IsValid` check karte hain.

```csharp
[HttpPost]
public IActionResult Create(Student student)
{
    if (!ModelState.IsValid)
    {
        return View(student);   // form + errors wapas
    }

    return RedirectToAction("Index");
}
```

### Web API Controller mein

```csharp
[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(Student student)
    {
        return Ok(student);
    }
}
```

Yahan `ModelState.IsValid` **manually check karne ki zaroorat nahi.**

`[ApiController]` attribute ki wajah se, agar `ModelState` invalid hai, toh ASP.NET Core **automatically HTTP 400 Bad Request** response bhej deta hai — aur action method execute hi nahi hota.

👉 Is behavior ko **automatic HTTP 400 response behavior** kehte hain.

### Comparison table

|                            | MVC Controller                    | API Controller (`[ApiController]`)             |
| -------------------------- | --------------------------------- | ---------------------------------------------- |
| Base class                 | `Controller`                      | `ControllerBase`                               |
| `ModelState.IsValid` check | **Manually** karna padta hai      | **Automatic** (invalid par 400 return)         |
| Invalid data par           | `return View(model)` (form wapas) | HTTP **400 Bad Request** (JSON error response) |
| Response type              | HTML View                         | JSON / data                                    |
| Complex type binding       | Form / Query / Route se           | Default **`[FromBody]`** (JSON se)             |

> 💡 `[ApiController]` ke saath complex types (jaise `Student`) ko by default **request body (JSON)** se bind kiya jata hai. Ye bhi `[ApiController]` ke "automatic behaviors" mein aata hai.

---

## 13. Express.js se comparison

Tum Node.js/Express mein kaam karte ho, toh isse relate karna easy hoga:

| ASP.NET Core                   | Express.js                                                       |
| ------------------------------ | ---------------------------------------------------------------- |
| Route data (`/Student/Edit/2`) | `req.params.id`                                                  |
| Query string (`?id=10`)        | `req.query.id`                                                   |
| Form / JSON body               | `req.body`                                                       |
| Model Binding (automatic)      | Tumhe **manually** `req.params`, `req.body` se nikalna padta hai |
| Data Annotations               | `Joi` / `express-validator` / `Zod` schema                       |
| `ModelState.IsValid`           | `validationResult(req).isEmpty()`                                |
| `[ApiController]` auto 400     | Validation middleware jo `res.status(400).json(...)` bhejta hai  |

### Express mein kaisa dikhega

```js
// Express: manually sab nikalna padta hai
app.post("/students", (req, res) => {
  const { name, email, age } = req.body; // ← ASP.NET mein ye Model Binding karti hai

  // Manual validation
  if (!name || age < 18) {
    return res.status(400).json({ message: "Invalid data" }); // ← ASP.NET mein [ApiController] karta hai
  }

  // save to DB (Mongo / MySQL)
});
```

### ASP.NET Core mein same cheez

```csharp
[ApiController]
[Route("api/students")]
public class StudentController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(Student student)
    {
        // student already bind + validate ho chuka hai
        // invalid hota toh yahan aane se pehle hi 400 mil jata
        return Ok(student);
    }
}
```

**Takeaway:** ASP.NET Core mein framework **binding + validation + 400 response** teeno khud handle kar leta hai. Express mein ye sab tumhe **middleware ya manually** karna padta hai.

---

## -----------------------------------------------------------------------------------

## Entity Core Framwork - alg se md file proper tarike se explain kiya hi

> Code first approach
> Database first approach

Creating CRUD Application using Code First Approach in ASP.NET Core 6
Creating CRUD Application using Database First Approach in ASP.NET Core 6

## -------------------------------------------------------------------------

# Session in ASP.NET Core (MVC) — Complete Notes

> Ye notes tumhare original notes ka properly explained version hain. Koi point miss nahi kiya, bas structure, examples aur interview angle add kiya hai.

---

## 1. Problem kya hai? (Session ki zarurat kyun)

ASP.NET Core MVC me data ek action se dusre action/view tak bhejne ke kai tarike hain: **ViewBag, ViewData, TempData**.

| Technique              | Data kab tak rehta hai                                                                                                                                                               |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `ViewBag` / `ViewData` | Sirf **current request** (Controller → View). Redirect hote hi gaya.                                                                                                                 |
| `TempData`             | Next request tak (ek redirect), **read hote hi delete** ho jata hai. Dobara chahiye to `TempData.Keep()` / `Peek()` use karna padta hai, yaani baar baar action me bhejna padta hai. |
| **Session**            | Multiple requests, multiple actions, multiple views — jab tak session **expire ya remove** na ho.                                                                                    |

**Simple analogy:**

- `ViewBag` = chit jo sirf tumhare haath se view ke haath me gayi, phir phek di.
- `TempData` = chit jo ek baar padhne ke baad automatically fat jati hai.
- `Session` = locker jisme tum data rakh do, aur kisi bhi action se key se nikal lo.

**Rule of thumb:** Agar same data **kai action methods me chahiye** → Session.

---

## 2. Session kya hai? (Definition — interview ke liye)

- Session ek **State Management technique** hai.
- **Session State** = ASP.NET Core ka scenario jisme user ke web app browse karte waqt uska data store hota hai.
- Ye app ke maintain kiye hue **store** me data persist karta hai, **ek hi client ke multiple requests ke across**.
- HTTP **stateless** hai (har request independent). Session is stateless nature ka workaround hai.

### Session ki important properties

1. **Server-side** hai — actual data server pe rehta hai. Browser ko sirf ek **session cookie (Session ID)** milti hai.
2. **Browser-specific** hai — Chrome me bana session Firefox me nahi milega, chahe URL copy kar lo. Kyunki cookie alag hai.
3. **Every user ka alag session** — 100 users app use kar rahe hain to 100 alag sessions, har ek ka **unique Session ID**.
4. **Ephemeral data** hai — matlab temporary; kuch time baad automatically delete (default **20 minutes idle**).
5. **Browser close = session cookie delete** (default behaviour), isse naya session banega jab tak tum dobara create na karo.
6. TempData ke unlike, session apni **expiration time tak persist** karta hai.
7. Session ka data **cache** (by default in-memory) me hota hai.

### Kya store karein, kya nahi?

- **Critical / permanent data** (orders, payments, user profile) → **Database** me.
- **Frequently used, multiple actions/views me chahiye wala data** (UserId, role, cart ids, display name) → **Session me cache** karke rakho (performance optimization).

---

## 3. Session implement karne ke Steps

### Step 1 — `Program.cs` me setup

```csharp
// Build se PEHLE: service register
builder.Services.AddSession();

var app = builder.Build();

// Build ke BAAD: middleware pipeline me add
app.UseSession();
```

> **Order matters:** `UseSession()` ko `UseRouting()` ke baad aur `MapControllerRoute()` / endpoints se **pehle** rakho, warna controller me `HttpContext.Session` kaam nahi karega (exception aayega).

### Step 2 — Session variable create karna (Set)

Pehle namespace import:

```csharp
using Microsoft.AspNetCore.Http;
```

Session set karne ka syntax:

```csharp
HttpContext.Session.SetString("MyKey", "ProgramMentor");
//                             ^key      ^value
```

Controller me example:

```csharp
public IActionResult Privacy()
{
    // Privacy action chalega to ye session ban jayega.
    // Isko baad me KEY ki help se kisi bhi action me access kar sakte hain.
    HttpContext.Session.SetString("MyKey", "ProgramMentor");

    return View();
}
```

> Notes me bola tha ki abhi session controller ke andar bana rahe hain, production me kaha banate hain wo baad me dekhna. **Production practice:** session ki keys ko ek `static class SessionKeys` me constants bana ke rakho (typos se bachne ke liye), aur session set karne ka kaam usually **Login action / service layer** me hota hai.

### Step 3 — Session variable access karna (Get)

```csharp
HttpContext.Session.GetString("MyKey");
```

Dusre action me use:

```csharp
public IActionResult About()
{
    // Pehle null check — session expire ho gaya ho to GetString null deta hai
    if (HttpContext.Session.GetString("MyKey") != null)
    {
        ViewBag.Data = HttpContext.Session.GetString("MyKey").ToString();
    }

    return View();
}
```

View me dikhana:

```html
<h3>@ViewBag.Data</h3>
```

**Flow samjho:**

1. User pehle `Privacy` action chalata hai → session **create** hota hai.
2. Phir `About` action chalata hai → wahan session se data **nikal ke ViewBag me daal** diya → view me dikh gaya.
3. Ye same code kitne bhi action methods me use kar sakte ho. Session **across multiple actions** mil jata hai.

> Session khud se destroy karna padta hai (Remove method se), ya fir idle timeout / browser close pe ho jata hai.

---

## 4. Session ko directly View me access karna (Step 4)

Agar action method ke bina **seedha view me** session padhna hai to `IHttpContextAccessor` chahiye.

### 4.1 `Program.cs` me register karo

```csharp
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Ya shortcut (recommended):
// builder.Services.AddHttpContextAccessor();
```

### 4.2 View me import + inject

```html
@using Microsoft.AspNetCore.Http @inject
Microsoft.AspNetCore.Http.IHttpContextAccessor accessor
<!-- accessor ki help se session ko view ke andar directly access karenge -->

<h3>@accessor.HttpContext.Session.GetString("MyKey")</h3>
```

### Dono tarike ka summary

| Tarika         | Kaise                                                                              |
| -------------- | ---------------------------------------------------------------------------------- |
| Via Action     | Action me `GetString` → `ViewBag` me daalo → view me `@ViewBag.Data`               |
| Direct in View | `IHttpContextAccessor` inject → `@accessor.HttpContext.Session.GetString("MyKey")` |

Dono tarike view me use kar sakte hain.

> **Best practice:** Direct view access chhote cases me theek hai (jaise navbar me user ka naam). Business logic hamesha controller/service me rakho.

---

## 5. Session ko Destroy kaise karein

### Remove method

```csharp
public IActionResult Logout()
{
    if (HttpContext.Session.GetString("MyKey") != null)
    {
        HttpContext.Session.Remove("MyKey");   // sirf wo key delete hogi
    }
    return View();
}
```

Jaise hi user `Logout` action pe aata hai, `Remove` chalta hai aur **us key ka session data destroy** ho jata hai.

### Destroy hone ke saare tarike

| Tarika                              | Kya hota hai                                                           |
| ----------------------------------- | ---------------------------------------------------------------------- |
| `HttpContext.Session.Remove("key")` | Ek specific key delete                                                 |
| `HttpContext.Session.Clear()`       | Us user ke **saare** session keys delete (logout me ye zyada safe hai) |
| Idle timeout (default 20 min)       | Automatically expire                                                   |
| Browser band karna                  | Session cookie delete → purana session nahi milega                     |

> **Logout me `Clear()` use karna better hai**, kyunki `Remove` sirf ek key hataata hai, baaki data (jaise role, userId) reh sakta hai.

---

## 6. Session ka Timeout change karna

Default: **20 minutes** (idle). Kam ya zyada karne ke liye `Program.cs`:

```csharp
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // yaha 20 se 10 min kar diya
});
```

**Idle timeout ka matlab:** Jab tak user har request bhejta rehta hai, timer reset hota rehta hai. Agar 10 min tak **koi request nahi aayi**, tab session expire.

### Production-style full config

```csharp
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;      // JavaScript cookie read nahi kar sakti (XSS se bachav)
    options.Cookie.IsEssential = true;   // GDPR/cookie-consent ke bina bhi cookie bheji jayegi
});
```

| Option                      | Kaam                                                                                                       |
| --------------------------- | ---------------------------------------------------------------------------------------------------------- |
| `IdleTimeout`               | Kitne idle time ke baad session expire                                                                     |
| `Cookie.HttpOnly = true`    | JS se session cookie access block (security)                                                               |
| `Cookie.IsEssential = true` | Consent policy ke bina bhi cookie allowed. Warna cookie consent enable hone pe session kaam hi nahi karega |

---

## 7. Session me multiple data types

Notes me bola tha "multiple type ka data store kar sakte hain". Exact baat:

```csharp
// String
HttpContext.Session.SetString("Name", "Sourabh");
string name = HttpContext.Session.GetString("Name");

// Int
HttpContext.Session.SetInt32("UserId", 101);
int? userId = HttpContext.Session.GetInt32("UserId");
```

> Session natively sirf **string, int, byte[]** support karta hai. **Object / List** store karna ho to **JSON serialize** karke string me daalo:

```csharp
using System.Text.Json;

// Set
var user = new { Id = 1, Name = "Sourabh", Role = "Admin" };
HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));

// Get
var json = HttpContext.Session.GetString("User");
var obj = json == null ? null : JsonSerializer.Deserialize<UserDto>(json);
```

---

## 8. Project: Login with Database + Session + Logout (ASP.NET Core 6)

### Phases

1. ASP.NET Core app me **Session setup**
2. **Database First Approach** implement (Code First bhi use kar sakte the)
3. **Login & Logout** task

### Phase 1 — Session setup

```csharp
builder.Services.AddSession();
// ...
app.UseSession();
```

Ye dono lines se session register ho gaya.

### Phase 2 — Database First Approach

1. **3 NuGet packages install** karo:
   - `Microsoft.EntityFrameworkCore.SqlServer` (ya jo DB use kar rahe ho, e.g. MySQL ke liye Pomelo)
   - `Microsoft.EntityFrameworkCore.Tools`
   - `Microsoft.EntityFrameworkCore.Design`
2. **Scaffold command** run karo (existing DB se models + DbContext ban jata hai):

```powershell
Scaffold-DbContext "Server=.;Database=MyDb;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```

3. **Connection string ko `DbContext` class se hata ke `appsettings.json` me move** karo (hardcoded connection string security + flexibility ke liye bura hai):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=MyDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

```csharp
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

### Phase 3 — Login & Logout

**Plan:** Login action me DB se user verify karo, sahi hai to session variable create karo. Baki pages me us session ko access karke check karo user logged in hai ya nahi. Logout me session clear.

```csharp
[HttpPost]
public IActionResult Login(string email, string password)
{
    // DB me user dhundo
    var user = _context.Users.FirstOrDefault(u => u.Email == email);

    // NOTE: production me password HASH compare karo (BCrypt), plain text kabhi nahi
    if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
    {
        // Login success -> session variables create
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.Name);
        return RedirectToAction("Dashboard");
    }

    ViewBag.Error = "Invalid email or password";
    return View();
}

public IActionResult Dashboard()
{
    // Protected page: session nahi hai to login pe bhejo
    if (HttpContext.Session.GetInt32("UserId") == null)
        return RedirectToAction("Login");

    ViewBag.Name = HttpContext.Session.GetString("UserName");
    return View();
}

public IActionResult Logout()
{
    HttpContext.Session.Clear();             // saara session data delete
    return RedirectToAction("Login");
}
```

> Notes me aage "ab Register karenge" likha tha — Register wala part next lecture/phase hai (new user DB me insert, password hash karke save).

**Project analogy:** Jaise MediSetu me patient/doctor login karta hai aur alag-alag pages pe uski identity chahiye, waise yahan `UserId` session me rakh ke har action me check karte hain. Real projects me tum JWT + RBAC use karte ho; session usi ka **server-side, cookie-based** cousin hai.

---

## 9. Session vs TempData vs ViewBag vs ViewData (Quick Compare)

| Feature          | ViewBag           | ViewData                  | TempData                          | Session                                 |
| ---------------- | ----------------- | ------------------------- | --------------------------------- | --------------------------------------- |
| Direction        | Controller → View | Controller → View         | Action → Action (redirect)        | Kahin bhi                               |
| Lifetime         | Current request   | Current request           | Next request tak (read pe delete) | Idle timeout / logout tak               |
| Type safety      | Dynamic           | Dictionary (cast chahiye) | Dictionary (cast chahiye)         | Typed helpers (`GetString`, `GetInt32`) |
| Storage          | —                 | —                         | Cookie / Session                  | Server-side store (cache)               |
| Redirect survive | No                | No                        | Yes (once)                        | Yes (multiple)                          |

---

## 10. Important Cautions / Production Notes

1. **Default session store = in-memory** (`IDistributedMemoryCache`). Server restart ya multiple servers (load balancer, AWS ALB ke peeche multiple EC2) pe session **lost / inconsistent** ho sakta hai. Production me **Redis distributed cache** use karo:
   ```csharp
   builder.Services.AddStackExchangeRedisCache(o => o.Configuration = "localhost:6379");
   builder.Services.AddSession();
   ```
2. **Sensitive data** (password, card details) session me kabhi mat rakho.
3. Session **memory consume** karta hai — bahut bada data mat dalo.
4. Session ka data **sirf us user ka** hai; shared/global data ke liye `IMemoryCache` use karo.
5. Real authentication ke liye **Cookie Authentication / Identity / JWT** better hai; session login ka simple demo/learning approach hai.

# Bind Multiple Models with a Single View in ASP.NET Core (ViewModel)

> Ye tumhare original notes ka properly explained version hai. Koi point miss nahi kiya. Jo cheezein notes me directly nahi thi (jaise `Student` / `Teacher` model classes, table headers, alternatives), unko maine add kiya hai aur "Added" mark kiya hai.

---

## 1. Problem kya hai?

Ek normal MVC view me hum **ek hi model** bind karte hain:

```html
@model Student
```

Ye approach tab tak chalti hai jab view ko **sirf ek model** ka data dikhana ho.

**Problem:** Agar ek hi page pe **do (ya zyada) alag models** ka data dikhana ho, jaise **Students ki list + Teachers ki list**, to?

Ek view me `@model` directive **sirf ek baar** likh sakte ho. Matlab:

```html
@model Student @model Teacher // ERROR: ek view me do @model nahi chalega
```

Isliye "single view + single model" wali approach yaha kaam nahi karti. Iske liye alag approach chahiye: **ViewModel**.

---

## 2. Solution: ViewModel

**Idea:**

1. Ek **3rd model class** banao.
2. Us class me **Model1 aur Model2 ki properties** register karo (yaha: Student list + Teacher list).
3. Is 3rd class ko hi bolte hain **ViewModel**.
4. Controller me is ViewModel ka object banao, data bharo, aur `View(viewModel)` se view ko pass karo.
5. View me **sirf ye ek ViewModel** `@model` ke through bind hota hai.

**Definition (interview ke liye):**

> ViewModel ek aisi class hoti hai jo **ek ya zyada models (ya alag-alag data)** ko combine karke view ko ek hi object me deti hai. Ye specifically view ki zarurat ke hisaab se design hoti hai.

**Analogy (tumhare project se):**
MediSetu me ek **Patient Dashboard** socho jisme ek hi page pe **Appointments ki list + Prescriptions ki list + Lab Reports ki list** dikhani hai. Teeno alag models/tables hain, lekin page ek hai. To tum ek `PatientDashboardViewModel` banate ho jisme teeno lists hoti hain, aur wahi ek object view ko dete ho. Bilkul yahi concept Student + Teacher ke saath hai.

---

## 3. Step by Step Implementation

### Step 1: Individual Models (Added: notes me nahi tha, properties se infer kiya)

Notes me jo data tha usse in models ki properties pata chalti hain:

```csharp
namespace Bind_Multiple_Models_With_Single_View.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Standard { get; set; }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Subject { get; set; }
        public int Experience { get; set; }   // years me
        public int Salary { get; set; }
    }
}
```

### Step 2: ViewModel class banao (3rd model)

```csharp
namespace Bind_Multiple_Models_With_Single_View.Models
{
    public class SchoolViewModel
    {
        public List<Student> myStudents { get; set; }
        public List<Teacher> myTeachers { get; set; }
    }
}
```

**Samjho:**

- Hum **list of data** dikhana chahte hain, isliye properties ka type `List<Student>` aur `List<Teacher>` rakha.
- Agar sirf ek student/teacher dikhana hota to `Student` / `Teacher` (single object) rakhte.
- Ye dono models ki properties ek class me **register / combine** ho gayi.

> **Note (correction):** Notes ke last line me "List<Student> and List<Employee>" likha tha. Yaha **Employee nahi, Teacher** hona chahiye. Code me `List<Teacher>` hi hai, wo sahi hai.

> **Naming convention:** C# me properties ke naam normally **PascalCase** me rakhte hain (`MyStudents`, `MyTeachers`). Notes me `myStudents` / `myTeachers` hai, wo bhi chalta hai, lekin professional code me PascalCase use karo.

### Step 3: Controller Action me ViewModel bharo aur View ko pass karo

```csharp
public IActionResult Index()
{
    // 1) Student ka data (abhi dummy / hardcoded data hai; real project me DB se aayega)
    List<Student> students = new List<Student>
    {
        new Student { Id = 1, Name = "Rahul Sharma", Gender = "Male",   Standard = 10 },
        new Student { Id = 2, Name = "Priya Verma",  Gender = "Female", Standard = 12 },
        new Student { Id = 3, Name = "Amit Kumar",   Gender = "Male",   Standard = 9  },
        new Student { Id = 4, Name = "Sneha Patel",  Gender = "Female", Standard = 11 },
        new Student { Id = 5, Name = "Rohit Singh",  Gender = "Male",   Standard = 8  },
        new Student { Id = 6, Name = "Anjali Gupta", Gender = "Female", Standard = 10 },
        new Student { Id = 7, Name = "Karan Joshi",  Gender = "Male",   Standard = 12 }
    };

    // 2) Teacher ka data
    List<Teacher> teachers = new List<Teacher>
    {
        new Teacher { Id = 1, Name = "Amit Sharma",  Email = "amit@gmail.com",   Gender = "Male",   Subject = "Mathematics",      Experience = 8,  Salary = 55000 },
        new Teacher { Id = 2, Name = "Priya Verma",  Email = "priya@gmail.com",  Gender = "Female", Subject = "Science",          Experience = 5,  Salary = 45000 },
        new Teacher { Id = 3, Name = "Rahul Patel",  Email = "rahul@gmail.com",  Gender = "Male",   Subject = "English",          Experience = 10, Salary = 65000 },
        new Teacher { Id = 4, Name = "Sneha Gupta",  Email = "sneha@gmail.com",  Gender = "Female", Subject = "Computer Science", Experience = 6,  Salary = 50000 },
        new Teacher { Id = 5, Name = "Rohit Singh",  Email = "rohit@gmail.com",  Gender = "Male",   Subject = "Physics",          Experience = 12, Salary = 75000 },
        new Teacher { Id = 6, Name = "Anjali Joshi", Email = "anjali@gmail.com", Gender = "Female", Subject = "Chemistry",        Experience = 4,  Salary = 40000 },
        new Teacher { Id = 7, Name = "Karan Mehta",  Email = "karan@gmail.com",  Gender = "Male",   Subject = "Biology",          Experience = 7,  Salary = 52000 }
    };

    // 3) ViewModel ka object banao aur dono lists assign karo
    SchoolViewModel svm = new SchoolViewModel()
    {
        myStudents = students,   // ViewModel ki property me student list pass kar di
        myTeachers = teachers    // ViewModel ki property me teacher list pass kar di
    };

    // 4) Ek hi ViewModel object view ko bhej do
    return View(svm);
}
```

**Flow:**

```
Controller (Index)
   |-- students list banayi
   |-- teachers list banayi
   |-- SchoolViewModel me dono daali
   |-- return View(svm)
          |
          v
View (Index.cshtml)  --> @model SchoolViewModel --> Model.myStudents / Model.myTeachers
```

### Step 4: View me ViewModel bind karo aur data dikhao

```html
@model SchoolViewModel
```

> **Important:** Notes ke hisaab se sirf `SchoolViewModel` tabhi likh sakte ho jab `Views/_ViewImports.cshtml` me ye namespace import ho:
>
> ```html
> @using Bind_Multiple_Models_With_Single_View.Models
> ```
>
> Agar ye import nahi hai to full name likhna padega:
>
> ```html
> @model Bind_Multiple_Models_With_Single_View.Models.SchoolViewModel
> ```

#### Student ki table (Added: table structure, notes me sirf `foreach` tha)

```html
<h2>Students</h2>
<table class="table table-bordered">
  <thead>
    <tr>
      <th>Id</th>
      <th>Name</th>
      <th>Gender</th>
      <th>Standard</th>
    </tr>
  </thead>
  <tbody>
    @foreach (var item in Model.myStudents) {
    <tr>
      <td>@item.Id</td>
      <td>@item.Name</td>
      <td>@item.Gender</td>
      <td>@item.Standard</td>
    </tr>
    }
  </tbody>
</table>
```

Is tarah hum **Student model ki properties** access kar lete hain, `Model.myStudents` ke through.

#### Teacher ki table

```html
<h2>Teachers</h2>
<table class="table table-bordered">
  <thead>
    <tr>
      <th>Id</th>
      <th>Name</th>
      <th>Email</th>
      <th>Gender</th>
      <th>Subject</th>
      <th>Experience</th>
      <th>Salary</th>
    </tr>
  </thead>
  <tbody>
    @foreach (var item in Model.myTeachers) {
    <tr>
      <td>@item.Id</td>
      <td>@item.Name</td>
      <td>@item.Email</td>
      <td>@item.Gender</td>
      <td>@item.Subject</td>
      <td>@item.Experience Years</td>
      <td>@item.Salary</td>
    </tr>
    }
  </tbody>
</table>
```

Is tarah hum **Teacher model ki properties** access karte hain, `Model.myTeachers` ke through.

> **Small tip:** `@item.Experience Years` me "Years" plain text hai, wo `Experience` ke baad display ho jayega (jaise "8 Years"). Agar kabhi confusion ho to `@item.Experience <text>Years</text>` ya `@(item.Experience) Years` use kar sakte ho.

---

## 4. Poora Concept Ek Nazar Me

| Step | Kya karna hai                                               | Kahan            |
| ---- | ----------------------------------------------------------- | ---------------- |
| 1    | Individual models banao (`Student`, `Teacher`)              | `Models/`        |
| 2    | ViewModel banao jisme dono ki lists ho (`SchoolViewModel`)  | `Models/`        |
| 3    | Controller me data bharo, ViewModel me daalo, `View(svm)`   | `HomeController` |
| 4    | View me `@model SchoolViewModel` likho                      | `Index.cshtml`   |
| 5    | `Model.myStudents` / `Model.myTeachers` pe `foreach` chalao | `Index.cshtml`   |

**Key line yaad rakho:**

> Jitne models ek view me chahiye, un sabki properties **pehle ViewModel me register** karo. Phir **ek hi ViewModel** view ko do.

---

## 5. Kab List, Kab Single Object? (Notes ka last point)

Notes me bola tha: "apne ko pta list of data rahega isliye `List<Student>` aur `List<Teacher>` banaya".

| Situation                                      | ViewModel property type                    |
| ---------------------------------------------- | ------------------------------------------ |
| Multiple records dikhane hain (table)          | `List<Student>` / `IEnumerable<Student>`   |
| Ek hi record dikhana hai (detail/profile page) | `Student`                                  |
| Dono chahiye                                   | Dono properties ViewModel me rakh sakte ho |

---

## 6. Multiple Models ko Single View me dikhane ke Alternative Tarike (Added)

Interview me aksar poochte hain "ViewModel ke alawa aur kya options hain?". Jawab:

| Tarika                              | Kaise                                                      | Limitation                                                             |
| ----------------------------------- | ---------------------------------------------------------- | ---------------------------------------------------------------------- |
| **ViewModel** (recommended)         | Alag class me sab combine                                  | Ek extra class banani padti hai                                        |
| **ViewBag / ViewData**              | Ek model `@model` me, doosra `ViewBag.Teachers = teachers` | Strongly-typed nahi, cast karna padta hai, errors runtime pe aate hain |
| **Tuple**                           | `@model (List<Student>, List<Teacher>)`                    | Code padhne me kam readable, `Item1`, `Item2`                          |
| **Partial Views / View Components** | Page ko chhote parts me todo, har part ka apna model       | Extra setup, bade pages ke liye achha                                  |

**Kyun ViewModel best hai?**

- **Strongly typed** (compile time pe errors pakde jaate hain)
- Clean aur readable code
- Alag-alag models ke saath-saath **extra view-specific data** (jaise page title, total count) bhi add kar sakte ho
- Domain/DB models ko view ki zarurat se alag rakhta hai (separation of concerns)

---

## 7. ViewModel me Extra Data Bhi Daal Sakte Ho (Added)

ViewModel sirf models ki lists tak limited nahi hai:

```csharp
public class SchoolViewModel
{
    public string SchoolName { get; set; }
    public int TotalStudents { get; set; }
    public List<Student> myStudents { get; set; }
    public List<Teacher> myTeachers { get; set; }
}
```

```csharp
SchoolViewModel svm = new SchoolViewModel()
{
    SchoolName = "Medicaps Public School",
    TotalStudents = students.Count,
    myStudents = students,
    myTeachers = teachers
};
```

View me: `<h1>@Model.SchoolName</h1>` aur `<p>Total: @Model.TotalStudents</p>`

---

## 8. Real Project Me Kaise Hoga (DB se data)

Abhi notes me data **hardcoded** hai (sirf demo ke liye). Real project me:

```csharp
public IActionResult Index()
{
    var svm = new SchoolViewModel
    {
        myStudents = _context.Students.ToList(),
        myTeachers = _context.Teachers.ToList()
    };
    return View(svm);
}
```

Yaha `_context` tumhara EF Core `DbContext` hai (jaise pichle Session wale notes me DB First approach me banaya tha).

---

## 9. Common Mistakes / Errors

| Error                                                                            | Reason                                               | Fix                                                                                  |
| -------------------------------------------------------------------------------- | ---------------------------------------------------- | ------------------------------------------------------------------------------------ |
| `The type or namespace 'SchoolViewModel' could not be found`                     | `_ViewImports.cshtml` me namespace import nahi hai   | `@using ...Models` add karo ya full name likho                                       |
| `NullReferenceException` on `Model.myStudents`                                   | ViewModel ki list initialize/assign nahi hui         | Controller me list assign karo, ya ViewModel me constructor me `new List<Student>()` |
| `The model item passed into the ViewDataDictionary is of type X, but requires Y` | Controller se alag type bheja, view me alag `@model` | Dono jagah same ViewModel type rakho                                                 |
| Do `@model` likhne pe error                                                      | Ek view me ek hi `@model` allowed                    | ViewModel use karo                                                                   |

**Tip (null se bachne ke liye):**

```csharp
public class SchoolViewModel
{
    public List<Student> myStudents { get; set; } = new List<Student>();
    public List<Teacher> myTeachers { get; set; } = new List<Teacher>();
}
```

---

## 10. Interview Questions (Quick Revision)

**Q1. Ek view me multiple models kaise bind karte hain?**
ViewModel banate hain, jisme saare models ki properties hoti hain, aur wahi ek ViewModel `@model` se view me bind karte hain.

**Q2. ViewModel kya hota hai?**
Ek class jo view ki zarurat ke hisaab se ek ya zyada models/data combine karke view ko deti hai.

**Q3. Kya ek view me do `@model` directive likh sakte hain?**
Nahi, ek view me sirf ek `@model` allowed hai.

**Q4. ViewModel aur Domain Model (Entity) me kya difference hai?**
Domain/Entity model DB table ko represent karta hai. ViewModel UI ki zarurat ke hisaab se banta hai, aur isme multiple entities ya extra fields ho sakte hain.

**Q5. ViewModel ke alawa aur kaunse tarike hain?**
ViewBag/ViewData, Tuple, Partial Views / View Components. Best practice ViewModel hai (strongly typed).

**Q6. ViewBag se multiple models bhejne me kya problem hai?**
Dynamic hai, type safety nahi, compile time error nahi milte, runtime pe fail hota hai.

**Q7. `_ViewImports.cshtml` ka kya kaam hai?**
Common `@using`, `@addTagHelper` directives ek jagah rakhta hai, taaki har view me baar baar likhna na pade.

**Q8. ViewModel me kab List aur kab single object rakhte hain?**
Multiple records (table) ke liye `List<T>`, ek record (detail page) ke liye single `T`.

---

## 11. One-Page Cheat Sheet

```csharp
// ---------- Models ----------
public class SchoolViewModel
{
    public List<Student> myStudents { get; set; } = new();
    public List<Teacher> myTeachers { get; set; } = new();
}

// ---------- Controller ----------
public IActionResult Index()
{
    var svm = new SchoolViewModel
    {
        myStudents = students,
        myTeachers = teachers
    };
    return View(svm);          // ek hi object
}
```

```html
<!-- ---------- View ---------- -->
@model SchoolViewModel @foreach (var s in Model.myStudents) {
<tr>
  <td>@s.Name</td>
</tr>
} @foreach (var t in Model.myTeachers) {
<tr>
  <td>@t.Name</td>
</tr>
}
```

**Yaad rakhne wali line:**

> **Multiple models + single view = ViewModel.** Ek 3rd class banao, usme sab models ki properties daalo, aur wahi ek ViewModel view ko do.

## ----------------------------------------------------------

## Partial View in ASP.NET Core 6 ....

partial view jb bhi banayege apne to \_ se usss file ko save krke rkhege

and usss file ko apne Shared folder ke ander create krke rkheeg

Partial view ko apne kissi main view ko ke saath attach krna padega then apne usko used kr skte hi wrna yese partail view ko run hi nhi kr skte hi h...

ek view ke ander apne multiple partail view ko add on kr skte hi ...

jo common html ka markup hota hi usko used krne ke liye apne partial view ka used krte hi ...

Partail view allow you to reuse common markups across the different view of the application..

Partial view ko sperately run nhi kr skte hi apne ko usko kisssi ke nder to inject krke used krna padega.........

apne isko

<partial name = "_partialViewFileName"/>

to ye automatic shared folder ke ander jyega ur partial View wli file ko run kr dega ...

Partial View With Models(Strongly typed Partial View)

me phele single products ko represent krne ke liye ek partail view bana rha hu...

\_ProductsPartialView.cshtml
ek layout bana dunga ur baaar baar saare project ko me display krwa dunga....

View model ke saath bind rhega to isliye me isko bol rha hu strongly type viewed.

Products.cshtml

rhega iske

ProductPartial ko call

ek baar dusre product

Partial View ek hi rhega bus data change hote rhega

## ------------------------------------------------------------------------

# Introduction To ASP.NET Core WebApi (RestFul APIs)

1. Web API kya hota hai?

Web API ek aisi service hai jo data return karti hai (HTML page nahi). Isse bahut saare clients interact kar sakte hain:

Mobile apps (Android/iOS)
Desktop apps
Browsers (React, Angular, JS)
IoT devices
Doosre servers (server-to-server)

Core idea: Ek baar API banao, har jagah use karo. Business logic aur database ek hi jagah rehta hai, aur sab clients usi se data lete hain.

Mobile App ─┐
Desktop App ─┤
Browser ─┼──► Web API ──► Database
IoT Device ─┤
Other Server─┘ 2. Data Format: JSON vs XML

Web API data usually in do formats me bhejta hai:

Feature JSON XML
Full form JavaScript Object Notation Extensible Markup Language
Size Light weight Heavy (tags zyada)
Readability Easy Thoda verbose
Usage today Default (90%+) Legacy/enterprise systems

JSON example:

json
{
"id": 1,
"name": "Sourabh",
"city": "Indore"
}

XML example:

xml
<Student>
<Id>1</Id>
<Name>Sourabh</Name>
<City>Indore</City>
</Student>

ASP.NET Core me default JSON hi hota hai. XML chahiye to formatter add karna padta hai.

3. HTTP Protocol aur HTTP Verbs

Client ko data tabhi milega jab wo HTTP rules follow kare. Un rules ke andar HTTP Verbs (Methods) aate hain:

Verb Kaam CRUD Example
GET Data padhna Read GET /api/students
POST Naya data banana Create POST /api/students
PUT Poora data update karna Update PUT /api/students/1
PATCH Partial update Update PATCH /api/students/1
DELETE Data delete karna Delete DELETE /api/students/1

Har response ke saath Status Code bhi aata hai:

Code Matlab
200 OK Success
201 Created Naya resource ban gaya
204 No Content Success, par return karne ko kuch nahi
400 Bad Request Client ne galat data bheja
401 Unauthorized Login nahi hai
403 Forbidden Login hai par permission nahi
404 Not Found Resource nahi mila
500 Internal Server Error Server me bug 4. RESTful API kya hai?

REST = Representational State Transfer.

Ye ek architectural style (guidelines ka set) hai. Ye koi technology ya library nahi hai. Jab hum Web API ko in rules ke hisaab se banate hain, to use RESTful API bolte hain.

Web API aur REST ka relation:

Web API = jo cheez hum banate hain (implementation)
REST = jis style/rules se banate hain (guidelines)
REST ke 6 main principles
Client-Server separation: Frontend aur backend alag alag hote hain.
Stateless: Server pichli request yaad nahi rakhta. Har request me poori info honi chahiye (jaise JWT token).
Uniform Interface: Resource ke liye consistent URL aur HTTP verbs.
Cacheable: Responses cache ho sakte hain (performance ke liye).
Layered System: Beech me load balancer, proxy, gateway ho sakta hai.
Resources identified by URL: Har cheez ek resource hai.
RESTful URL design

❌ Galat (verb URL me):

GET /api/getAllStudents
POST /api/createStudent
POST /api/deleteStudent

✅ Sahi (noun URL me, verb HTTP method me):

GET /api/students → sab students
GET /api/students/5 → ek student
POST /api/students → naya student
PUT /api/students/5 → update
DELETE /api/students/5 → delete 5. MVC vs Web API: Difference kya hai?

Ye sabse important confusion hai.

MVC kya hai?

Model-View-Controller. Isme server HTML page (View) banakar bhejta hai.

Browser ──► Controller ──► Model (DB) ──► View (HTML) ──► Browser
Web API kya hai?

Isme View hota hi nahi. Sirf data (JSON/XML) return hota hai.

Any Client ──► Controller ──► Model (DB) ──► JSON ──► Client
Comparison table
Point MVC Web API
Return karta hai HTML View JSON / XML data
View Hota hai (Razor) Nahi hota
Clients Sirf browser Browser, mobile, IoT, desktop, sab
Use case Website banana Service/backend banana
Controller base Controller ControllerBase
Attribute Optional [ApiController]
Content negotiation Kam use Core feature
ASP.NET Core me dono ek jaise hi hain

Purane ASP.NET Framework me MVC aur Web API alag alag frameworks the. ASP.NET Core me dono merge ho gaye hain aur same pipeline use karte hain. Farak bas ye hai ki controller kya return kar raha hai.

6.  Code Example
    csharp
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
    private static List<Student> students = new()
    {
    new Student { Id = 1, Name = "Sourabh", City = "Indore" },
    new Student { Id = 2, Name = "Rahul", City = "Bhopal" }
    };

        // GET: api/students
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(students);                    // 200 + JSON
        }

        // GET: api/students/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound(); // 404
            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public IActionResult Create(Student student)
        {
            students.Add(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student); // 201
        }

        // PUT: api/students/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Student updated)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            student.Name = updated.Name;
            student.City = updated.City;
            return NoContent();                     // 204
        }

        // DELETE: api/students/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            students.Remove(student);
            return NoContent();
        }

    }

Important cheezein:

[ApiController]: automatic model validation, binding, aur 400 responses deta hai
[Route("api/[controller]")]: URL api/students ban jata hai
ControllerBase: View support ke bina lightweight base class
Ok(), NotFound(), CreatedAtAction(): ye sab status code ke saath response bhejte hain 7. Web API kyun use karte hain?
Platform independent: .NET API ko Java, Python, JS, Swift, koi bhi client call kar sakta hai.
Multiple clients, ek backend: Code duplicate nahi hota.
Frontend/Backend separation: Teams alag kaam kar sakti hain (React team aur .NET team).
Scalability: Backend ko alag scale kar sakte ho (Docker, load balancer, cloud).
Third-party integration: Payment gateway, SMS, maps, sab APIs se hi judte hain.
Microservices: Bade system ko chhote chhote API services me tod sakte ho.
Security centralized: Auth (JWT), validation, rate limiting ek hi jagah. 8. Request ka poora flow

1. Client (Mobile) ──► GET https://myapp.com/api/students/1
   Headers: Authorization: Bearer <JWT>
2. Middleware pipeline (HTTPS, Auth, CORS, Routing)
3. Controller action execute
4. Service/Repository ──► Database query
5. Data milta hai ──► C# object
6. JSON me serialize hota hai
7. Response: 200 OK + { "id":1, "name":"Sourabh" }

Agar apne ko alg client tk data ko pauchna hi to apne webapi ka used krege alg alg client mtlb

agar kissi ko data desktop pr chiye ya mobile papplication ppr chiye to apne waha pr webapi ka concept used krenge....

WebApi is an application programming interface (API) that is used to enable communication or interaction with software componenets with each other.

agar maan loi apne webapi ke through aadil ka data insert kiya hi

to whi desktop appplication wle ke pass bhi chiye ur mobile applications wle client ke pass bhi aadil ka data jana chiye ....

Webapi is the enhanced form of the web applications.

Webapi is a set of endpoints that alllow different software applications to communicate with each other over the internet.

ASP.net core webapi is a framework developed by microsoft that allows you to build and create RESTful APIs using ASP.NET Core Platform.

It is similar to asp.net mvc since it contains the mvc features

Webapi me koi view nhi rehta hi bus apne usko hit krte hi and wo apne kucch data return krke deta hi ....

REST architectural Pattern specifies a set of constraints that a system adhere to .

1. Uniform Interface
2. Client Server
3. Stateless
4. Cacheable

# Create & Understand ASP.NET Core Web API Project Template....
 