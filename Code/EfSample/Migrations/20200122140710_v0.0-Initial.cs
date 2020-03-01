using Microsoft.EntityFrameworkCore.Migrations;

namespace EfSample.Migrations
{
    public partial class v00Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Isbn = table.Column<string>(maxLength: 200, nullable: false),
                    Bookcase = table.Column<string>(maxLength: 10, nullable: false),
                    AuthorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.Sql(@"
                    SET IDENTITY_INSERT [dbo].[Authors] ON 

                    INSERT [dbo].[Authors] ([Id], [Name], [LastName]) VALUES (1, N'Bandon', N'Sanderson')
                    INSERT [dbo].[Authors] ([Id], [Name], [LastName]) VALUES (2, N'Isaac', N'Asimov')
                    SET IDENTITY_INSERT [dbo].[Authors] OFF
                    SET IDENTITY_INSERT [dbo].[Books] ON 

                    INSERT [dbo].[Books] ([Id], [Title], [Isbn], [Bookcase], [AuthorId]) VALUES (1, N'El Imperio final', N'ERFD-A4F2', N'P3Z22', 1)
                    INSERT [dbo].[Books] ([Id], [Title], [Isbn], [Bookcase], [AuthorId]) VALUES (2, N'El pozo de la ascensión', N'AS45-F7T7', N'P4Z12', 1)
                    INSERT [dbo].[Books] ([Id], [Title], [Isbn], [Bookcase], [AuthorId]) VALUES (3, N'El héroe de las eras', N'45S6-7T4R', N'P2Z14', 1)
                    INSERT [dbo].[Books] ([Id], [Title], [Isbn], [Bookcase], [AuthorId]) VALUES (4, N'Fundación', N'89AA-A65S', N'P8Z3', 2)
                    INSERT [dbo].[Books] ([Id], [Title], [Isbn], [Bookcase], [AuthorId]) VALUES (5, N'Fundación e Imperio', N'ASD5-A5SD', N'P14Z16', 2)
                    INSERT [dbo].[Books] ([Id], [Title], [Isbn], [Bookcase], [AuthorId]) VALUES (6, N'Segunda Fundación', N'A54D-HJ32', N'P19Z4', 2)
                    SET IDENTITY_INSERT [dbo].[Books] OFF"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
