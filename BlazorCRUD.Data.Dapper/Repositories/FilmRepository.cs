using BlazorCRUD.Model;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BlazorCRUD.Data.Dapper.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private string ConnectionString;

        public FilmRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public async Task<bool> DeleteFilm(int id)
        {
            var db = dbConnection();
            var sql=@"Delete FROM Films WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql.ToString(), new { Id = id });
            return result > 0;
        }

        public async Task<IEnumerable<Film>> GetAllFilms()
        {
            var db = dbConnection();
            var sql = @"SELECT Id, Title, Director,Description, ReleaseDate FROM Films";
            return await db.QueryAsync<Film>(sql.ToString(), new { });
        }

        public async Task<Film> GetFilmDetails(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT Id, Title, Director,ReleaseDate, Description FROM Films WHERE Id = @Id ";
            return await db.QueryFirstOrDefaultAsync<Film>(sql.ToString(), new { Id = id });

        }

        public async Task<bool> InsertFilm(Film film)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO Films (Title, Director, ReleaseDate, Description) VALUES (@Title, @Director, @ReleaseDate, @Description)";
            var result = await db.ExecuteAsync(sql.ToString(), new {film.Title,film.Director,film.ReleaseDate,film.Description});
            return result > 0;
        }
        
        public async Task<bool> UpdateFilm(Film film)
        {
            var db = dbConnection();
            var sql = @"UPDATE Films SET Title = @Title, Director = @Director, Description = @Description, ReleaseDate = @ReleaseDate WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, film);
            return result > 0;
        }
    }
}
