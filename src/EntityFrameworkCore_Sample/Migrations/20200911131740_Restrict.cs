using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCore_Sample.Migrations
{
    public partial class Restrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakesCourses_Courses_CourseId",
                table: "TakesCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_TakesCourses_Students_StudentId",
                table: "TakesCourses");

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCourses_Courses_CourseId",
                table: "TakesCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCourses_Students_StudentId",
                table: "TakesCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakesCourses_Courses_CourseId",
                table: "TakesCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_TakesCourses_Students_StudentId",
                table: "TakesCourses");

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCourses_Courses_CourseId",
                table: "TakesCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCourses_Students_StudentId",
                table: "TakesCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
