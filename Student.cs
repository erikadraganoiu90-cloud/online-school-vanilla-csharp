using System;
using System.Collections.Generic;
using System.Text;

namespace online_school
{
    internal class Student
    {
        private string id;
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
                }firstName=value ;
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



    }
}
