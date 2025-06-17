using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.DTO.Rentals;
using ArtGallery.Interfaces.IRepositories;
using ArtGallery.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Services.Services;

/// <summary>
/// Сервис для управления арендой.
/// </summary>
public class RentalService : IRentalService
{
    private readonly IRentalRepository _repository;
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует сервис.
    /// </summary>
    /// <param name="repository">Репозиторий аренды.</param>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="repository"/> или <paramref name="context"/> null.</exception>
    public RentalService(IRentalRepository repository, GalleryDbContext context)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Создает аренду.
    /// </summary>
    /// <param name="rentalDto">Данные аренды.</param>
    /// <returns>Созданная аренда.</returns>
    public async Task<RentalResponseDto> CreateRentalAsync(CreateRentalDto rentalDto)
    {
        var painting = await _context.Paintings.FindAsync(rentalDto.PaintingId);
        if (painting == null)
            throw new KeyNotFoundException("Painting not found.");

        var currentStatus = await _context.PaintingMovements
            .Where(m => m.PaintingId == rentalDto.PaintingId)
            .OrderByDescending(m => m.MovementDate)
            .Select(m => m.MovementType)
            .FirstOrDefaultAsync() ?? "Available";

        if (currentStatus == "Rented")
            throw new InvalidOperationException("This painting is already rented.");

        var rental = new Rental
        {
            CounterpartyId = rentalDto.CounterpartyId,
            PaintingId = rentalDto.PaintingId,
            StartDate = rentalDto.StartDate.ToUniversalTime(),
            EndDate = rentalDto.EndDate.ToUniversalTime(),
            Price = rentalDto.Price
        };

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _repository.AddAsync(rental);

            var movement = new PaintingMovement
            {
                PaintingId = rentalDto.PaintingId,
                CounterpartyId = rentalDto.CounterpartyId,
                MovementDate = DateTime.UtcNow,
                MovementType = "Rented",
                Notes = $"Rental created for painting ID {rentalDto.PaintingId}"
            };
            _context.PaintingMovements.Add(movement);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Database update error: {ex.InnerException?.Message ?? ex.Message}");
            throw;
        }

        return new RentalResponseDto
        {
            Id = rental.Id,
            CounterpartyId = rental.CounterpartyId,
            PaintingId = rental.PaintingId,
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            Price = rental.Price
        };
    }

    /// <summary>
    /// Получает аренду по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    /// <returns>Аренда.</returns>
    public async Task<RentalResponseDto> GetRentalByIdAsync(int id)
    {
        var rental = await _repository.GetByIdAsync(id);
        if (rental == null) throw new KeyNotFoundException("Rental not found.");
        return new RentalResponseDto
        {
            Id = rental.Id,
            CounterpartyId = rental.CounterpartyId,
            PaintingId = rental.PaintingId,
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            Price = rental.Price
        };
    }

    /// <summary>
    /// Получает все аренды.
    /// </summary>
    /// <returns>Список аренд.</returns>
    public async Task<IEnumerable<RentalResponseDto>> GetAllRentalsAsync()
    {
        var rentals = await _repository.GetAllAsync();
        return rentals.Select(r => new RentalResponseDto
        {
            Id = r.Id,
            CounterpartyId = r.CounterpartyId,
            PaintingId = r.PaintingId,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            Price = r.Price
        });
    }

    /// <summary>
    /// Удаляет аренду.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    public async Task DeleteRentalAsync(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var rental = await _repository.GetByIdAsync(id);
            if (rental == null) throw new KeyNotFoundException("Rental not found.");

            var movement = new PaintingMovement
            {
                PaintingId = rental.PaintingId,
                CounterpartyId = rental.CounterpartyId,
                MovementDate = DateTime.UtcNow,
                MovementType = "Available",
                Notes = $"Rental with ID {rental.Id} ended"
            };
            _context.PaintingMovements.Add(movement);

            await _repository.DeleteAsync(id);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Database update error: {ex.InnerException?.Message ?? ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Получает аренды по названию картины.
    /// </summary>
    /// <param name="paintingTitle">Название картины.</param>
    /// <returns>Список аренд.</returns>
    public async Task<IEnumerable<RentalResponseDto>> GetRentalsByPaintingTitleAsync(string paintingTitle)
    {
        var rentals = await _context.Rentals
            .Include(r => r.Painting)
            .Where(r => r.Painting.Title == paintingTitle)
            .ToListAsync();

        return rentals.Select(r => new RentalResponseDto
        {
            Id = r.Id,
            CounterpartyId = r.CounterpartyId,
            PaintingId = r.PaintingId,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            Price = r.Price
        });
    }
}