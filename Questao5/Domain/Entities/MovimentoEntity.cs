namespace Questao5.Domain.Entities
{
    public class MovimentoEntity
    {
        public Guid IdMovimento { get; set; }
        public string? IdContaCorrente { get; set; }
        public DateTime DataMovimento { get; set; }
        public char TipoMovimento { get; set; } 
        public decimal Valor { get; set; }
    }
}