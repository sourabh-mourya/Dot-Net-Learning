using System;


namespace accessSpecifiers
{
    

        class Access
    {
        
        protected void show()
        {
            Console.WriteLine("Protected method is calling");
        }

        private void show1()
        {
            Console.WriteLine("Private Method is calling ");
        }


        public static void Main(String[] args)
        {
            Access a = new Access();
            a.show1();
        }





    }

    // class Program :Access
    // {
        
    //     public static void Main()
    //     {

    //             Access a = new Access();
    //             // a.show();
    //             Program p = new Program();
    //             p.show();
    //             p.show1();
    //     }
    // }

}