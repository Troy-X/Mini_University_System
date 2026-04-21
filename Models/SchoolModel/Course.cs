namespace SchoolSystem.Models.SchoolModel
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public int CreditUnit {  get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
