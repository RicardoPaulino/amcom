using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using System.Net;

namespace Questao5.Application.Handlers
{
    public class MoveCurrentAccountHandler : IRequestHandler<MoveCurrentAccountRequest, MoveCurrentAccountReponse>
    {
        private readonly IIdempotanceRepository _idempotenciaRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        public MoveCurrentAccountHandler(
               IIdempotanceRepository idempotenciaRepository,
               IMovementRepository movementRepository,
               IContaCorrenteRepository contaCorrenteRepository)
        {
            _idempotenciaRepository = idempotenciaRepository;
            _movementRepository = movementRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
        }
        public Task<MoveCurrentAccountReponse> Handle(MoveCurrentAccountRequest request, CancellationToken cancellationToken)
        {
            if (request.TypeMovement == 'C' || request.TypeMovement == 'D')
            {
                var idIdemPotence = _idempotenciaRepository.ExistsAsync(request.IdRequest);
                if (idIdemPotence.Result == 0)
                {
                    
                    var contaCorrente = _contaCorrenteRepository.BuscarContaCorrente(request.AccountNumber);
                    if (contaCorrente != null && contaCorrente.Ativo == true)
                    {
                        _movementRepository.Adicionar(new MovimentoEntity
                        {
                            IdMovimento = Guid.NewGuid(),
                            IdContaCorrente = contaCorrente.IdContaCorrente,
                            DataMovimento = DateTime.Now,
                            TipoMovimento = request.TypeMovement,
                            Valor = request.Amount

                        });
                        _idempotenciaRepository.SaveAsync(request.IdRequest, request);
                    }
                    else if (contaCorrente != null && contaCorrente!.Ativo == false)
                        return Task.FromResult(new MoveCurrentAccountReponse { StatusCode = HttpStatusCode.OK });
                    else
                        return Task.FromResult(new MoveCurrentAccountReponse { StatusCode = HttpStatusCode.OK });

                    return Task.FromResult(new MoveCurrentAccountReponse
                    {
                        StatusCode = HttpStatusCode.OK
                        //ResponseMessage =
                        //$"Foi creditado na conta {request.AccountNumber}, a quantia de" +
                        //$" R${request.Amount.ToString("F2", new System.Globalization.CultureInfo("pt-BR"))}"
                    });
                }
                else
                    return Task.FromResult(new MoveCurrentAccountReponse { StatusCode = HttpStatusCode.OK });
            }

            return Task.FromResult(new MoveCurrentAccountReponse { StatusCode = HttpStatusCode.OK });
        }
    }

}