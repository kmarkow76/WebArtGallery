using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с перемещениями картин.
/// </summary>
public class PaintingMovementRepository : IPaintingMovementRepository
{
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует репозиторий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="context"/> null.</exception>
    public PaintingMovementRepository(GalleryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает перемещение картины по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор перемещения.</param>
    /// <returns>Перемещение или null.</returns>
    public async Task<PaintingMovement?> GetByIdAsync(int id)
    {
        return await _context.PaintingMovements
            .Include(pm => pm.Painting)
            .Include(pm => pm.Counterparty)
            .FirstOrDefaultAsync(pm => pm.Id == id);
    }

    /// <summary>
    /// Получает все перемещения картин.
    /// </summary>
    /// <returns>Список перемещений.</returns>
    public async Task<IEnumerable<PaintingMovement>> GetAllAsync()
    {
        return await _context.PaintingMovements
            .Include(pm => pm.Painting)
            .Include(pm => pm.Counterparty)
            .ToListAsync();
    }

    /// <summary>
    /// Добавляет перемещение картины.
    /// </summary>
    /// <param name="paintingMovement">Данные перемещения.</param>
    public async Task AddAsync(PaintingMovement paintingMovement)
    {
        _context.PaintingMovements.Add(paintingMovement);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет перемещение картины.
    /// </summary>
    /// <param name="paintingMovement">Обновленные данные перемещения.</param>
    public async Task UpdateAsync(PaintingMovement paintingMovement)
    {
        _context.PaintingMovements.Update(paintingMovement);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет перемещение картины.
    /// </summary>
    /// <param name="id">Идентификатор перемещения.</param>
    public async Task DeleteAsync(int id)
    {
        var paintingMovement = await _context.PaintingMovements.FindAsync(id);
        if (paintingMovement != null)
        {
            _context.PaintingMovements.Remove(paintingMovement);
            await _context.SaveChangesAsync();
        }
    }
}