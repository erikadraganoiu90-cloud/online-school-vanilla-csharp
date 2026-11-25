using online_school.Students;

namespace online_school
{
    internal class ViewStudent
    {
        private const int Width = 64;

        private BookRepository bookRepository;
        private CourseRepository courseRepository;
        private EnrolmentRepository enrolmentRepository;
        private StudentRepository studentRepository;


        private Student student;

        public ViewStudent(Student student)
        {
            this.bookRepository = new BookRepository();
            this.courseRepository = new CourseRepository();
            this.enrolmentRepository = new EnrolmentRepository();
            this.studentRepository = new StudentRepository();

            this.student = student;
            Play();
        }

        private void Play()
        {
            bool running = true;

            while (running)
            {
                Meniu();
                Console.Write("  Alege o optiune: ");

                if (!int.TryParse(Console.ReadLine(), out int alegere))
                {
                    alegere = -1;
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
                    case 8:
                        ShowTotalPayment();
                        break;

                    case 9:
                        ShowProfile();
                        break;

                    default:
                        Error("Optiune invalida. Alege un numar din meniu.");
                        Pause();
                        break;
                }
            }

            Header("La revedere");
            Console.WriteLine("  La revedere, " + student.FirstName + "!");
            Console.WriteLine();
        }

        private void Meniu()
        {
            Header("Meniu");

            Section("CARTI");
            MenuItem(1, "Cartile mele");
            MenuItem(2, "Adauga o carte");
            MenuItem(3, "Sterge o carte");
            Console.WriteLine();

            Section("CURSURI");
            MenuItem(4, "Cursurile mele");
            MenuItem(5, "Cursuri disponibile");
            MenuItem(6, "Inscrie-te la un curs");
            MenuItem(7, "Renunta la un curs");
            MenuItem(8, "Total de plata");
            MenuItem(9, "Profil");
            Console.WriteLine();

            MenuItem(0, "Iesire");
            Console.WriteLine();
        }

        private void ShowBooks()
        {
            Header("Cartile mele");

            List<Book> books = bookRepository.GetByStudentId(student.id);

            if (books.Count == 0)
            {
                Info("Nu ai nicio carte. Adauga una din meniu, optiunea 2.");
                Pause();
                return;
            }

            PrintBooks(books);
            Console.WriteLine();
            Console.WriteLine("  Total: " + books.Count + " carti");
            Pause();
        }

        private void AddBook()
        {
            Header("Adauga o carte");

            Console.Write("  Numele cartii (Enter gol = inapoi): ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            try
            {
                bookRepository.Add(student.id, name.Trim(), DateTime.Now);
                Success("Cartea \"" + name.Trim() + "\" a fost adaugata.");
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }

            Pause();
        }

        private void RemoveBook()
        {
            Header("Sterge o carte");

            List<Book> books = bookRepository.GetByStudentId(student.id);

            if (books.Count == 0)
            {
                Info("Nu ai nicio carte de sters.");
                Pause();
                return;
            }

            PrintBooks(books);

            int number = ReadNumber("Numarul cartii", books.Count);
            if (number == 0)
            {
                return;
            }

            Book selectedBook = books[number - 1];

            if (!Confirm("Stergi cartea \"" + selectedBook.BookName + "\"?"))
            {
                Info("Nu s-a sters nimic.");
                Pause();
                return;
            }

            try
            {
                bookRepository.Remove(student.id, selectedBook.BookName, DateTime.Now);
                Success("Cartea \"" + selectedBook.BookName + "\" a fost stearsa.");
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }

            Pause();
        }

        private void ShowCourses()
        {
            Header("Cursurile mele");

            List<Enrolment> enrolments = enrolmentRepository.GetByStudentId(student.id);

            if (enrolments.Count == 0)
            {
                Info("Nu esti inscris la niciun curs. Vezi optiunea 5.");
                Pause();
                return;
            }

            PrintEnrolments(enrolments);
            Console.WriteLine();
            Console.WriteLine("  Total: " + enrolments.Count + " cursuri");
            Pause();
        }

        private List<Course> GetAvailableCourses()
        {
            List<Course> courses = courseRepository.GetAll();
            List<Enrolment> enrolments = enrolmentRepository.GetByStudentId(student.id);
            List<Course> availableCourses = new List<Course>();

            foreach (Course course in courses)
            {
                bool isEnrolled = false;

                foreach (Enrolment enrolment in enrolments)
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
            Header("Cursuri disponibile");

            List<Course> availableCourses = GetAvailableCourses();

            if (availableCourses.Count == 0)
            {
                Info("Esti deja inscris la toate cursurile.");
                Pause();
                return;
            }

            PrintCourses(availableCourses);
            Pause();
        }

        private void Enrol()
        {
            Header("Inscrie-te la un curs");

            List<Course> availableCourses = GetAvailableCourses();

            if (availableCourses.Count == 0)
            {
                Info("Esti deja inscris la toate cursurile.");
                Pause();
                return;
            }

            PrintCourses(availableCourses);

            int number = ReadNumber("Numarul cursului", availableCourses.Count);
            if (number == 0)
            {
                return;
            }

            Course selectedCourse = availableCourses[number - 1];

            if (!Confirm("Te inscrii la \"" + selectedCourse.Name + "\" (" + selectedCourse.Price + " lei)?"))
            {
                Info("Nu s-a facut nicio inscriere.");
                Pause();
                return;
            }

            try
            {
                Enrolment enrolment = new Enrolment(student.id, selectedCourse.Id);
                enrolmentRepository.Add(enrolment);
                Success("Te-ai inscris la \"" + selectedCourse.Name + "\".");
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
            int numberOfStudents = enrolmentRepository.GetStudentsCountByCourseId(selectedCourse.Id);
            if (numberOfStudents >= selectedCourse.MaxStudents)
            {
                Console.WriteLine( "Cursul este plin." );
                return;
            }

            Pause();

            if (selectedCourse.StartDate.Date < DateTime.Today)
            {
                Console.WriteLine("Cursul a inceput deja.");
            }
        }

        private void LeaveCourse()
        {
            Header("Renunta la un curs");

            List<Enrolment> enrolments = enrolmentRepository.GetByStudentId(student.id);

            if (enrolments.Count == 0)
            {
                Info("Nu esti inscris la niciun curs.");
                Pause();
                return;
            }

            PrintEnrolments(enrolments);

            int number = ReadNumber("Numarul cursului", enrolments.Count);
            if (number == 0)
            {
                return;
            }

            Enrolment selectedEnrolment = enrolments[number - 1];
            string courseName = CourseName(selectedEnrolment.CourseId);

            if (!Confirm("Renunti la \"" + courseName + "\"?"))
            {
                Info("Esti in continuare inscris.");
                Pause();
                return;
            }

            try
            {
                enrolmentRepository.Remove(student.id, selectedEnrolment.CourseId);
                Success("Ai renuntat la \"" + courseName + "\".");
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }

            Pause();
        }

        private void PrintBooks(List<Book> books)
        {
            Console.WriteLine("  " + Cell("#", 4) + Cell("Carte", 40) + "Adaugata la");
            Line('-');

            for (int i = 0; i < books.Count; i++)
            {
                Book book = books[i];
                Console.WriteLine("  " + Cell((i + 1) + ".", 4) + Cell(book.BookName, 40) + book.CreatedAt.ToString("yyyy-MM-dd"));
            }
        }

        private void PrintCourses(List<Course> courses)
        {
            Console.WriteLine("  " + Cell("#", 4) + Cell("Curs", 32) + Cell("Incepe", 12) + "Pret");
            Line('-');

            for (int i = 0; i < courses.Count; i++)
            {
                Course course = courses[i];
                Console.WriteLine("  " + Cell((i + 1) + ".", 4) + Cell(course.Name, 32) + Cell(course.StartDate.ToString("yyyy-MM-dd"), 12) + course.Price + " lei");
            }
        }

        private void PrintEnrolments(List<Enrolment> enrolments)
        {
            Console.WriteLine("  " + Cell("#", 4) + Cell("Curs", 32) + Cell("Incepe", 12) + "Inscris la");
            Line('-');

            for (int i = 0; i < enrolments.Count; i++)
            {
                Enrolment enrolment = enrolments[i];
                Course? course = courseRepository.GetById(enrolment.CourseId);
                string startDate = course == null ? "-" : course.StartDate.ToString("yyyy-MM-dd");

                Console.WriteLine("  " + Cell((i + 1) + ".", 4) + Cell(CourseName(enrolment.CourseId), 32) + Cell(startDate, 12) + enrolment.CreatedAt.ToString("yyyy-MM-dd"));
            }
        }

        private string CourseName(string courseId)
        {
            Course? course = courseRepository.GetById(courseId);

            if (course == null)
            {
                return "(curs sters)";
            }

            return course.Name;
        }

        private void Header(string title)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            string name = student.FirstName + " " + student.LastName;

            Line('=');
            Console.WriteLine("  ONLINE SCHOOL" + name.PadLeft(Width - 15));
            Line('=');
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  " + title);
            Console.ResetColor();
            Console.WriteLine();
        }

        private void Section(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + title);
            Console.ResetColor();
        }

        private void MenuItem(int number, string text)
        {
            Console.WriteLine("   " + number + "  " + text);
        }

        private void Line(char character)
        {
            Console.WriteLine(new string(character, Width));
        }

        private string Cell(string text, int width)
        {
            if (text.Length >= width)
            {
                return text.Substring(0, width - 4) + "... ";
            }

            return text.PadRight(width);
        }

        private int ReadNumber(string prompt, int max)
        {
            Console.WriteLine();

            while (true)
            {
                Console.Write("  " + prompt + " (1-" + max + ", 0 = inapoi): ");

                if (int.TryParse(Console.ReadLine(), out int number) && number >= 0 && number <= max)
                {
                    return number;
                }

                Error("Scrie un numar intre 0 si " + max + ".");
            }
        }

        private bool Confirm(string question)
        {
            Console.Write("  " + question + " (d/n): ");
            string? answer = Console.ReadLine();

            return answer != null && answer.Trim().ToLower() == "d";
        }

        private void Success(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  OK  " + message);
            Console.ResetColor();
        }

        private void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  !!  " + message);
            Console.ResetColor();
        }

        private void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  " + message);
            Console.ResetColor();
        }

        private void Pause()
        {
            Console.WriteLine();
            Console.Write("  Apasa Enter pentru meniu...");
            Console.ReadLine();
        }
        private void ShowTotalPayment()
        {
            var enrolments = enrolmentRepository.GetByStudentId(student.id);
            decimal total = 0m;

            foreach (var enrolment in enrolments)
            {
                var course = courseRepository.GetById(enrolment.CourseId);

                if (course != null)
                {
                    total = total + course.Price;
                }
            }

            Console.WriteLine("Total de plata: " + total.ToString("F2") + " lei");
        }

        private void ShowProfile()
        {
            Console.WriteLine("ID: " + student.id);
            Console.WriteLine("Nume: " + student.FirstName + " " + student.LastName);
            Console.WriteLine("Email: " + student.Email);


            Console.Write("Doresti sa schimbi parola? (da/nu): ");
            string answer = Console.ReadLine();

            if (!string.Equals(answer, "da", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            Console.Write("Parola noua: ");
            string newPassword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                Console.WriteLine("Parola nu poate fi goala.");
                return;
            }

            student.Password = newPassword;
            studentRepository.Update(student);


            Console.WriteLine("Parola a fost schimbata.");
        }

    }
}
