using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace BaiTap2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<Student> students = new List<Student>
            {
                new Student(1, "An", 15),
                new Student(2, "Binh", 18),
                new Student(3, "Cuong", 14),
                new Student(4, "Ai", 17),
                new Student(5, "Dung", 19),
                new Student(6, "Anh", 16)
            };

            // a. In danh sách toàn bộ danh sách học sinh
            Console.WriteLine("a. Toàn bộ danh sách học sinh:");
            foreach (var s in students)
                Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}");

            // b. Tìm và in học sinh có tuổi từ 15 đến 18
            Console.WriteLine("\nb. Học sinh có tuổi từ 15 đến 18:");
            var filteredByAge = students.Where(s => s.Age >= 15 && s.Age <= 18);
            foreach (var s in filteredByAge)
                Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}");

            // c. Tìm và in học sinh có tên bắt đầu bằng 'A'
            Console.WriteLine("\nc. Học sinh có tên bắt đầu bằng chữ 'A':");
            var filteredByName = students.Where(s => s.Name.StartsWith("A"));
            foreach (var s in filteredByName)
                Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}");

            // d. Tính tổng tuổi của tất cả học sinh
            Console.WriteLine("\nd. Tổng tuổi của tất cả học sinh:");
            int totalAge = students.Sum(s => s.Age);
            Console.WriteLine($"Tổng tuổi: {totalAge}");

            // e. Tìm và in học sinh có tuổi lớn nhất
            Console.WriteLine("\ne. Học sinh có tuổi lớn nhất:");
            int maxAge = students.Max(s => s.Age);
            var oldest = students.Where(s => s.Age == maxAge);
            foreach (var s in oldest)
                Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}");

            // f. Sắp xếp học sinh theo tuổi tăng dần và in ra
            Console.WriteLine("\nf. Danh sách học sinh theo tuổi tăng dần:");
            var sortedByAge = students.OrderBy(s => s.Age);
            foreach (var s in sortedByAge)
                Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}");
        }
    }
}
