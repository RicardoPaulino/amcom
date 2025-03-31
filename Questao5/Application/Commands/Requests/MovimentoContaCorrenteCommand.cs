using MediatR;
using Questao5.Domain.Dtos;

namespace Questao5.Application.Commands.Requests;
public class MovimentoContaCorrenteCommand : IRequest<Response>{
    public Guid IdRequisicao { get; set; }
    public int IdContaCorrente { get; set; }
    public char TipoMovimentacao { get; set; }
    public DateTime DataMovimentacao { get; set; }
    public decimal Valor { get; set; } 
}