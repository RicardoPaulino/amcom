namespace Questao5.Application.Queries.Responses
{
    public class ConsultarSaldoContaResponse
    {
        public decimal Saldo { get; set; }
        public int NumeroContaCorrente { get; set; }
        public string? NomeTitular { get; set; }
        public DateTime DataHoraConsulta { get; set; }
        public string? TipoFalha { get; set; }
    }
}