using online_school.Students;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
namespace online_school 
{
    
    internal class BookRepository
    {
        
      private string filePath;
        private List<Book> books  ;

        public BookRepository()
        {
            this.filePath = Path.Combine(AppContext.BaseDirectory, "Books", "books.txt");
            this.books = new List<Book>();
            Load();
        }

        public void Load()
        {
            books.Clear();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("fisierul nu a fost gasit");
                return;
            }

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                books.Add(new Book(line));
            }
        }
        public List<Book> GetAll()
        {
            return new List<Book>(books);
        }
        public void Write()
        {
            List<string> lines = new List<string>();

            foreach (Book book in books)
            {
                lines.Add(book.ToString());
              


            }

            File.WriteAllLines(filePath, lines);
        }


        public Book? GetById(string id)
        {
            foreach (Book book in books)
            {
                if (book.Id == id)
                {
                    return book;
                }
            }
            return null;
        }
        public Book Add(Book book)
        {
            books.Add(book);
            Write();
            return book;
        }

         public List<Book> GetByStudentId(string studentId)
        {
            List <Book> studentBooks= new List<Book> ();
            foreach(Book book in books)
            {
                if (book.StudentId == studentId)
                {
                    studentBooks.Add(book);
                }
            }
            return studentBooks;
           
        }

    }
}
