using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;

namespace ArtGallery.Interfaces.IServices;

/// <summary>
/// Сервис для работы с картинами.
/// </summary>
public interface IPaintingService : IPaintingRepository
{
    /// <summary>
    /// Получает картины с их статусом.
    /// </summary>
    /// <returns>Список картин с статусом.</returns>
    Task<IEnumerable<object>> GetPaintingsWithStatusAsync();

    /// <summary>
    /// Добавляет картину с начальным перемещением.
    /// </summary>
    /// <param name="painting">Данные картины.</param>
    Task AddWithInitialMovementAsync(Painting painting);

    /// <summary>
    /// Получает перемещения картины по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор картины.</param>
    /// <returns>Список перемещений.</returns>
    Task<IEnumerable<object>> GetMovementsByPaintingIdAsync(int id);

    /// <summary>
    /// Получает картины по жанру.
    /// </summary>
    /// <param name="genreId">Идентификатор жанра.</param>
    /// <returns>Список картин.</returns>
    Task<IEnumerable<object>> GetPaintingsByGenreAsync(int genreId);
}