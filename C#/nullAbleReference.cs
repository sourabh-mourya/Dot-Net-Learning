
#nullable enable

using System;
using System.Collections.Generic;

namespace NullableReferenceTypes
{
    public class Book
    {
        // Nullable properties: null value allowed
        public string? Title { get; set; }

        public List<int>? Authors { get; set; }

        // Non-nullable property
        public string Name { get; set; } = "Unknown";

        // Method can return a string or null
        public string? GetData()
        {
            return null;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Non-nullable reference type
            string name = "Sourabh";

            // Nullable reference type
            string? title = null;

            Console.WriteLine("Name: " + name);
            Console.WriteLine("Title: " + title);

            // Null-coalescing operator (??)
            string result = title ?? "No title available";
            Console.WriteLine("Result: " + result);

            // Create Book object
            Book book = new Book();

            book.Title = "C# Programming";
            book.Authors = new List<int> { 101, 102, null };
            book.Name = "My Book";

            Console.WriteLine("Book Name: " + book.Name);
            Console.WriteLine("Book Title: " + book.Title);

            // Check nullable list before accessing it
            if (book.Authors != null)
            {
                foreach (int author in book.Authors)
                {
                    Console.WriteLine("Author ID: " + author);
                }
            }
            else
            {
                Console.WriteLine("Null value of that author",book.Authors);
            }

            // Method returning null
            string? data = book.GetData();

            // Provide fallback value if data is null
            string finalData = data ?? "No data found";
            Console.WriteLine("Data: " + finalData);
        }
    }
}
