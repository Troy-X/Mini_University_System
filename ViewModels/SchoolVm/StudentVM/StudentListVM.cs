namespace SchoolSystem.ViewModels.SchoolVm.StudentVM
{
    public class StudentListVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int StudentMatricNo { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPhoneNO { get; set; }
        public string StudentGender { get; set; }
        public DateOnly StudentDOB { get; set; }
        public string StudentDepartmentName { get; set; }
        public string StudentClassName { get; set; }
    }
}
