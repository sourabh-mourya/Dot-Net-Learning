
using System;
using System.Collections;

namespace non_generic_collections
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ================= ARRAYLIST =================
            Console.WriteLine("\n----- ArrayList -----");

            ArrayList arrayList = new ArrayList();

            arrayList.Add(10);                  // Element add
            arrayList.Add(20);
            arrayList.Add(30);
            arrayList.Add("Hello");
            arrayList.Add(true);

            arrayList.AddRange(new object[] { 40, 50 }); // Multiple elements add
            arrayList.Insert(0, "First");       // Specific index par add
            arrayList.InsertRange(1, new object[] { "A", "B" });

            arrayList.Remove("Hello");          // Specific value remove
            arrayList.RemoveAt(0);              // Specific index remove
            arrayList.RemoveRange(0, 2);        // Index 0 se 2 elements remove

            Console.WriteLine(arrayList.Contains(10)); // Value present hai?
            Console.WriteLine(arrayList.IndexOf(20));  // Value ka index
            Console.WriteLine("Count: " + arrayList.Count);
            Console.WriteLine("Capacity: " + arrayList.Capacity);

            arrayList.Reverse();                // Order reverse
            arrayList.Reverse();                // Original order wapas

            // Sort tab use karo jab elements mutually comparable hon.
            ArrayList nums = new ArrayList { 30, 10, 20 };
            nums.Sort();                        // Ascending order
            nums.Reverse();                     // Descending order

            object[] arr = arrayList.ToArray(); // Array mein convert
            arrayList.CopyTo(arr, 0);            // Copy into array of sufficient size

            foreach (object item in arrayList)
                Console.WriteLine(item);

            arrayList.Clear();                  // Saare elements remove


            // ================= HASHTABLE =================
            Console.WriteLine("\n----- Hashtable -----");

            Hashtable ht = new Hashtable();

            ht.Add(1, "World");                 // Key-value pair add
            ht.Add(2, "Hello");
            ht["name"] = "Sourabh";             // Add ya existing value update
            ht[1] = "Updated World";            // Existing key ki value update

            Console.WriteLine(ht[1]);           // Key se value access
            Console.WriteLine(ht.ContainsKey(1));    // Key exists?
            Console.WriteLine(ht.ContainsValue("Hello")); // Value exists?
            Console.WriteLine("Count: " + ht.Count);

            foreach (DictionaryEntry item in ht)
                Console.WriteLine($"Key = {item.Key}, Value = {item.Value}");

            Console.WriteLine("Only Keys:");
            foreach (object key in ht.Keys)
                Console.WriteLine(key);

            Console.WriteLine("Only Values:");
            foreach (object value in ht.Values)
                Console.WriteLine(value);

            ht.Remove(2);                       // Key 2 remove
            ht.Clear();                         // Saare pairs remove


            // ================= STACK =================
            Console.WriteLine("\n----- Stack -----");

            Stack st = new Stack();

            st.Push(10);                        // Element add
            st.Push("Two");
            st.Push(30);

            Console.WriteLine("Peek: " + st.Peek()); // Top element, remove nahi hoga
            Console.WriteLine("Contains: " + st.Contains(10));
            Console.WriteLine("Count: " + st.Count);

            object top = st.Pop();              // Top element remove aur return
            Console.WriteLine("Popped: " + top);

            foreach (object item in st)
                Console.WriteLine(item);        // Top se bottom tak

            object[] stackArray = st.ToArray(); // Array mein convert
            st.Clear();                         // Stack empty


            // ================= QUEUE =================
            Console.WriteLine("\n----- Queue -----");

            Queue q = new Queue();

            q.Enqueue(10);                      // Element add
            q.Enqueue("Twenty");
            q.Enqueue(30);

            Console.WriteLine("Peek: " + q.Peek()); // Front element, remove nahi hoga
            Console.WriteLine("Contains: " + q.Contains(10));
            Console.WriteLine("Count: " + q.Count);

            object removed = q.Dequeue();      // Front element remove aur return
            Console.WriteLine("Dequeued: " + removed);

            foreach (object item in q)
                Console.WriteLine(item);        // Front se rear tak

            object[] queueArray = q.ToArray();  // Array mein convert
            q.Clear();                          // Queue empty


            // ================= SORTEDLIST =================
            Console.WriteLine("\n----- SortedList -----");

            SortedList sl = new SortedList();

            sl.Add(3, "Charlie");               // Key-value pair add
            sl.Add(1, "Alice");
            sl.Add(2, "Bob");

            sl[4] = "David";                    // Add ya update by key

            Console.WriteLine(sl[1]);           // Key 1 ki value
            Console.WriteLine(sl.ContainsKey(2));   // Key exists?
            Console.WriteLine(sl.ContainsValue("Bob")); // Value exists?
            Console.WriteLine("Count: " + sl.Count);

            Console.WriteLine("Key at index 0: " + sl.GetKey(0));
            Console.WriteLine("Value at index 0: " + sl.GetByIndex(0));
            Console.WriteLine("Index of key 3: " + sl.IndexOfKey(3));
            Console.WriteLine("Index of value Bob: " + sl.IndexOfValue("Bob"));

            foreach (DictionaryEntry item in sl)
                Console.WriteLine($"Key = {item.Key}, Value = {item.Value}");

            Console.WriteLine("Only Keys:");
            foreach (object key in sl.Keys)
                Console.WriteLine(key);

            Console.WriteLine("Only Values:");
            foreach (object value in sl.Values)
                Console.WriteLine(value);

            sl.Remove(2);                       // Key 2 remove
            sl.RemoveAt(0);                     // Index 0 remove
            sl.Clear();                         // Saare pairs remove
        }
    }
}
