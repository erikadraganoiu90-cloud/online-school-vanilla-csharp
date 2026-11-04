using online_school.Students;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace online_school
{
    internal class ViewStudent
    {
        private BookRepository bookRepository;


        private Student student;




        public ViewStudent(Student student)
        {
            this.bookRepository = new BookRepository();

            this.student = student;
            Play();

        }


        private void Meniu()
        {
            Console.WriteLine("1->show books");
            Console.WriteLine("2->add  book");
            Console.WriteLine("3->remove book");

        }


        private void Play()
        {
            bool running = true;

            int alegere = -1;
            while (running)
            {
                Meniu();

                alegere = Int32.Parse(Console.ReadLine());

                switch (alegere)
                {

                    case 1:
                        ShowBooks();
                        break;
                    default:
                        Console.WriteLine("invalid value");
                        break;
                     

                }
            }



        }

        private void  ShowBooks()
        {
            Console.WriteLine("Books:");
            foreach (Book book in bookRepository.GetByStudentId(student.id))
            {
                Console.WriteLine(book);
            }

        }

        private void TestAddBook(Book book)
        {
            bookRepository.Add(book);
            Book savedBook = bookRepository.GetById(book.Id);
            if (savedBook != null)
            {
                Console.WriteLine("Carte salvata");
            }
            else
            {
                Console.WriteLine("Carte nesalvata");
            }
        }
    }
}
