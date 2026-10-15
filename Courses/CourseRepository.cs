using System.Globalization;

namespace online_school
{
    internal class CourseRepository
    {
        private string filePath;
        private List<Course> courses;

        public CourseRepository()
        {
            this.filePath = Path.Combine(AppContext.BaseDirectory, "Courses", "courses.txt");
            this.courses = new List<Course>();
            Load();
        }

        public void Load()
        {
            courses.Clear();

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

                courses.Add(new Course(line));
            }
        }

        public void Write()
        {
            List<string> lines = new List<string>();

            foreach (Course course in courses)
            {
                lines.Add(course.Id + "|"
                    + course.Name + "|"
                    + course.Department + "|"
                    + course.Description + "|"
                    + course.DurationHours + "|"
                    + course.Price.ToString(CultureInfo.InvariantCulture) + "|"
                    + course.MaxStudents + "|"
                    + course.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            }

            File.WriteAllLines(filePath, lines);
        }

        public List<Course> GetAll()
        {
            return new List<Course>(courses);
        }

        public Course? GetById(string id)
        {
            foreach (Course course in courses)
            {
                if (course.Id == id)
                {
                    return course;
                }
            }
            return null;
        }

        public Course Add(Course course)
        {
            courses.Add(course);
            Write();
            return course;
        }

        public bool Update(Course course)
        {
            Course? existing = GetById(course.Id);

            if (existing == null)
            {
                return false;
            }

            courses[courses.IndexOf(existing)] = course;
            Write();
            return true;
        }

        public bool Delete(string id)
        {
            Course? course = GetById(id);

            if (course == null)
            {
                return false;
            }

            courses.Remove(course);
            Write();
            return true;
        }
    }
}
