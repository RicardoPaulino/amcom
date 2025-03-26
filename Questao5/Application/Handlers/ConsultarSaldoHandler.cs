using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Repositories;
using System.Net;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoHandler : IRequestHandler<ConsultarSaldoRequest, (HttpStatusCode, ConsultarSaldoContaResponse)>
    {
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ConsultarSaldoHandler(
               IMovimentoRepository movementRepository,
               IContaCorrenteRepository contaCorrenteRepository)
        {
            _movimentoRepository = movementRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<(HttpStatusCode, ConsultarSaldoContaResponse)> Handle(ConsultarSaldoRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = _contaCorrenteRepository.BuscarContaCorrente(request.NumeroContaCorrente);
            if (contaCorrente == null)
            {
                return (HttpStatusCode.BadRequest, new ConsultarSaldoContaResponse { DataHoraConsulta = DateTime.Now, TipoFalha = "INVALID_ACCOUNT" });
            }

            if (!contaCorrente.Ativo)
            {
                return (HttpStatusCode.BadRequest, new ConsultarSaldoContaResponse { DataHoraConsulta = DateTime.Now, TipoFalha = "INACTIVE_ACCOUNT" });
            }
            var movimentacao = _movimentoRepository.Existe(contaCorrente.IdContaCorrente!);
            if (movimentacao)
            {
                decimal saldo = _movimentoRepository.BuscarSaldo(contaCorrente.IdContaCorrente!);
                var result = new ConsultarSaldoContaResponse
                {
                    Saldo = saldo,
                    NumeroContaCorrente = contaCorrente.Numero,
                    NomeTitular = contaCorrente.Nome!,
                    DataHoraConsulta = DateTime.Now
                };
                return (HttpStatusCode.OK, result);
            }
            else
            {
                var result = new ConsultarSaldoContaResponse
                {
                    Saldo = 0,
                    NumeroContaCorrente = contaCorrente.Numero,
                    NomeTitular = contaCorrente.Nome!,
                    DataHoraConsulta = DateTime.Now
                };
                return (HttpStatusCode.OK, result);
            }            
        }
    }
}