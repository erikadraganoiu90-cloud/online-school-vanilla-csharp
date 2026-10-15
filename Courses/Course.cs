using System.Globalization;

namespace online_school
{
    internal class Course
    {
        private string id = string.Empty;
        private string name = string.Empty;
        private string department = string.Empty;
        private string description = string.Empty;
        private int durationHours;
        private decimal price;
        private int maxStudents;
        private DateTime startDate;

        public Course(string name, string department, string description, int durationHours, decimal price, int maxStudents, DateTime startDate)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Department = department;
            Description = description;
            DurationHours = durationHours;
            Price = price;
            MaxStudents = maxStudents;
            StartDate = startDate;
        }

        public Course(string line)
        {
            string[] parts = line.Split('|');

            if (parts.Length != 8)
            {
                throw new ArgumentException("Invalid course line: " + line);
            }

            Id = parts[0];
            Name = parts[1];
            Department = parts[2];
            Description = parts[3];
            DurationHours = int.Parse(parts[4]);
            Price = decimal.Parse(parts[5], CultureInfo.InvariantCulture);
            MaxStudents = int.Parse(parts[6]);
            StartDate = DateTime.ParseExact(parts[7], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public string Id
        {
            get { return id; }
            set
            {
                if (!Guid.TryParse(value, out _))
                {
                    throw new ArgumentException("Id must be a valid Guid");
                }
                id = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                name = value;
            }
        }

        public string Department
        {
            get { return department; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Department cannot be empty");
                }
                department = value;
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Description cannot be null");
                }
                description = value;
            }
        }

        public int DurationHours
        {
            get { return durationHours; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Duration must be greater than 0");
                }
                durationHours = value;
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative");
                }
                price = value;
            }
        }

        public int MaxStudents
        {
            get { return maxStudents; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Max students must be greater than 0");
                }
                maxStudents = value;
            }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value.Date; }
        }

        public override string ToString()
        {
            return Id + " " + Name + " (" + Department + ") - " + DurationHours + "h, " + Price + " lei, max " + MaxStudents + " studenti, incepe " + StartDate.ToString("dd.MM.yyyy");
        }
    }
}
