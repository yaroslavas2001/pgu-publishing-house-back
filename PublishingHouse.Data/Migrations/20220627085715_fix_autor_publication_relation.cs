using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublishingHouse.Data.Migrations
{
    public partial class fix_autor_publication_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicationsAuthors_Authors_PublicationId",
                table: "PublicationsAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationsAuthors_Publications_AuthorId",
                table: "PublicationsAuthors");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationsAuthors_Authors_AuthorId",
                table: "PublicationsAuthors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationsAuthors_Publications_PublicationId",
                table: "PublicationsAuthors",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicationsAuthors_Authors_AuthorId",
                table: "PublicationsAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationsAuthors_Publications_PublicationId",
                table: "PublicationsAuthors");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationsAuthors_Authors_PublicationId",
                table: "PublicationsAuthors",
                column: "PublicationId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationsAuthors_Publications_AuthorId",
                table: "PublicationsAuthors",
                column: "AuthorId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
