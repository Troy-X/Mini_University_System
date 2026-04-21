namespace SchoolSystem.ViewModels.SchoolVm.StudentCourseVM
{
    public class StudentCourseCreateVM
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateOnly DateEnrolled { get; set; }
        public int Grade { get; set; }
    }
}
