using System;

namespace METHOD_HIDING
{

    public class ParentClass
    {
        public void show()
        {
            Console.WriteLine("Parent class function is callingggggggg .....");
        }
    }

    public class ChildClass : ParentClass
    {
        public new void show()
        {
            Console.WriteLine("Child Class function is callinggggg ........");
        }
    }


    public class Program
    {
        public static void Main(String[] args)
        {

            ChildClass cc = new ChildClass();
            ((ParentClass)cc).show();//calling the parent class function
            cc.show();//calll the child class function 

            ParentClass cc1 = new ChildClass();
            cc1.show();//calling the parent class function
            //isse whi method caal parent class me se jo ki apne defined krke rkhi hi chlid class me 



        }
    }
}