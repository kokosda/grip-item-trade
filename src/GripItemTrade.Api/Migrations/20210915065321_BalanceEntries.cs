using Microsoft.EntityFrameworkCore.Migrations;

namespace GripItemTrade.Api.Migrations
{
    public partial class BalanceEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceEntry_Accounts_AccountId",
                table: "BalanceEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_BalanceEntry_Customers_CustomerId",
                table: "BalanceEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionOperationEntries_BalanceEntry_BalanceEntryId",
                table: "TransactionOperationEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BalanceEntry",
                table: "BalanceEntry");

            migrationBuilder.RenameTable(
                name: "BalanceEntry",
                newName: "BalanceEntries");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceEntry_CustomerId",
                table: "BalanceEntries",
                newName: "IX_BalanceEntries_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceEntry_AccountId",
                table: "BalanceEntries",
                newName: "IX_BalanceEntries_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BalanceEntries",
                table: "BalanceEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceEntries_Accounts_AccountId",
                table: "BalanceEntries",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceEntries_Customers_CustomerId",
                table: "BalanceEntries",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionOperationEntries_BalanceEntries_BalanceEntryId",
                table: "TransactionOperationEntries",
                column: "BalanceEntryId",
                principalTable: "BalanceEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceEntries_Accounts_AccountId",
                table: "BalanceEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_BalanceEntries_Customers_CustomerId",
                table: "BalanceEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionOperationEntries_BalanceEntries_BalanceEntryId",
                table: "TransactionOperationEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BalanceEntries",
                table: "BalanceEntries");

            migrationBuilder.RenameTable(
                name: "BalanceEntries",
                newName: "BalanceEntry");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceEntries_CustomerId",
                table: "BalanceEntry",
                newName: "IX_BalanceEntry_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceEntries_AccountId",
                table: "BalanceEntry",
                newName: "IX_BalanceEntry_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BalanceEntry",
                table: "BalanceEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceEntry_Accounts_AccountId",
                table: "BalanceEntry",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceEntry_Customers_CustomerId",
                table: "BalanceEntry",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionOperationEntries_BalanceEntry_BalanceEntryId",
                table: "TransactionOperationEntries",
                column: "BalanceEntryId",
                principalTable: "BalanceEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
