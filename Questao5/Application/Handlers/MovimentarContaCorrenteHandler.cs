using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using System.Net;

namespace Questao5.Application.Handlers
{
    public class MovimentarContaCorrenteHandler : IRequestHandler<MovimentarContaCorrenteRequest, (HttpStatusCode, MovimentarContaCorrenteReponse)>
    {
        private readonly IIdempotanciaRepository _idempotenciaRepository;
        private readonly IMovimentoRepository _movementRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        public MovimentarContaCorrenteHandler(
               IIdempotanciaRepository idempotenciaRepository,
               IMovimentoRepository movementRepository,
               IContaCorrenteRepository contaCorrenteRepository)
        {
            _idempotenciaRepository = idempotenciaRepository;
            _movementRepository = movementRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<(HttpStatusCode, MovimentarContaCorrenteReponse)> Handle(MovimentarContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            if (request.TypeMovement != 'C' && request.TypeMovement != 'D')
                return (HttpStatusCode.BadRequest, new MovimentarContaCorrenteReponse { Message = "INVALID_TYPE" });

            if (request.Amount <= 0)
                return (HttpStatusCode.BadRequest, new MovimentarContaCorrenteReponse { Message = "INVALID_VALUE" });

            var contaCorrente = _contaCorrenteRepository.BuscarContaCorrente(request.AccountNumber);
            if (contaCorrente == null)
                return (HttpStatusCode.BadRequest, new MovimentarContaCorrenteReponse { Message = "INVALID_ACCOUNT" });

            if (!contaCorrente.Ativo)
                return (HttpStatusCode.BadRequest, new MovimentarContaCorrenteReponse { Message = "INACTIVE_ACCOUNT" });

            var idempotencia = await _idempotenciaRepository.Existe(request.IdRequest);
            if (idempotencia != null)
                return (HttpStatusCode.BadRequest, new MovimentarContaCorrenteReponse { Message = "DUPLCATED_REQUEST" });

            if (idempotencia == null)
                _idempotenciaRepository.Salvar(request.IdRequest, request);

            _movementRepository.Adicionar(new MovimentoEntity
            {
                IdMovimento = Guid.NewGuid(),
                IdContaCorrente = contaCorrente.IdContaCorrente,
                DataMovimento = DateTime.Now,
                TipoMovimento = request.TypeMovement,
                Valor = request.Amount
            });
            _idempotenciaRepository.Update(request.IdRequest);


            return (HttpStatusCode.BadRequest, new MovimentarContaCorrenteReponse { Message = "SUCCESS" });
        }
    }

}