namespace SchoolSystem.Models.SchoolModel
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int StudentMatricNo { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPhoneNO { get; set; }
        public string StudentGender { get; set; }
        public DateOnly StudentDOB { get; set; }
        public int StudentDepartmentId { get; set; }
        public StudentDepartment StudentDepartment {  get; set; }
        public int StudentClassId { get; set; }
        public StudentClass StudentClass {  get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();

    }
}
