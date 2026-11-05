using System.Globalization;

namespace online_school
{
    internal class Enrolment
    {
        private string studentId = string.Empty;
        private string courseId = string.Empty;
        private DateTime createdAt;

        public Enrolment(string studentId, string courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
            CreatedAt = DateTime.Now;
        }

        public Enrolment(string line)
        {
            string[] parts = line.Split('|');

            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid enrolment line: " + line);
            }

            StudentId = parts[0];
            CourseId = parts[1];
            CreatedAt = DateTime.ParseExact(parts[2], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public string StudentId
        {
            get { return studentId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("StudentId cannot be empty");
                }
                studentId = value;
            }
        }

        public string CourseId
        {
            get { return courseId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("CourseId cannot be empty");
                }
                courseId = value;
            }
        }

        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        public override string ToString()
        {
            return StudentId + "|" + CourseId + "|" + CreatedAt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
