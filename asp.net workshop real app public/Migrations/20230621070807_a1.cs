using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp.networkshoprealapppublic.Migrations
{
    /// <inheritdoc />
    public partial class a1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pics",
                columns: table => new
                {
                    picId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    apartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pics", x => x.picId);
                    table.ForeignKey(
                        name: "FK_Pics_Apartments_apartmentId",
                        column: x => x.apartmentId,
                        principalTable: "Apartments",
                        principalColumn: "apartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pics_apartmentId",
                table: "Pics",
                column: "apartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pics");
        }
    }
}
