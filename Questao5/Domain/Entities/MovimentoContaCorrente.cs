namespace Questao5.Domain.Entities;
public class MovimentoContaCorrente{
    public string? IdMovimentacao { get; set; }
    public int IdContaCorrente { get; set; }
    public DateTime DataMovimentacao { get; set; }
    public decimal Valor { get; set; }
    public char TipoMovimentacao { get; set; }
}