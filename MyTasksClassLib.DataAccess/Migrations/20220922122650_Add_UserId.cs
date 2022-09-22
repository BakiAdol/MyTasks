using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTasksClassLib.DataAccess.Migrations
{
    public partial class Add_UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MyTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MyTasks");
        }
    }
}
