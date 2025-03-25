using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Repositories;
using System.Net;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoHandler : IRequestHandler<ConsultarSaldoRequest, ConsultarSaldoContaResponse>
    {
        private readonly IIdempotanceRepository _idempotenciaRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ConsultarSaldoHandler(
               IIdempotanceRepository idempotenciaRepository,
               IMovementRepository movementRepository,
               IContaCorrenteRepository contaCorrenteRepository)
        {
            _idempotenciaRepository = idempotenciaRepository;
            _movementRepository = movementRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public Task<ConsultarSaldoContaResponse> Handle(ConsultarSaldoRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = _contaCorrenteRepository.BuscarContaCorrente(request.NumeroContaCorrente);
            if (contaCorrente != null && contaCorrente.Ativo == true)
            {
                decimal saldo = _movementRepository.BuscarSaldo(contaCorrente.IdContaCorrente!);
                return Task.FromResult(new ConsultarSaldoContaResponse { Saldo = saldo, NumeroContaCorrente = contaCorrente.Numero });
            }

            return Task.FromResult(new ConsultarSaldoContaResponse { Saldo = 0, NumeroContaCorrente = 0 });
        }
    }
}
