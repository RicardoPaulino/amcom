using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories;
public class MovimentoContaCorrenteRepository : IMovimentoContaCorrenteRepository
{
    private readonly DatabaseConfig databaseConfig;

    public MovimentoContaCorrenteRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public void AdicionarMovimentacao(MovimentoContaCorrente movimentoContaCorrente)
    {
        using var connection = new SqliteConnection(databaseConfig.Name);

        connection.Execute("INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)" +
            " VALUES (@IdMovimentacao, @IdContaCorrente, @DataMovimentacao, @TipoMovimentacao, @Valor)"
            , movimentoContaCorrente);
    }
    public bool Existe(string idContaCorrente)
    {
        using var connection = new SqliteConnection(databaseConfig.Name);
        var result = connection.QueryFirstOrDefault<int>(
            "SELECT COUNT(1) FROM movimento WHERE idcontacorrente = @IdContaCorrente",
            new { IdContaCorrente = idContaCorrente });
        return result > 0;
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