using System;
using System.Collections.Generic;
using System.Text;

namespace online_school.Students
{
    internal class Student
    {
        public string id;
        private string firstName;
        private string lastName;
        private string email;
        private int age;
        private string password;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("TEXT NOT NULL");
                }
                firstName = value;
            }

        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("TEXT NOT NULL");
                }
                lastName = value;
            }

        }
        public string Email
        {
            get { return email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("TEXT NOT NULL");
                }
                email = value;
            }

        }
        public int Age
        {
            get { return age; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("INT NOT NULL CHECK (age>0)");
                }
                age = value;
            }

        }
        public string Password
        {
            get { return password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("TEXT NOT NULL");
                }
                password = value;
            }

        }

        public Student(string firstName, string lastName, string email, string password, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password; ;
            Age = age;
        }

        public Student(string line)
        {
            string[] parts = line.Split('|');
            if (parts.Length != 6)
            {
                throw new ArgumentException("Invalid student line: " + line);
            }
            id = parts[0];
            FirstName=parts[1];
            LastName=parts[2];
            Email = parts[3];
            Age = int.Parse(parts[4]);  
            Password = parts[5];
           

        }

        public override string ToString()
        {
            return id + "|" + FirstName + "|" + LastName + "|" + Email + "|" + Age + "|"+ Password;
        }
    }
}
