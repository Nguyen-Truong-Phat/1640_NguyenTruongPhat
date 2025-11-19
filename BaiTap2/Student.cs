using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap2
{
    public class Student
    {
        public int Id { get; set; }         // Mã số học sinh
        public string Name { get; set; }    // Tên học sinh
        public int Age { get; set; }        // Tuổi học sinh

        public Student(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
