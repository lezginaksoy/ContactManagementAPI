using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ContactManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class initalcontactmanagment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FundContacts",
                table: "FundContacts");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FundContacts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FundContacts",
                table: "FundContacts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FundContacts_FundId",
                table: "FundContacts",
                column: "FundId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FundContacts",
                table: "FundContacts");

            migrationBuilder.DropIndex(
                name: "IX_FundContacts_FundId",
                table: "FundContacts");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FundContacts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FundContacts",
                table: "FundContacts",
                columns: new[] { "FundId", "ContactId" });
        }
    }
}
