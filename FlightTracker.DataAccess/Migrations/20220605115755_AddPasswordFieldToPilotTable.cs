using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightTracker.DataAccess.Migrations
{
    public partial class AddPasswordFieldToPilotTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Pilots",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Pilots");
        }
    }
}
