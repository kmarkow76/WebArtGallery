using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using ArtGallery.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Services.Services;

/// <summary>
/// Сервис для управления картинами.
/// </summary>
public class PaintingService : IPaintingService
{
    private readonly IPaintingRepository _repository;
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует сервис.
    /// </summary>
    /// <param name="repository">Репозиторий картин.</param>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="repository"/> или <paramref name="context"/> null.</exception>
    public PaintingService(IPaintingRepository repository, GalleryDbContext context)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает картину по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор картины.</param>
    /// <returns>Картина или null.</returns>
    public async Task<Painting> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Получает все картины.
    /// </summary>
    /// <returns>Список картин.</returns>
    public async Task<IEnumerable<Painting>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    /// <summary>
    /// Добавляет картину.
    /// </summary>
    /// <param name="painting">Данные картины.</param>
    public async Task AddAsync(Painting painting)
    {
        await _repository.AddAsync(painting);
    }

    /// <summary>
    /// Обновляет картину.
    /// </summary>
    /// <param name="painting">Обновленные данные картины.</param>
    public async Task UpdateAsync(Painting painting)
    {
        await _repository.UpdateAsync(painting);
    }

    /// <summary>
    /// Удаляет картину.
    /// </summary>
    /// <param name="id">Идентификатор картины.</param>
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// Получает картины с их статусом.
    /// </summary>
    /// <returns>Список картин с статусом.</returns>
    public async Task<IEnumerable<object>> GetPaintingsWithStatusAsync()
    {
        var paintings = _context.Paintings
            .Include(p => p.Genre)
            .Include(p => p.Artist);
        var movements = _context.PaintingMovements
            .Include(m => m.Counterparty);

        var query = from p in paintings
                    join m in movements on p.Id equals m.PaintingId into pm
                    from m in pm.DefaultIfEmpty()
                    group m by new { p.Id, p.Title, GenreName = p.Genre.Name, ArtistName = p.Artist.Firstname + " " + p.Artist.Lastname, p.CreationDate } into g
                    select new
                    {
                        Id = g.Key.Id,
                        Title = g.Key.Title,
                        GenreName = g.Key.GenreName,
                        ArtistName = g.Key.ArtistName,
                        CreationDate = g.Key.CreationDate,
                        Status = g.Select(m => m.MovementType).FirstOrDefault()
                    };

        var result = await query.ToListAsync();
        return result.Select(x => new
        {
            x.Id,
            x.Title,
            x.GenreName,
            x.ArtistName,
            x.CreationDate,
            Status = x.Status ?? "Available"
        });
    }

    /// <summary>
    /// Добавляет картину с начальным перемещением.
    /// </summary>
    /// <param name="painting">Данные картины.</param>
    public async Task AddWithInitialMovementAsync(Painting painting)
    {
        await _repository.AddAsync(painting);
        await _context.SaveChangesAsync();

        var initialMovement = new PaintingMovement
        {
            PaintingId = painting.Id,
            MovementType = "Available",
            MovementDate = DateTime.UtcNow,
            Notes = "Picture added to gallery"
        };
        _context.PaintingMovements.Add(initialMovement);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Получает перемещения картины по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор картины.</param>
    /// <returns>Список перемещений.</returns>
    public async Task<IEnumerable<object>> GetMovementsByPaintingIdAsync(int id)
    {
        return await _context.PaintingMovements
            .Where(m => m.PaintingId == id)
            .Include(m => m.Counterparty)
            .OrderByDescending(m => m.MovementDate)
            .Select(m => new
            {
                m.Id,
                m.PaintingId,
                m.MovementType,
                m.MovementDate,
                CounterpartyName = m.Counterparty.Name,
                m.Notes
            }).ToListAsync();
    }

    /// <summary>
    /// Получает картины по жанру.
    /// </summary>
    /// <param name="genreId">Идентификатор жанра.</param>
    /// <returns>Список картин.</returns>
    public async Task<IEnumerable<object>> GetPaintingsByGenreAsync(int genreId)
    {
        return await _context.Paintings
            .Where(p => p.GenreId == genreId)
            .Include(p => p.Genre)
            .Include(p => p.Artist)
            .Select(p => new
            {
                p.Id,
                p.Title,
                p.CreationDate,
                GenreName = p.Genre.Name,
                ArtistName = p.Artist.Firstname + " " + p.Artist.Lastname
            }).ToListAsync();
    }
}