using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GripItemTrade.Api.Migrations
{
    public partial class BalanceEntryTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "BalanceEntries",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "BalanceEntries");
        }
    }
}
