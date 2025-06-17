using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с жанрами.
/// </summary>
public class GenreRepository : IGenreRepository
{
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует репозиторий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="context"/> null.</exception>
    public GenreRepository(GalleryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает жанр по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор жанра.</param>
    /// <returns>Жанр или null.</returns>
    public async Task<Genre?> GetByIdAsync(int id)
    {
        return await _context.Genres
            .Include(g => g.Paintings)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    /// <summary>
    /// Получает все жанры.
    /// </summary>
    /// <returns>Список жанров.</returns>
    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await _context.Genres
            .Include(g => g.Paintings)
            .ToListAsync();
    }

    /// <summary>
    /// Добавляет жанр.
    /// </summary>
    /// <param name="genre">Данные жанра.</param>
    public async Task AddAsync(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет жанр.
    /// </summary>
    /// <param name="genre">Обновленные данные жанра.</param>
    public async Task UpdateAsync(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет жанр.
    /// </summary>
    /// <param name="id">Идентификатор жанра.</param>
    public async Task DeleteAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre != null)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}