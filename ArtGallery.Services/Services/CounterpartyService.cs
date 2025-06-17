using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.DTO.Counterparties;
using ArtGallery.Interfaces.IRepositories;
using ArtGallery.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Services.Services;

/// <summary>
/// Сервис для управления контрагентами.
/// </summary>
public class CounterpartyService : ICounterpartyService
{
    private readonly ICounterpartyRepository _repository;
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует сервис.
    /// </summary>
    /// <param name="repository">Репозиторий контрагентов.</param>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="repository"/> или <paramref name="context"/> null.</exception>
    public CounterpartyService(ICounterpartyRepository repository, GalleryDbContext context)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Создает контрагента.
    /// </summary>
    /// <param name="counterpartyDto">Данные контрагента.</param>
    /// <returns>Созданный контрагент.</returns>
    public async Task<CounterpartyResponseDto> CreateCounterpartyAsync(CreateCounterpartyDto counterpartyDto)
    {
        var counterparty = new Counterparty
        {
            Name = counterpartyDto.Name,
            PhoneNumber = counterpartyDto.PhoneNumber,
            Address = counterpartyDto.Address,
            Email = counterpartyDto.Email,
            ContactInfo = counterpartyDto.ContactInfo,
            MoneyExpenses = new List<MoneyExpense>(),
            MoneyIncomes = new List<MoneyIncome>(),
            Rentals = new List<Rental>()
        };

        await _repository.AddAsync(counterparty);
        await _context.SaveChangesAsync();

        return new CounterpartyResponseDto
        {
            Id = counterparty.Id,
            Name = counterparty.Name,
            PhoneNumber = counterparty.PhoneNumber,
            Address = counterparty.Address,
            Email = counterparty.Email,
            ContactInfo = counterparty.ContactInfo
        };
    }

    /// <summary>
    /// Получает контрагента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    /// <returns>Контрагент.</returns>
    public async Task<CounterpartyResponseDto> GetCounterpartyByIdAsync(int id)
    {
        var counterparty = await _repository.GetByIdAsync(id);
        if (counterparty == null) throw new KeyNotFoundException("Counterparty not found.");
        return new CounterpartyResponseDto
        {
            Id = counterparty.Id,
            Name = counterparty.Name,
            PhoneNumber = counterparty.PhoneNumber,
            Address = counterparty.Address,
            Email = counterparty.Email,
            ContactInfo = counterparty.ContactInfo
        };
    }

    /// <summary>
    /// Получает всех контрагентов.
    /// </summary>
    /// <returns>Список контрагентов.</returns>
    public async Task<IEnumerable<CounterpartyResponseDto>> GetAllCounterpartiesAsync()
    {
        var counterparties = await _repository.GetAllAsync();
        return counterparties.Select(c => new CounterpartyResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            PhoneNumber = c.PhoneNumber,
            Address = c.Address,
            Email = c.Email,
            ContactInfo = c.ContactInfo
        });
    }

    /// <summary>
    /// Обновляет контрагента.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    /// <param name="counterpartyDto">Обновленные данные контрагента.</param>
    public async Task UpdateCounterpartyAsync(int id, UpdateCounterpartyDto counterpartyDto)
    {
        var counterparty = await _repository.GetByIdAsync(id);
        if (counterparty == null) throw new KeyNotFoundException("Counterparty not found.");

        counterparty.Name = counterpartyDto.Name ?? counterparty.Name;
        counterparty.PhoneNumber = counterpartyDto.PhoneNumber ?? counterparty.PhoneNumber;
        counterparty.Address = counterpartyDto.Address ?? counterparty.Address;
        counterparty.Email = counterpartyDto.Email ?? counterparty.Email;
        counterparty.ContactInfo = counterpartyDto.ContactInfo ?? counterparty.ContactInfo;

        await _repository.UpdateAsync(counterparty);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет контрагента.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    public async Task DeleteCounterpartyAsync(int id)
    {
        await _repository.DeleteAsync(id);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Получает контрагентов с доходом выше порога.
    /// </summary>
    /// <param name="threshold">Порог дохода.</param>
    /// <returns>Список контрагентов.</returns>
    public async Task<IEnumerable<CounterpartyResponseDto>> GetCounterpartiesWithTotalIncomeAboveAsync(decimal threshold)
    {
        var counterparties = await _context.Counterparties
            .Include(c => c.MoneyIncomes)
            .ToListAsync();

        var result = counterparties
            .Where(c => c.MoneyIncomes.Sum(mi => mi.Amount) > threshold)
            .Select(c => new CounterpartyResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Email = c.Email,
                ContactInfo = c.ContactInfo
            });

        return result;
    }

    /// <summary>
    /// Получает контрагентов с арендной стоимостью выше порога.
    /// </summary>
    /// <param name="threshold">Порог стоимости аренды.</param>
    /// <returns>Список контрагентов.</returns>
    public async Task<IEnumerable<CounterpartyResponseDto>> GetCounterpartiesWithTotalRentalCostAboveAsync(decimal threshold)
    {
        var counterparties = await _context.Counterparties
            .Include(c => c.Rentals)
            .ToListAsync();

        var result = counterparties
            .Where(c => c.Rentals.Sum(r => r.Price) > threshold)
            .Select(c => new CounterpartyResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Email = c.Email,
                ContactInfo = c.ContactInfo
            });

        return result;
    }
}