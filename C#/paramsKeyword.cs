
using System;

namespace PARAMS_KEYWORD
{
    public class program
    {
        // 'a' fixed parameter hai, nums multiple values accept karega
        public int add(int a, params int[] nums)
        {
            int sum = 0;
            sum += a;

            foreach (int i in nums)
            {
                sum += i;
            }

            return sum;
        }

        // Ye method multiple values directly accept karta hai
        public int add1(params int[] nums)
        {
            int sum = 0;

            foreach (int i in nums)
            {
                sum += i;
            }

            return sum;
        }

        public static void Main(string[] args)
        {
            program p = new program();

            Console.WriteLine(p.add(10, 20, 30, 40, 50));
            Console.WriteLine(p.add1(10, 20, 30, 40, 50));
        }
    }
}
