using EntityFrameworkCore_Sample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EntityFrameworkCore_Sample.Mappings
{
    public class TakesCourseMapping : IEntityTypeConfiguration<TakesCourse>
    {
        public void Configure(EntityTypeBuilder<TakesCourse> builder)
        {
            builder
                .HasKey(t => new { t.StudentId, t.CourseId });

            builder
                .Property(t => t.Semester)
                .HasColumnType("varchar(15)")
                .IsRequired();

            builder
                .Property(t => t.CourseId)                
                .IsRequired();

            builder
                .HasOne(t => t.Student)
                .WithMany(s => s.TakesCourses)
                .HasForeignKey(t => t.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.Course)
                .WithMany(c => c.TakesCourses)
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
