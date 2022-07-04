using Microsoft.EntityFrameworkCore.Migrations;
using FlightTracker.DataAccess.Entities;
using FlightTracker.DataAccess.Helpers;

namespace FlightTracker.DataAccess.Migrations
{
    public partial class SetupUserAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Creates an admin user
            migrationBuilder.InsertData("Pilots",
                new string[]
                {
                    nameof(Pilot.FirstName),
                    nameof(Pilot.LastName),
                    nameof(Pilot.Role),
                    nameof(Pilot.Username),
                    nameof(Pilot.Password)
                },
                new object[]
                {
                    "Admin",
                    "User",
                    (int)Role.Administrator,
                    "admin",
                    Obfuscator.Encrypt("admin123")
                });

            // Creates a User user
            migrationBuilder.InsertData("Pilots",
                new string[]
                {
                    nameof(Pilot.FirstName),
                    nameof(Pilot.LastName),
                    nameof(Pilot.Role),
                    nameof(Pilot.Username),
                    nameof(Pilot.Password)
                },
                new object[]
                {
                    "Test",
                    "User",
                    (int)Role.User,
                    "user",
                    Obfuscator.Encrypt("user123")
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
