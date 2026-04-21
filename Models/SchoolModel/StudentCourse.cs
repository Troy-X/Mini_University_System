namespace SchoolSystem.Models.SchoolModel
{
    public class StudentCourse
    {
        public int StudentCourseId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateOnly DateEnrolled {  get; set; }
        public int Grade { get; set; }
    }
}
