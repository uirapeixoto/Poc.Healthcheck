using Newtonsoft.Json;
using Poc.Healthcheck.Helpers.Domain.DTOs.ViaCEPHttpService;
using Poc.Helalthcheck.Helpers.Domain.Interface.services;

namespace Poc.Healthcheck.Helpers.Infra.HttpServices
{
    public class ViaCEPHttpService : IViaCepService
    {
        #nullable disable
        public async Task<ViaCEPEnderecoResponseDTO> BuscarEnderecoPorCEPAsync(string cep)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    var url = new Uri($"https://viacep.com.br/ws/{cep}/json");
                    var response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                    
                    var _ = response.EnsureSuccessStatusCode();

                    if(response.Content is object)
                    {
                        var stream = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<ViaCEPEnderecoResponseDTO>(stream,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });

                        return data;
                    }

                    return new ViaCEPEnderecoResponseDTO();
                }
            }
            catch (HttpRequestException e) when (e.Message.Contains("301"))
            {
                throw new HttpRequestException(e.Message);
            }
        }
    }
}
