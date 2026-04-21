namespace SchoolSystem.Models.SchoolModel
{
    public class StudentClass
    {
        public int StudentClassId { get; set; }
        public string StudentClassName { get; set; }
        public string StudentLevel { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
