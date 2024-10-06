using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessCardInformation.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBusinessCardRequiredFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "BusinessCards",
            nullable: false, 
            oldClrType: typeof(string),
            oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "BusinessCards",
                nullable: false, 
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "BusinessCards",
                nullable: false, 
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "BusinessCards",
                nullable: false, 
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "BusinessCards",
                nullable: false, 
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "BusinessCards",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
           name: "Name",
           table: "BusinessCards",
           nullable: true, 
           oldClrType: typeof(string),
           oldNullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "BusinessCards",
                nullable: true, 
                oldClrType: typeof(string),
                oldNullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "BusinessCards",
                nullable: true, 
                oldClrType: typeof(DateTime),
                oldNullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "BusinessCards",
                nullable: true, 
                oldClrType: typeof(string),
                oldNullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "BusinessCards",
                nullable: true, 
                oldClrType: typeof(string),
                oldNullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "BusinessCards",
                nullable: true, 
                oldClrType: typeof(string),
                oldNullable: false);
        }
    }
}
