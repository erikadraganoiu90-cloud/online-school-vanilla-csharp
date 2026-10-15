namespace online_school
{
    internal class App
    {
        static void Main(string[] args)
        {
            
            Guid id= Guid.NewGuid();

            Console.WriteLine(id);

            string id2= Guid.NewGuid().ToString();
            Console.WriteLine(id2);
        }
    }
}
