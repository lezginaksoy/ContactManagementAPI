using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedFundContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FundContacts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "FundContacts");
        }
    }
}
