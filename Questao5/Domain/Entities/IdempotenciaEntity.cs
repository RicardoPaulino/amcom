﻿namespace Questao5.Domain.Entities
{
    public class IdempotenciaEntity
    {
        public string? Chave_Idempotencia { get; set; }
        public string? Resultado { get; set; }
    }
}