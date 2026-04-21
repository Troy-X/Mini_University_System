using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.ViewModels.SchoolVm.StudentExamVM;
using SchoolSystem.ViewModels.SchoolVm.StudentVM;
using SchoolSystem.ViewModels.SchoolVm.ExamVM;
using System.Security.AccessControl;
using System.Threading.Tasks;
using SchoolSystem.Models.SchoolModel;

namespace SchoolSystem.Controllers.SchoolController
{
    public class StudentExamController : Controller
    {
        private readonly AppDbContext _context;
        public StudentExamController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var results = await _context.StudentExams.Select(r => new StudentExamListVM()
            {
                StudentExamId = r.StudentExamId,
                StudentName = r.Student.StudentLastName + " " + r.Student.StudentFirstName,
                ExamName = r.Exam.ExamName,
                Score = r.Score,
                Grade = r.Grade
            }).AsNoTracking().ToListAsync();

            return View(results);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Students"] = await _context.Students.Select(s => new StudentListVM()
            {
                StudentId = s.StudentId,
                StudentName = s.StudentLastName + " " + s.StudentFirstName,
            }).AsNoTracking().ToListAsync();

            ViewData["Exams"] = await _context.Exams.Select(e => new ExamListVM()
            {
                ExamId = e.ExamId,
                ExamName = e.ExamName
            }).AsNoTracking().ToArrayAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentExamCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Students"] = await _context.Students.Select(s => new StudentListVM()
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentLastName + " " + s.StudentFirstName,
                }).AsNoTracking().ToListAsync();

                ViewData["Exams"] = await _context.Exams.Select(e => new ExamListVM()
                {
                    ExamId = e.ExamId,
                    ExamName = e.ExamName
                }).AsNoTracking().ToArrayAsync();

                return View(model);
            }

            var result = new StudentExam()
            {
                StudentId = model.StudentId,
                ExamId = model.ExamId,
                Score = model.Score,
                Grade = model.Grade
            };
            
            await _context.StudentExams.AddAsync(result);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
