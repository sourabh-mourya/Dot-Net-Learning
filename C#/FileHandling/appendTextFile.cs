using System;
using System.IO;


namespace AppendText
{
    
    public class Program
    {
        public static void Main(String[] args)
        {
            
            string path="data.txt";
            
            File.AppendAllText(path,"New Data is append from appendAllText function ");


            Console.WriteLine("Append data in file is successfully .....");



        }
    }
}