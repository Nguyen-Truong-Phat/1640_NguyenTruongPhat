using Dal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus
{
    public class StudentService
    {
        public List<Student> GetAll()
        {
            using (StudentModel context = new StudentModel())
            {
                // Sử dụng .Include("Faculty") để lấy kèm tên Khoa hiển thị lên GridView
                return context.Students.Include("Faculty").Include("Major").ToList();
            }
        }

        public List<Student> GetAllHasNoMajor()
        {
            using (StudentModel context = new StudentModel())
            {
                // Lọc những sinh viên có MajorID bằng null
                return context.Students.Where(p => p.MajorID == null).Include("Faculty").ToList();
            }
        }

        public List<Student> GetAllHasNoMajor(int facultyID)
        {
            using (StudentModel context = new StudentModel())
            {
                return context.Students
                              .Where(p => p.MajorID == null && p.FacultyID == facultyID)
                              .ToList();
            }
        }

        public Student FindById(string studentId)
        {
            using (StudentModel context = new StudentModel())
            {
                return context.Students.FirstOrDefault(p => p.StudentID == studentId);
            }
        }

        public void InsertUpdate(Student s)
        {
            using (StudentModel context = new StudentModel())
            {
                context.Students.AddOrUpdate(s);
                context.SaveChanges();
            }
        }

        public void Delete(string studentId)
        {
            using (StudentModel context = new StudentModel())
            {
                var student = context.Students.FirstOrDefault(p => p.StudentID == studentId);
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                }
            }
        }
    }
}
