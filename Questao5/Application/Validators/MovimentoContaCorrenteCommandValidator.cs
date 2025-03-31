using FluentValidation;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Resources;

namespace Questao5.Application.Validators;

public class MovimentoContaCorrenteCommandValidator : AbstractValidator<MovimentoContaCorrenteCommand>
{
    public MovimentoContaCorrenteCommandValidator()
    {
        RuleFor(x => x.TipoMovimentacao)
            .Must(tipo => tipo == 'C' || tipo == 'D')
            .WithMessage(Mensagem.TipoMovimentacaoInvalido);

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage(Mensagem.ValorInvalido);
    }
}