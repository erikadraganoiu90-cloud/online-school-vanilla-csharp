using online_school.Students;

namespace online_school
{
    internal class App
    {
        static void Main(string[] args)
        {


            StudentRepository studentRepository = new StudentRepository();

            while (true)
            {
                Console.Write("Email: ");
                string email = Console.ReadLine();

                Student student = studentRepository.GetByEmail(email);

                if (student == null)
                {
                    Console.WriteLine("Nu exista un student cu acest email.");
                    continue;
                }

                new ViewStudent(student);
                break;
            }



        }
    }
}
