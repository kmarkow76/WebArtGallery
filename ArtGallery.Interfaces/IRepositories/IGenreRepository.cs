using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.IRepositories;

/// <summary>
/// Репозиторий для работы с жанрами.
/// </summary>
public interface IGenreRepository
{
    /// <summary>
    /// Получает жанр по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор жанра.</param>
    /// <returns>Жанр или null.</returns>
    Task<Genre?> GetByIdAsync(int id);

    /// <summary>
    /// Получает все жанры.
    /// </summary>
    /// <returns>Список жанров.</returns>
    Task<IEnumerable<Genre>> GetAllAsync();

    /// <summary>
    /// Добавляет жанр.
    /// </summary>
    /// <param name="genre">Данные жанра.</param>
    Task AddAsync(Genre genre);

    /// <summary>
    /// Обновляет жанр.
    /// </summary>
    /// <param name="genre">Обновленные данные жанра.</param>
    Task UpdateAsync(Genre genre);

    /// <summary>
    /// Удаляет жанр.
    /// </summary>
    /// <param name="id">Идентификатор жанра.</param>
    Task DeleteAsync(int id);
}