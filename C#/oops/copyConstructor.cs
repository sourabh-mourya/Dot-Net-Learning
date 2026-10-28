using System;


namespace Copy_Constructor
{
    public class Example
    {

        int age;
        string name;

        public Example(int age, String name)
        {
            this.age = age;
            this.name = name;
        }

        public Example(Example e)
        {
            Console.WriteLine("The Copy construtor is  calling ..... ");
            this.age = e.age;
            this.name = e.name;
        }

        public void showData()
        {
            Console.WriteLine("The value of age = {0}  name = {1}", age, name);
        }

        public static void Main(String[] args)
        {
            Example e = new Example(10, "Hello world");
            e.showData();
            Example e1 = new Example(e);

            e1.showData();
        }

    }
}