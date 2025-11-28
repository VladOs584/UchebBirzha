using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UchebBirzha.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategorySet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    CompletedTasksCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ExecutorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskSet_CategorySet_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategorySet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskSet_UserSet_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "UserSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskSet_UserSet_ExecutorId",
                        column: x => x.ExecutorId,
                        principalTable: "UserSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BidSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProposedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    ExecutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidSet_TaskSet_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidSet_UserSet_ExecutorId",
                        column: x => x.ExecutorId,
                        principalTable: "UserSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    ExecutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewSet", x => x.Id);
                    table.CheckConstraint("CK_Review_Rating", "[Rating] >= 1 AND [Rating] <= 5");
                    table.ForeignKey(
                        name: "FK_ReviewSet_TaskSet_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewSet_UserSet_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "UserSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewSet_UserSet_ExecutorId",
                        column: x => x.ExecutorId,
                        principalTable: "UserSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskAttachmentSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAttachmentSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAttachmentSet_TaskSet_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategorySet",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Задачи по математике, алгебре, геометрии", "Математика" },
                    { 2, "Задачи по программированию на различных языках", "Программирование" },
                    { 3, "Задачи по физике и механике", "Физика" },
                    { 4, "Задачи по химии и биохимии", "Химия" },
                    { 5, "Задачи по английскому языку и переводы", "Английский язык" },
                    { 6, "Задачи по экономике и финансам", "Экономика" },
                    { 7, "Задачи по истории и обществознанию", "История" },
                    { 8, "Прочие учебные задачи", "Другое" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidSet_CreatedAt",
                table: "BidSet",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BidSet_ExecutorId",
                table: "BidSet",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_BidSet_IsAccepted",
                table: "BidSet",
                column: "IsAccepted");

            migrationBuilder.CreateIndex(
                name: "IX_BidSet_TaskId",
                table: "BidSet",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySet_Name",
                table: "CategorySet",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSet_AuthorId",
                table: "ReviewSet",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSet_CreatedAt",
                table: "ReviewSet",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSet_ExecutorId",
                table: "ReviewSet",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSet_Rating",
                table: "ReviewSet",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSet_TaskId",
                table: "ReviewSet",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachmentSet_TaskId",
                table: "TaskAttachmentSet",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachmentSet_UploadedAt",
                table: "TaskAttachmentSet",
                column: "UploadedAt");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSet_CategoryId",
                table: "TaskSet",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSet_CreatedAt",
                table: "TaskSet",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSet_CustomerId",
                table: "TaskSet",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSet_Deadline",
                table: "TaskSet",
                column: "Deadline");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSet_ExecutorId",
                table: "TaskSet",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSet_Status",
                table: "TaskSet",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_UserSet_Email",
                table: "UserSet",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSet_IsActive",
                table: "UserSet",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_UserSet_Rating",
                table: "UserSet",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_UserSet_Role",
                table: "UserSet",
                column: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidSet");

            migrationBuilder.DropTable(
                name: "ReviewSet");

            migrationBuilder.DropTable(
                name: "TaskAttachmentSet");

            migrationBuilder.DropTable(
                name: "TaskSet");

            migrationBuilder.DropTable(
                name: "CategorySet");

            migrationBuilder.DropTable(
                name: "UserSet");
        }
    }
}
