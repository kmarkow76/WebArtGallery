using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с контрагентами.
/// </summary>
public class CounterpartyRepository : ICounterpartyRepository
{
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует репозиторий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="context"/> null.</exception>
    public CounterpartyRepository(GalleryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает контрагента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    /// <returns>Контрагент или null.</returns>
    public async Task<Counterparty?> GetByIdAsync(int id)
    {
        return await _context.Counterparties
            .Include(c => c.MoneyExpenses)
            .Include(c => c.MoneyIncomes)
            .Include(c => c.Rentals)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Получает всех контрагентов.
    /// </summary>
    /// <returns>Список контрагентов.</returns>
    public async Task<IEnumerable<Counterparty>> GetAllAsync()
    {
        return await _context.Counterparties
            .Include(c => c.MoneyExpenses)
            .Include(c => c.MoneyIncomes)
            .Include(c => c.Rentals)
            .ToListAsync();
    }

    /// <summary>
    /// Добавляет контрагента.
    /// </summary>
    /// <param name="counterparty">Данные контрагента.</param>
    public async Task AddAsync(Counterparty? counterparty)
    {
        _context.Counterparties.Add(counterparty);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет контрагента.
    /// </summary>
    /// <param name="counterparty">Обновленные данные контрагента.</param>
    public async Task UpdateAsync(Counterparty? counterparty)
    {
        _context.Counterparties.Update(counterparty);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет контрагента.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    public async Task DeleteAsync(int id)
    {
        var counterparty = await _context.Counterparties.FindAsync(id);
        if (counterparty != null)
        {
            _context.Counterparties.Remove(counterparty);
            await _context.SaveChangesAsync();
        }
    }
}