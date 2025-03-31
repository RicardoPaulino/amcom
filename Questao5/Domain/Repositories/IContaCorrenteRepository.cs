using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IContaCorrenteRepository
    {
        public ContaCorrente BuscarContaCorrente(int idContaCorrente);
    }
}