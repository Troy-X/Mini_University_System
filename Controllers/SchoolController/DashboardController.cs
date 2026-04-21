using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Data;
using SchoolSystem.ViewModels.SchoolVm.DashboardVM;
using System.Linq;

namespace SchoolSystem.Controllers.SchoolController
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var vm = new DashboardListVM
            {
                // 📊 CARDS
                TotalStudents = _context.Students.Count(),
                TotalTeachers = _context.Teachers.Count(),
                TotalCourses = _context.Courses.Count(),
                TotalDepartments = _context.StudentDepartments.Count(),

               
            };

            return View(vm);
        }
    }
}