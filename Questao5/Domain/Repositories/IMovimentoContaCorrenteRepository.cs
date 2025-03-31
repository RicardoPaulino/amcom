using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{ 

    public interface IMovimentoContaCorrenteRepository
    {
        void AdicionarMovimentacao(MovimentoContaCorrente movimentoContaCorrente);
        bool Existe(string idContaCorrente);
        decimal BuscarSaldo(string numeroContaCorrente);
    }
}