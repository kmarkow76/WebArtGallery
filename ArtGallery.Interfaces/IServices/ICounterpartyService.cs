using ArtGallery.DTO.Counterparties;

namespace ArtGallery.Interfaces.IServices;

/// <summary>
/// Сервис для работы с контрагентами.
/// </summary>
public interface ICounterpartyService
{
    /// <summary>
    /// Создает контрагента.
    /// </summary>
    /// <param name="counterpartyDto">Данные контрагента.</param>
    /// <returns>Созданный контрагент.</returns>
    Task<CounterpartyResponseDto> CreateCounterpartyAsync(CreateCounterpartyDto counterpartyDto);

    /// <summary>
    /// Получает контрагента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    /// <returns>Контрагент.</returns>
    Task<CounterpartyResponseDto> GetCounterpartyByIdAsync(int id);

    /// <summary>
    /// Получает всех контрагентов.
    /// </summary>
    /// <returns>Список контрагентов.</returns>
    Task<IEnumerable<CounterpartyResponseDto>> GetAllCounterpartiesAsync();

    /// <summary>
    /// Обновляет контрагента.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    /// <param name="counterpartyDto">Обновленные данные контрагента.</param>
    Task UpdateCounterpartyAsync(int id, UpdateCounterpartyDto counterpartyDto);

    /// <summary>
    /// Удаляет контрагента.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    Task DeleteCounterpartyAsync(int id);

    /// <summary>
    /// Получает контрагентов с доходом выше порога.
    /// </summary>
    /// <param name="threshold">Порог дохода.</param>
    /// <returns>Список контрагентов.</returns>
    Task<IEnumerable<CounterpartyResponseDto>> GetCounterpartiesWithTotalIncomeAboveAsync(decimal threshold);

    /// <summary>
    /// Получает контрагентов с арендной стоимостью выше порога.
    /// </summary>
    /// <param name="threshold">Порог стоимости аренды.</param>
    /// <returns>Список контрагентов.</returns>
    Task<IEnumerable<CounterpartyResponseDto>> GetCounterpartiesWithTotalRentalCostAboveAsync(decimal threshold);
}