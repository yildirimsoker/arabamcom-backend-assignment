using Arabam.Com.Application.Common.Interfaces;
using Arabam.Com.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Arabam.Com.Infrastructure.Repositories
{
    public class AdvertVisitsRepository : IAdvertVisitsRepository
    {
        private readonly IConfiguration _configuration;

        public AdvertVisitsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddAsync(AdvertVisits entity)
        {
          
            var sql = @"INSERT INTO AdvertVisits (AdvertId, IPAddress, VisitDate) 
                        OUTPUT INSERTED.[Id]
                        Values (@AdvertId, @IPAddress, @VisitDate);";
            
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var id = await connection.QuerySingleAsync<int>(sql, entity);
                return id;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM AdvertVisits WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IEnumerable<AdvertVisits>> GetAllAsync()
        {
            var sql = "SELECT * FROM AdvertVisits";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<AdvertVisits>(sql);
                return result.ToList();
            }
        }

        public async Task<AdvertVisits> GetAsync(int id)
        {
            var sql = "SELECT * FROM AdvertVisits WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<AdvertVisits>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(AdvertVisits entity)
        {
            var sql = "UPDATE AdvertVisits SET AdvertId = @AdvertId, IPAddress = @IPAddress, VisitDate = @VisitDate WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

    }
}
