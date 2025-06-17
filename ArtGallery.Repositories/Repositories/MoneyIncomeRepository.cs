using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с доходами.
/// </summary>
public class MoneyIncomeRepository : IMoneyIncomeRepository
{
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует репозиторий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="context"/> null.</exception>
    public MoneyIncomeRepository(GalleryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает доход по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор дохода.</param>
    /// <returns>Доход или null.</returns>
    public async Task<MoneyIncome?> GetByIdAsync(int id)
    {
        return await _context.MoneyIncomes
            .Include(mi => mi.Counterparty)
            .FirstOrDefaultAsync(mi => mi.Id == id);
    }

    /// <summary>
    /// Получает все доходы.
    /// </summary>
    /// <returns>Список доходов.</returns>
    public async Task<IEnumerable<MoneyIncome>> GetAllAsync()
    {
        return await _context.MoneyIncomes
            .Include(mi => mi.Counterparty)
            .ToListAsync();
    }

    /// <summary>
    /// Добавляет доход.
    /// </summary>
    /// <param name="moneyIncome">Данные дохода.</param>
    public async Task AddAsync(MoneyIncome moneyIncome)
    {
        _context.MoneyIncomes.Add(moneyIncome);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет доход.
    /// </summary>
    /// <param name="moneyIncome">Обновленные данные дохода.</param>
    public async Task UpdateAsync(MoneyIncome moneyIncome)
    {
        _context.MoneyIncomes.Update(moneyIncome);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет доход.
    /// </summary>
    /// <param name="id">Идентификатор дохода.</param>
    public async Task DeleteAsync(int id)
    {
        var moneyIncome = await _context.MoneyIncomes.FindAsync(id);
        if (moneyIncome != null)
        {
            _context.MoneyIncomes.Remove(moneyIncome);
            await _context.SaveChangesAsync();
        }
    }
}