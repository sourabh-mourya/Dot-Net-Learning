using System;
using System.Globalization;

namespace METHOD_OVERRIDING
{

    public class parent
    {

        public virtual void show()
        {
            Console.WriteLine("Parent class function is calling ....");
        }
    }


    public class child : parent
    {
        public override void show()
        {
            Console.WriteLine("Child class function is calling .....");
        }

    }


    public class Program
    {
        public static void Main(String[] args)
        {

         child childObject=new child();
         childObject.show();

         parent childObjectParentReference = new child();
         childObjectParentReference.show();//child class ka function call kyuki ye jakar wha dekhta virtual keyword hi then wo child class me jakar dekhega ki iss method ko kisne override kiya usko call kr dega

         
          
        }
    }


}