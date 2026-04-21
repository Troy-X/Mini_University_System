using SchoolSystem.Models.SchoolModel;

namespace SchoolSystem.ViewModels.SchoolVm.StudentExamVM
{
    public class StudentExamListVM
    {
        public int StudentExamId { get; set; }
        public string StudentName { get; set; }
        public string ExamName { get; set; }
        public int Score { get; set; }
        public int Grade { get; set; }
    }
}
