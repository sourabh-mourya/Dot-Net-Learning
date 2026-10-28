using System;

namespace DataTypeNamespace
{
    public class DataTypeClass
    {
        public void PDataType()
        {
            int a = 10;
            float b = 23.32F;
            double c = 1234.2342;
            char d = 'a';
            string s = "sourabh";
            bool e = true;

            Console.WriteLine("a={0} b={1} c={2} d={3} s={4} e={5}", a, b, c, d, s, e);
        }

        public void DDataType()
        {
            Int16 a=23;
            Int32 b=78993;
            Int64 c=194010;

            Double d= 1234.234;
            String s="Hello World";


            Console.WriteLine($"a={a} b={b} c={c} d={d} s={s}");
            
        }

        public static void Main(string[] args)
        {
            DataTypeClass obj = new DataTypeClass();
            obj.PDataType();


            obj.DDataType();
        }
    }
}