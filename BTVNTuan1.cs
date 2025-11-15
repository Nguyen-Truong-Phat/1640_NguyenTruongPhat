using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTVN1
{
    class Bac1
    {
        protected float a, b;
        public Bac1(float a, float b)
        {
            this.a = a;
            this.b = b;
        }
        public virtual void implement()
        {
            if(a == 0)
            {
                Console.WriteLine("Phương trình vô nghiệm");
            }
            else
            {
                float x = -b / a;
                Console.WriteLine("Nghiệm của phương trình là: x = " + x);
            }
        }
    }
    class Bac2 : Bac1
    {
        private float c;
        public Bac2(float a, float b, float c) : base(a, b)
        {
            this.c = c;
        }
        public override void implement()
        {
            if (a == 0)
            {
                Bac1 pt1 = new Bac1(b, c);
                Console.Write("Phương trình trở thành bậc 1: ");
                pt1.implement();
            }
            else
            {
                float delta = (b * b) - (4 * a * c);
                if (delta < 0)
                    Console.WriteLine("Phương trình vô nghiệm.");
                else if (delta == 0)
                {
                    float x = -b / (2 * a);
                    Console.WriteLine("Phương trình có nghiệm kép: x1 = x2 = " + x);
                }
                else
                {
                    double x1 = ((-b + Math.Sqrt(delta)) / (2 * a));
                    double x2 = ((-b - Math.Sqrt(delta)) / (2 * a));
                    Console.WriteLine("Phương trình có hai nghiệm phân biệt:\n x1 = " + x1 + "\n x2 = " + x2);
                }
            }
        }
    }
    internal class BTVNTuan1
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine("Giải phương trình bậc 2: ax^2 + bx + c = 0");
            Console.Write("Nhập liệu cho biến a: ");
            float a = float.Parse(Console.ReadLine());
            Console.Write("Nhập liệu cho biến b: ");
            float b = float.Parse(Console.ReadLine());
            Console.Write("Nhập liệu cho biến c: ");
            float c = float.Parse(Console.ReadLine());
            var ptb2 = new Bac2(a,b,c);
            ptb2.implement();

            Console.ReadKey();
        }
    }
}
