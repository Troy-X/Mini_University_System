namespace SchoolSystem.Models.SchoolModel
{
    public class StudentExam
    {
        public int StudentExamId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public int Score { get; set; }
        public int Grade { get; set; }
    }
}
