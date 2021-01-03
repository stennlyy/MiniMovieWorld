using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniMovieWorld.Data.Migrations
{
    public partial class AddTables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActorRate_Actors_ActorId",
                table: "UserActorRate");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActorRate_AspNetUsers_UserId",
                table: "UserActorRate");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRate_AspNetUsers_UserId",
                table: "UserRate");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRate_Movies_MovieId",
                table: "UserRate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRate",
                table: "UserRate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserActorRate",
                table: "UserActorRate");

            migrationBuilder.RenameTable(
                name: "UserRate",
                newName: "UserRates");

            migrationBuilder.RenameTable(
                name: "UserActorRate",
                newName: "UserActorRates");

            migrationBuilder.RenameIndex(
                name: "IX_UserRate_UserId",
                table: "UserRates",
                newName: "IX_UserRates_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRate_MovieId",
                table: "UserRates",
                newName: "IX_UserRates_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRate_IsDeleted",
                table: "UserRates",
                newName: "IX_UserRates_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_UserActorRate_UserId",
                table: "UserActorRates",
                newName: "IX_UserActorRates_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserActorRate_IsDeleted",
                table: "UserActorRates",
                newName: "IX_UserActorRates_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_UserActorRate_ActorId",
                table: "UserActorRates",
                newName: "IX_UserActorRates_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRates",
                table: "UserRates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserActorRates",
                table: "UserActorRates",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserMovieComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMovieComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMovieComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMovieComments_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserMovieCommentId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_UserMovieComments_UserMovieCommentId",
                        column: x => x.UserMovieCommentId,
                        principalTable: "UserMovieComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IsDeleted",
                table: "Comments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserMovieCommentId",
                table: "Comments",
                column: "UserMovieCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovieComments_IsDeleted",
                table: "UserMovieComments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovieComments_MovieId",
                table: "UserMovieComments",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovieComments_UserId",
                table: "UserMovieComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActorRates_Actors_ActorId",
                table: "UserActorRates",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActorRates_AspNetUsers_UserId",
                table: "UserActorRates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRates_AspNetUsers_UserId",
                table: "UserRates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRates_Movies_MovieId",
                table: "UserRates",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActorRates_Actors_ActorId",
                table: "UserActorRates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActorRates_AspNetUsers_UserId",
                table: "UserActorRates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRates_AspNetUsers_UserId",
                table: "UserRates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRates_Movies_MovieId",
                table: "UserRates");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "UserMovieComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRates",
                table: "UserRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserActorRates",
                table: "UserActorRates");

            migrationBuilder.RenameTable(
                name: "UserRates",
                newName: "UserRate");

            migrationBuilder.RenameTable(
                name: "UserActorRates",
                newName: "UserActorRate");

            migrationBuilder.RenameIndex(
                name: "IX_UserRates_UserId",
                table: "UserRate",
                newName: "IX_UserRate_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRates_MovieId",
                table: "UserRate",
                newName: "IX_UserRate_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRates_IsDeleted",
                table: "UserRate",
                newName: "IX_UserRate_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_UserActorRates_UserId",
                table: "UserActorRate",
                newName: "IX_UserActorRate_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserActorRates_IsDeleted",
                table: "UserActorRate",
                newName: "IX_UserActorRate_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_UserActorRates_ActorId",
                table: "UserActorRate",
                newName: "IX_UserActorRate_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRate",
                table: "UserRate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserActorRate",
                table: "UserActorRate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActorRate_Actors_ActorId",
                table: "UserActorRate",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActorRate_AspNetUsers_UserId",
                table: "UserActorRate",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRate_AspNetUsers_UserId",
                table: "UserRate",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRate_Movies_MovieId",
                table: "UserRate",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
