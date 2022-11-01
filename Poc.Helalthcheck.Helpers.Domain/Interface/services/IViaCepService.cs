using Poc.Healthcheck.Helpers.Domain.DTOs.ViaCEPHttpService;

namespace Poc.Helalthcheck.Helpers.Domain.Interface.services
{
    public interface IViaCepService
    {
        Task<ViaCEPEnderecoResponseDTO> BuscarEnderecoPorCEPAsync(string cep);
    }
}
