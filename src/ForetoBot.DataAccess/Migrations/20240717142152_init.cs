using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ForetoBot.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MimeType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Text = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Texts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ru = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    En = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Texts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DomanCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    NameId = table.Column<Guid>(type: "uuid", nullable: true),
                    DescriptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    LabelId = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomanCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomanCategories_Files_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomanCategories_Texts_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Texts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomanCategories_Texts_NameId",
                        column: x => x.NameId,
                        principalTable: "Texts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DomanCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true),
                    TitleSoundId = table.Column<Guid>(type: "uuid", nullable: true),
                    DescriptionSoundId = table.Column<Guid>(type: "uuid", nullable: true),
                    DomanCategoryId = table.Column<int>(type: "integer", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomanCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomanCards_DomanCategories_DomanCategoryId",
                        column: x => x.DomanCategoryId,
                        principalTable: "DomanCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomanCards_Files_DescriptionSoundId",
                        column: x => x.DescriptionSoundId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomanCards_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomanCards_Files_TitleSoundId",
                        column: x => x.TitleSoundId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomanCards_Texts_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Texts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomanCards_DescriptionSoundId",
                table: "DomanCards",
                column: "DescriptionSoundId");

            migrationBuilder.CreateIndex(
                name: "IX_DomanCards_DomanCategoryId",
                table: "DomanCards",
                column: "DomanCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DomanCards_ImageId",
                table: "DomanCards",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DomanCards_TitleId",
                table: "DomanCards",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_DomanCards_TitleSoundId",
                table: "DomanCards",
                column: "TitleSoundId");

            migrationBuilder.CreateIndex(
                name: "IX_DomanCategories_DescriptionId",
                table: "DomanCategories",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DomanCategories_LabelId",
                table: "DomanCategories",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_DomanCategories_NameId",
                table: "DomanCategories",
                column: "NameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomanCards");

            migrationBuilder.DropTable(
                name: "DomanCategories");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Texts");
        }
    }
}
