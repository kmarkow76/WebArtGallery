using ArtGallery.DTO.Counterparties;

namespace ArtGallery.Interfaces.ServicesInterfaces;

public interface ICounterpartyService
{
    Task<CounterpartyResponseDto> CreateCounterpartyAsync(CreateCounterpartyDto counterpartyDto);
    Task<CounterpartyResponseDto> GetCounterpartyByIdAsync(int id);
    Task<IEnumerable<CounterpartyResponseDto>> GetAllCounterpartiesAsync();
    Task UpdateCounterpartyAsync(int id, UpdateCounterpartyDto counterpartyDto);
    Task DeleteCounterpartyAsync(int id);
    Task<IEnumerable<CounterpartyResponseDto>> GetCounterpartiesWithTotalIncomeAboveAsync(decimal threshold);
    Task<IEnumerable<CounterpartyResponseDto>> GetCounterpartiesWithTotalRentalCostAboveAsync(decimal threshold);
}