using Microsoft.EntityFrameworkCore.Migrations;
using FlightTracker.DataAccess.Entities;
using System;

namespace FlightTracker.DataAccess.Migrations
{
    public partial class AddSampleFlights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            int ownerId = 1;
            for (int i = 0; i < 10; i++)
            {
                ownerId = ownerId == 1 ? 2 : 1;
                migrationBuilder.InsertData("Flights",
                    new string[]
                    {
                    nameof(Flight.Distance),
                    nameof(Flight.Duration),
                    nameof(Flight.Date),
                    "OwnerId",
                    nameof(Flight.Comments)
                    },
                    new object[]
                    {
                    (i+10)*5,
                    TimeSpan.FromMinutes(30),
                    DateTime.Now,
                    ownerId,
                    $"Test {i}"
                    });
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
