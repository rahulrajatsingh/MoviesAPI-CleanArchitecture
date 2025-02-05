using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MovieContextSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert seed data into the Movies table
            migrationBuilder.Sql("INSERT INTO Movies (MovieName, DirectorName, ReleaseYear) VALUES ('Inception', 'Christopher Nolan', '2010')");
            migrationBuilder.Sql("INSERT INTO Movies (MovieName, DirectorName, ReleaseYear) VALUES ('The Dark Knight', 'Christopher Nolan', '2008')");
            migrationBuilder.Sql("INSERT INTO Movies (MovieName, DirectorName, ReleaseYear) VALUES ('Interstellar', 'Christopher Nolan', '2014')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Optional: Remove the seed data (useful for rolling back)
            migrationBuilder.Sql("DELETE FROM Movies WHERE MovieName IN ('Inception', 'The Dark Knight', 'Interstellar')");
        }
    }
}
