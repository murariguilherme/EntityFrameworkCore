using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore_Sample.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ClassNumber { get; set; }
        public List<TakesCourse> TakesCourses { get; set; }

        public Course()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
