using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
        public ContaCorrenteEntity BuscarContaCorrente(int numeroContaCorrente)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var result = connection.QueryFirstOrDefault<ContaCorrenteEntity>(
                "SELECT IdContaCorrente, Numero, Nome, Ativo FROM ContaCorrente WHERE Numero = @Numero",
                new { Numero = numeroContaCorrente });           
            return result;
        }
    }
}