using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Repositories
{
    public class IdempotenceRepository : IIdempotanceRepository
    {       
        private readonly DatabaseConfig databaseConfig;

        public IdempotenceRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public Task<int> ExistsAsync(Guid idIdemPotence)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var result =  connection.Query<int>("SELECT COUNT(1) FROM Idempotencia WHERE chave_idempotencia = @Chave_idempotencia", new { Chave_idempotencia = idIdemPotence });
           
            return Task.FromResult(result.FirstOrDefault());
        }

        public void SaveAsync(Guid id, MoveCurrentAccountRequest response)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var responseJson = System.Text.Json.JsonSerializer.Serialize(response);
            connection.Execute("INSERT INTO Idempotencia (chave_idempotencia, Requisicao) VALUES (@Id, @Response)", new { Id = id, Response = responseJson.ToString() });
           
        }

        //public async Task<object?> GetAsync(Guid idIdemPotence)
        //{
        //   //var response =  await _connection.ExecuteScalarAsync("SELECT Response FROM Idempotence WHERE Id = @Id", new { Id = idIdemPotence });
        //   // return Task.FromResult(response);
        //}
    }
}
