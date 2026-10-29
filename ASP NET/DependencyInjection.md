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
1. Transient — Har injection par naya object
Jab bhi DI container service ko resolve karta hai, ek naya object create hota hai.

Maan lo ek hi request mein tumne IService1 ko 3 baar inject/resolve kiya:
- First injection → Object A
- Second injection → Object B
- Third injection → Object C
Next request mein bhi har resolution par naye objects banenge.

Yaad rakho: Har resolution = new instance.
2. Scoped — Ek HTTP request mein ek hi object
Ek HTTP request ke andar tum IService1 ko kitni bhi baar inject/resolve karo, DI container wahi instance reuse karta hai.
- Request 1: First → Object A, Second → Object A, Third → Object A
- Request 2: First → Object B, Second → Object B
Request 2 mein naya object isliye bana kyunki woh ek alag HTTP request hai.

Yaad rakho: One HTTP request = one instance per registered service.
3. Singleton — Puri application mein ek hi object
Application mein service ka instance pehli baar resolve hone par create hota hai. Uske baad sabhi requests mein wahi instance reuse hota hai.
- Request 1 → Object A
- Request 2 → Object A
- Request 3 → Object A
Chahe same request ho ya different request, instance wahi rahega. Application restart hone par naya instance banega.

Yaad rakho: One application lifetime = one instance per registration.