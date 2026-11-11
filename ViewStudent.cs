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
        private CourseRepository courseRepository;
        private EnrolmentRepository enrolmentRepository;

        private Student student;




        public ViewStudent(Student student)
        {
            this.bookRepository = new BookRepository();
            this.courseRepository = new CourseRepository();
            this.enrolmentRepository = new EnrolmentRepository();
            this.student = student;
            Play();

        }
         


        private void Meniu()
        {
            Console.WriteLine($"Salut, {student.FirstName} {student.LastName}!");
            Console.WriteLine("0 -> exit");
            Console.WriteLine("1->show books");
            Console.WriteLine("2->add  book");
            Console.WriteLine("3->remove book");
            Console.WriteLine("4->show courses");
            Console.WriteLine("5->show available courses");
            Console.WriteLine("6 -> enrol");
            Console.WriteLine("7 -> leave course");


        }


        private void Play()
        {
            bool running = true;

            int alegere = -1;
            while (running)
            {
                Meniu();

                if (!int.TryParse(Console.ReadLine(), out  alegere))
                {
                    Console.WriteLine("invalid value");
                    continue;
                }

                switch (alegere)
                {
                    case 0:
                        running = false;
                        break;
                    case 1:
                        ShowBooks();
                        break;
                    case 2:
                        AddBook();
                        break;
                    case 3:
                        RemoveBook();
                        break;
                    case 4:
                        ShowCourses();
                        break;
                    case 5:
                        ShowAvailableCourses();
                        break;
                    case 6:
                        Enrol();
                        break;
                    case 7:
                        LeaveCourse();
                        break;


                    default:
                        Console.WriteLine("invalid value");
                        break;
                    
                     

                }
            }
            

        }

        private void ShowBooks()
        {
            var books = bookRepository.GetByStudentId(student.id);
            int number = 0;

            foreach (Book book in books)
            {
                number++;
                Console.WriteLine(number + ". " + book.BookName + " (" + book.CreatedAt.ToString("yyyy-MM-dd") + ")");

            }

            if (number == 0)
            {
                Console.WriteLine("Nu ai nicio carte");
                return;
            }

            Console.WriteLine("Total: " + number);

        }

        private void AddBook()
        {
            Console.WriteLine("Numele cartii:");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Numele cartii nu poate fi gol.");
                return;
            }

            try
            {
                bookRepository.Add(student.id, name, DateTime.Now);
                Console.WriteLine("Cartea a fost adaugata.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void RemoveBook()
        {
            Console.WriteLine("Numele cartii:");
            string name = Console.ReadLine();

            try
            {
                bookRepository.Remove(student.id, name, DateTime.Now);
                Console.WriteLine("Cartea a fost stearsa.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void ShowCourses()
        {
            var enrolments = enrolmentRepository.GetByStudentId(student.id);


            if (enrolments.Count == 0)
            {
                Console.WriteLine("Nu esti inscris la niciun curs");
                return;
            }

            foreach (var enrolment in enrolments)
            {
                var course = courseRepository.GetById(enrolment.CourseId);

                Console.WriteLine(course.Name + " | " + course.StartDate + " | " + enrolment.CreatedAt);




            }
        }
        private List<Course> GetAvailableCourses()
        {
            var courses = courseRepository.GetAll();
            var enrolments = enrolmentRepository.GetByStudentId(student.id);
            var availableCourses = new List<Course>();

            foreach (Course course in courses)
            {
                bool isEnrolled = false;

                foreach (var enrolment in enrolments)
                {
                    if (enrolment.CourseId == course.Id)
                    {
                        isEnrolled = true;
                        break;
                    }
                }

                if (!isEnrolled)
                {
                    availableCourses.Add(course);
                }
            }

            return availableCourses;
        }

        private void ShowAvailableCourses()
        {
            var availableCourses = GetAvailableCourses();

            if (availableCourses.Count == 0)
            {
                Console.WriteLine("Nu exista cursuri disponibile.");
                return;
            }

            for (int i = 0; i < availableCourses.Count; i++)
            {
                Course course = availableCourses[i];
                Console.WriteLine((i + 1) + ". " + course.Name + " - " + course.Price + " lei");

            }
        }

        private void Enrol()
        {
            var availableCourses = GetAvailableCourses();

            if (availableCourses.Count == 0)
            {
                Console.WriteLine("Nu exista cursuri disponibile.");
                return;
            }

            for (int i = 0; i < availableCourses.Count; i++)
            {
                Course course = availableCourses[i];
                Console.WriteLine((i + 1) + ". " + course.Name + " - " + course.Price + " lei");

            }

            Console.WriteLine("Numarul cursului:");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int number) ||
                number < 1 ||
                number > availableCourses.Count)
            {
                Console.WriteLine("Numar invalid.");
                return;
            }

            Course selectedCourse = availableCourses[number - 1];

            try
            {
                var enrolment = new Enrolment(student.id, selectedCourse.Id);
                enrolmentRepository.Add(enrolment);
                Console.WriteLine("Inscriere reusita.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void LeaveCourse()
        {
            var enrolments = enrolmentRepository.GetByStudentId(student.id);

            if (enrolments.Count == 0)
            {
                Console.WriteLine("Nu esti inscris la niciun curs.");
                return;
            }

            for (int i = 0; i < enrolments.Count; i++)
            {
                var enrolment = enrolments[i];
                var course = courseRepository.GetById(enrolment.CourseId);

                Console.WriteLine((i + 1) + ". " + course.Name + " | Inceput: " + course.StartDate + " | " +"Inscriere: " + enrolment.CreatedAt);

            }

            Console.WriteLine("Numarul cursului:");

            if (!int.TryParse(Console.ReadLine(), out int number) ||
                number < 1 ||
                number > enrolments.Count)
            {
                Console.WriteLine("Numar invalid.");
                return;
            }

            var selectedEnrolment = enrolments[number - 1];

            try
            {
                enrolmentRepository.Remove(student.id, selectedEnrolment.CourseId);
                Console.WriteLine("Inscrierea a fost eliminata.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }






    }
}
