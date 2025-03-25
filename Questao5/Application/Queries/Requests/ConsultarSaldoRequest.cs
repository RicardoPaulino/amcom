using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class ConsultarSaldoRequest : IRequest<ConsultarSaldoContaResponse>
    {
        public Guid IdRequest { get; set; }
        public int NumeroContaCorrente { get; set; }
    }
}