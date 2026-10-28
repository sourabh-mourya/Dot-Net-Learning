using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCollectionsDemo
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }

    // 2. Encapsulation Example: Class exposing IEnumerable to protect internal data
    public class Company
    {
        // Private mutable list
        private List<Employee> _employees = new List<Employee>();

        public Company()
        {
            _employees.Add(new Employee { Id = 1, Name = "Bill", Department = "IT" });
            _employees.Add(new Employee { Id = 2, Name = "Steve", Department = "HR" });
            _employees.Add(new Employee { Id = 3, Name = "Happy", Department = "IT" });
        }

        // Exposing as IEnumerable (Read-only view)
        // Outside code cannot call .Add() or .Clear() on this!
        public IEnumerable<Employee> Employees => _employees;
    }

    class Program
    {
        // 1. Method Parameter Flexibility Example:
        // By taking IEnumerable<T>, this method works for ANY collection (List, Array, HashSet, etc.)
        static void PrintCollection(string title, IEnumerable<Employee> collection)
        {
            Console.WriteLine($"\n--- {title} ---");
            foreach (var item in collection)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Dept: {item.Department}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine("1. REAL-WORLD USE OF IEnumerable AS METHOD PARAMETER");
            Console.WriteLine("===============================================================");
            
            // Pass a List:
            List<Employee> listEmployees = new List<Employee>()
            {
                new Employee() { Id = 101, Name = "Rahul", Department = "Finance" },
                new Employee() { Id = 102, Name = "Amit", Department = "IT" }
            };
            PrintCollection("Passed a List<Employee>", listEmployees);

            // Pass an Array to the EXACT SAME method:
            Employee[] arrayEmployees = new Employee[]
            {
                new Employee() { Id = 201, Name = "Pooja", Department = "Marketing" },
                new Employee() { Id = 202, Name = "Rohit", Department = "Operations" }
            };
            PrintCollection("Passed an Employee[] Array", arrayEmployees);


            Console.WriteLine("\n===============================================================");
            Console.WriteLine("2. REAL-WORLD USE OF IEnumerable FOR ENCAPSULATION / SECURITY");
            Console.WriteLine("===============================================================");
            Company myCompany = new Company();

            // Caller can read data:
            foreach (var emp in myCompany.Employees)
            {
                Console.WriteLine($"Company Employee: {emp.Name}");
            }
            // myCompany.Employees.Add(...); // ❌ COMPILATION ERROR! Protects your internal list!


            Console.WriteLine("\n===============================================================");
            Console.WriteLine("3. REAL-WORLD USE OF IEnumerator (Parallel / Side-by-Side Traversal)");
            Console.WriteLine("===============================================================");
            // Scenario: Comparing two collections side-by-side (which foreach cannot easily do together)
            List<int> listA = new List<int> { 10, 20, 30 };
            List<int> listB = new List<int> { 15, 20, 25 };

            using (var enumA = listA.GetEnumerator())
            using (var enumB = listB.GetEnumerator())
            {
                while (enumA.MoveNext() && enumB.MoveNext())
                {
                    Console.WriteLine($"Comparing A ({enumA.Current}) vs B ({enumB.Current}) -> Equal? {enumA.Current == enumB.Current}");
                }
            }


            Console.WriteLine("\n===============================================================");
            Console.WriteLine("4. REAL-WORLD USE OF IQueryable (In-Memory Simulation)");
            Console.WriteLine("===============================================================");
            // IQueryable builds expression trees (best for SQL Server / EF Core)
            IQueryable<Employee> queryable = listEmployees.AsQueryable();
            var filtered = queryable.Where(e => e.Department == "IT");

            foreach (var emp in filtered)
            {
                Console.WriteLine($"[IQueryable Filtered] ID: {emp.Id}, Name: {emp.Name}");
            }
        }
    }
}
