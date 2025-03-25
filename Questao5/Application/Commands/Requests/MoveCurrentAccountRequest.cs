using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class MoveCurrentAccountRequest : IRequest<MoveCurrentAccountReponse>
    {
        public Guid IdRequest { get; set; }
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public char TypeMovement { get; set; }
    }
}