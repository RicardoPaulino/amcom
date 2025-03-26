using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IIdempotanciaRepository
    {
        public Task<IdempotenciaEntity> Existe(Guid idIdemPotence);
        public void Salvar(Guid id, MovimentarContaCorrenteRequest response);
        public void Update(Guid idIdemPotence);
    }
}