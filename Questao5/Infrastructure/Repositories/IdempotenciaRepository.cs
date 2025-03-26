using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories
{
    public class IdempotenciaRepository : IIdempotanciaRepository
    {       
        private readonly DatabaseConfig databaseConfig;

        public IdempotenciaRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<IdempotenciaEntity> Existe(Guid idIdemPotence)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var result = await connection.QueryFirstOrDefaultAsync<IdempotenciaEntity>(
                "SELECT chave_idempotencia, resultado FROM Idempotencia WHERE chave_idempotencia = @Chave_idempotencia",
                new { Chave_idempotencia = idIdemPotence });
            return result;
        }

        public void Salvar(Guid id, MovimentarContaCorrenteRequest response)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var responseJson = System.Text.Json.JsonSerializer.Serialize(response);
            connection.Execute("INSERT INTO Idempotencia (chave_idempotencia, Requisicao) VALUES (@Id, @Response)", new { Id = id, Response = responseJson.ToString() });
           
        }
        public void Update(Guid id)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            connection.Execute("UPDATE Idempotencia SET Resultado = @Response WHERE chave_idempotencia = @Id", new { Id = id, Response = "Realizado com Sucesso." });
        }
    }
}