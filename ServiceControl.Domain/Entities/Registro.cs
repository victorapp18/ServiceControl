using ServiceControl.Domain.Enums;

namespace ServiceControl.Domain.Entities
{
    public class Registro
    {
        public string Id { get; set; }
        public string ServicoExecutado { get; set; }
        public DateTime Data { get; set; }
        public string Responsavel { get; set; }
        public double Temperatura { get; set; }
        public CondicaoClimatica CondicaoClimatica { get; set; }
    }
}
