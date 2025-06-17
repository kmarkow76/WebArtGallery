using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.IRepositories;

/// <summary>
/// Репозиторий для работы с художниками.
/// </summary>
public interface IPainterRepository
{
    /// <summary>
    /// Получает художника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    /// <returns>Художник или null.</returns>
    Task<Painter?> GetByIdAsync(int id);

    /// <summary>
    /// Получает всех художников.
    /// </summary>
    /// <returns>Список художников.</returns>
    Task<IEnumerable<Painter>> GetAllAsync();

    /// <summary>
    /// Добавляет художника.
    /// </summary>
    /// <param name="painter">Данные художника.</param>
    Task AddAsync(Painter painter);

    /// <summary>
    /// Обновляет художника.
    /// </summary>
    /// <param name="painter">Обновленные данные художника.</param>
    Task UpdateAsync(Painter painter);

    /// <summary>
    /// Удаляет художника.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Получает художника по имени и фамилии.
    /// </summary>
    /// <param name="firstname">Имя художника.</param>
    /// <param name="lastname">Фамилия художника.</param>
    /// <returns>Художник или null.</returns>
    Task<Painter?> GetByFullNameAsync(string firstname, string lastname);
}