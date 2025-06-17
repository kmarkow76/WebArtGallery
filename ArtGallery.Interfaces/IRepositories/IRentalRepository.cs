using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.IRepositories;

/// <summary>
/// Репозиторий для работы с арендой.
/// </summary>
public interface IRentalRepository
{
    /// <summary>
    /// Получает аренду по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    /// <returns>Аренда или null.</returns>
    Task<Rental?> GetByIdAsync(int id);

    /// <summary>
    /// Получает все аренды.
    /// </summary>
    /// <returns>Список аренд.</returns>
    Task<IEnumerable<Rental>> GetAllAsync();

    /// <summary>
    /// Добавляет аренду.
    /// </summary>
    /// <param name="rental">Данные аренды.</param>
    Task AddAsync(Rental rental);

    /// <summary>
    /// Обновляет аренду.
    /// </summary>
    /// <param name="rental">Обновленные данные аренды.</param>
    Task UpdateAsync(Rental rental);

    /// <summary>
    /// Удаляет аренду.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    Task DeleteAsync(int id);
}