using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/v1/moviment")]
    public class MovimentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MovimentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CriarMovimentacao")]
        public async Task<IActionResult> CriarMovimentacao(
            [FromBody] MoveCurrentAccountRequest command,
            CancellationToken cancelationToken)
        {
            var response = await _mediator.Send(command, cancelationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("ConsultarSaldo")]
        public async Task<IActionResult> BuscarSaldo(
            [FromHeader(Name = "IdRequest")] Guid idRequest,
            [FromHeader(Name = "NumeroContaCorrente")] int numeroContaCorrente,
            CancellationToken cancelationToken)
        {
            var query = new ConsultarSaldoRequest
            {
                IdRequest = idRequest,
                NumeroContaCorrente = numeroContaCorrente
            };

            var response = await _mediator.Send(query, cancelationToken);
            return Ok(response);
        }
    }
}