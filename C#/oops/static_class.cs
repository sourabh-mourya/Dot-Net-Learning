using System;

namespace STATIC_CLASS
{

    static class Product
    {

        public static int productID;
        public static string productName;


        static Product()
        {
            productID = 10;
            productName = "hello world";
        }

        public static void showData()
        {
            Console.WriteLine("The product ID ={0} and Product Name = {1}", productID, productName);
        }

    }

    public class Program
    {
        public static void Main(String[] args)
        {
            Product.showData();
        }
    }
}