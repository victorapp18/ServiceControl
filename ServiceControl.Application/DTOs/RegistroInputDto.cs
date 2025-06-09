using System;

namespace ServiceControl.Application.DTOs
{
    public class RegistroInputDto
    {
        public string ServicoExecutado { get; set; }
        public DateTime Data { get; set; }
        public string Responsavel { get; set; }
        public string Cidade { get; set; }
    }
}
