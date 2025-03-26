using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.Core;
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
            [FromBody] MovimentarContaCorrenteRequest command,
            CancellationToken cancelationToken)
        {
            var (statusCode, response) = await _mediator.Send(command, cancelationToken);
            return StatusCode((int)statusCode, response);
        }

        [HttpGet]
        [Route("ConsultarSaldo")]
        public async Task<IActionResult> BuscarSaldo(
            [FromHeader(Name = "NumeroContaCorrente")] int numeroContaCorrente,
            CancellationToken cancelationToken)
        {
            var query = new ConsultarSaldoRequest
            {
                NumeroContaCorrente = numeroContaCorrente
            };

            var (statusCode, response) = await _mediator.Send(query, cancelationToken);
            return StatusCode((int)statusCode, response);
        }
    }
}