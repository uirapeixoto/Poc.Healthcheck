namespace Poc.Healthcheck.Helpers.Domain.DTOs.ViaCEPHttpService
{
    public class ViaCEPEnderecoResponseDTO
    {
        #nullable disable
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
    
    }
}
