using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.IRepositories;

/// <summary>
/// Репозиторий для работы с перемещениями картин.
/// </summary>
public interface IPaintingMovementRepository
{
    /// <summary>
    /// Получает перемещение картины по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор перемещения.</param>
    /// <returns>Перемещение или null.</returns>
    Task<PaintingMovement?> GetByIdAsync(int id);

    /// <summary>
    /// Получает все перемещения картин.
    /// </summary>
    /// <returns>Список перемещений.</returns>
    Task<IEnumerable<PaintingMovement>> GetAllAsync();

    /// <summary>
    /// Добавляет перемещение картины.
    /// </summary>
    /// <param name="paintingMovement">Данные перемещения.</param>
    Task AddAsync(PaintingMovement paintingMovement);

    /// <summary>
    /// Обновляет перемещение картины.
    /// </summary>
    /// <param name="paintingMovement">Обновленные данные перемещения.</param>
    Task UpdateAsync(PaintingMovement paintingMovement);

    /// <summary>
    /// Удаляет перемещение картины.
    /// </summary>
    /// <param name="id">Идентификатор перемещения.</param>
    Task DeleteAsync(int id);
}