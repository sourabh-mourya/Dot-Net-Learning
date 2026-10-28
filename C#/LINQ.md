# LINQ (Language Integrated Query) – C# Notes

## 1. LINQ kya hai?

- **Full form:** Language Integrated Query
- **Namespace:** `using System.Linq;`
- C# ke andar hi data ko query karne ka SQL-jaisa API hai.
- **Data source kuch bhi ho sakta hai:** List, Array, Collection, DataSet, SQL Server (EF Core), XML.
- Query us data source par execute hoti hai aur result hume object/collection me milta hai.
- Fayda: lambe `foreach` + `if-else` ki jagah 1 line me filter/sort/transform.

Kbhi bhi travser krna filter data of linq method syntax ya kissi to apne ko wha pr for each loop laga ka used krna hi
var whereData = students.Where(a => a.Age > 20);
foreach (var student in whereData)
{
Console.WriteLine(
$"{student.Id} | {student.Name} | {student.Age} | " +
$"{student.Department} | {student.Marks}"
);
}


**Without LINQ**

```csharp
List<int> evenNumbers = new List<int>();
foreach (var n in numbers)
{
    if (n % 2 == 0) evenNumbers.Add(n);
}
```

**With LINQ**

```csharp
var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
```

---

## 2. Do Syntax Styles

| Syntax                                                       | Example                                       |
| ------------------------------------------------------------ | --------------------------------------------- |
| **Method Syntax** (Fluent API) – industry standard, ~95% use | `numbers.Where(n => n % 2 == 0)`              |
| **Query Syntax** (SQL jaisa)                                 | `from n in numbers where n % 2 == 0 select n` |

> ASP.NET Core / EF Core me **Method Syntax** hi use hota hai.

---

## 3. Sample Data (aage ke examples isi par)

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Department { get; set; }
    public double Marks { get; set; }
}

List<Student> students = new List<Student>
{
    new Student { Id = 1, Name = "Rahul", Age = 20, Department = "IT",    Marks = 85 },
    new Student { Id = 2, Name = "Priya", Age = 22, Department = "CS",    Marks = 92 },
    new Student { Id = 3, Name = "Amit",  Age = 19, Department = "IT",    Marks = 68 },
    new Student { Id = 4, Name = "Sneha", Age = 21, Department = "CS",    Marks = 78 },
    new Student { Id = 5, Name = "Vikas", Age = 23, Department = "Civil", Marks = 54 }
};
```

---

## 4. Important Methods

### 4.1 Filtering – `Where()`

Condition ke basis par elements filter karta hai.

```csharp
var csStudents = students.Where(s => s.Department == "CS");
var toppers    = students.Where(s => s.Department == "CS" && s.Marks >= 80);
```

### 4.2 Projection – `Select()` / `SelectMany()`

- `Where()` decide karta hai **kaunse elements** aayenge.
- `Select()` decide karta hai **har element ka output kya** hoga.

```csharp
List<string> names = students.Select(s => s.Name).ToList();

// Anonymous object
var summary = students.Select(s => new { StudentName = s.Name, Score = s.Marks });
```

### 4.3 Sorting

| Method                            | Use               |
| --------------------------------- | ----------------- |
| `OrderBy()`                       | Ascending         |
| `OrderByDescending()`             | Descending        |
| `ThenBy()` / `ThenByDescending()` | Secondary sorting |
| `Reverse()`                       | Sequence reverse  |

```csharp
var sorted = students.OrderBy(s => s.Department).ThenBy(s => s.Name);
```

### 4.4 Pagination – `Skip()` / `Take()`

```csharp
// Top 3 toppers
var top3 = students.OrderByDescending(s => s.Marks).Take(3);

// Page-wise data
int pageSize = 2, pageNumber = 2;
var page2Data = students
    .Skip((pageNumber - 1) * pageSize)   // pehle ke records skip
    .Take(pageSize)                      // agle pageSize records lo
    .ToList();
```

> Pagination se pehle hamesha **stable ordering** (jaise `OrderBy(x => x.Id)`) lagao.

### 4.5 Element Selection – Interview Favorite

| Method              | Record na mile        | 1 se zyada mile   |
| ------------------- | --------------------- | ----------------- |
| `First()`           | Exception             | Pehla le leta hai |
| `FirstOrDefault()`  | `null`/default (safe) | Pehla le leta hai |
| `Single()`          | Exception             | Exception         |
| `SingleOrDefault()` | `null`/default        | Exception         |

```csharp
Student s = students.FirstOrDefault(st => st.Id == 3);
if (s != null) Console.WriteLine($"Found: {s.Name}");
```

> EF Core me async versions use hote hain: `FirstOrDefaultAsync()`, `SingleOrDefaultAsync()`.

### 4.6 Checking – `Any()`, `All()`, `Contains()`

```csharp
bool hasFailed  = students.Any(s => s.Marks < 60);   // koi ek bhi?
bool allAdults  = students.All(s => s.Age >= 18);    // sabhi?
bool emailExist = users.Any(u => u.Email == email);  // DB me exist karta hai?
```

> **Performance tip:** list khaali hai ya nahi check karne ke liye `Count() > 0` nahi, `Any()` use karo (Any pehla element milte hi return ho jata hai).

### 4.7 Aggregation – `Count`, `Sum`, `Average`, `Min`, `Max`

```csharp
int    total   = students.Count();
double avg     = students.Average(s => s.Marks);
double highest = students.Max(s => s.Marks);
double sum     = students.Sum(s => s.Marks);

// Modern C#: pura object milta hai
Student topper = students.MaxBy(s => s.Marks);
```

(Extra: `MinBy()`, `LongCount()`, `Aggregate()`)

### 4.8 Conversion

| Method                      | Use                                  |
| --------------------------- | ------------------------------------ |
| `ToList()`                  | `List<T>` banata hai                 |
| `ToArray()`                 | Array banata hai                     |
| `ToDictionary()`            | Dictionary banata hai                |
| `ToHashSet()`               | HashSet banata hai                   |
| `Cast<T>()` / `OfType<T>()` | Type cast / type ke hisaab se filter |

### 4.9 Set Operations

| Method        | Use                         |
| ------------- | --------------------------- |
| `Distinct()`  | Duplicates remove           |
| `Union()`     | Dono ke unique elements     |
| `Intersect()` | Common elements             |
| `Except()`    | Pehli me jo doosri me nahi  |
| `Concat()`    | Jodna (duplicates ke saath) |

### 4.10 Grouping – `GroupBy()`

```csharp
var grouped = students.GroupBy(s => s.Department);

foreach (var group in grouped)
{
    Console.WriteLine($"Department: {group.Key} (Total: {group.Count()})");
    foreach (var st in group)
        Console.WriteLine($"  - {st.Name} ({st.Marks})");
}
```

### 4.11 `SelectMany()` – Nested list ko flatten karna

```csharp
public class Order
{
    public int OrderId { get; set; }
    public List<string> Items { get; set; }
}

var listInList = orders.Select(o => o.Items);               // List<List<string>>
List<string> allItems = orders.SelectMany(o => o.Items).ToList();
// ["Mobile", "Cover", "Laptop", "Mouse"]
```

### 4.12 `Join()` – SQL Inner Join jaisa

```csharp
var joinedData = students.Join(
    departments,
    student => student.Department,    // Student ki key
    dept    => dept.Code,             // Department ki key
    (student, dept) => new
    {
        StudentName = student.Name,
        Dept = dept.Code,
        HOD = dept.HeadOfDept
    }
);
```

(Related: `GroupJoin()`, `Zip()`)

---

## 5. Method Chaining

Multiple LINQ methods ko ek ke baad ek lagana.

```csharp
int[] numbers = { 10, 40, 20, 50, 30 };

var result = numbers
    .Where(n => n > 20)
    .OrderBy(n => n)
    .ToList();    // 30, 40, 50
```

---

## 6. Deferred vs Immediate Execution (Most Important Concept)

**Rule:** Jab tak query par `foreach`, `.ToList()`, `.ToArray()`, `.Count()` jaisa kuch nahi lagate, query **execute nahi hoti** – wo sirf query definition hoti hai.

```csharp
List<int> numbers = new List<int> { 1, 2, 3 };

var query = numbers.Where(n => n > 1);   // sirf define hui (Deferred)
numbers.Add(4);                          // list me naya item

foreach (var item in query)
    Console.WriteLine(item);             // Output: 2, 3, 4  <-- 4 bhi aaya!
```

**Immediate execution** in methods se hoti hai:
`ToList()`, `ToArray()`, `ToDictionary()`, `Count()`, `Sum()`, `First()`

---

## 7. IEnumerable<T> vs IQueryable<T>

| Feature               | `IEnumerable<T>`                                | `IQueryable<T>`                                                   |
| --------------------- | ----------------------------------------------- | ----------------------------------------------------------------- |
| Kahan chalta hai?     | Application memory (RAM) me                     | Database server me                                                |
| Kaise kaam karta hai? | Pura data memory me laata hai, fir C# me filter | SQL banata hai (`SELECT ... WHERE`), sirf filtered data laata hai |
| Performance           | Badi table par slow                             | Fast                                                              |
| Use case              | Local List, Array, Dictionary                   | EF Core `DbContext.Students`                                      |

---

## 8. Real-World Example

> "Delhi ke customers ke naam aur 2026 ka total order amount, jinka total 5000 se zyada ho, highest pehle."

```csharp
var premiumDelhiCustomers = customers
    .Where(c => c.City == "Delhi")
    .Select(c => new
    {
        Name = c.Name,
        TotalSpent = c.Orders
            .Where(o => o.OrderDate.Year == 2026)
            .Sum(o => o.Amount)
    })
    .Where(result => result.TotalSpent > 5000)
    .OrderByDescending(result => result.TotalSpent)
    .ToList();
```

---

## Quick Revision

- LINQ = C# me SQL-jaisi querying (List, Array, DB, XML sab par).
- Industry me **Method Syntax** use hota hai.
- `Where` = filter, `Select` = transform, `OrderBy` = sort, `Skip/Take` = pagination.
- `FirstOrDefault` safe hai; `Single` exactly 1 expect karta hai.
- `Any()` > `Count() > 0`.
- Query **deferred** hoti hai jab tak `ToList()` / `foreach` na lage.
- DB ke saath kaam: `IQueryable` (SQL me chalta hai), `IEnumerable` (memory me).
