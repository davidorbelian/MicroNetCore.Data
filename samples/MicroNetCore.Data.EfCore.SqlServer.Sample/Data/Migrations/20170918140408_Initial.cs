using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "SampleClassTwo",
                table => new
                {
                    Id = table.Column<long>("bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_SampleClassTwo", x => x.Id); });

            migrationBuilder.CreateTable(
                "SampleClassOne",
                table => new
                {
                    Id = table.Column<long>("bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>("nvarchar(max)", nullable: true),
                    SampleClassTwoId = table.Column<long>("bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleClassOne", x => x.Id);
                    table.ForeignKey(
                        "FK_SampleClassOne_SampleClassTwo_SampleClassTwoId",
                        x => x.SampleClassTwoId,
                        "SampleClassTwo",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_SampleClassOne_SampleClassTwoId",
                "SampleClassOne",
                "SampleClassTwoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "SampleClassOne");

            migrationBuilder.DropTable(
                "SampleClassTwo");
        }
    }
}