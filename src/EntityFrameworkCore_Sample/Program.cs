using Bogus;
using EntityFrameworkCore_Sample.Data;
using EntityFrameworkCore_Sample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCore
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Inserting students...");
            var rows = await InsertStudentsAsync();
            Console.WriteLine($"Rows affected: {rows}");
            Console.WriteLine();

            Console.WriteLine("Inserting courses...");
            rows = await InsertCouses();
            Console.WriteLine($"Rows affected: {rows}");
            Console.WriteLine();

            Console.WriteLine("Searching students starts name with 'a'");
            var students = await GetStudentsNameStartsWith("a");
            Console.WriteLine($"Rows founded: {students.Count()}");
            Console.WriteLine();

            Console.WriteLine("All students starts name with 'a' will study Math in first semester!");
            Console.WriteLine("Setting Math to students..");
            rows = await SetMathFirstSemesterToStudentsStartsWithA();
            Console.WriteLine($"Rows affected: {rows}");
            Console.WriteLine();


            Console.WriteLine("Do you want to delete all data from database? Y/N");          
            var key = Console.ReadKey();
            Console.ReadLine();
            rows = await DatabaseDeleteAction(key);
            Console.WriteLine($"Rows affected: {rows}");
            Console.ReadLine();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<ApplicationDbContext>();
        }

        private static async Task<int> DatabaseDeleteAction(ConsoleKeyInfo key)
        {           
            if (key.Key.ToString() != "Y") return 0;
            
            var db = new ApplicationDbContext();
            var rows = await db.Database.ExecuteSqlRawAsync("delete from dbo.TakesCourses");
            rows += await db.Database.ExecuteSqlRawAsync("delete from dbo.Students");
            rows += await db.Database.ExecuteSqlRawAsync("delete from dbo.Courses");

            return rows;
        }

        private static async Task<int> InsertStudentsAsync()
        {
            var db = new ApplicationDbContext();

            if (db.Students.Any()) return 0;

            var faker = new Faker<Student>("en")
                        .RuleFor(s => s.Name, f => f.Person.FullName)
                        .RuleFor(s => s.BirthDay, f => f.Person.DateOfBirth)
                        .RuleFor(s => s.Phone, f => f.Phone.PhoneNumberFormat(6));

            var students = faker.Generate(50); 

            await db.Students.AddRangeAsync(students);
            return await db.SaveChangesAsync();

        }

        private static async Task<int> InsertCouses()
        {
            var db = new ApplicationDbContext();

            if (db.Courses.Any()) return 0;

            var couselist = new List<Course>();
            var course1 = new Course() { ClassNumber = "124A", Title = "Math" };
            var course2 = new Course() { ClassNumber = "124B", Title = "English" };

            couselist.Add(course1);
            couselist.Add(course2);

            db.AddRange(couselist);

            return await db.SaveChangesAsync();
        }

        private static async Task<IEnumerable<Student>> GetStudentsNameStartsWith(string word)
        {
            var db = new ApplicationDbContext();

            var studentlist = await db.Students.Where(s => s.Name.StartsWith(word)).ToListAsync();

            return studentlist;            
        }

        private static async Task<Course> GetMathCourse()
        {
            var db = new ApplicationDbContext();
            return await db.Courses.FirstOrDefaultAsync(c => c.Title == "Math");
        }

        private static async Task RemoveDataFromTakesCourse(ApplicationDbContext db)
        {
            var takesCourseList = await db.TakesCourses.AsNoTracking().ToListAsync();
            db.TakesCourses.RemoveRange((IEnumerable<TakesCourse>)takesCourseList);
            await db.SaveChangesAsync();
            takesCourseList.Clear();
        }

        private static async Task<int> SetMathFirstSemesterToStudentsStartsWithA()
        {
            var db = new ApplicationDbContext();
            await RemoveDataFromTakesCourse(db);

            var studentlist = await GetStudentsNameStartsWith("a");
            var mathCourse = await GetMathCourse();
            var takesCourseList = new List<TakesCourse>();

            studentlist.ToList().ForEach(s => {
                takesCourseList.Add(new TakesCourse { CourseId = mathCourse.Id, Semester = "First", StudentId = s.Id });
            });

            await db.TakesCourses.AddRangeAsync(takesCourseList);
            return await db.SaveChangesAsync();
        }
    }
}
