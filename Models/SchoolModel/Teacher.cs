namespace SchoolSystem.Models.SchoolModel
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherPhoneNO { get; set; }
        public string TeacherQualification { get; set; }
        public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
