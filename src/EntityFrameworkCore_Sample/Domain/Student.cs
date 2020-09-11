using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore_Sample.Domain
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }

        public List<TakesCourse> TakesCourses { get; set; }

        public Student()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
