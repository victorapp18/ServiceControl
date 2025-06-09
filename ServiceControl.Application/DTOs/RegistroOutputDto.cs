namespace ServiceControl.Application.DTOs
{
    public class RegistroOutputDto
    {
        public string Id { get; set; }
        public string ServicoExecutado { get; set; }
        public DateTime Data { get; set; }
        public string Responsavel { get; set; }
        public double Temperatura { get; set; }
        public string CondicaoClimatica { get; set; }
        public string Cidade { get; set; }
    }
}