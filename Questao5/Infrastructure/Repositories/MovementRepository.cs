using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public MovementRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
        public void Adicionar(MovimentoEntity movement)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
           
            connection.Execute("INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)" +
                " VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)"
                , movement);

        }

        public decimal BuscarSaldo(string numeroContaCorrente)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var saldo = connection.QueryFirstOrDefault<decimal>(
                "SELECT (SUM(CASE WHEN tipomovimento = 'C' THEN Valor ELSE 0 END) - SUM(CASE WHEN tipomovimento = 'D' THEN Valor ELSE 0 END)) " +
                "FROM movimento WHERE idcontacorrente = @NumeroContaCorrente",
                new { NumeroContaCorrente = numeroContaCorrente });
            return saldo;
        }
    }
}
