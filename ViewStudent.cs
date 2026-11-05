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
                    case 2:
                        AddBook();
                        break;
                    case 3:
                        RemoveBook();
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
        private void AddBook()
        {
            Console.WriteLine("Numele cartii:");
            string name = Console.ReadLine();
            bookRepository.Add(student.id, name, DateTime.Now);



        }
        private void RemoveBook()
        {
            Console.WriteLine("Numele cartii:");
            string name = Console.ReadLine();
            bookRepository.Remove(student.id, name, DateTime.Now);
        }



         
    }
}
