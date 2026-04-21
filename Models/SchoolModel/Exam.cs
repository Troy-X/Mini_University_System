namespace SchoolSystem.Models.SchoolModel
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string Term {  get; set; }
        public string Session {  get; set; }
        public ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
    }
}
