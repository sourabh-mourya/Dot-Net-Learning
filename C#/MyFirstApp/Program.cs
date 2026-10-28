using System;
using School;
using College;

namespace MyProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            calc.Accept();
            calc.Add();
            calc.Sub();
            calc.Mul();
            calc.Divide();

            Student s=new Student();
            s.Show();

            Teacher t=new Teacher();
            t.Show();

            Academics.Course a= new Academics.Course();
            a.Show();

        }
    }
}