using Questao5.Application.Commands.Requests;

namespace Questao5.Domain.Repositories
{
    public interface IIdempotanceRepository
    {
        public Task<int> ExistsAsync(Guid idIdemPotence);
        public void SaveAsync(Guid id, MoveCurrentAccountRequest response);
        //public Task<object?> GetAsync(Guid idIdempotence);
    }
}