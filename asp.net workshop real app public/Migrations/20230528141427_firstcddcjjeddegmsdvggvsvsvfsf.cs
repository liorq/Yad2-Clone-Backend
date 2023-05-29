using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp.networkshoprealapppublic.Migrations
{
    /// <inheritdoc />
    public partial class firstcddcjjeddegmsdvggvsvsvfsf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "dateOfEntering",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateOfEntering",
                table: "Apartments");
        }
    }
}
