using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaiPTB2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float a, b, c, delta;
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Nhập giá trị của a =");
            a = float.Parse(Console.ReadLine());
            Console.WriteLine("Nhập giá trị của b =");
            b = float.Parse(Console.ReadLine());
            Console.WriteLine("Nhập giá trị của c =");
            c = float.Parse(Console.ReadLine());

            delta = (b*b) - (4 * a * c);
            if (delta < 0)
            {
                Console.WriteLine("Phương trình vô nghiệm");
            }
            else if (delta == 0)
            {
                float x = -b / (2 * a);
                Console.WriteLine("Phương trình có nghiệm kép x1 = x2 = " + x);
            }
            else 
            { 
                double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                Console.WriteLine("Phương trình có 2 nghiệm phân biệt:");
                Console.WriteLine("x1 = " + x1);
                Console.WriteLine("x2 = " + x2);
            }

                Console.ReadKey();

        }
    }
}
