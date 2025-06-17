using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.IRepositories;

/// <summary>
/// Репозиторий для работы с билетами.
/// </summary>
public interface ITicketRepository
{
    /// <summary>
    /// Получает билет по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор билета.</param>
    /// <returns>Билет или null.</returns>
    Task<Ticket?> GetByIdAsync(int id);

    /// <summary>
    /// Получает все билеты.
    /// </summary>
    /// <returns>Список билетов.</returns>
    Task<IEnumerable<Ticket>> GetAllAsync();

    /// <summary>
    /// Добавляет билет.
    /// </summary>
    /// <param name="ticket">Данные билета.</param>
    Task AddAsync(Ticket ticket);

    /// <summary>
    /// Обновляет билет.
    /// </summary>
    /// <param name="ticket">Обновленные данные билета.</param>
    Task UpdateAsync(Ticket ticket);

    /// <summary>
    /// Удаляет билет.
    /// </summary>
    /// <param name="id">Идентификатор билета.</param>
    Task DeleteAsync(int id);
}