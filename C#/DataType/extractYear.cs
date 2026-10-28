using System;

namespace yearExtract
{
    
    public class extractYear
    {
        
            public static void Main(String[] args)
        {
            String date1="12052016";
            String date2="13062019";


            int d1=Convert.ToInt32(date1);
            int d2=int.Parse(date2);
            //Both of them were used to convert from string to int i used only for pratice purpose


            int y1=d1%1000;
            int y2=d2%1000;


            Console.WriteLine(y2-y1);
        }

    }
}