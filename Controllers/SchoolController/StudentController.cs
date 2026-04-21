using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models.SchoolModel;
using SchoolSystem.ViewModels.SchoolVm.StudentClassVM;
using SchoolSystem.ViewModels.SchoolVm.StudentDepartmentVM;
using SchoolSystem.ViewModels.SchoolVm.StudentVM;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers.SchoolController
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        public StudentController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _context.StudentDepartments.Select(d => new StudentDepartmentListVM()
            {
                StudentDepartmentId = d.StudentDepartmentId,
                StudentDepartmentName = d.StudentDepartmentName,
                StudentDepartmentCode = d.StudentDepartmentCode
            }).AsNoTracking().ToListAsync();

            var classes = await _context.StudentClasses.Select(c => new StudentClassListVM()
            {
                StudentClassId = c.StudentClassId,
                StudentClassName = c.StudentClassName,
                StudentLevel = c.StudentLevel,
                TeacherName = c.Teacher.TeacherLastName + " " + c.Teacher.TeacherFirstName
            }).AsNoTracking().ToListAsync();

            var students = await _context.Students.Select(s => new StudentListVM()
            {
                StudentId = s.StudentId,
                StudentName = s.StudentFirstName + " " + s.StudentLastName,
                StudentMatricNo = s.StudentMatricNo,
                StudentEmail = s.StudentEmail,
                StudentPhoneNO = s.StudentPhoneNO,
                StudentGender = s.StudentGender,
                StudentDOB = s.StudentDOB,
                StudentDepartmentName = s.StudentDepartment.StudentDepartmentName,
                StudentClassName = s.StudentClass.StudentClassName
            }).AsNoTracking().ToListAsync();

            ViewData["Departments"] = departments;
            ViewData["Classes"] = classes;

            return View(students);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _context.StudentDepartments.Select(d => new StudentDepartmentListVM()
            {
                StudentDepartmentId = d.StudentDepartmentId,
                StudentDepartmentName = d.StudentDepartmentName,
                StudentDepartmentCode = d.StudentDepartmentCode
            }).AsNoTracking().ToListAsync();
            ViewData["Departments"] = departments;

            var classes = await _context.StudentClasses.Select(c => new StudentClassListVM()
            {
                StudentClassId = c.StudentClassId,
                StudentClassName = c.StudentClassName,
                StudentLevel = c.StudentLevel,
                TeacherName = c.Teacher.TeacherLastName + " " + c.Teacher.TeacherFirstName
            }).AsNoTracking().ToListAsync();
            ViewData["Classes"] = classes;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _context.StudentDepartments.Select(d => new StudentDepartmentListVM()
                {
                    StudentDepartmentId = d.StudentDepartmentId,
                    StudentDepartmentName = d.StudentDepartmentName,
                    StudentDepartmentCode = d.StudentDepartmentCode
                }).AsNoTracking().ToListAsync();
                ViewData["Departments"] = departments;

                var classes = await _context.StudentClasses.Select(c => new StudentClassListVM()
                {
                    StudentClassId = c.StudentClassId,
                    StudentClassName = c.StudentClassName,
                    StudentLevel = c.StudentLevel,
                    TeacherName = c.Teacher.TeacherLastName + " " + c.Teacher.TeacherFirstName
                }).AsNoTracking().ToListAsync();
                ViewData["Classes"] = classes;

                return View(model);
            }

            var student = new Student()
            {
                StudentFirstName = model.StudentFirstName,
                StudentLastName = model.StudentLastName,
                StudentMatricNo= model.StudentMatricNo,
                StudentEmail= model.StudentEmail,
                StudentPhoneNO= model.StudentPhoneNO,
                StudentGender = model.StudentGender,
                StudentDOB= model.StudentDOB,
                StudentDepartmentId= model.StudentDepartmentId,
                StudentClassId= model.StudentClassId
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var departments = await _context.StudentDepartments.Select(d => new StudentDepartmentListVM()
            {
                StudentDepartmentId = d.StudentDepartmentId,
                StudentDepartmentName = d.StudentDepartmentName,
                StudentDepartmentCode = d.StudentDepartmentCode
            }).AsNoTracking().ToListAsync();
            ViewData["Departments"] = departments;

            var classes = await _context.StudentClasses.Select(c => new StudentClassListVM()
            {
                StudentClassId = c.StudentClassId,
                StudentClassName = c.StudentClassName,
                StudentLevel = c.StudentLevel,
                TeacherName = c.Teacher.TeacherLastName + " " + c.Teacher.TeacherFirstName
            }).AsNoTracking().ToListAsync();
            ViewData["Classes"] = classes;

            var students = await _context.Students.FindAsync(id);
            if (students == null) return NotFound();

            var vm = new StudentEditVM()
            {
                StudentId = students.StudentId,
                StudentFirstName = students.StudentFirstName,
                StudentLastName = students.StudentLastName,
                StudentMatricNo = students.StudentMatricNo,
                StudentEmail= students.StudentEmail,
                StudentPhoneNO = students.StudentPhoneNO,
                StudentGender = students.StudentGender,
                StudentDOB = students.StudentDOB,
                StudentDepartmentId = students.StudentDepartmentId,
                StudentClassId = students.StudentClassId,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentEditVM model)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _context.StudentDepartments.Select(d => new StudentDepartmentListVM()
                {
                    StudentDepartmentId = d.StudentDepartmentId,
                    StudentDepartmentName = d.StudentDepartmentName,
                    StudentDepartmentCode = d.StudentDepartmentCode
                }).AsNoTracking().ToListAsync();
                ViewData["Departments"] = departments;

                var classes = await _context.StudentClasses.Select(c => new StudentClassListVM()
                {
                    StudentClassId = c.StudentClassId,
                    StudentClassName = c.StudentClassName,
                    StudentLevel = c.StudentLevel,
                    TeacherName = c.Teacher.TeacherLastName + " " + c.Teacher.TeacherFirstName
                }).AsNoTracking().ToListAsync();
                ViewData["Classes"] = classes;

                return View(model);
            }

            var students = await _context.Students.FindAsync(model.StudentId);
            if (students == null) return NotFound();

            students.StudentFirstName = model.StudentFirstName;
            students.StudentLastName = model.StudentLastName;
            students.StudentMatricNo = model.StudentMatricNo;
            students.StudentEmail = model.StudentEmail;
            students.StudentPhoneNO = model.StudentPhoneNO;
            students.StudentGender = model.StudentGender;
            students.StudentDOB = model.StudentDOB;
            students.StudentDepartmentId = model.StudentDepartmentId;
            students.StudentClassId = model.StudentClassId;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var departments = await _context.StudentDepartments.Select(d => new StudentDepartmentListVM()
            {
                StudentDepartmentId = d.StudentDepartmentId,
                StudentDepartmentName = d.StudentDepartmentName,
                StudentDepartmentCode = d.StudentDepartmentCode
            }).AsNoTracking().ToListAsync();
            ViewData["Departments"] = departments;

            var classes = await _context.StudentClasses.Select(c => new StudentClassListVM()
            {
                StudentClassId = c.StudentClassId,
                StudentClassName = c.StudentClassName,
                StudentLevel = c.StudentLevel,
                TeacherName = c.Teacher.TeacherLastName + " " + c.Teacher.TeacherFirstName
            }).AsNoTracking().ToListAsync();
            ViewData["Classes"] = classes;

            var students = await _context.Students.FindAsync(id);
            if (students == null) return NotFound();

            var vm = new StudentDelete()
            {
                StudentId = students.StudentId,
                StudentFirstName = students.StudentFirstName,
                StudentLastName = students.StudentLastName,
                StudentMatricNo = students.StudentMatricNo,
                StudentEmail = students.StudentEmail,
                StudentPhoneNO = students.StudentPhoneNO,
                StudentGender = students.StudentGender,
                StudentDOB = students.StudentDOB,
                StudentDepartmentId = students.StudentDepartmentId,
                StudentClassId = students.StudentClassId,
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(StudentDelete model)
        {
            var departments = await _context.StudentDepartments.Select(d => new StudentDepartmentListVM()
            {
                StudentDepartmentId = d.StudentDepartmentId,
                StudentDepartmentName = d.StudentDepartmentName,
                StudentDepartmentCode = d.StudentDepartmentCode
            }).AsNoTracking().ToListAsync();
            ViewData["Departments"] = departments;

            var classes = await _context.StudentClasses.Select(c => new StudentClassListVM()
            {
                StudentClassId = c.StudentClassId,
                StudentClassName = c.StudentClassName,
                StudentLevel = c.StudentLevel,
                TeacherName = c.Teacher.TeacherLastName + " " + c.Teacher.TeacherFirstName
            }).AsNoTracking().ToListAsync();
            ViewData["Classes"] = classes;

            var students = await _context.Students.FindAsync(model.StudentId);
            if (students == null) return NotFound();

            _context.Students.Remove(students);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
