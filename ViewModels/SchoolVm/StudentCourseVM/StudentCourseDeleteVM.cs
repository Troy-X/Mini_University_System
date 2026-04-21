namespace SchoolSystem.ViewModels.SchoolVm.StudentCourseVM
{
    public class StudentCourseDeleteVM
    {
        public int StudentCourseId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateOnly DateEnrolled { get; set; }
        public int Grade { get; set; }
    }
}
