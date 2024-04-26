using Microsoft.EntityFrameworkCore;
using StudentDataDB.Controllers;
using StudentDataDB.Data;
using StudentDataDB.Models;

namespace StudentDataDB.Businesses
{
    public class StudentDataBusiness
    {
        private readonly StudentDbContext _studentDbContext;

        public StudentDataBusiness(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public async Task<int> GetTotalMarkObtained(Guid studentId)
        {
            var marksheet = await _studentDbContext.Marksheets.FindAsync(studentId);
            return marksheet.MarksObtained;
        }

        public async Task<double> GetTotalPercentageObtained(Guid studentId)
        {
            var marksheet = await _studentDbContext.Marksheets.FindAsync(studentId);
            return (double)marksheet.MarksObtained / marksheet.TotalMarks * 100;
        }

        public async Task<IEnumerable<Marksheet>> GetAllMarksById(Guid studentId)
        {
            return await _studentDbContext.Marksheets.Where(m => m.StudentId == studentId).ToListAsync();
        }

        public async Task AddMarks(Marksheet marksheet)
        {
            _studentDbContext.Marksheets.Add(marksheet);
            await _studentDbContext.SaveChangesAsync();
        }

        public async Task UpdateMarks(Guid marksheetId, Marksheet updatedMarksheet)
        {
            var marksheet = await _studentDbContext.Marksheets.FindAsync(marksheetId);
            marksheet.MathsMarks = updatedMarksheet.MathsMarks;
            marksheet.ScienceMarks = updatedMarksheet.ScienceMarks;
            marksheet.TotalMarks = updatedMarksheet.TotalMarks;
            marksheet.MarksObtained = updatedMarksheet.MarksObtained;
            await _studentDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentWithMarks>> GetStudentList()
        {
            var students = await _studentDbContext.Students.Include(s => s.Marksheets).ToListAsync();
            var studentWithMarksList = new List<StudentWithMarks>();
            foreach (var student in students)
            {
                var totalMarks = student.Marksheets.Sum(m => m.TotalMarks);
                var totalMarkObtained = student.Marksheets.Sum(m => m.MarksObtained);
                var totalPercentage = (double)totalMarkObtained / totalMarks * 100;
                studentWithMarksList.Add(new StudentWithMarks(student, totalMarks, totalMarkObtained, totalPercentage));
            }
            return studentWithMarksList;
        }

        public async Task<IEnumerable<StudentWithMarks>> GetTopThree(int @class)
        {
            var students = await _studentDbContext.Students.Include(s => s.Marksheets).Where(s => s.Class == @class).ToListAsync();
            var studentWithMarksList = new List<StudentWithMarks>();
            foreach (var student in students)
            {
                var totalMarks = student.Marksheets.Sum(m => m.TotalMarks);
                var totalMarkObtained = student.Marksheets.Sum(m => m.MarksObtained);
                var totalPercentage = (double)totalMarkObtained / totalMarks * 100;
                studentWithMarksList.Add(new StudentWithMarks(student, totalMarks, totalMarkObtained, totalPercentage));
            }
            return studentWithMarksList.OrderByDescending(s => s.TotalPercentage).Take(3);
        }
    }
}