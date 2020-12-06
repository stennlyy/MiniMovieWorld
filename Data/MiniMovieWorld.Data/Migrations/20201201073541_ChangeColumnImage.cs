using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniMovieWorld.Data.Migrations
{
    public partial class ChangeColumnImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Images_ImageId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Images_ImageId",
                table: "Directors");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Images_ImageId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Writers_Images_ImageId",
                table: "Writers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Writers_ImageId",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ImageId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Directors_ImageId",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_ImageId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Actors");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Writers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Actors");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Writers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Directors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Actors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Writers_ImageId",
                table: "Writers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ImageId",
                table: "Movies",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_ImageId",
                table: "Directors",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_ImageId",
                table: "Actors",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_IsDeleted",
                table: "Images",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Images_ImageId",
                table: "Actors",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Images_ImageId",
                table: "Directors",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Images_ImageId",
                table: "Movies",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Writers_Images_ImageId",
                table: "Writers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
