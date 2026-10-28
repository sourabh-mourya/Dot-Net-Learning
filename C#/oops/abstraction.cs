using System;
using System.Security.Cryptography.X509Certificates;

namespace ABSTRACTION
{

    class Employee
    {

        public int EmpId;
        public string EmpName;
        public double GrossPay;

        double TaxDeduction = 0.1;//10% ur ye private variable hi kyuki iske agge koi specifer nhi laga hi 
        double netSalary;

        public Employee(int EmpId, string EmpName, double GrossPay)
        {
            this.EmpId = EmpId;
            this.EmpName = EmpName;
            this.GrossPay = GrossPay;
        }

        //ab salary calculate krunga and wo private rhegi 

        void calculateSalary()
        {
            if (GrossPay >= 20000)
            {
                netSalary = GrossPay - (TaxDeduction * GrossPay);
                Console.WriteLine("Your Salary is {0}", netSalary);
            }
            else
            {
                Console.WriteLine("Your Salary is {0}", netSalary);

            }


        }
        public void showEmployeeDetails()
        {
            Console.WriteLine("Employee ID is {0}",this.EmpId);
            Console.WriteLine("Employee Name is {0}",this.EmpName);
            this.calculateSalary();//uss function ko mene yaha pr call krwa jisske method hide apne isko call krege to showEmployee details ko to caculate salary method wo call kr dega automatic.

        }

    }


    class Program
    {
        public static void Main(String[] args)
        {
                Employee hello = new Employee(101,"hello world",45000);
                hello.showEmployeeDetails();
        }
    }




}