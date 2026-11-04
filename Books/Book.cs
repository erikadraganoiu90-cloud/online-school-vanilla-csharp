using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace online_school
{
    public class Book
    {
        private string id=string.Empty;
        private string studentId= string.Empty;
        private string bookName= string.Empty;
        private DateTime createdAt;

        public Book(string id,string studentId,string bookName, DateTime createdAt)
        {
            Id = id;
            StudentId = studentId;
            BookName = bookName;
            CreatedAt = DateTime.Now;
        }

        public Book(string line)
        {
            string[] parts = line.Split('|');

            if (parts.Length != 4)
            {
                throw new ArgumentException("Invalid course line: " + line);
            }

            Id= parts[0];
            StudentId = parts[1];
            BookName=parts[2];
            CreatedAt  = DateTime.ParseExact(parts[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        public string Id
        {
            get {  return id; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("TEXT NOT NULL");
                }
                id = value;
            }
        }

        public string StudentId
        {
            get { return studentId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("TEXT NOT NULL");
                }
                studentId = value;
            }
        }
        public string BookName
        {
            get { return bookName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("TEXT NOT NULL");
                }
                bookName = value;
            }
        }


        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }
        public override string ToString()
        {
            return Id+"|"+StudentId+"|"+BookName+"|"+CreatedAt.ToString("yyyy-MM-dd");
        }

    }
}
