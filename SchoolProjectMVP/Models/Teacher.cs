using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolProjectMVP.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string EmployeeNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public string WorkPhone { get; set; }
    }
}
