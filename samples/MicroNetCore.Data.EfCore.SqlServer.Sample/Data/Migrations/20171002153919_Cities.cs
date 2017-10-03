using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Migrations
{
    public partial class Cities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "City",
                table => new
                {
                    Id = table.Column<long>("bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_City", x => x.Id); });

            migrationBuilder.CreateTable(
                "UserCity",
                table => new
                {
                    Entity1Id = table.Column<long>("bigint", nullable: false),
                    Entity2Id = table.Column<long>("bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCity", x => new {x.Entity1Id, x.Entity2Id});
                    table.ForeignKey(
                        "FK_UserCity_User_Entity1Id",
                        x => x.Entity1Id,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UserCity_City_Entity2Id",
                        x => x.Entity2Id,
                        "City",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_UserCity_Entity2Id",
                "UserCity",
                "Entity2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "UserCity");

            migrationBuilder.DropTable(
                "City");
        }
    }
}