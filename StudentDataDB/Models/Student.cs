namespace StudentDataDB.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string? StudentName { get; set; }

        public DateOnly StudentJoinDate { get; set; }
        public int Class { get; set; }
        public object? Marksheets { get; internal set; }
    }
}
