using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HosptitalManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class ios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Appointments");
        }
    }
}
