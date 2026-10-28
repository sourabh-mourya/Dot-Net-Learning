using System;
using System.IO;


namespace DELETE_FILE
{

    public class Program
    {
        public static void Main(String[] args)
        {

            string path = "data2.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine("File Delete is succcessfully.......");
            }
            else
            {
                Console.WriteLine("File Does not exits....");
            }
        }
    }
}