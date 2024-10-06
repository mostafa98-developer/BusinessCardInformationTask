using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessCardInformation.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.CreateTable(
                    name: "BusinessCards",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                        Email = table.Column<string>(type: "nvarchar(256)", nullable: false), // Specify length for index optimization
                        Phone = table.Column<string>(type: "nvarchar(15)", nullable: false),  // Specify length for phone numbers
                        Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        PhotoBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_BusinessCards", x => x.Id);
                        // Enforce unique constraints on Email and Phone
                        table.UniqueConstraint("AK_BusinessCards_Email", x => x.Email);
                        table.UniqueConstraint("AK_BusinessCards_Phone", x => x.Phone);
                    }
                 );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCards");
        }
    }
}
