using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models.SchoolModel;
using SchoolSystem.ViewModels.SchoolVm.StudentCourseVM;
using SchoolSystem.ViewModels.SchoolVm.StudentVM;
using SchoolSystem.ViewModels.SchoolVm.CourseVM;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers.SchoolController
{
    public class StudentCourseController : Controller
    {
        private readonly AppDbContext _context;
        public StudentCourseController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var registration = await _context.StudentCourses.Select(sc => new StudentCourseListVM()
            {
                StudentCourseId = sc.StudentCourseId,
                StudentName = sc.Student.StudentLastName + " " + sc.Student.StudentFirstName,
                CourseTitle = sc.Course.CourseTitle,
                DateEnrolled = sc.DateEnrolled,
                Grade = sc.Grade
            }).AsNoTracking().ToListAsync();


            return View(registration);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Students"] = await _context.Students.Select(s => new StudentListVM()
            {
                StudentId = s.StudentId,
                StudentName = s.StudentLastName + " " + s.StudentFirstName
            }).AsNoTracking().ToListAsync();

            ViewData["Courses"] = await _context.Courses.Select(c => new CourseListVM()
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle
            }).AsNoTracking().ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCourseCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Students"] = await _context.Students.Select(s => new StudentListVM()
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentLastName + " " + s.StudentFirstName
                }).AsNoTracking().ToListAsync();

                ViewData["Courses"] = await _context.Courses.Select(c => new CourseListVM()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle
                }).AsNoTracking().ToListAsync();
                return View(model);
            }

            var registration = new StudentCourse()
            {
                StudentId = model.StudentId,
                CourseId = model.CourseId,
                DateEnrolled =model.DateEnrolled,
                Grade = model.Grade
            };

            await _context.StudentCourses.AddAsync(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Students"] = await _context.Students.Select(s => new StudentListVM()
            {
                StudentId = s.StudentId,
                StudentName = s.StudentLastName + " " + s.StudentFirstName
            }).AsNoTracking().ToListAsync();

            ViewData["Courses"] = await _context.Courses.Select(c => new CourseListVM()
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle
            }).AsNoTracking().ToListAsync();

            var registrations = await _context.StudentCourses.FindAsync(id);
            if (registrations == null) return NotFound();

            var vm = new StudentCourseEditVM()
            {
                StudentCourseId = registrations.StudentCourseId,
                StudentId = registrations.StudentId,
                CourseId= registrations.CourseId,
                DateEnrolled = registrations.DateEnrolled,
                Grade = registrations.Grade
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentCourseEditVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Students"] = await _context.Students.Select(s => new StudentListVM()
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentLastName + " " + s.StudentFirstName
                }).AsNoTracking().ToListAsync();

                ViewData["Courses"] = await _context.Courses.Select(c => new CourseListVM()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle
                }).AsNoTracking().ToListAsync();

                return View(model);
            }
            var registrations = await _context.StudentCourses.FindAsync(model.StudentCourseId);
            if (registrations == null) return NotFound();

            registrations.StudentId = model.StudentCourseId;
            registrations.CourseId = model.CourseId;
            registrations.DateEnrolled = model.DateEnrolled;
            registrations.Grade = model.Grade;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            ViewData["Students"] = await _context.Students.Select(s => new StudentListVM()
            {
                StudentId = s.StudentId,
                StudentName = s.StudentLastName + " " + s.StudentFirstName
            }).AsNoTracking().ToListAsync();

            ViewData["Courses"] = await _context.Courses.Select(c => new CourseListVM()
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle
            }).AsNoTracking().ToListAsync();

            var registrations = await _context.StudentCourses.FindAsync(id);
            if (registrations == null) return NotFound();

            var vm = new StudentCourseDeleteVM()
            {
                StudentCourseId = registrations.StudentCourseId,
                StudentId = registrations.StudentId,
                CourseId = registrations.CourseId,
                DateEnrolled = registrations.DateEnrolled,
                Grade = registrations.Grade
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(StudentCourseDeleteVM model)
        {
            ViewData["Students"] = await _context.Students.Select(s => new StudentListVM()
            {
                StudentId = s.StudentId,
                StudentName = s.StudentLastName + " " + s.StudentFirstName
            }).AsNoTracking().ToListAsync();

            ViewData["Courses"] = await _context.Courses.Select(c => new CourseListVM()
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle
            }).AsNoTracking().ToListAsync();

            var registrations = await _context.StudentCourses.FindAsync(model.StudentCourseId);
            if (registrations == null) return NotFound();

            _context.StudentCourses.Remove(registrations);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
