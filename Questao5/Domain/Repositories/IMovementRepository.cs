using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IMovementRepository
    {
        public void Adicionar(MovimentoEntity amount);
        public decimal BuscarSaldo(string numeroContaCorrente);
    }
}