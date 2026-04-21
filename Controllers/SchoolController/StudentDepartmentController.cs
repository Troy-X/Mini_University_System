using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models.SchoolModel;
using SchoolSystem.ViewModels.SchoolVm.StudentDepartmentVM;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers.SchoolController
{
    public class StudentDepartmentController : Controller
    {
        private readonly AppDbContext _context;
        public StudentDepartmentController(AppDbContext context)
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
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDepartmentCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var studentDepartment = new StudentDepartment()
            {
                StudentDepartmentName = model.StudentDepartmentName,
                StudentDepartmentCode = model.StudentDepartmentCode
            };

            await _context.StudentDepartments.AddAsync(studentDepartment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var departments = await _context.StudentDepartments.FindAsync(id);
            if (departments == null) return NotFound();

            var vm = new StudentDepartmentEditVM()
            {
                StudentDepartmentId = departments.StudentDepartmentId,
                StudentDepartmentName=departments.StudentDepartmentName,
                StudentDepartmentCode=departments.StudentDepartmentCode
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentDepartmentEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var departments = await _context.StudentDepartments.FindAsync(model.StudentDepartmentId);
            if (departments == null) return NotFound();

            departments.StudentDepartmentName= model.StudentDepartmentName;
            departments.StudentDepartmentCode= model.StudentDepartmentCode;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var departments = await _context.StudentDepartments.FindAsync(id);
            if (departments == null) return NotFound();

            var vm = new StudentDepartmentDeleteVM()
            {
                StudentDepartmentId = departments.StudentDepartmentId,
                StudentDepartmentName = departments.StudentDepartmentName,
                StudentDepartmentCode = departments.StudentDepartmentCode
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(StudentDepartmentDeleteVM model)
        {
            var departments = await _context.StudentDepartments.FindAsync(model.StudentDepartmentId);
            if (departments == null) return NotFound();

            _context.StudentDepartments.Remove(departments);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
