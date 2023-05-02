using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELEARN.Migrations
{
    public partial class CreateIsMainColumnCourseImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "CourseImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "CourseImages");
        }
    }
}
