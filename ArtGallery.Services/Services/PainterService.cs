using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.DTO.Painters;
using ArtGallery.Interfaces.IRepositories;
using ArtGallery.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Services.Services;

/// <summary>
/// Сервис для управления художниками.
/// </summary>
public class PainterService : IPainterService
{
    private readonly IPainterRepository _repository;
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует сервис.
    /// </summary>
    /// <param name="repository">Репозиторий художников.</param>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="repository"/> или <paramref name="context"/> null.</exception>
    public PainterService(IPainterRepository repository, GalleryDbContext context)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Создает художника.
    /// </summary>
    /// <param name="painterDto">Данные художника.</param>
    /// <returns>Созданный художник.</returns>
    public async Task<PainterResponseDto> CreatePainterAsync(CreatePainterDto painterDto)
    {
        if (string.IsNullOrWhiteSpace(painterDto.Firstname) || string.IsNullOrWhiteSpace(painterDto.Lastname))
            throw new ArgumentException("Firstname and Lastname are required.");

        var existingPainter = await _repository.GetByFullNameAsync(painterDto.Firstname, painterDto.Lastname);
        if (existingPainter != null)
            throw new InvalidOperationException("Painter with this name already exists.");

        var painter = new Painter
        {
            Firstname = painterDto.Firstname,
            Lastname = painterDto.Lastname,
            Yearsoflife = painterDto.Yearsoflife,
            Historicalbackground = painterDto.Historicalbackground
        };

        await _repository.AddAsync(painter);
        await _context.SaveChangesAsync();

        return new PainterResponseDto
        {
            Id = painter.Id,
            Firstname = painter.Firstname,
            Lastname = painter.Lastname,
            Yearsoflife = painter.Yearsoflife,
            Historicalbackground = painter.Historicalbackground
        };
    }

    /// <summary>
    /// Получает художника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    /// <returns>Художник.</returns>
    public async Task<PainterResponseDto> GetPainterByIdAsync(int id)
    {
        var painter = await _repository.GetByIdAsync(id);
        if (painter == null) throw new KeyNotFoundException("Painter not found.");
        return new PainterResponseDto
        {
            Id = painter.Id,
            Firstname = painter.Firstname,
            Lastname = painter.Lastname,
            Yearsoflife = painter.Yearsoflife,
            Historicalbackground = painter.Historicalbackground
        };
    }

    /// <summary>
    /// Получает всех художников.
    /// </summary>
    /// <returns>Список художников.</returns>
    public async Task<IEnumerable<PainterResponseDto>> GetAllPaintersAsync()
    {
        var painters = await _repository.GetAllAsync();
        return painters.Select(p => new PainterResponseDto
        {
            Id = p.Id,
            Firstname = p.Firstname,
            Lastname = p.Lastname,
            Yearsoflife = p.Yearsoflife,
            Historicalbackground = p.Historicalbackground
        });
    }

    /// <summary>
    /// Обновляет художника.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    /// <param name="painterDto">Обновленные данные художника.</param>
    public async Task UpdatePainterAsync(int id, UpdatePainterDto painterDto)
    {
        var painter = await _repository.GetByIdAsync(id);
        if (painter == null) throw new KeyNotFoundException("Painter not found.");

        painter.Firstname = painterDto.Firstname ?? painter.Firstname;
        painter.Lastname = painterDto.Lastname ?? painter.Lastname;
        painter.Yearsoflife = painterDto.Yearsoflife ?? painter.Yearsoflife;
        painter.Historicalbackground = painterDto.Historicalbackground ?? painter.Historicalbackground;

        await _repository.UpdateAsync(painter);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет художника.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    public async Task DeletePainterAsync(int id)
    {
        await _repository.DeleteAsync(id);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Получает художника по имени и фамилии.
    /// </summary>
    /// <param name="firstname">Имя художника.</param>
    /// <param name="lastname">Фамилия художника.</param>
    /// <returns>Художник.</returns>
    public async Task<PainterResponseDto> GetPainterByFullNameAsync(string firstname, string lastname)
    {
        var painter = await _repository.GetByFullNameAsync(firstname, lastname);
        if (painter == null) throw new KeyNotFoundException("Painter not found.");
        return new PainterResponseDto
        {
            Id = painter.Id,
            Firstname = painter.Firstname,
            Lastname = painter.Lastname,
            Yearsoflife = painter.Yearsoflife,
            Historicalbackground = painter.Historicalbackground
        };
    }

    /// <summary>
    /// Получает художников по диапазону лет.
    /// </summary>
    /// <param name="startYear">Начальный год.</param>
    /// <param name="endYear">Конечный год.</param>
    /// <returns>Список художников.</returns>
    public async Task<IEnumerable<PainterResponseDto>> GetPaintersByYearRangeAsync(string startYear, string endYear)
    {
        var painters = await _context.Painters
            .Where(p => p.Yearsoflife.Contains(startYear) && p.Yearsoflife.Contains(endYear))
            .ToListAsync();
        return painters.Select(p => new PainterResponseDto
        {
            Id = p.Id,
            Firstname = p.Firstname,
            Lastname = p.Lastname,
            Yearsoflife = p.Yearsoflife,
            Historicalbackground = p.Historicalbackground
        });
    }

    /// <summary>
    /// Получает художника по имени и фамилии.
    /// </summary>
    /// <param name="firstname">Имя художника.</param>
    /// <param name="lastname">Фамилия художника.</param>
    /// <returns>Художник или null.</returns>
    public async Task<Painter?> GetByFullNameAsync(string firstname, string lastname)
    {
        return await _repository.GetByFullNameAsync(firstname, lastname);
    }
}