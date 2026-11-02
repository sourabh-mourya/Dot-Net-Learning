# EF Core Code-First — 7 Simple Steps (ASP.NET Core MVC + MySQL)

```
1. Packages install  →  2. Model class  →  3. DbContext class  →  4. Program.cs mein register
        →  5. Migration add  →  6. Database update  →  7. Controller mein use
```

---

## Step 1: Packages install karo

Pehle ek baar `dotnet-ef` tool install karo (global):

```bash
dotnet tool install --global dotnet-ef
```

Phir project folder mein (jahan `.csproj` hai):

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

> ⚠️ Saare `Microsoft.EntityFrameworkCore.*` packages ka **same major version** (jaise 9.0.x) rakho, warna `NU1605` / `Method not found` error aayega.
> SQL Server use karna ho toh `Pomelo...` ki jagah `Microsoft.EntityFrameworkCore.SqlServer` lagao.

---

## Step 2: Model class banao

`Models/Student.cs`

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student
{
    [Key]                                   // Primary Key banega
    public int Id { get; set; }

    [Column("Student Name", TypeName = "varchar(100)")]   // Table mein column ka naam + type
    public string Name { get; set; }
}
```

---

## Step 3: DbContext class banao

`Models/StudentDBContext.cs`

```csharp
using Microsoft.EntityFrameworkCore;

public class StudentDBContext : DbContext
{
    // options ko parent DbContext ko pass karna zaroori hai
    public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options)
    {
    }

    // Isi se "Students" table banegi
    public DbSet<Student> Students { get; set; }
}
```

---

## Step 4: Connection string likho aur Program.cs mein register karo

**`appsettings.json`**

```json
"ConnectionStrings": {
  "dbcs": "Server=localhost;Port=3306;Database=CodeFirstApproach;User=root;Password=yourpassword;"
}
```

**`Program.cs`** (`builder.Build()` se **pehle**)

```csharp
using Microsoft.EntityFrameworkCore;

var connectionString = builder.Configuration.GetConnectionString("dbcs");

builder.Services.AddDbContext<StudentDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
```

> `"dbcs"` ka naam dono jagah **exactly same** hona chahiye.
> MySQL server **chalu** hona chahiye, kyunki `AutoDetect` DB se connect hota hai.

---

## Step 5: Migration add karo

```bash
dotnet ef migrations add InitialCreate
```

Isse `Migrations/` folder ban jata hai (table banane ka C# code).

> Migration ke naam mein hyphen (`-`) mat rakho.

---

## Step 6: Database update karo

```bash
dotnet ef database update
```

Ab MySQL mein `CodeFirstApproach` database aur `Students` table ban jayegi.

> Baad mein naya column chahiye: model mein property add karo → `dotnet ef migrations add AddEmail` → `dotnet ef database update`.

---

## Step 7: Controller mein use karo

```csharp
public class HomeController : Controller
{
    private readonly StudentDBContext studentDB;

    // Constructor Injection: DbContext khud inject ho jata hai
    public HomeController(StudentDBContext studentDB)
    {
        this.studentDB = studentDB;
    }

    // READ: saare students
    public IActionResult Index()
    {
        var students = studentDB.Students.ToList();
        return View(students);
    }

    // CREATE
    [HttpPost]
    public IActionResult Create(Student student)
    {
        studentDB.Students.Add(student);
        studentDB.SaveChanges();            // SaveChanges ke bina DB mein save nahi hoga
        return RedirectToAction("Index");
    }
}
```

**Baaki operations:**

```csharp
var one = studentDB.Students.Find(1);                          // ek record (id se)

var s = studentDB.Students.Find(1);                            // UPDATE
s.Name = "New Name";
studentDB.SaveChanges();

var d = studentDB.Students.Find(1);                            // DELETE
studentDB.Students.Remove(d);
studentDB.SaveChanges();
```

---

## Quick Checklist

- [ ] `dotnet-ef` tool installed
- [ ] EF packages same version par
- [ ] Model class (`[Key]` ke saath)
- [ ] DbContext class (`DbSet<Student>` ke saath)
- [ ] Connection string + `AddDbContext` in `Program.cs`
- [ ] `dotnet ef migrations add InitialCreate`
- [ ] `dotnet ef database update`
- [ ] Controller mein constructor injection + `SaveChanges()`