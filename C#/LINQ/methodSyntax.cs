using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Department { get; set; } = string.Empty;
        public double Marks { get; set; }
    }

    public class Department
    {
        public string Code { get; set; } = string.Empty;
        public string HeadOfDept { get; set; } = string.Empty;
    }

    public class Order
    {
        public int OrderId { get; set; }
        public List<string> Items { get; set; } = new List<string>();
    }

    public class Program
    {
        // Helper: students print karne ke liye
        static void PrintStudents(IEnumerable<Student> list)
        {
            foreach (Student s in list)
            {
                Console.WriteLine(
                    $"{s.Id} | {s.Name} | {s.Age} | {s.Department} | {s.Marks}"
                );
            }
        }

        static void Title(string text)
        {
            Console.WriteLine();
            Console.WriteLine($"===== {text} =====");
        }

        public static void Main(string[] args)
        {
            // ---------- Sample Data ----------
            List<Student> students = new List<Student>
            {
                new Student { Id = 1, Name = "Rahul", Age = 20, Department = "IT",    Marks = 85 },
                new Student { Id = 2, Name = "Priya", Age = 22, Department = "CS",    Marks = 92 },
                new Student { Id = 3, Name = "Amit",  Age = 19, Department = "IT",    Marks = 68 },
                new Student { Id = 4, Name = "Sneha", Age = 21, Department = "CS",    Marks = 78 },
                new Student { Id = 5, Name = "Vikas", Age = 23, Department = "Civil", Marks = 54 }
            };

            List<Department> departments = new List<Department>
            {
                new Department { Code = "CS", HeadOfDept = "Dr. Sharma" },
                new Department { Code = "IT", HeadOfDept = "Dr. Verma" }
            };

            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 1, Items = new List<string> { "Mobile", "Cover" } },
                new Order { OrderId = 2, Items = new List<string> { "Laptop", "Mouse" } }
            };

            Title("All Students");
            PrintStudents(students);

            // ---------- 1. Where() : Filtering ----------
            Title("1. Where() - Age > 20");
            var whereData = students.Where(a => a.Age > 20);
            PrintStudents(whereData);

            Title("1. Where() - CS department AND Marks >= 80");
            var toppers = students.Where(s => s.Department == "CS" && s.Marks >= 80);
            PrintStudents(toppers);

            // ---------- 2. Select() : Projection ----------
            Title("2. Select() - Student Names");
            var selectData = students.Select(s => s.Name);
            foreach (var name in selectData)
            {
                Console.WriteLine(name);
            }

            Title("2. Select() - Anonymous Object (Name + Score)");
            var summary = students.Select(s => new { StudentName = s.Name, Score = s.Marks });
            foreach (var item in summary)
            {
                Console.WriteLine($"{item.StudentName} -> {item.Score}");
            }

            // ---------- 3. OrderBy / OrderByDescending / ThenBy ----------
            Title("3. OrderBy() - Marks Ascending");
            PrintStudents(students.OrderBy(s => s.Marks));

            Title("3. OrderByDescending() - Marks Descending");
            PrintStudents(students.OrderByDescending(s => s.Marks));

            Title("3. OrderBy().ThenBy() - Department then Name");
            PrintStudents(students.OrderBy(s => s.Department).ThenBy(s => s.Name));

            // ---------- 4. Take() / Skip() : Pagination ----------
            Title("4. Take() - Top 3 by Marks");
            PrintStudents(students.OrderByDescending(s => s.Marks).Take(3));

            Title("4. Skip() + Take() - Page 2 (pageSize = 2)");
            int pageSize = 2;
            int pageNumber = 2;
            var page2Data = students
                .OrderBy(s => s.Id)                    // stable ordering zaroori hai
                .Skip((pageNumber - 1) * pageSize)     // pehle ke records skip
                .Take(pageSize)                        // agle pageSize records lo
                .ToList();
            PrintStudents(page2Data);

            // ---------- 5. First / FirstOrDefault / Single / SingleOrDefault ----------
            Title("5. FirstOrDefault() - Id == 3");
            Student? found = students.FirstOrDefault(st => st.Id == 3);
            if (found != null)
            {
                Console.WriteLine($"Found: {found.Name}");
            }

            Title("5. FirstOrDefault() - Id == 99 (not found -> null)");
            Student? notFound = students.FirstOrDefault(st => st.Id == 99);
            Console.WriteLine(notFound == null ? "Student not found (null)" : notFound.Name);

            Title("5. First() vs Single()");
            Console.WriteLine("First (Age > 20): " + students.First(s => s.Age > 20).Name);   // pehla match
            Console.WriteLine("Single (Id == 2): " + students.Single(s => s.Id == 2).Name);   // exactly 1 match

            // ---------- 6. Any() / All() / Contains() ----------
            Title("6. Any() / All()");
            bool hasFailed = students.Any(s => s.Marks < 60);
            bool isNotEmpty = students.Any();
            bool allAdults = students.All(s => s.Age >= 18);
            Console.WriteLine($"Any student with Marks < 60 : {hasFailed}");
            Console.WriteLine($"List is not empty           : {isNotEmpty}");
            Console.WriteLine($"All students 18+            : {allAdults}");

            // ---------- 7. Aggregation ----------
            Title("7. Count / Sum / Average / Min / Max / MaxBy");
            Console.WriteLine($"Total Students : {students.Count()}");
            Console.WriteLine($"Count (Age>20) : {students.Count(s => s.Age > 20)}");
            Console.WriteLine($"Total Marks    : {students.Sum(s => s.Marks)}");
            Console.WriteLine($"Average Marks  : {students.Average(s => s.Marks)}");
            Console.WriteLine($"Min Marks      : {students.Min(s => s.Marks)}");
            Console.WriteLine($"Max Marks      : {students.Max(s => s.Marks)}");

            Student topper = students.MaxBy(s => s.Marks)!;   // .NET 6+
            Console.WriteLine($"Topper         : {topper.Name} with {topper.Marks}");

            // ---------- 8. Conversion ----------
            Title("8. ToList / ToArray / ToDictionary");
            List<string> nameList = students.Select(s => s.Name).ToList();
            string[] nameArray = students.Select(s => s.Name).ToArray();
            Dictionary<int, string> idNameMap = students.ToDictionary(s => s.Id, s => s.Name);
            Console.WriteLine("ToList       : " + string.Join(", ", nameList));
            Console.WriteLine("ToArray      : " + string.Join(", ", nameArray));
            Console.WriteLine("ToDictionary : " + string.Join(", ", idNameMap.Select(kv => $"{kv.Key}={kv.Value}")));

            // ---------- 9. Set Operations ----------
            Title("9. Distinct / Union / Intersect / Except / Concat");
            int[] a = { 1, 2, 3, 3, 4 };
            int[] b = { 3, 4, 5 };
            Console.WriteLine("Distinct  : " + string.Join(", ", a.Distinct()));
            Console.WriteLine("Union     : " + string.Join(", ", a.Union(b)));
            Console.WriteLine("Intersect : " + string.Join(", ", a.Intersect(b)));
            Console.WriteLine("Except    : " + string.Join(", ", a.Except(b)));
            Console.WriteLine("Concat    : " + string.Join(", ", a.Concat(b)));

            // ---------- 10. GroupBy() ----------
            Title("10. GroupBy() - Department wise");
            var groupedByDept = students.GroupBy(s => s.Department);
            foreach (var group in groupedByDept)
            {
                Console.WriteLine($"Department: {group.Key} (Total: {group.Count()})");
                foreach (var st in group)
                {
                    Console.WriteLine($"  - {st.Name} ({st.Marks} marks)");
                }
            }

            // ---------- 11. SelectMany() : Flatten ----------
            Title("11. Select() vs SelectMany()");
            var listInList = orders.Select(o => o.Items);               // List<List<string>>
            Console.WriteLine($"Select     -> {listInList.Count()} inner lists");

            List<string> allItems = orders.SelectMany(o => o.Items).ToList();   // flat list
            Console.WriteLine("SelectMany -> " + string.Join(", ", allItems));

            // ---------- 12. Join() : SQL Inner Join jaisa ----------
            Title("12. Join() - Students + Departments");
            var joinedData = students.Join(
                departments,
                student => student.Department,     // Student ki key
                dept => dept.Code,                 // Department ki key
                (student, dept) => new
                {
                    StudentName = student.Name,
                    Dept = dept.Code,
                    HOD = dept.HeadOfDept
                }
            );
            foreach (var item in joinedData)
            {
                Console.WriteLine($"{item.StudentName} | {item.Dept} | {item.HOD}");
            }

            // ---------- 13. Method Chaining ----------
            Title("13. Method Chaining");
            int[] numbers = { 10, 40, 20, 50, 30 };
            var result = numbers
                .Where(n => n > 20)
                .OrderBy(n => n)
                .ToList();
            Console.WriteLine(string.Join(", ", result));   // 30, 40, 50

            // ---------- 14. Deferred Execution ----------
            Title("14. Deferred Execution");
            List<int> list = new List<int> { 1, 2, 3 };
            var query = list.Where(n => n > 1);   // sirf define hui, chali nahi
            list.Add(4);                          // query chalne se pehle item add
            foreach (var item in query)           // ab execute hogi
            {
                Console.WriteLine(item);          // 2, 3, 4  <-- 4 bhi aayega
            }

            // Immediate Execution: ToList() turant chala deta hai
            var immediate = list.Where(n => n > 1).ToList();
            list.Add(5);
            Console.WriteLine("Immediate (ToList) : " + string.Join(", ", immediate));   // 2, 3, 4 (5 nahi)
        }
    }
}