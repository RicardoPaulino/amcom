using MediatR;
using Questao5.Application.Commands.Responses;
using System.Net;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentarContaCorrenteRequest : IRequest<(HttpStatusCode, MovimentarContaCorrenteReponse)>
    {
        public Guid IdRequest { get; set; }
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public char TypeMovement { get; set; }
    }
}