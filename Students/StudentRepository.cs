using System;
using System.Collections.Generic;
using System.Text;

namespace online_school.Students
{
    internal class StudentRepository
    {

        private string filePath;
        private List<Student> students;

      public StudentRepository()
        {
            this.filePath = Path.Combine(AppContext.BaseDirectory, "Students", "student.txt");
            this.students=new List<Student>();
            Load();
        }
        public void Load()
        {
            students.Clear();
            if (!File.Exists(filePath))
            {
                return;
            }
            foreach(string line in File.ReadAllLines(filePath))
            {
                if(string.IsNullOrWhiteSpace(line)) continue;
                students.Add(new Student(line));
            }
        }



        public void Afisare()
        {
            foreach (Student student in students)
            {
                Console.WriteLine(student);
            }
        }
       
            
    }
}
