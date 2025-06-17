using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.IRepositories;

/// <summary>
/// Репозиторий для работы с контрагентами.
/// </summary>
public interface ICounterpartyRepository
{
    /// <summary>
    /// Получает контрагента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    /// <returns>Контрагент или null.</returns>
    Task<Counterparty?> GetByIdAsync(int id);

    /// <summary>
    /// Получает всех контрагентов.
    /// </summary>
    /// <returns>Список контрагентов.</returns>
    Task<IEnumerable<Counterparty>> GetAllAsync();

    /// <summary>
    /// Добавляет контрагента.
    /// </summary>
    /// <param name="counterparty">Данные контрагента.</param>
    Task AddAsync(Counterparty? counterparty);

    /// <summary>
    /// Обновляет контрагента.
    /// </summary>
    /// <param name="counterparty">Обновленные данные контрагента.</param>
    Task UpdateAsync(Counterparty? counterparty);

    /// <summary>
    /// Удаляет контрагента.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    Task DeleteAsync(int id);
}