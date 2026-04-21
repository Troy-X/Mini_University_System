using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models.SchoolModel;
using SchoolSystem.ViewModels.SchoolVm.TeacherVM;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers.SchoolController
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context)
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
            return View(teachers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var teacher = new Teacher
            {
                TeacherFirstName = model.TeacherFirstName,
                TeacherLastName = model.TeacherLastName,
                TeacherEmail = model.TeacherEmail,
                TeacherPhoneNO= model.TeacherPhoneNO,
                TeacherQualification= model.TeacherQualification
            };

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var teachers = await _context.Teachers.FindAsync(id);
            if (teachers == null) return NotFound();

            var vm = new TeacherEditVM()
            {
                TeacherId = teachers.TeacherId,
                TeacherFirstName= teachers.TeacherFirstName,
                TeacherLastName = teachers.TeacherLastName,
                TeacherEmail= teachers.TeacherEmail,
                TeacherPhoneNO = teachers.TeacherPhoneNO,
                TeacherQualification = teachers.TeacherQualification
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeacherEditVM model)
        {
            var teachers = await _context.Teachers.FindAsync(model.TeacherId);
            if (teachers == null) return NotFound();

            teachers.TeacherFirstName = model.TeacherFirstName;
            teachers.TeacherLastName = model.TeacherLastName;
            teachers.TeacherEmail = model.TeacherEmail;
            teachers.TeacherPhoneNO= model.TeacherPhoneNO;
            teachers.TeacherQualification= model.TeacherQualification;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var teachers = await _context.Teachers.FindAsync(id);
            if (teachers == null) return NotFound();

            var vm = new TeacherDeleteVM()
            {
                TeacherId = teachers.TeacherId,
                TeacherFirstName = teachers.TeacherFirstName,
                TeacherLastName = teachers.TeacherLastName,
                TeacherEmail = teachers.TeacherEmail,
                TeacherPhoneNO = teachers.TeacherPhoneNO,
                TeacherQualification = teachers.TeacherQualification
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(TeacherDeleteVM model)
        {
            var teachers = await _context.Teachers.FindAsync(model.TeacherId);
            if (teachers == null) return NotFound();

            _context.Teachers.Remove(teachers);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
