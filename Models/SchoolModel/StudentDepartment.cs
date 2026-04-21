namespace SchoolSystem.Models.SchoolModel
{
    public class StudentDepartment
    {
        public int StudentDepartmentId { get; set; }
        public string StudentDepartmentName { get; set; }
        public string StudentDepartmentCode { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
