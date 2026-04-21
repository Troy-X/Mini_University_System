using Microsoft.EntityFrameworkCore;
using SchoolSystem.Models.SchoolModel;
namespace SchoolSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
             .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            // StudentCourse
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            // StudentExam
            modelBuilder.Entity<StudentExam>()
                .HasOne(se => se.Student)
                .WithMany(s => s.StudentExams)
                .HasForeignKey(se => se.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentExam>()
                .HasOne(se => se.Exam)
                .WithMany(e => e.StudentExams)
                .HasForeignKey(se => se.ExamId)
                .OnDelete(DeleteBehavior.NoAction);

            // Student → Class
            modelBuilder.Entity<Student>()
                .HasOne(s => s.StudentClass)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.StudentClassId)
                .OnDelete(DeleteBehavior.NoAction);


            // Course → Teacher
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

           
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<StudentDepartment> StudentDepartments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
    }
}
