using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublishingHouse.Data.Migrations
{
    public partial class add_reviews_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReviewId",
                table: "Files",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_ReviewId",
                table: "Files",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PublicationId",
                table: "Reviews",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Reviews_ReviewId",
                table: "Files",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Reviews_ReviewId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Files_ReviewId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Files");
        }
    }
}
