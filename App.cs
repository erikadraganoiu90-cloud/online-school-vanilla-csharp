using online_school.Students;

namespace online_school
{
    internal class App
    {
        static void Main(string[] args)
        {

            Book book = new Book("12|101|Lorelei|2026-04-22");

            BookRepository bookRepository = new BookRepository();


            try
            {

                Book b = bookRepository.Remove("101", "Lorelei2", DateTime.Now);


                Console.WriteLine(b);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            Student student = new Student("101|Ana|Popa|ana.popa@email.com|16|P@ssw0rd1! ");
            ViewStudent view =new ViewStudent(student);



        }
    }
}
