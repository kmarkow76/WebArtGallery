using ArtGallery.Data.Models;
using ArtGallery.DTO.Painters;

namespace ArtGallery.Interfaces.IServices;

/// <summary>
/// Сервис для работы с художниками.
/// </summary>
public interface IPainterService
{
    /// <summary>
    /// Создает художника.
    /// </summary>
    /// <param name="painterDto">Данные художника.</param>
    /// <returns>Созданный художник.</returns>
    Task<PainterResponseDto> CreatePainterAsync(CreatePainterDto painterDto);

    /// <summary>
    /// Получает художника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    /// <returns>Художник.</returns>
    Task<PainterResponseDto> GetPainterByIdAsync(int id);

    /// <summary>
    /// Получает всех художников.
    /// </summary>
    /// <returns>Список художников.</returns>
    Task<IEnumerable<PainterResponseDto>> GetAllPaintersAsync();

    /// <summary>
    /// Обновляет художника.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    /// <param name="painterDto">Обновленные данные художника.</param>
    Task UpdatePainterAsync(int id, UpdatePainterDto painterDto);

    /// <summary>
    /// Удаляет художника.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    Task DeletePainterAsync(int id);

    /// <summary>
    /// Получает художника по имени и фамилии.
    /// </summary>
    /// <param name="firstname">Имя художника.</param>
    /// <param name="lastname">Фамилия художника.</param>
    /// <returns>Художник.</returns>
    Task<PainterResponseDto> GetPainterByFullNameAsync(string firstname, string lastname);

    /// <summary>
    /// Получает художников по диапазону лет.
    /// </summary>
    /// <param name="startYear">Начальный год.</param>
    /// <param name="endYear">Конечный год.</param>
    /// <returns>Список художников.</returns>
    Task<IEnumerable<PainterResponseDto>> GetPaintersByYearRangeAsync(string startYear, string endYear);

    /// <summary>
    /// Получает художника по имени и фамилии.
    /// </summary>
    /// <param name="firstname">Имя художника.</param>
    /// <param name="lastname">Фамилия художника.</param>
    /// <returns>Художник или null.</returns>
    Task<Painter?> GetByFullNameAsync(string firstname, string lastname);
}