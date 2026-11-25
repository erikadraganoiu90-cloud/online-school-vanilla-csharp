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
            foreach (Enrolment existing in enrolments)
            {
                if (existing.StudentId == enrolment.StudentId &&
                    existing.CourseId == enrolment.CourseId)
                {
                    throw new InvalidOperationException(
                        "Studentul este deja inscris la acest curs."
                    );
                }
            }

            enrolments.Add(enrolment);
            Write();
            return enrolment;
        }

        public List<Enrolment> GetByStudentId(string studentId)
        {
            List<Enrolment> result = new List<Enrolment>();

            foreach (Enrolment enrolment in GetAll())
            {
                if (enrolment.StudentId == studentId)
                {
                    result.Add(enrolment);
                }
            }

            return result;
        }

        public Enrolment Remove(string studentId, string courseId)
        {
            foreach (Enrolment enrolment in enrolments)
            {
                if (enrolment.StudentId.Equals(studentId) &&
                    enrolment.CourseId.Equals(courseId))
                {
                    enrolments.Remove(enrolment);
                    Write();
                    return enrolment;
                }
            }

            throw new ArgumentException("Enrolment not found for this student and course");
        }
        public int GetStudentsCountByCourseId(string courseId)
        {
            int numberOfStudents = 0;
            foreach(Enrolment enrolment in enrolments)
            {
                if (enrolment.CourseId.Equals(courseId))
                {
                    numberOfStudents++;
                }
            }
            return numberOfStudents;
        }




    }
}
