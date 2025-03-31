using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Dtos;

namespace Questao5.Api.Controllers;
[ApiController]
[Route("api/v1")]
public class MovimentoController : ControllerBase
{
    private readonly IMediator _mediator;

    public MovimentoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("movimentarcontacorrente")]
    public async Task<Response> MovimentarContaCorrente([FromBody] MovimentoContaCorrenteCommand command, CancellationToken cancellationToken)
    {
        
        return await _mediator.Send(command, cancellationToken);
    }
}