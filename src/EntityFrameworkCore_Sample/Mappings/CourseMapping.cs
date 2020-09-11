using EntityFrameworkCore_Sample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
namespace EntityFrameworkCore_Sample.Mappings
{
    public class CourseMapping : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Title)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder
                .Property(c => c.ClassNumber)
                .HasColumnType("varchar(10)")
                .IsRequired();
        }
    }
}
