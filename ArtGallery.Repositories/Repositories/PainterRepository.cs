using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с художниками.
/// </summary>
public class PainterRepository : IPainterRepository
{
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует репозиторий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="context"/> null.</exception>
    public PainterRepository(GalleryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает художника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    /// <returns>Художник или null.</returns>
    public async Task<Painter?> GetByIdAsync(int id)
    {
        return await _context.Painters
            .Include(p => p.Paintings)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Получает всех художников.
    /// </summary>
    /// <returns>Список художников.</returns>
    public async Task<IEnumerable<Painter>> GetAllAsync()
    {
        return await _context.Painters
            .Include(p => p.Paintings)
            .ToListAsync();
    }

    /// <summary>
    /// Добавляет художника.
    /// </summary>
    /// <param name="painter">Данные художника.</param>
    public async Task AddAsync(Painter painter)
    {
        _context.Painters.Add(painter);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет художника.
    /// </summary>
    /// <param name="painter">Обновленные данные художника.</param>
    public async Task UpdateAsync(Painter painter)
    {
        _context.Painters.Update(painter);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет художника.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    public async Task DeleteAsync(int id)
    {
        var painter = await _context.Painters.FindAsync(id);
        if (painter != null)
        {
            _context.Painters.Remove(painter);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Получает художника по имени и фамилии.
    /// </summary>
    /// <param name="firstname">Имя художника.</param>
    /// <param name="lastname">Фамилия художника.</param>
    /// <returns>Художник или null.</returns>
    public async Task<Painter?> GetByFullNameAsync(string firstname, string lastname)
    {
        return await _context.Painters
            .FirstOrDefaultAsync(p => p.Firstname == firstname && p.Lastname == lastname);
    }
}