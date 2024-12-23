namespace FirstApp.Models
{
    public class StudentMarks
    {
        public int Id { get; set; }

        // Foreign key referencing Student
        public int StudentId { get; set; }

        // Navigation property
        public Student? Student { get; set; }

        public string Grade { get; set; }

        public string TotalMarks { get; set; }

    }
}
