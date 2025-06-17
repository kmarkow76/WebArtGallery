using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с арендой.
/// </summary>
public class RentalRepository : IRentalRepository
{
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует репозиторий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="context"/> null.</exception>
    public RentalRepository(GalleryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает аренду по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    /// <returns>Аренда или null.</returns>
    public async Task<Rental?> GetByIdAsync(int id)
    {
        return await _context.Rentals
            .Include(r => r.Counterparty)
            .Include(r => r.Painting)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <summary>
    /// Получает все аренды.
    /// </summary>
    /// <returns>Список аренд.</returns>
    public async Task<IEnumerable<Rental>> GetAllAsync()
    {
        return await _context.Rentals
            .Include(r => r.Counterparty)
            .Include(r => r.Painting)
            .ToListAsync();
    }

    /// <summary>
    /// Добавляет аренду.
    /// </summary>
    /// <param name="rental">Данные аренды.</param>
    public async Task AddAsync(Rental rental)
    {
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет аренду.
    /// </summary>
    /// <param name="rental">Обновленные данные аренды.</param>
    public async Task UpdateAsync(Rental rental)
    {
        _context.Rentals.Update(rental);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет аренду.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    public async Task DeleteAsync(int id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental != null)
        {
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
        }
    }
}