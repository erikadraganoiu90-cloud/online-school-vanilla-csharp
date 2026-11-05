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
            this.filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Books", "books.txt");
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
        public Book Add(string studentId,string bookName,DateTime createdAt)
        {
            //conditii de existenta 

            //verificam unicitatea cartii
            List<Book> stundentBooks = this.GetByStudentId(studentId);

            foreach(Book book in  stundentBooks)
            {
                if (book.BookName.Equals(bookName))
                {
                    throw new ArgumentException("Book with same name already assigned to you");
                }
            }
          
            Book b = new Book(Guid.NewGuid().ToString(),studentId,bookName,createdAt);

            books.Add(b);

            Write();


            return b;



  
        }

         public List<Book> GetByStudentId(string studentId)
        {
            List <Book> studentBooks= new List<Book> ();
            foreach(Book book in books)
            {
                if (book.StudentId.Equals(studentId))
                {
                    studentBooks.Add(book);
                }
            }
            return studentBooks;
           
        }

        public Book Remove(string studentId, string bookName, DateTime createdAt)
        {
            List<Book> studentBooks = this.GetByStudentId(studentId);
            foreach(Book book in studentBooks)
            {
                if (book.BookName.Equals(bookName))
                {
                    books.Remove(book);
                    Write();
                    return book;
                }
            }
            throw new ArgumentException("Book not fund for this student");
        }


    }
}
