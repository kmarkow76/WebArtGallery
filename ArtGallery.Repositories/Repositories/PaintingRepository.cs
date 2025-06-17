using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с картинами.
/// </summary>
public class PaintingRepository : IPaintingRepository
{
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует репозиторий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="context"/> null.</exception>
    public PaintingRepository(GalleryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает картину по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор картины.</param>
    /// <returns>Картина или null.</returns>
    public async Task<Painting?> GetByIdAsync(int id)
    {
        return await _context.Paintings
            .Include(p => p.Genre)
            .Include(p => p.Artist)
            .Include(p => p.Rentals)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Получает все картины.
    /// </summary>
    /// <returns>Список картин.</returns>
    public async Task<IEnumerable<Painting>> GetAllAsync()
    {
        return await _context.Paintings
            .Include(p => p.Genre)
            .Include(p => p.Artist)
            .Include(p => p.Rentals)
            .ToListAsync();
    }

    /// <summary>
    /// Добавляет картину.
    /// </summary>
    /// <param name="painting">Данные картины.</param>
    public async Task AddAsync(Painting painting)
    {
        _context.Paintings.Add(painting);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет картину.
    /// </summary>
    /// <param name="painting">Обновленные данные картины.</param>
    public async Task UpdateAsync(Painting painting)
    {
        _context.Paintings.Update(painting);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет картину.
    /// </summary>
    /// <param name="id">Идентификатор картины.</param>
    public async Task DeleteAsync(int id)
    {
        var painting = await _context.Paintings.FindAsync(id);
        if (painting != null)
        {
            _context.Paintings.Remove(painting);
            await _context.SaveChangesAsync();
        }
    }
}