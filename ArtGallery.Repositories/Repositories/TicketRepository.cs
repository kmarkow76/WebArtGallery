using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с билетами.
/// </summary>
public class TicketRepository : ITicketRepository
{
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует репозиторий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="context"/> null.</exception>
    public TicketRepository(GalleryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает билет по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор билета.</param>
    /// <returns>Билет или null.</returns>
    public async Task<Ticket?> GetByIdAsync(int id)
    {
        return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
    }

    /// <summary>
    /// Получает все билеты.
    /// </summary>
    /// <returns>Список билетов.</returns>
    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await _context.Tickets.ToListAsync();
    }

    /// <summary>
    /// Добавляет билет.
    /// </summary>
    /// <param name="ticket">Данные билета.</param>
    public async Task AddAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет билет.
    /// </summary>
    /// <param name="ticket">Обновленные данные билета.</param>
    public async Task UpdateAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет билет.
    /// </summary>
    /// <param name="id">Идентификатор билета.</param>
    public async Task DeleteAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket != null)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
    }
}