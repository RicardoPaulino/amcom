using Questao5.Application.Abstractions.Command;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Dtos;
using Questao5.Domain.Repositories;

namespace Questao5.Application.Handlers;

public class MovimentoContaCorrenteHandler : ICommand<MovimentoContaCorrenteCommand, Response>
{
    private readonly IMovimentoContaCorrenteRepository _movementRepository;
    private readonly IContaCorrenteRepository _contaCorrenteRepository;

    public MovimentoContaCorrenteHandler(
        IMovimentoContaCorrenteRepository movimentoContaCorrenteRepository, 
        IContaCorrenteRepository contaCorrenteRepository)
    {
        _movementRepository = movimentoContaCorrenteRepository;
        _contaCorrenteRepository = contaCorrenteRepository;
    }
    public Task<Response> Handle(MovimentoContaCorrenteCommand command, CancellationToken cancellationToken)
    {
        if(command == null)
            return new Task<Response>({ })
        return Task.FromResult(new Response());
    }
}
