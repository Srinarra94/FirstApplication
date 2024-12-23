using System.ComponentModel.DataAnnotations;

namespace FirstApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        // Navigation property for related Studentmarks
        public ICollection<StudentMarks>? StudentMarks { get; set; }
    }
}
