# Entity Framework Core — ORM & Code-First Approach (ASP.NET Core MVC)

> Hinglish notes, interview-oriented. Node/Express + MongoDB (Mongoose) ke analogies saath mein diye hain taaki concept jaldi clear ho.

---

## 1. ORM kya hota hai?

### Definition

> To address the data access requirements of ASP.NET Core MVC application, you can use an **ORM (Object Relational Mapping)** framework.

Yahan ORM ko **framework** bola ja raha hai.

### Data access matlab?

**Data access = apne wahi database operations** (insert, select, update, delete). ORM is poore process ko **simplify** kar deta hai.

> **ORM is a tool for storing data from domain objects to relational database.**

| Term                    | Matlab                                           |
| ----------------------- | ------------------------------------------------ |
| **Domain objects**      | Apni **model classes** (Student, Employee, etc.) |
| **Relational database** | **SQL Server / MySQL** jaisa database            |

### Problem jo ORM solve karta hai

Maan lo application mein multiple objects hain aur unhe database mein store karwana hai.

- Hum apni application ke **objects ko direct database mein store nahi kara sakte** (C# object aur DB table ka format alag hota hai).
- Beech mein ek **framework** use karna padta hai, jiska naam hai **ORM**.

```
C# Object (Student)  ⇄  ORM  ⇄  Database Table (Students)
```

### ORM sirf store nahi, retrieve bhi karta hai

Kabhi-kabhi application mein database se objects **get** bhi karwane padte hain. Wahan bhi ORM hi kaam aata hai.

### ORM kya-kya karta hai?

**Case 1: Classes hain, table nahi** (Code-First)

Apne ko sirf **classes** banani hain. ORM un classes ko dekh kar database mein **table generate** kar deta hai.

**Case 2: Table pehle se bani hui hai, classes nahi** (Database-First)

Database pehle se hai lekin model classes nahi hain. ORM **database ko dekh kar corresponding classes generate** kar deta hai.

> Ye dono kaam ORM **automatic** kar deta hai.

### Analogy

Maan lo tum **Mongoose** use karte ho Node.js mein:

- JS object (`{ name: "Sourabh" }`) seedha MongoDB mein nahi jata, beech mein Mongoose schema/model hota hai.
- Wahi kaam ASP.NET Core mein **ORM (EF Core)** karta hai, bas relational DB (SQL Server/MySQL) ke liye.

### ASP.NET Core mein kaun sa ORM?

ASP.NET Core mein jo ORM use hota hai uska naam hai **Entity Framework Core (EF Core)**.

---

## 2. Entity Framework Core kya hai?

> **Entity Framework Core is the new version of Entity Framework after EF6, but it is redesigned.**

### Features

| Feature            | Matlab                                   |
| ------------------ | ---------------------------------------- |
| **Open source**    | Source code public hai (GitHub par)      |
| **Lightweight**    | Halka aur fast                           |
| **Extensible**     | Customize/extend kar sakte ho            |
| **Cross-platform** | Windows, Linux, macOS sab par chalta hai |

### Important points

- Entity Framework Core ek **data access technology** hai.
- EF Core koi **tool nahi** hai, balki ek **Object/Relational Mapping (ORM) framework** hai.
- Ye **ADO.NET ka enhancement** hai, jo developer ko database mein data **access aur store** karne ka **automated mechanism** deta hai.

### ADO.NET vs EF Core (interview mein useful)

|             | ADO.NET                    | EF Core                             |
| ----------- | -------------------------- | ----------------------------------- |
| Level       | Low-level                  | High-level (ORM)                    |
| SQL queries | Manually likhni padti hain | LINQ se, SQL khud generate hota hai |
| Mapping     | Manual (reader → object)   | Automatic                           |
| Code        | Zyada boilerplate          | Kam code                            |

```csharp
// ADO.NET (manual)
var cmd = new SqlCommand("SELECT * FROM Students", conn);
var reader = cmd.ExecuteReader();
while (reader.Read()) { ... }

// EF Core (automatic)
var students = studentDB.Students.ToList();
```

### Database Providers

Bahut saare databases hote hain (SQL Server, MySQL, PostgreSQL, SQLite...). EF Core mein **Database Provider** hota hai, jo decide karta hai ki kaun sa database use ho raha hai. Phir us database ke saath data **get, add, update, delete** wagairah kiya jata hai.

| Database   | Provider Package                          |
| ---------- | ----------------------------------------- |
| SQL Server | `Microsoft.EntityFrameworkCore.SqlServer` |
| MySQL      | `Pomelo.EntityFrameworkCore.MySql`        |
| PostgreSQL | `Npgsql.EntityFrameworkCore.PostgreSQL`   |
| SQLite     | `Microsoft.EntityFrameworkCore.Sqlite`    |

---

## 3. EF Core ke 2 Approaches

EF Core development **2 approaches** support karta hai:

1. **Code-First**
2. **Database-First**

|                            | Code-First                                       | Database-First                 |
| -------------------------- | ------------------------------------------------ | ------------------------------ |
| Pehle kya banta hai        | **Model classes**                                | **Database tables**            |
| Phir kya generate hota hai | Classes se **tables**                            | Tables se **classes**          |
| Use case                   | Naya project (ASP.NET Core MVC mein most common) | Existing database ho           |
| Command                    | `dotnet ef migrations add` + `database update`   | `dotnet ef dbcontext scaffold` |

Is note mein hum **Code-First** padh rahe hain.

---

## 4. Code-First Approach kya hai?

Code-First approach mein:

- Hum **model classes khud likhte hain** (jaise `Employee`, `Student`).
- EF Core un **model classes ko dekh kar database mein tables generate** karta hai.
- Humein khud **database ya table banane ki zaroorat nahi**. Code-First khud kar deta hai.

> **In code-first approach the Entity Framework Core creates database objects based on model classes that you create to represent application data.**

**Database objects** ka matlab yahan **tables** hai.

### Key points

- Ye ASP.NET Core MVC mein **sabse common** approach hai.
- Tum apni application **model classes aur properties code karke** develop karte ho, aur database objects banane ka kaam **EF Core ko delegate** kar dete ho.
- Tum **apne custom classes** banakar apna model define karte ho, phir un models ke base par database create hota hai.

> Bas model classes create karni hain, baaki table creating ka kaam Entity Framework automatic kar dega.

### Code-First mein humein khud kya-kya likhna padta hai?

Code-First approach mein ye **dono cheezein khud likhni padti hain**:

1. **Model class** (jaise `Student`)
2. **DbContext class** (jaise `StudentDBContext`), jisme `DbSet<Student>` define hota hai

Phir **migration** banti hai, jisme simple C# code ke form mein likha hota hai ki database mein kis type se table banegi. Us migration ko run karne par table create ho jati hai.

---

## 5. Code-First ka overall flow

```
1. Model class banao              (Student.cs)
        │
        ▼
2. DbContext class banao          (StudentDBContext.cs)
   └── isme DbSet<Student> define karo
        │
        ▼
3. Connection String likho        (appsettings.json)
        │
        ▼
4. Program.cs mein register karo  (AddDbContext)
        │
        ▼
5. Migration banao                (dotnet ef migrations add ...)
        │
        ▼
6. Migration run karo             (dotnet ef database update)
        │
        ▼
   Database mein Table ban gayi ✅
```

### Migration kya hota hai (simple language)

Migration ek **code file** hoti hai jisme simple C# code ke form mein likha hota hai ki **database mein kis structure/type se table banegi**. Phir hum us migration ko **run** karte hain, aur database mein table create ho jati hai.

---

## 6. Step 1: Packages install karna

ASP.NET Core MVC application mein **3 packages** install karne padte hain:

| Package                                   | Kaam                                                                                                           |
| ----------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| `Microsoft.EntityFrameworkCore.SqlServer` | **Database provider.** SQL Server use kar rahe hain isliye ye likha hai                                        |
| `Microsoft.EntityFrameworkCore.Tools`     | Migrations manage karne aur DbContext **scaffold** karne ke liye (**Scaffold = code ka automatic generation**) |
| `Microsoft.EntityFrameworkCore.Design`    | EF Core ka saara **design-time logic** isme hota hai                                                           |

### ⚠️ Provider package database ke hisaab se badlega

Notes mein SQL Server use hua hai. Agar **MySQL** use kar rahe ho (jaise tumhare project mein), toh `SqlServer` ki jagah **Pomelo** package lagta hai:

```bash
# SQL Server ke liye
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

# MySQL ke liye
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### ⚠️ Version rule (bahut important)

Saare `Microsoft.EntityFrameworkCore.*` packages ka **same major version** hona chahiye (jaise sab 9.0.x). Mismatch hone par:

- `NU1605` (package downgrade) error aata hai, ya
- `Method not found` error aata hai migration chalate waqt.

Aur **unused provider package** (jaise MySQL use karte hue `SqlServer`) install mat rakho, wo apna version conflict la sakta hai.

### `dotnet-ef` global tool (alag se)

`dotnet ef` command packages ke saath nahi aata. Ye ek **alag global tool** hai:

```bash
dotnet tool install --global dotnet-ef
dotnet ef --version
```

---

## 7. Step 2: Model class banana

Pehle model class banate hain (jaise `Student`):

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student
{
    [Key]   // Database jab table create karega toh isko Primary Key bana dega
    public int Id { get; set; }

    [Column("Student Name", TypeName = "varchar(100)")]
    // Jab database mein table generate hogi toh column ka naam "Name" nahi hoga,
    // balki "Student Name" hoga, aur type varchar(100) hoga
    public string Name { get; set; }
}
```

### Attributes samjho

| Attribute         | Kaam                                                         | Namespace                                      |
| ----------------- | ------------------------------------------------------------ | ---------------------------------------------- |
| `[Key]`           | Property ko **Primary Key** banata hai                       | `System.ComponentModel.DataAnnotations`        |
| `[Column("...")]` | Table mein column ka **naam** (aur type) customize karta hai | `System.ComponentModel.DataAnnotations.Schema` |

### `[Column]` ke parameters

```csharp
[Column("Student Name", TypeName = "varchar(100)")]
         ▲                  ▲
         │                  └── Database column ka data type
         └── Database column ka naam
```

### Dhyan dene wali baatein

- Agar `[Key]` na bhi lagao, toh EF Core naming convention se `Id` ya `StudentId` naam ki property ko **khud primary key** maan leta hai.
- `int Id` primary key ho toh by default **auto-increment (identity)** hoti hai.
- Agar `[Column]` na lagao, toh column ka naam **property ke naam** jaisa hi hoga (`Name`).
- Column ke naam mein space rakhna possible hai, lekin practically avoid karna better hai (queries mein dikkat aati hai).

> Model class create karne ke baad **saath mein ek DbContext class bhi create karni padti hai.** Agle section mein wahi hai.

---

## 8. DbContext class kya hoti hai?

### Definition

> **The DbContext class is an integral part of Entity Framework.**

DbContext wahi class hai jo hum **application ke andar use karenge**, aur wahi class **database ke saath interact** bhi karegi.

> It is this class that manages the database connection and is used to retrieve and save data in the database.

### DbContext ki bade level ki zimmedariyan

DbContext class ki **bahut badi zimmedari** hai:

1. **Database se connection establish** karna
2. Database se **data retrieve** karna
3. Database mein **data save** karna

(Isse detail mein agle sections mein dekhenge: section 10.)

### Important points

| Point                         | Matlab                                                                                     |
| ----------------------------- | ------------------------------------------------------------------------------------------ |
| **Session**                   | DbContext ka jo object banate hain wo **database ke saath ek session** represent karta hai |
| **Unit of Work + Repository** | DbContext in dono patterns ka **combination** hai                                          |
| **Coordinator**               | DbContext, EF Core ke saath co-ordinate karke tumhe data **query aur save** karne deta hai |
| **DbSet**                     | DbContext, `DbSet<ModelName>` type ka use karke properties define karta hai                |
| **Model ke baad**             | Model class create karne ke baad DbContext class define karte hain                         |

### Unit of Work aur Repository (interview concept)

| Pattern          | Matlab                                                                          |
| ---------------- | ------------------------------------------------------------------------------- |
| **Repository**   | Data access ko abstract karna (`DbSet<Student>` = Students table ka repository) |
| **Unit of Work** | Multiple changes ko track karke **ek saath** save karna (`SaveChanges()`)       |

```csharp
studentDB.Students.Add(s1);
studentDB.Students.Add(s2);
studentDB.SaveChanges();   // dono ek saath DB mein jayenge (Unit of Work)
```

### StudentDBContext ka code

```csharp
using Microsoft.EntityFrameworkCore;

public class StudentDBContext : DbContext
{
    // Constructor: options ko parent (DbContext) class ko pass kar dete hain
    public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options)
    {
    }

    // DbSet<Student> = Student model ke liye table.
    // Isi property se Students table generate hoti hai
    public DbSet<Student> Students { get; set; }
}
```

### Code ko line-by-line samjho

**1. `: DbContext`**

Hum `DbContext` class ko **inherit** karte hain, taaki DbContext ki **saari properties aur methods child class (`StudentDBContext`) ke paas aa jayein**.

**2. `DbSet<Student> Students`**

- `DbSet<Student>` ka matlab: **Student type ki table**.
- Is `DbSet` se jo class banayi (Student), wo database mein **table generate** karti hai.
- Table ka naam by default **property ka naam** hota hai (`Students`).

**3. Constructor aur `base(options)`**

Constructor ke ek parameter `options` aata hai. Wahi `options` ko `base(options)` mein **wapas parent `DbContext` ko pass** kar dete hain (matlab parent constructor call ho jata hai). Iska detail agle section mein hai.

---

## 9. DbContext Constructor aur DbContextOptions (step-by-step)

### 9.1 Constructor ko samjho

```csharp
public StudentDBContext(DbContextOptions options) : base(options)
{

}
```

Isko step-by-step samjho 👇

**Step 1: `DbContextOptions options`**

- `DbContextOptions` ek **configuration object** hai.
- Iske andar **database provider** (SQL Server, MySQL etc.), **connection string** aur **other configurations** ki information hoti hai.
- Ye configuration **Dependency Injection** ke through constructor mein aati hai.

> For the DbContext class to be able to do any useful work, it needs an instance of **DbContextOptions** class.

**Benefit kya hai?** `DbContextOptions` ka instance wo configuration information **carry** karta hai, jaise connection string, database provider jo use karna hai, etc.

**Step 2: `: base(options)`**

Yahan `base` ka matlab **parent class** hai.

Tumhari `StudentDBContext` class `DbContext` se inherit ho rahi hai:

```csharp
public class StudentDBContext : DbContext
```

Isliye `: base(options)` ka matlab hai: **jo options mile hain, unhe parent `DbContext` class ke constructor ko pass kar do.**

EF Core internally in options ka use karke database ke saath interaction configure karta hai.

### 9.2 Recommended constructor (generic version)

Generic `DbContextOptions` ki jagah **strongly typed `DbContextOptions<StudentDBContext>`** recommend kiya jata hai:

```csharp
public StudentDBContext(
    DbContextOptions<StudentDBContext> options
) : base(options)
{

}
```

**Iska benefit:** options **specifically `StudentDBContext` ke liye** configure hote hain.

> ⚠️ Jab ek se zyada DbContext hote hain (section 12), tab generic version zaroori ho jata hai, warna DI container ko samajh nahi aata ki kaun si DbContext ko kaun se options dene hain.

### 9.3 Kya constructor ke andar additional code likh sakte hain?

**Yes**, constructor ke andar additional initialization code likh sakte ho:

```csharp
public StudentDBContext(
    DbContextOptions<StudentDBContext> options
) : base(options)
{
    // Additional initialization if required
}
```

Lekin **normally EF Core configuration constructor ke andar nahi karte.** Configuration `Program.cs` mein hoti hai.

### 9.4 Database configuration kahan karte hain?

Generally **`Program.cs`** mein:

```csharp

var connectionString = builder.Configuration.GetConnectionString("dbcs");

builder.Services.AddDbContext<StudentDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

```

Aur **`appsettings.json`** mein:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> 💡 Ye SQL Server ka example hai. MySQL ke liye `UseMySql(...)` aur MySQL wali connection string section 13 aur 14 mein hai.

### 9.5 Complete flow samjho

```
Program.cs
   │   Database provider + Connection String configure
   ▼
Dependency Injection
   │
   ▼
StudentDBContext Constructor
   │   DbContextOptions receive karta hai
   ▼
base(options)
   │
   ▼
DbContext (Parent Class)
   │   EF Core database configuration handle karta hai
   ▼
DbSet<Student> Students
       Students table ke through database operations
```

Yani `Program.cs` mein jo configuration (connection string + provider) set karte hain, wo `DbContextOptions` ke through `DbContext` tak pahunchti hai.

### 9.6 Interview-ready answer

> _"We inherit our DbContext class from Entity Framework Core's DbContext class to interact with the database. Its constructor accepts DbContextOptions through dependency injection and passes them to the base DbContext constructor using base(options). This allows EF Core to configure the database provider and connection."_

### 9.7 Important

Tumhare Code-First approach mein `DbSet<Student> Students` entity ko **database table ke saath map** karne ke liye use hota hai. **Migration** ke through EF Core database schema **create/update** karta hai.

---

## 10. DbContext banate hi kyun hain? (Main kaam aur reasons)

DbContext EF Core ke **most important concepts** mein se ek hai. Isko banane ka main reason **sirf database se connect karna nahi hai**. Ye application aur database ke beech ek **bridge + Unit of Work** ki tarah kaam karta hai.

### 10.1 DbContext ki zarurat kyun padti hai?

Suppose tum ek **Student Management System** bana rahe ho. Database mein multiple tables hain:

- Students
- Teachers
- Courses
- Departments
- Employees

Ab application ko in tables par **CRUD operations** perform karne hain.

Agar EF Core mein DbContext nahi hota, toh tumhe **database connection, SQL queries, commands aur data mapping** jaise kaafi operations **manually handle** karne padte.

👉 DbContext ye saare operations manage karne ke liye **ek central entry point** provide karta hai.

### 10.2 DbContext ke main responsibilities

#### 1. Database ke saath interaction

```csharp
public class StudentDBContext : DbContext
{
    public StudentDBContext(
        DbContextOptions<StudentDBContext> options
    ) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
}
```

Ab tum database se students fetch kar sakte ho:

```csharp
var students = studentDB.Students.ToList();
```

EF Core internally required SQL query execute karta hai. Approximately generated SQL:

```sql
SELECT * FROM Students;
```

#### 2. Database tables ko represent karna

`DbSet<T>` ke through tum apne **Models ko database tables ke saath map** karte ho:

```csharp
public DbSet<Student> Students { get; set; }

public DbSet<Teacher> Teachers { get; set; }

public DbSet<Course> Courses { get; set; }
```

Yahan **ek DbContext multiple entities ko manage** kar raha hai.

#### 3. CRUD Operations perform karna

**Create**

```csharp
Student student = new Student()
{
    Name = "Rahul",
    Age = 20
};

studentDB.Students.Add(student);

studentDB.SaveChanges();
```

**Read**

```csharp
var students = studentDB.Students.ToList();
```

**Update**

```csharp
var student = studentDB.Students.Find(1);

if (student != null)
{
    student.Name = "Amit";
    studentDB.SaveChanges();
}
```

**Delete**

```csharp
var student = studentDB.Students.Find(1);

if (student != null)
{
    studentDB.Students.Remove(student);
    studentDB.SaveChanges();
}
```

#### 4. Change Tracking ⭐

Ye DbContext ki **bahut important responsibility** hai.

Maan lo tumne database se ek student fetch kiya:

```csharp
var student = studentDB.Students.Find(1);
```

Ab tumne uska name change kar diya:

```csharp
student.Name = "Rahul Sharma";
```

**Tumne manually `UPDATE` SQL query nahi likhi.** Aisa kyun?

Because EF Core ka **Change Tracker** track karta hai ki entity ki **original state** kya thi aur ab uski **current state** kya hai.

Jab tum:

```csharp
studentDB.SaveChanges();
```

execute karte ho, EF Core **detected changes ke according SQL UPDATE query** execute karta hai.

**Change Tracking Flow:**

```
Database se Student Fetch (Name = Amit)
        │
        ▼
Change Tracker: Original value track karta hai
        │
        ▼
Name Change: Amit → Rahul Sharma
        │
        ▼
SaveChanges()
        │
        ▼
UPDATE SQL execute hoti hai
```

> 💡 Agar sirf data **read** karna hai (update nahi), toh `AsNoTracking()` use kar sakte ho taaki change tracking ka overhead na lage:
> `var students = studentDB.Students.AsNoTracking().ToList();`

#### 5. `SaveChanges()` aur Transaction Management

DbContext changes ko database mein save karne ka mechanism provide karta hai:

```csharp
studentDB.Students.Add(student);
studentDB.Teachers.Add(teacher);

studentDB.SaveChanges();
```

Normally, **ek `SaveChanges()` call ke andar multiple tracked changes relational database mein ek transaction ke through save hote hain.**

**Benefit:** agar save operation fail hota hai, toh transaction **rollback** ho sakti hai (aadhe changes save nahi hote).

#### 6. Relationship Management

Maan lo:

- Ek **Department** mein multiple **Students** hain.
- Ek **Student** kisi ek **Department** se belong karta hai.

Tum EF Core mein relationships define kar sakte ho:

```csharp
public class Department
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Student> Students { get; set; }
}
```

```csharp
public class Student
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int DepartmentId { get; set; }          // Foreign Key

    public Department Department { get; set; }     // Navigation property
}
```

DbContext aur EF Core in relationships ko manage karne mein help karte hain (migration mein **Foreign Key** bhi ban jati hai).

> 💡 Related data ek saath load karna ho toh `Include()` use hota hai:
> `studentDB.Students.Include(s => s.Department).ToList();`

### 10.3 Overall DbContext ka architecture

```
ASP.NET Core Application
   │   Controllers / Services / Repositories
   ▼
DbContext
   ├── Database Interaction
   ├── Change Tracking
   ├── SaveChanges()
   ├── Transactions
   ├── Relationship Mapping
   └── Entity Management
   │
   ▼
DbSet Properties
   Students | Teachers | Courses | Departments
   │
   ▼
Database
   SQL Server / MySQL / PostgreSQL
```

### 10.4 Interview-ready answer

**Question:** Why do we create DbContext in Entity Framework Core?

> _"DbContext is the primary class in Entity Framework Core that coordinates database operations. It acts as a bridge between the application and database. It provides DbSet properties to represent entities, manages change tracking, performs CRUD operations, handles SaveChanges, and manages database transactions. It also works as a Unit of Work."_

### 10.5 Production-level concept: DbContext ka Lifetime (Scoped)

DbContext ko generally **Scoped Lifetime** mein register karte hain:

```csharp
builder.Services.AddDbContext<StudentDBContext>(options =>
    options.UseSqlServer(connectionString)
);
```

`AddDbContext` by default Scoped hi register karta hai. Iska reason:

- **Har HTTP request ke liye generally ek DbContext instance.**
- Same request ke andar **injected instance reuse** hota hai.
- Request complete hone par **DI container usko dispose** kar deta hai.
- DbContext **thread-safe nahi** hota, isliye same instance ko **parallel threads mein share nahi** karna chahiye.

### 10.6 Final takeaway

> DbContext **sirf database connection establish nahi karta.** Ye EF Core ka **central component** hai jo **entity management, querying, change tracking aur database mein changes persist karne** ki responsibility handle karta hai.

---

## 11. DbSet kya hai? Kya DbSet banana compulsory hai?

Sawal: **DbContext connection ko create karta hai, toh kya jis-jis model ke saath baat karni hai uska DbSet banana bhi zaroori hai?**

Haan, lekin ek **important distinction** samjho: **DbContext banane ke liye DbSet banana compulsory nahi hai.** Dono ka purpose **alag-alag** hai.

### 11.1 DbContext ka kaam

DbContext **database ke saath interaction manage** karta hai.

```csharp
public class StudentDBContext : DbContext
{
    public StudentDBContext(
        DbContextOptions<StudentDBContext> options
    ) : base(options)
    {
    }
}
```

Itna likhne se bhi tumhara **DbContext valid** hai. Isme koi DbSet define nahi hai.

### 11.2 DbSet ka kaam

`DbSet` kisi **particular entity ke through database table ko query aur manipulate** karne ka interface provide karta hai.

```csharp
public class StudentDBContext : DbContext
{
    public StudentDBContext(
        DbContextOptions<StudentDBContext> options
    ) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }

    public DbSet<Teacher> Teachers { get; set; }
}
```

Ab tum:

```csharp
var students = studentDB.Students.ToList();

var teachers = studentDB.Teachers.ToList();
```

kar sakte ho.

### 11.3 Kya DbSet ke bina bhi database se interact kar sakte hain?

**Yes!** EF Core mein tum `DbSet` property define kiye bina bhi entities ko **`Set<T>()`** se access kar sakte ho:

```csharp
var students = studentDB.Set<Student>().ToList();
```

Yaani **explicit `DbSet` property compulsory nahi hai.**

### 11.4 ⚠️ Lekin ek important condition (Code-First mein dhyan rakhna)

`Set<T>()` tabhi kaam karta hai jab wo entity **EF Core ke model ka part ho.** Entity model mein 3 tarah se aati hai:

1. Kisi aur DbContext property mein `DbSet<T>` ke roop mein define ho.
2. Kisi doosri entity ke **relationship / navigation property** se reachable ho.
3. `OnModelCreating` mein `modelBuilder.Entity<Student>()` se explicitly register ki gayi ho.

Agar entity kisi bhi tarah model mein nahi hai, toh `Set<Student>()` call karne par **`InvalidOperationException`** aata hai (type model mein included nahi hai), aur **migration mein uska table bhi nahi banta.**

```csharp
// Bina DbSet ke entity register karna (OnModelCreating mein)
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Student>();
}
```

### 11.5 Practical rule

| Situation                            | Kya karna chahiye                                                     |
| ------------------------------------ | --------------------------------------------------------------------- |
| Normal Code-First project            | Har entity ke liye `DbSet<T>` define karo (clean aur readable)        |
| Dynamic / generic repository         | `Set<T>()` use karo                                                   |
| Entity sirf relationship se aati hai | DbSet optional hai, lekin readability ke liye define karna better hai |

### 11.6 DbContext vs DbSet vs Model vs Migration (summary)

| Cheez         | Kya represent karti hai                                   |
| ------------- | --------------------------------------------------------- |
| **DbContext** | Database session / Unit of Work                           |
| **DbSet**     | Entity collection, generally ek database table            |
| **Model**     | Database entity ka structure                              |
| **Migration** | Model changes ko database schema mein apply karne ka code |

---

## 12. 50 Models hain toh kya 50 DbContext banenge?

**Nahi!** Agar tumhare paas 50 Models hain, toh tumhe **50 DbContext classes banane ki zarurat nahi.** Generally **ek hi DbContext class mein multiple `DbSet` properties** define karte hain.

### 12.1 Suppose tumhare paas 5 Models hain

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Course
{
    public int Id { get; set; }
    public string CourseName { get; set; }
}

public class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }
}

public class Employee
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
}
```

### 12.2 Ek hi DbContext class mein sabko register karoge

```csharp
using Microsoft.EntityFrameworkCore;

namespace MyProject.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(
            DbContextOptions<ApplicationDBContext> options
        ) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
```

Isi tarah **50 models hain toh 50 `DbSet` properties** bana sakte ho.

### 12.3 Database mein kya hoga?

```
ApplicationDBContext   (Single DbContext Class)
   │
   ├── Students
   ├── Teachers
   ├── Courses
   ├── Departments
   ├── Employees
   └── ... 45 more tables
```

> **One DbContext → Multiple database tables**

### 12.4 Program.cs mein registration

```csharp
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
```

### 12.5 Production-level understanding

| Cheez         | Kaam                                                                                      |
| ------------- | ----------------------------------------------------------------------------------------- |
| **DbContext** | Database session / unit of work ko represent karta hai                                    |
| **DbSet**     | Entity collection ko represent karta hai, generally corresponding database table ke saath |
| **Model**     | Database entity ka structure define karta hai                                             |
| **Migration** | Model changes ko database schema mein apply karne mein help karti hai                     |

### 12.6 Kya multiple DbContext bhi bana sakte hain?

**Haan!** Large applications mein aisa hota hai:

| DbContext              | Kya manage karta hai       |
| ---------------------- | -------------------------- |
| `ApplicationDBContext` | Users, Students, Employees |
| `PaymentDBContext`     | Payments, Transactions     |
| `AuditDBContext`       | Audit Logs                 |

Lekin ye **requirement aur database architecture par depend** karta hai.

Multiple DbContext ke saath ye cheezein yaad rakho:

- Har DbContext ko `Program.cs` mein **alag register** karna padta hai.
- Constructor mein **generic `DbContextOptions<T>`** use karna zaroori hai.
- Migration command mein **`--context`** flag lagana padta hai, kyunki EF ko batana padta hai ki kaun si DbContext ke liye migration banani hai:

```bash
dotnet ef migrations add InitialPayment --context PaymentDBContext
dotnet ef database update --context PaymentDBContext
```

### 12.7 Yaad rakhne wali line

> **50 Models ≠ 50 DbContext.** Ek DbContext mein 50 ya usse zyada `DbSet` properties ho sakti hain.

---

## 13. Step 3: Connection String (appsettings.json)

`appsettings.json` file mein connection string likhte hain.

### ⚠️ Notes mein jo likha hai wo sahi nahi hai

```json
"ConnectionStrings": {
    "dbcs": "Server=localhost:3306;Database=CodeFirstApproach;Trusted_Connection=True"
}
```

Isme **SQL Server aur MySQL ka mix** ho gaya hai:

- `Trusted_Connection=True` → **SQL Server (Windows auth)** ka option hai
- Port `3306` → **MySQL** ka default port hai

### ✅ MySQL ke liye sahi connection string

```json
"ConnectionStrings": {
  "dbcs": "Server=localhost;Port=3306;Database=CodeFirstApproach;User=root;Password=yourpassword;"
}
```

### ✅ SQL Server ke liye sahi connection string

```json
"ConnectionStrings": {
  "dbcs": "Server=localhost;Database=CodeFirstApproach;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Dhyan dene wali baatein

- Key ka naam (`dbcs`) aur `GetConnectionString("dbcs")` mein likha naam **exactly same** hona chahiye.
- `ConnectionStrings` JSON ka **root level** object hona chahiye.
- Password ko plain text mein GitHub par push mat karo. Development mein `dotnet user-secrets` use karo.
- `Database=CodeFirstApproach` pehle se bana hona zaroori nahi, EF Core migration run hone par **khud bana deta hai**.

---

## 14. Step 4: Program.cs mein register karna

Connection string ko `Program.cs` mein **register** karte hain:

```csharp
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// 1. appsettings.json se connection string nikalo
var connectionString = builder.Configuration.GetConnectionString("dbcs");

// 2. DbContext ko DI container mein register karo (MySQL ke liye)
builder.Services.AddDbContext<StudentDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();
// ... baaki middleware
```

SQL Server ke liye:

```csharp
builder.Services.AddDbContext<StudentDBContext>(options =>
    options.UseSqlServer(connectionString));
```

### Kya hota hai yahan?

| Line                                  | Kaam                                                                                                  |
| ------------------------------------- | ----------------------------------------------------------------------------------------------------- |
| `GetConnectionString("dbcs")`         | `appsettings.json` se connection string read karta hai                                                |
| `AddDbContext<StudentDBContext>`      | DbContext ko **Dependency Injection container** mein register karta hai (default **Scoped** lifetime) |
| `UseMySql(...)` / `UseSqlServer(...)` | Batata hai ki **kaun sa database provider** use karna hai                                             |

### Dhyan dene wali baatein

- Ye code `builder.Build()` se **pehle** likhna hai.
- `BuildServiceProvider()` ka use is jagah **mat karo**. `builder.Configuration` seedha available hai.
- `ServerVersion.AutoDetect` startup/migration par DB se connect hota hai, toh **MySQL server chalu** hona chahiye. Isse bachne ke liye version hardcode kar sakte ho:

```csharp
options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));
```

---

## 15. Step 5: Migration add karna aur run karna

Migration run karne ke baad database ke andar **tables generate** ho jati hain.

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

| Command                                  | Kaam                                                                          |
| ---------------------------------------- | ----------------------------------------------------------------------------- |
| `dotnet ef migrations add InitialCreate` | Model classes ko dekh kar **migration file** (C# code) generate karta hai     |
| `dotnet ef database update`              | Migration ko **run** karke database mein tables **actually create** karta hai |

### Is process mein kya-kya banta hai?

```
Project
└── Migrations/
    ├── 20261102xxxxxx_InitialCreate.cs            ← Up() / Down() methods
    ├── 20261102xxxxxx_InitialCreate.Designer.cs
    └── StudentDBContextModelSnapshot.cs           ← current model ka snapshot
```

Database mein:

- `Students` table (tumhare model se)
- `__EFMigrationsHistory` table (EF track karta hai ki kaun si migrations apply ho chuki hain)

### Migration file ke andar kya hota hai?

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    // Table banane ka code
    migrationBuilder.CreateTable(
        name: "Students",
        columns: table => new
        {
            Id = table.Column<int>(nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", ...),
            StudentName = table.Column<string>(type: "varchar(100)", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Students", x => x.Id);
        });
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    // Undo karne ka code (rollback)
    migrationBuilder.DropTable(name: "Students");
}
```

- `Up()` = changes **apply** karna
- `Down()` = changes **undo** karna

### Dhyan dene wali baatein

- Migration ke naam mein **hyphen (`-`) mat rakho**, kyunki ye C# class ka naam ban jata hai. `InitialCreate` ya `AddStudentTable` jaisa naam rakho.
- Commands project folder mein chalao, jahan `.csproj` file hai.
- Multiple DbContext ho toh `--context` flag lagao (section 12.6).
- Agar multiple projects hain, toh `--project` aur `--startup-project` flags lagane pad sakte hain.

---

## 16. Baad mein extra column add karna ho toh?

Notes ka question:

> Agar mujhe baad mein koi extra kaam karna pada, matlab extra column add karna pada, toh tab kya karenge?

**Answer: Model change karo → nayi migration banao → update karo.**

Hum table ko database mein **manually change nahi karte**. Hamesha model se hi change karte hain, taaki code aur database **sync** rahein.

### Example: `Email` column add karna

**Step 1: Model mein property add karo**

```csharp
public class Student
{
    [Key]
    public int Id { get; set; }

    [Column("Student Name", TypeName = "varchar(100)")]
    public string Name { get; set; }

    public string Email { get; set; }    // ← nayi property
}
```

**Step 2: Nayi migration banao**

```bash
dotnet ef migrations add AddEmailToStudent
```

**Step 3: Database update karo**

```bash
dotnet ef database update
```

EF Core sirf **naya column add** karega (`ALTER TABLE ... ADD`), purana data safe rehta hai.

### Dhyan dene wali baatein

- Har change ke liye **nayi migration** banti hai, purani migration ko edit/delete nahi karte.
- Agar naya column `NOT NULL` hai aur table mein pehle se data hai, toh default value ya nullable (`string?`) rakhna padta hai, warna error aa sakta hai.
- Last migration undo karni ho (database update se pehle): `dotnet ef migrations remove`

---

## 17. Controller mein DbContext access karna

Notes mein tumne database mein **manually data insert** kar diya hai. Ab us data ko **controller ke andar access** karna hai.

> Ye step zaroori hai, tabhi hum controller ke andar database data access kar sakte hain.

Iske liye controller mein **DbContext ka object** chahiye hoga.

### Constructor Injection

```csharp
public class HomeController : Controller
{
    private readonly StudentDBContext studentDB;

    // Constructor Injection
    public HomeController(StudentDBContext studentDB)
    {
        this.studentDB = studentDB;
    }
}
```

### Iska matlab kya hai? (Dependency Injection)

Hum `new StudentDBContext()` **khud nahi likhte**. Jab request aati hai, ASP.NET Core ka **DI container**:

1. Dekhta hai ki `HomeController` ko `StudentDBContext` chahiye (constructor mein).
2. `Program.cs` mein registered `AddDbContext<StudentDBContext>` se uska **object khud bana deta hai**.
3. Wo object controller ke constructor mein **inject** kar deta hai.

```
Program.cs: AddDbContext<StudentDBContext>(...)    ← Registration
                     │
                     ▼
         DI Container (object banata hai)
                     │
                     ▼
HomeController(StudentDBContext studentDB)          ← Injection
```

### Fields ke baare mein

| Part                         | Matlab                                                                |
| ---------------------------- | --------------------------------------------------------------------- |
| `private`                    | Sirf is class ke andar accessible                                     |
| `readonly`                   | Sirf constructor mein assign ho sakta hai, baad mein badal nahi sakta |
| `this.studentDB = studentDB` | Constructor ke parameter ko class ke field mein save karna            |

### Controller mein data fetch karna

```csharp
public IActionResult Index()
{
    // SELECT * FROM Students
    var students = studentDB.Students.ToList();
    return View(students);
}
```

### Basic CRUD operations (quick recap)

```csharp
// READ (saare records)
var all = studentDB.Students.ToList();

// READ (ek record, id se)
var one = studentDB.Students.Find(1);

// READ (filter)
var filtered = studentDB.Students.Where(s => s.Name == "Sourabh").ToList();

// CREATE
studentDB.Students.Add(new Student { Name = "Rahul" });
studentDB.SaveChanges();

// UPDATE
var s = studentDB.Students.Find(1);
s.Name = "Updated Name";
studentDB.SaveChanges();

// DELETE
var d = studentDB.Students.Find(1);
studentDB.Students.Remove(d);
studentDB.SaveChanges();
```

> ⚠️ `SaveChanges()` ke bina Add/Update/Remove database mein **save nahi hota**. (Yahi Unit of Work pattern hai.)

### Pichli notes se connection (Model Validation)

Pichle notes ka `Create(Student student)` action ab EF Core ke saath yun use hoga:

```csharp
[HttpPost]
public IActionResult Create(Student student)
{
    if (ModelState.IsValid)
    {
        studentDB.Students.Add(student);
        studentDB.SaveChanges();
        return RedirectToAction("Index");
    }
    return View(student);
}
```

---

## 18. Common Errors aur Fixes

Ye errors Code-First setup mein aksar aate hain:

| Error                                                                          | Reason                                                          | Fix                                                                            |
| ------------------------------------------------------------------------------ | --------------------------------------------------------------- | ------------------------------------------------------------------------------ |
| `Could not execute because dotnet-ef was not found`                            | `dotnet-ef` tool installed nahi hai                             | `dotnet tool install --global dotnet-ef`, phir naya terminal kholo             |
| `add-migration is not recognized`                                              | `Add-Migration` sirf **Visual Studio PMC** ka command hai       | VS Code mein `dotnet ef migrations add ...` use karo                           |
| `NU1608` / `Method not found ... AbstractionsStrings`                          | EF packages ke versions mismatch (jaise Pomelo 9.x aur EF 10.x) | Saare EF packages same major version par lao                                   |
| `NU1605 Detected package downgrade`                                            | Ek package 10.x maang raha hai, doosra 9.x par pin hai          | Unused packages (jaise `SqlServer`, `Tools`) hatao ya sab same version par lao |
| `NETSDK1004 Assets file not found`                                             | `obj` folder delete ho gaya                                     | `dotnet restore` chalao                                                        |
| `Unable to connect to any of the specified MySQL hosts`                        | MySQL server band hai / connection string galat                 | MySQL start karo, connection string check karo                                 |
| Migration name error                                                           | Name mein hyphen (`-`)                                          | `InitialCreate` jaisa naam rakho                                               |
| `Cannot create a DbSet for 'X' because this type is not included in the model` | `Set<X>()` use kiya lekin entity model mein registered nahi hai | `DbSet<X>` define karo ya `modelBuilder.Entity<X>()` add karo                  |
| `More than one DbContext was found`                                            | Multiple DbContext hain aur `--context` nahi diya               | `--context <Name>` flag lagao                                                  |

---

## 19. Mongoose / Express se comparison

| EF Core (ASP.NET Core)                           | Mongoose / Node.js                                                                                     |
| ------------------------------------------------ | ------------------------------------------------------------------------------------------------------ |
| Model class (`Student`)                          | Mongoose Schema                                                                                        |
| `DbContext`                                      | Mongoose connection (`mongoose.connect`)                                                               |
| `DbSet<Student>`                                 | Mongoose Model (`mongoose.model('Student', schema)`)                                                   |
| `appsettings.json` connection string             | `.env` mein `MONGO_URI`                                                                                |
| `AddDbContext` in `Program.cs`                   | `mongoose.connect(...)` in `app.js`                                                                    |
| `Migrations` (schema versioning)                 | MongoDB schemaless hai, migration ki zaroorat kam padti hai (SQL mein Sequelize/Knex migrations jaisa) |
| `studentDB.Students.ToList()`                    | `Student.find()`                                                                                       |
| `studentDB.Students.Add(s); SaveChanges()`       | `new Student(data).save()` ya `Student.create(data)`                                                   |
| Change Tracking (`SaveChanges()` pe auto UPDATE) | Mongoose document `doc.save()` (modified fields track karta hai)                                       |
| `Include()` (related data load)                  | `.populate()`                                                                                          |
| Constructor Injection                            | `require()` / import karke use karna                                                                   |
| Ek DbContext, 50 DbSets                          | Ek DB connection, 50 Mongoose models                                                                   |

### Same kaam dono mein

```js
// Mongoose
const students = await Student.find();
```

```csharp
// EF Core
var students = studentDB.Students.ToList();
```

### Ek bada difference

- **MongoDB** schemaless hai: model change karo, collection khud adjust ho jata hai.
- **SQL (EF Core)** mein table ka structure fix hota hai, isliye model badalne par **migration** zaroori hai.

---
# Entity Framework Core — Database-First Approach (ASP.NET Core MVC)

> Hinglish notes, interview-oriented. Ye **Code-First ka bilkul reverse** hai. Node.js ke analogies bhi diye hain.

---

## Table of Contents

1. [Database-First Approach kya hai?](#1-database-first-approach-kya-hai)
2. [Code-First vs Database-First](#2-code-first-vs-database-first)
3. [Kab use karte hain?](#3-kab-use-karte-hain)
4. [Overall flow](#4-overall-flow)
5. [Step 1: Packages install karna](#5-step-1-packages-install-karna)
6. [Step 2: Scaffold-DbContext command chalana](#6-step-2-scaffold-dbcontext-command-chalana)
7. [Generated files ka structure aur code](#7-generated-files-ka-structure-aur-code)
8. [Database update ho jaye toh Model aur DbContext kaise update karein?](#8-database-update-ho-jaye-toh-model-aur-dbcontext-kaise-update-karein)
9. [Step 3: Connection string ko appsettings.json mein move karna](#9-step-3-connection-string-ko-appsettingsjson-mein-move-karna)
10. [Step 4: Program.cs mein register karna](#10-step-4-programcs-mein-register-karna)
11. [Step 5: Controller mein DbContext use karna](#11-step-5-controller-mein-dbcontext-use-karna)
12. [View mein data dikhana](#12-view-mein-data-dikhana)
13. [Common Errors aur Fixes](#13-common-errors-aur-fixes)
14. [Node.js se comparison](#14-nodejs-se-comparison)
15. [Interview Questions & Answers](#15-interview-questions--answers)
16. [Quick Revision Cheat Sheet](#16-quick-revision-cheat-sheet)

---

## 1. Database-First Approach kya hai?

Jo humne **Code-First approach** mein kiya tha, ye uska **bilkul reverse** hai.

```
Code-First      :  Model Classes  ───►  Database Tables
Database-First  :  Existing Database & Tables  ───►  Generated Model Classes
```

Apne paas **pehle se hi database** hai, aur uske according hum **model classes generate** karenge. Saath mein **DbContext class bhi automatic generate** ho jati hai.

### Definition

> **In the Database-First approach, Entity Framework Core creates model classes and properties corresponding to the existing database objects such as tables and columns.**

> **The Database-First approach is applicable in scenarios where a database already exists for the application.**

### Kaise kaam karta hai?

```
Existing Database Tables
        │
        ▼
   Scaffold-DbContext command execute karte hain
        │
        ▼
Model classes + DbContext class
apne project folder structure ke andar AUTOMATIC aa jati hain
```

### Scaffold kya hota hai?

**Scaffold = code ka automatic generation.** Is command ko run karne ke baad database ke against jo **model classes + DbContext class** hoti hain, wo apne project mein khud generate ho jati hain.

---

## 2. Code-First vs Database-First

| | Code-First | Database-First |
|---|---|---|
| Starting point | **Model classes** | **Existing database** |
| Direction | Code → Database | Database → Code |
| Kaun generate hota hai | Tables (migration se) | Model classes + DbContext (scaffold se) |
| Main command | `dotnet ef migrations add` + `dotnet ef database update` | `dotnet ef dbcontext scaffold` |
| DbContext | Khud likhte hain | **Automatic generate** hota hai |
| Source of truth | **Code** (models) | **Database** |
| Schema change kaise | Model badlo → nayi migration | DB badlo → **dubara scaffold** |
| Migrations folder | Hota hai | Normally **nahi** hota |

> ⚠️ Database-First mein **migrations nahi chalate** (`dotnet ef migrations add` nahi). Yahan database hi truth hai, code usse follow karta hai.

---

## 3. Kab use karte hain?

- Database **pehle se bana hua** hai (legacy / existing project).
- DBA (Database Administrator) database design karta hai aur developers sirf application banate hain.
- Doosre team ya doosri application ka database use karna hai.
- Bahut saari tables hain aur unki classes manually likhna time-consuming hai.

---

## 4. Overall flow

```
1. Packages install karo
        │
        ▼
2. Scaffold command chalao  →  Models/ + Data/ApplicationDBContext.cs generate
        │
        ▼
3. Connection string ko DbContext se hatake appsettings.json mein daalo
        │
        ▼
4. Program.cs mein AddDbContext se register karo
        │
        ▼
5. Controller mein DbContext inject karke data use karo
        │
        ▼
   View mein data dikh jayega ✅
```

---

## 5. Step 1: Packages install karna

Wahi **3 packages** install karte hain jo Code-First mein karte the:

| Package | Kaam |
|---|---|
| `Microsoft.EntityFrameworkCore.SqlServer` | Database provider (SQL Server). **MySQL bhi kar sakte hain** (niche dekho) |
| `Microsoft.EntityFrameworkCore.Tools` | Scaffold aur migration commands ke liye |
| `Microsoft.EntityFrameworkCore.Design` | Design-time logic (scaffold ke liye zaroori) |

### MySQL use kar rahe ho toh

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### SQL Server use kar rahe ho toh

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### `dotnet-ef` global tool (ek baar)

```bash
dotnet tool install --global dotnet-ef
dotnet ef --version
```

### ⚠️ Dhyan dene wali baatein

- Saare `Microsoft.EntityFrameworkCore.*` packages ka **same major version** rakho (jaise 9.0.x). Mismatch par `NU1605` / `Method not found` error aata hai.
- **Unused provider package** install mat rakho (MySQL use karte hue `SqlServer` mat rakho), warna version conflict ho sakta hai.
- `Design` package ke bina scaffold command fail hoti hai.

---

## 6. Step 2: Scaffold-DbContext command chalana

Packages install karne ke baad **Scaffold command** run karte hain.

### MySQL ke liye command

```bash
dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=CodeFirstApproach;User=root;Password=root;" Pomelo.EntityFrameworkCore.MySql -o Models -c ApplicationDBContext --context-dir Data --force
```

### SQL Server ke liye command

```bash
dotnet ef dbcontext scaffold "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c ApplicationDBContext --context-dir Data --force
```

### Command ko part-by-part samjho

| Part | Matlab |
|---|---|
| `dotnet ef dbcontext scaffold` | Database se code generate karne ka command |
| `"Server=...;Database=...;..."` | **Connection string** (kaun sa database) |
| `Pomelo.EntityFrameworkCore.MySql` | **Provider** (kaun sa database type: MySQL / SQL Server) |
| `-o Models` | **Output folder**: model classes `Models` folder mein banengi (`--output-dir` ka short form) |
| `-c ApplicationDBContext` | **DbContext class ka naam** (`--context` ka short form) |
| `--context-dir Data` | DbContext file **`Data` folder** mein banegi |
| `--force` | Agar files pehle se hain toh unhe **overwrite** kar do |

### Ye command automatic kya karti hai?

Upar wali command **automatically model classes aur DbContext class generate** kar deti hai, aur wo is tarah ke folder structure mein aa jati hain 👇

---

## 7. Generated files ka structure aur code

### Folder structure

```
Database-First-Approach-of-Entity-Core-Framework
│
├── Data
│   └── ApplicationDBContext.cs
│
├── Models
│   ├── Employee.cs
│   └── Student.cs
│
└── Database-First-Approach-of-Entity-Core-Framework.csproj
```

- Database ki **har table** ke liye ek **model class** (`Employee.cs`, `Student.cs`).
- Saari tables ke liye ek **DbContext class** (`ApplicationDBContext.cs`).
- Table ke **har column** ke liye model mein ek **property**.

### Generated model class ka example (`Student.cs`)

```csharp
public partial class Student
{
    public int Id { get; set; }

    public string StudentName { get; set; } = null!;
}
```

### Generated DbContext ka example (`ApplicationDBContext.cs`)

```csharp
public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    // ⚠️ Connection string yahan hardcoded aati hai (Step 3 mein hatani hai)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(
            "Server=localhost;Port=3306;Database=CodeFirstApproach;User=root;Password=root;",
            ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("Students");
            // ... columns ki configuration (Fluent API)
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
```

### Code mein kya-kya notice karo

| Cheez | Matlab |
|---|---|
| `partial class` | Class ko **dusri file mein extend** kar sakte ho. Regenerate hone par tumhara custom code safe rahta hai |
| `DbSet<Employee>`, `DbSet<Student>` | Har table ke liye ek `DbSet` automatic bana |
| `OnConfiguring` | Isme **connection string hardcoded** hoti hai (isko hatana hai) |
| `OnModelCreating` | **Fluent API** se table/column/key/relationship ki configuration |
| `OnModelCreatingPartial` | Apna extra configuration yahan likhne ke liye hook |

> 💡 Code-First mein hum `[Key]`, `[Column]` jaise attributes lagate the. Database-First mein ye configuration zyadatar `OnModelCreating` ke **Fluent API** mein generate hoti hai.

---

## 8. Database update ho jaye toh Model aur DbContext kaise update karein?

**Question:** *If we update our database, then how can we update our model and DbContext?*

**Answer: Wahi scaffold command dubara chalani hai.**

```bash
dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=CodeFirstApproach;User=root;Password=root;" Pomelo.EntityFrameworkCore.MySql -o Models -c ApplicationDBContext --context-dir Data --force
```

`--force` ki wajah se purani files **overwrite** ho jati hain aur nayi table/column ke hisaab se model aur DbContext **update** ho jate hain.

### ⚠️ Bahut important warning

`--force` **purani generated files ko overwrite** karta hai. Agar tumne generated model ya DbContext mein **manually kuch code likha hai, wo delete ho jayega.**

**Bachne ke tarike:**

1. **`partial class` use karo.** Apna extra code **alag file** mein partial class ke andar likho:

```csharp
// Models/Student.Custom.cs  (ye file overwrite nahi hogi)
public partial class Student
{
    public string DisplayName => $"Student: {StudentName}";
}
```

2. **DbContext ka extra config `OnModelCreatingPartial`** mein likho (alag partial file mein).

3. **Sirf specific tables dubara scaffold karo:**

```bash
dotnet ef dbcontext scaffold "..." Pomelo.EntityFrameworkCore.MySql -o Models -c ApplicationDBContext --context-dir Data --force --table Students --table Employees
```

> Regenerate karne ke baad `OnConfiguring` wapas aa jayega (hardcoded connection string ke saath). Isse bachne ke liye niche wala tip dekho.

---

## 9. Step 3: Connection string ko appsettings.json mein move karna

### Problem

Generated DbContext ke `OnConfiguring` method mein **connection string hardcoded** hoti hai. Ye **galat practice** hai (password code mein, GitHub par leak ho sakta hai, aur environment ke hisaab se change nahi hoti).

### Fix

**1. `OnConfiguring` method (ya uske andar ki connection string) DbContext se hata do.**

```csharp
// ❌ Ye poora method hata do
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseMySql("Server=localhost;...", ServerVersion.Parse("8.0.36-mysql"));
```

DbContext mein ye **constructor rehna zaroori hai** (ye already generate hota hai):

```csharp
public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
    : base(options)
{
}
```

**2. Connection string `appsettings.json` mein likho:**

```json
{
  "ConnectionStrings": {
    "dbcs": "Server=localhost;Port=3306;Database=CodeFirstApproach;User=root;Password=root;"
  }
}
```

### 💡 Pro tip: Shuruat se hi `OnConfiguring` generate hone se roko

Scaffold command mein `--no-onconfiguring` flag lagao, toh `OnConfiguring` method generate hi nahi hoga:

```bash
dotnet ef dbcontext scaffold "..." Pomelo.EntityFrameworkCore.MySql -o Models -c ApplicationDBContext --context-dir Data --force --no-onconfiguring
```

### 💡 Pro tip 2: Command mein password bhi mat likho

Connection string pehle `appsettings.json` mein daalo, phir scaffold command mein **uska naam** do:

```bash
dotnet ef dbcontext scaffold "Name=ConnectionStrings:dbcs" Pomelo.EntityFrameworkCore.MySql -o Models -c ApplicationDBContext --context-dir Data --force --no-onconfiguring
```

---

## 10. Step 4: Program.cs mein register karna

`Program.cs` mein ye lines `builder.Build()` se **pehle** likho:

```csharp
using Microsoft.EntityFrameworkCore;
using Database_First_Approach_of_Entity_Core_Framework.Data;   // DbContext ka namespace

var connectionString = builder.Configuration.GetConnectionString("dbcs");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
```

### Dhyan dene wali baatein

- `"dbcs"` ki jagah **wahi naam** rakhna jo tumne `appsettings.json` mein connection string ke **key** mein diya ho.
- `AddDbContext<...>` mein **apne generated DbContext ka naam** likho. Code-First wale notes mein `StudentDBContext` tha, yahan **`ApplicationDBContext`** (ya jo bhi naam `-c` flag mein diya) use karo.
- DbContext ka **namespace import** karna mat bhulna (`using ...Data;`), warna "type not found" error aata hai.
- Ye teen lines hi register karne ke liye kaafi hain.
- SQL Server ke liye `UseMySql(...)` ki jagah `UseSqlServer(connectionString)` lagega.
- `ServerVersion.AutoDetect` startup par DB se connect hota hai, toh MySQL server **chalu** hona chahiye.

---

## 11. Step 5: Controller mein DbContext use karna

Ab isko controller ke andar use karte hain, **Constructor Injection** se:

```csharp
public class HomeController : Controller
{
    private readonly ApplicationDBContext applicationDB;

    public HomeController(ApplicationDBContext applicationDB)
    {
        this.applicationDB = applicationDB;
    }
}
```

Jahan bhi database ka data chahiye, wahan:

```
applicationDB.DbSetName.Find() / ToList() / Where() ...
```

jo bhi operation lagana hai uske according use karo.

### Index action (saara data fetch)

```csharp
public IActionResult Index()
{
    var data = applicationDB.Students.ToList();
    return View(data);
}
```

### Baaki operations

```csharp
// Ek record (primary key se)
var one = applicationDB.Students.Find(1);

// Filter
var filtered = applicationDB.Students.Where(s => s.StudentName == "Rahul").ToList();

// Create
applicationDB.Students.Add(new Student { StudentName = "Amit" });
applicationDB.SaveChanges();

// Update
var s1 = applicationDB.Students.Find(1);
s1.StudentName = "Updated";
applicationDB.SaveChanges();

// Delete
var d = applicationDB.Students.Find(1);
applicationDB.Students.Remove(d);
applicationDB.SaveChanges();
```

> ⚠️ `SaveChanges()` ke bina Add/Update/Delete database mein save nahi hota.

---

## 12. View mein data dikhana

Controller ne `View(data)` return kiya, ab View mein **list bana ke** UI par data dikha dete hain.

`Views/Home/Index.cshtml`

```html
@model List<Student>

<table class="table">
    <thead>
        <tr>
            <th>Id</th>
            <th>Name</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var student in Model)
        {
            <tr>
                <td>@student.Id</td>
                <td>@student.StudentName</td>
            </tr>
        }
    </tbody>
</table>
```

Ab UI par database ka data dikh jayega ✅

> ⚠️ `@model List<Student>` ke liye `Student` ka namespace View mein accessible hona chahiye (`@using ProjectName.Models` ya `_ViewImports.cshtml` mein).

