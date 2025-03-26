using MediatR;
using Questao5.Application.Queries.Responses;
using System.Net;

namespace Questao5.Application.Queries.Requests
{
    public class ConsultarSaldoRequest : IRequest<(HttpStatusCode ,ConsultarSaldoContaResponse)>
    {
        public int NumeroContaCorrente { get; set; }
    }
}