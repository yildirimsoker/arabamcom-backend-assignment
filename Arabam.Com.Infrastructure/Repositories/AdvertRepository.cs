using Arabam.Com.Application.Common.Interfaces;
using Arabam.Com.Domain.Common;
using Arabam.Com.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Arabam.Com.Infrastructure.Repositories
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly IConfiguration _configuration;

        public AdvertRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Adverts WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IEnumerable<Adverts>> GetAllAsync()
        {
            var sql = "SELECT * FROM Adverts";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Adverts>(sql);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<Adverts>> GetAllAsync(Filter filter)
        {

            var sqlBuilder = new SqlBuilder();


            if (filter.Where.Count > 0)
            {
                foreach (var whereClause in filter.Where)
                {
                    sqlBuilder.Where($"{whereClause.Key} = N'{whereClause.Value}'");
                }
            }

            var sqlTemlate = sqlBuilder.AddTemplate($"SELECT * FROM Adverts" +
                                     $" {(filter.Where.Count() > 0 ? " /**where**/" : "")}" +
                                     $" ORDER BY {filter.OrderByColumnName} {filter.OrderBy}" +
                                     $" OFFSET {filter.Offset} ROWS" +
                                     $" FETCH NEXT {filter.Next} ROWS ONLY");


            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Adverts>(sqlTemlate.RawSql);
                return result.ToList();
            }
        }

        public async Task<int> GetCountAsync(Filter filter)
        {

            var sqlBuilder = new SqlBuilder();


            if (filter.Where.Count > 0)
            {
                foreach (var whereClause in filter.Where)
                {
                    sqlBuilder.Where($"{whereClause.Key} = N'{whereClause.Value}'");
                }
            }

            var sqlTemlate = sqlBuilder.AddTemplate($"SELECT COUNT(*) FROM Adverts" +
                                     $" {(filter.Where.Count() > 0 ? " /**where**/" : "")}");


            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteScalarAsync<int>(sqlTemlate.RawSql);
                return result;
            }
        }

        public async Task<Adverts> GetAsync(int id)
        {
            var sql = "SELECT * FROM Adverts WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Adverts>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<bool> GetAnyAsync(int id)
        {
            var sql = "SELECT CASE WHEN COUNT(Id) > 0 THEN 1 ELSE 0 END FROM Adverts WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleAsync<bool>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> AddAsync(Adverts entity)
        {
            var sql = @"INSERT INTO Adverts (MemberId,CityId,CategoryId,ModelId,TownId,CityName,TownName,ModelName,Year,Price,Title,Date,CategoryName,Km,Color,Gear,Fuel,FirstPhoto,SecondPhoto,UserInfo,UserPhone,Text) 
                        OUTPUT INSERTED.[Id]
                        Values (@MemberId,@CityId,@CategoryId,@ModelId,@TownId,@CityName,@TownName,@ModelName,@Year,@Price,@Title,@Date,@CategoryName,@Km,@Color,@Gear,@Fuel,@FirstPhoto,@SecondPhoto,@UserInfo,@UserPhone,@Text);";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var id = await connection.QuerySingleAsync<int>(sql, entity);
                return id;
            }
        }

        public async Task<int> UpdateAsync(Adverts entity)
        {

            var sql = $"UPDATE Adverts SET MemberId = @MemberId, CityId = @CityId, CategoryId = @CategoryId, TownId = @TownId, TownName = @TownName, ModelName = @ModelName, Year = @Year, Price = @Price, Title = @Title WHERE Id = @Id;";
                   
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}
