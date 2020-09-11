using EntityFrameworkCore_Sample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EntityFrameworkCore_Sample.Mappings
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.Name)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder
                .Property(s => s.Phone)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder
                .Property(s => s.BirthDay)                
                .IsRequired();
        }
    }
}
