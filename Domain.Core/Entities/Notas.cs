namespace Domain.Core.Entities
{
    public class Notas
    {
        public string Id { get; set; }
        public decimal Valor { get; set; }
        public string ClienteId { get; set; }
        public string Observacoes { get; set; }
        
    }
}