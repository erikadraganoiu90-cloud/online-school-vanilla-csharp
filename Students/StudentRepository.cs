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
            this.filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Students", "students.txt");
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

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                students.Add(new Student(line));
            }
        }

        public void Save()
        {
            List<string> lines = new List<string>();

            foreach (Student student in students)
            {
                lines.Add(student.ToString());
            }

            File.WriteAllLines(filePath, lines);
        }

        public List<Student> GetAll()
        {
            return new List<Student>(students);
        }

        public Student GetById(string id)
        {
            foreach (Student student in students)
            {
                if (student.id == id)
                {
                    return student;
                }
            }
            return null;
        }

        public void Add(Student student)
        {
            students.Add(student);
            Save();
        }

        public bool Update(Student student)
        {
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].id == student.id)
                {
                    students[i] = student;
                    Save();
                    return true;
                }
            }
            return false;
        }

        public bool Delete(string id)
        {
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].id == id)
                {
                    students.RemoveAt(i);
                    Save();
                    return true;
                }
            }
            return false;
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


 