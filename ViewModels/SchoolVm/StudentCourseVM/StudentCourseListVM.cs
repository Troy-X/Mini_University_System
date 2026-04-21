using SchoolSystem.Models.SchoolModel;

namespace SchoolSystem.ViewModels.SchoolVm.StudentCourseVM
{
    public class StudentCourseListVM
    {
        public int StudentCourseId { get; set; }
        public string StudentName { get; set; }
        public string CourseTitle { get; set; }
        public DateOnly DateEnrolled { get; set; }
        public int Grade { get; set; }
    }
}
