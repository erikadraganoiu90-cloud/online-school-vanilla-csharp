namespace online_school
{
    internal class EnrolmentRepository
    {
        private string filePath;
        private List<Enrolment> enrolments;

        public EnrolmentRepository()
        {
            this.filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Enrolments", "enrolments.txt");
            this.enrolments = new List<Enrolment>();
            Load();
        }

        public void Load()
        {
            enrolments.Clear();

            if (!File.Exists(filePath))
            {
                return;
            }

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                enrolments.Add(new Enrolment(line));
            }
        }

        public void Write()
        {
            List<string> lines = new List<string>();

            foreach (Enrolment enrolment in enrolments)
            {
                lines.Add(enrolment.ToString());
            }

            File.WriteAllLines(filePath, lines);
        }

        public List<Enrolment> GetAll()
        {
            return new List<Enrolment>(enrolments);
        }

        public Enrolment Add(Enrolment enrolment)
        {
            enrolments.Add(enrolment);
            Write();
            return enrolment;
        }
    }
}
