using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineForum.DAL.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    ThreadId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Downvotes = table.Column<int>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Upvotes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.ThreadId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Threads");
        }
    }
}
