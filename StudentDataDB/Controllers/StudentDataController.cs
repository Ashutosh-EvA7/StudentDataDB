using Microsoft.AspNetCore.Mvc;
using StudentDataDB.Businesses;
using StudentDataDB.Models;

namespace StudentDataDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentDataController : ControllerBase
    {
        private readonly StudentDataBusiness _studentDataBusiness;

        public StudentDataController(StudentDataBusiness studentDataBusiness)
        {
            _studentDataBusiness = studentDataBusiness;
        }

        [HttpGet("GetTotalMarkObtained/{studentId}")]
        public async Task<int> GetTotalMarkObtained(Guid studentId)
        {
            return await _studentDataBusiness.GetTotalMarkObtained(studentId);
        }

        [HttpGet("GetTotalPercentageObtained/{studentId}")]
        public async Task<double> GetTotalPercentageObtained(Guid studentId)
        {
            return await _studentDataBusiness.GetTotalPercentageObtained(studentId);
        }

        [HttpGet("GetAllMarksById/{studentId}")]
        public async Task<IEnumerable<Marksheet>> GetAllMarksById(Guid studentId)
        {
            return await _studentDataBusiness.GetAllMarksById(studentId);
        }

        [HttpPost("AddMarks")]
        public async Task<IActionResult> AddMarks([FromBody] Marksheet marksheet)
        {
            await _studentDataBusiness.AddMarks(marksheet);
            return Ok();
        }

        [HttpPut("UpdateMarks/{marksheetId}")]
        public async Task UpdateMarks(Guid marksheetId, [FromBody] Marksheet updatedMarksheet)
        {
            await _studentDataBusiness.UpdateMarks(marksheetId, updatedMarksheet);
        }

        [HttpGet("GetStudentList")]
        public async Task<IEnumerable<StudentWithMarks>> GetStudentList()
        {
            return await _studentDataBusiness.GetStudentList();
        }

        [HttpGet("GetTopThree/@class")]
        public async Task<IEnumerable<StudentWithMarks>> GetTopThree(int @class)
        {
            return await _studentDataBusiness.GetTopThree(@class);
        }
    }

    public class StudentWithMarks
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public int TotalMarks { get; set; }
        public int TotalMarkObtained { get; set; }
        public double TotalPercentage { get; set; }

        public StudentWithMarks(Models.Student student, int totalMarks, int totalMarkObtained, double totalPercentage)
        {
            Id = student.Id;
            StudentName = student.StudentName;
            TotalMarks = totalMarks;
            TotalMarkObtained = totalMarkObtained;
            TotalPercentage = totalPercentage;
        }
    }
}