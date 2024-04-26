namespace StudentDataDB.Models
{
    public class Marksheet
    {
        public Guid MarksheetId { get; set; }
        public string MathsMarks { get; set; }
        public string ScienceMarks { get; set; }
        public int TotalMarks { get; set; }
        public int MarksObtained { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
