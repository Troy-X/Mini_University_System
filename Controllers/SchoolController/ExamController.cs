using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models.SchoolModel;
using SchoolSystem.ViewModels.SchoolVm.ExamVM;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers.SchoolController
{
    public class ExamController : Controller
    {
        private readonly AppDbContext _context;
        public ExamController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var exams = await _context.Exams.Select(e => new ExamListVM()
            {
                ExamId = e.ExamId,
                ExamName = e.ExamName,
                Term = e.Term,
                Session = e.Session,
            }).AsNoTracking().ToListAsync();

            return View(exams);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExamCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var exam = new Exam()
            {
                ExamName = model.ExamName,
                Term = model.Term,
                Session = model.Session
            };

            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var exams = await _context.Exams.FindAsync(id);
            if (exams == null) return NotFound();

            var vm = new ExamEditVM()
            {
                ExamId = exams.ExamId,
                ExamName = exams.ExamName,
                Term = exams.Term,
                Session = exams.Session
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExamEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var exams = await _context.Exams.FindAsync(model.ExamId);
            if (exams == null) return NotFound();

            exams.ExamName = model.ExamName;
            exams.Term = model.Term;
            exams.Session = model.Session;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var exams = await _context.Exams.FindAsync(id);
            if (exams == null) return NotFound();

            var vm = new ExamDeleteVM()
            {
                ExamId = exams.ExamId,
                ExamName = exams.ExamName,
                Term = exams.Term,
                Session = exams.Session
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ExamDeleteVM model)
        {
            var exams = await _context.Exams.FindAsync(model.ExamId);
            if (exams == null) return NotFound();

            _context.Exams.Remove(exams);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
