using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models.SchoolModel;
using SchoolSystem.ViewModels.SchoolVm.StudentClassVM;
using SchoolSystem.ViewModels.SchoolVm.TeacherVM;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers.SchoolController
{
    public class StudentClassController : Controller
    {
        private readonly AppDbContext _context;
        public StudentClassController(AppDbContext context)
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
                TeacherQualification = t.TeacherQualification,
            }).AsNoTracking().ToListAsync();

            var classes = await _context.StudentClasses.Select(c => new StudentClassListVM()
            {
                StudentClassId = c.StudentClassId,
                StudentClassName = c.StudentClassName,
                StudentLevel = c.StudentLevel,
                TeacherName = c.Teacher.TeacherLastName + " " + c.Teacher.TeacherFirstName
            }).AsNoTracking().ToListAsync();

            ViewData["Teachers"] = teachers;

            return View(classes);

        }

        public async Task<IActionResult> Create()
        {
            var teachers = await _context.Teachers.Select(t => new TeacherListVM()
            {
                TeacherId = t.TeacherId,
                TeacherName = t.TeacherLastName + " " + t.TeacherFirstName,
                TeacherEmail = t.TeacherEmail,
                TeacherPhoneNO = t.TeacherPhoneNO,
                TeacherQualification = t.TeacherQualification,
            }).AsNoTracking().ToListAsync();

            ViewData["Teachers"] = teachers;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentClassCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                var teachers = await _context.Teachers.Select(t => new TeacherListVM()
                {
                    TeacherId = t.TeacherId,
                    TeacherName = t.TeacherLastName + " " + t.TeacherFirstName,
                    TeacherEmail = t.TeacherEmail,
                    TeacherPhoneNO = t.TeacherPhoneNO,
                    TeacherQualification = t.TeacherQualification,
                }).AsNoTracking().ToListAsync();

                ViewData["Teachers"] = teachers;

                return View(model);
            }

            var studentClass = new StudentClass
            {
                StudentClassName = model.StudentClassName,
                StudentLevel = model.StudentLevel,
                TeacherId = model.TeacherId,
            };

            await _context.StudentClasses.AddAsync(studentClass);
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
                TeacherQualification = t.TeacherQualification,
            }).AsNoTracking().ToListAsync();

            ViewData["Teachers"] = teachers;

            var classes = await _context.StudentClasses.FindAsync(id);
            if (classes == null) return NotFound();

            var vm = new StudentClassEditVM()
            {
                StudentClassId = classes.StudentClassId,
                StudentLevel = classes.StudentLevel,
                StudentClassName=classes.StudentClassName,
                TeacherId =classes.TeacherId,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentClassEditVM model)
        {
            if (!ModelState.IsValid)
            {
                var teachers = await _context.Teachers.Select(t => new TeacherListVM()
                {
                    TeacherId = t.TeacherId,
                    TeacherName = t.TeacherLastName + " " + t.TeacherFirstName,
                    TeacherEmail = t.TeacherEmail,
                    TeacherPhoneNO = t.TeacherPhoneNO,
                    TeacherQualification = t.TeacherQualification,
                }).AsNoTracking().ToListAsync();

                ViewData["Teachers"] = teachers;
                return View(model);
            }

            var classes = await _context.StudentClasses.FindAsync(model.StudentClassId);
            if (classes == null) return NotFound();

            classes.StudentClassName = model.StudentClassName;
            classes.StudentLevel = model.StudentLevel;
            classes.TeacherId = model.TeacherId;

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
                TeacherQualification = t.TeacherQualification,
            }).AsNoTracking().ToListAsync();

            ViewData["Teachers"] = teachers;

            var classes = await _context.StudentClasses.FindAsync(id);
            if (classes == null) return NotFound();

            var vm = new StudentClassDeleteVM()
            {
                StudentClassId = classes.StudentClassId,
                StudentLevel = classes.StudentLevel,
                StudentClassName = classes.StudentClassName,
                TeacherId = classes.TeacherId,
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(StudentClassDeleteVM model)
        {
            var classes = await _context.StudentClasses.FindAsync(model.StudentClassId);
            if (classes == null) return NotFound();

            _context.StudentClasses.Remove(classes);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
