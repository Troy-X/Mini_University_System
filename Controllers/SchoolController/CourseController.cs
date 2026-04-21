using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models.SchoolModel;
using SchoolSystem.ViewModels.SchoolVm.CourseVM;
using SchoolSystem.ViewModels.SchoolVm.TeacherVM;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers.SchoolController
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var teachers = await _context.Teachers.Select(t => new TeacherListVM()
            {
                TeacherId = t.TeacherId,
                TeacherName = t.TeacherLastName + " " + t.TeacherFirstName,
                TeacherEmail = t.TeacherEmail,
                TeacherPhoneNO = t.TeacherPhoneNO,
                TeacherQualification = t.TeacherQualification
            }).AsNoTracking().ToListAsync();


            var courses = await _context.Courses.Select(c => new CourseListVM()
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle,
                CourseCode = c.CourseCode,
                CreditUnit = c.CreditUnit,
                TeacherName = c.Teacher.TeacherLastName + " " + c.Teacher.TeacherFirstName
            }).AsNoTracking().ToListAsync();

           ViewData["Teachers"] = teachers;

            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            var teachers = await _context.Teachers.Select(t => new TeacherListVM()
            {
                TeacherId = t.TeacherId,
                TeacherName = t.TeacherLastName + " " + t.TeacherFirstName,
                TeacherEmail = t.TeacherEmail,
                TeacherPhoneNO = t.TeacherPhoneNO,
                TeacherQualification = t.TeacherQualification
            }).AsNoTracking().ToListAsync();
            ViewData["Teachers"] = teachers;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                var teachers = await _context.Teachers.Select(t => new TeacherListVM()
                {
                    TeacherId = t.TeacherId,
                    TeacherName = t.TeacherLastName + " " + t.TeacherFirstName,
                    TeacherEmail = t.TeacherEmail,
                    TeacherPhoneNO = t.TeacherPhoneNO,
                    TeacherQualification = t.TeacherQualification
                }).AsNoTracking().ToListAsync();
                ViewData["Teachers"] = teachers;
                return View(model);
            }

            var course = new Course()
            {
                CourseTitle = model.CourseTitle,
                CourseCode = model.CourseCode,
                CreditUnit = model.CreditUnit,
                TeacherId = model.TeacherId
            };

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

       public async Task<IActionResult> Edit(int id)
        {
            var teachers = await _context.Teachers.Select(t => new TeacherListVM()
            {
                TeacherId = t.TeacherId,
                TeacherName = t.TeacherLastName + " " + t.TeacherFirstName,
                TeacherEmail = t.TeacherEmail,
                TeacherPhoneNO = t.TeacherPhoneNO,
                TeacherQualification = t.TeacherQualification
            }).AsNoTracking().ToListAsync();
            ViewData["Teachers"] = teachers;

            var courses = await _context.Courses.FindAsync(id);
            if (courses == null) return NotFound();

            var vm = new CourseEditVM()
            {
                CourseId = courses.CourseId,
                CourseTitle = courses.CourseTitle,
                CourseCode = courses.CourseCode,
                CreditUnit = courses.CreditUnit,
                TeacherId = courses.TeacherId
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseEditVM model)
        {
            if (!ModelState.IsValid)
            {
                var teachers = await _context.Teachers.Select(t => new TeacherListVM()
                {
                    TeacherId = t.TeacherId,
                    TeacherName = t.TeacherLastName + " " + t.TeacherFirstName,
                    TeacherEmail = t.TeacherEmail,
                    TeacherPhoneNO = t.TeacherPhoneNO,
                    TeacherQualification = t.TeacherQualification
                }).AsNoTracking().ToListAsync();
                ViewData["Teachers"] = teachers;
                return View(model);
            }

            var courses = await _context.Courses.FindAsync(model.CourseId);
            if (courses == null) return NotFound();

            courses.CourseTitle = model.CourseTitle;
            courses.CourseCode = model.CourseCode;
            courses.CreditUnit = model.CreditUnit;
            courses.TeacherId = model.TeacherId;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var teachers = await _context.Teachers.Select(t => new TeacherListVM()
            {
                TeacherId = t.TeacherId,
                TeacherName = t.TeacherLastName + " " + t.TeacherFirstName,
                TeacherEmail = t.TeacherEmail,
                TeacherPhoneNO = t.TeacherPhoneNO,
                TeacherQualification = t.TeacherQualification
            }).AsNoTracking().ToListAsync();
            ViewData["Teachers"] = teachers;

            var courses = await _context.Courses.FindAsync(id);
            if (courses == null) return NotFound();

            var vm = new CourseDeleteVM()
            {
                CourseId = courses.CourseId,
                CourseTitle = courses.CourseTitle,
                CourseCode = courses.CourseCode,
                CreditUnit = courses.CreditUnit,
                TeacherId = courses.TeacherId
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CourseDeleteVM model)
        {
            var teachers = await _context.Teachers.Select(t => new TeacherListVM()
            {
                TeacherId = t.TeacherId,
                TeacherName = t.TeacherLastName + " " + t.TeacherFirstName,
                TeacherEmail = t.TeacherEmail,
                TeacherPhoneNO = t.TeacherPhoneNO,
                TeacherQualification = t.TeacherQualification
            }).AsNoTracking().ToListAsync();
            ViewData["Teachers"] = teachers;

            var courses = await _context.Courses.FindAsync(model.CourseId);
            if (courses == null) return NotFound();

            _context.Courses.Remove(courses);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
