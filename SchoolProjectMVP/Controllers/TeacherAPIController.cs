using Microsoft.AspNetCore.Mvc;
using SchoolProjectMVP.Models;
using System.Linq;

namespace SchoolProjectMVP.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _db;

        public TeacherAPIController(SchoolDbContext context)
        {
            _db = context;
        }


        [HttpPost("add")]
        public IActionResult AddTeacher([FromBody] Teacher teacher)
        {
            if (string.IsNullOrWhiteSpace(teacher.FullName))
                return BadRequest("Teacher name is required.");

            if (teacher.HireDate > DateTime.Now)
                return BadRequest("Hire date cannot be in the future.");

            if (!teacher.EmployeeNumber.StartsWith("T") || !int.TryParse(teacher.EmployeeNumber.Substring(1), out _))
                return BadRequest("Employee number must start with 'T' followed by digits.");

            if (_db.Teachers.Any(t => t.EmployeeNumber == teacher.EmployeeNumber))
                return BadRequest("Employee number already exists.");

            _db.Teachers.Add(teacher);
            _db.SaveChanges();
            return Ok("Teacher added successfully.");
        }


        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var teacher = _db.Teachers.Find(id);
            if (teacher == null)
                return NotFound("Teacher not found.");

            _db.Teachers.Remove(teacher);
            _db.SaveChanges();
            return Ok("Teacher deleted successfully.");
        }
    }
}
