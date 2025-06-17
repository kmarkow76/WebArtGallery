using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.IRepositories;

/// <summary>
/// Репозиторий для работы с картинами.
/// </summary>
public interface IPaintingRepository
{
    /// <summary>
    /// Получает картину по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор картины.</param>
    /// <returns>Картина или null.</returns>
    Task<Painting?> GetByIdAsync(int id);

    /// <summary>
    /// Получает все картины.
    /// </summary>
    /// <returns>Список картин.</returns>
    Task<IEnumerable<Painting>> GetAllAsync();

    /// <summary>
    /// Добавляет картину.
    /// </summary>
    /// <param name="painting">Данные картины.</param>
    Task AddAsync(Painting painting);

    /// <summary>
    /// Обновляет картину.
    /// </summary>
    /// <param name="painting">Обновленные данные картины.</param>
    Task UpdateAsync(Painting painting);

    /// <summary>
    /// Удаляет картину.
    /// </summary>
    /// <param name="id">Идентификатор картины.</param>
    Task DeleteAsync(int id);
}