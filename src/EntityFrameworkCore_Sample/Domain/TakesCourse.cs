using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore_Sample.Domain
{
    public class TakesCourse
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public string Semester { get; set; }
    }
}
