namespace SchoolSystem.ViewModels.SchoolVm.StudentVM
{
    public class StudentEditVM
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
        public int StudentClassId { get; set; }
    }
}
