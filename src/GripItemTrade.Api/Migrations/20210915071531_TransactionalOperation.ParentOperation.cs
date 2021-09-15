using Microsoft.EntityFrameworkCore.Migrations;

namespace GripItemTrade.Api.Migrations
{
    public partial class TransactionalOperationParentOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BalanceEntryCode",
                table: "TransactionOperationEntries",
                type: "nvarchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentOperationId",
                table: "TransactionalOperations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "BalanceEntries",
                type: "nvarchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionalOperations_ParentOperationId",
                table: "TransactionalOperations",
                column: "ParentOperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalOperations_TransactionalOperations_ParentOperationId",
                table: "TransactionalOperations",
                column: "ParentOperationId",
                principalTable: "TransactionalOperations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalOperations_TransactionalOperations_ParentOperationId",
                table: "TransactionalOperations");

            migrationBuilder.DropIndex(
                name: "IX_TransactionalOperations_ParentOperationId",
                table: "TransactionalOperations");

            migrationBuilder.DropColumn(
                name: "ParentOperationId",
                table: "TransactionalOperations");

            migrationBuilder.AlterColumn<string>(
                name: "BalanceEntryCode",
                table: "TransactionOperationEntries",
                type: "nvarchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "BalanceEntries",
                type: "nvarchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldNullable: true);
        }
    }
}
