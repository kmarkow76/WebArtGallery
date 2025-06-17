using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с расходами.
/// </summary>
public class MoneyExpenseRepository : IMoneyExpenseRepository
{
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует репозиторий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="context"/> null.</exception>
    public MoneyExpenseRepository(GalleryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает расход по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор расхода.</param>
    /// <returns>Расход или null.</returns>
    public async Task<MoneyExpense?> GetByIdAsync(int id)
    {
        return await _context.MoneyExpenses
            .Include(me => me.Counterparty)
            .Include(me => me.ExpenseArticle)
            .FirstOrDefaultAsync(me => me.Id == id);
    }

    /// <summary>
    /// Получает все расходы.
    /// </summary>
    /// <returns>Список расходов.</returns>
    public async Task<IEnumerable<MoneyExpense>> GetAllAsync()
    {
        return await _context.MoneyExpenses
            .Include(me => me.Counterparty)
            .Include(me => me.ExpenseArticle)
            .ToListAsync();
    }

    /// <summary>
    /// Добавляет расход.
    /// </summary>
    /// <param name="moneyExpense">Данные расхода.</param>
    public async Task AddAsync(MoneyExpense? moneyExpense)
    {
        _context.MoneyExpenses.Add(moneyExpense);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет расход.
    /// </summary>
    /// <param name="moneyExpense">Обновленные данные расхода.</param>
    public async Task UpdateAsync(MoneyExpense? moneyExpense)
    {
        _context.MoneyExpenses.Update(moneyExpense);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет расход.
    /// </summary>
    /// <param name="id">Идентификатор расхода.</param>
    public async Task DeleteAsync(int id)
    {
        var moneyExpense = await _context.MoneyExpenses.FindAsync(id);
        if (moneyExpense != null)
        {
            _context.MoneyExpenses.Remove(moneyExpense);
            await _context.SaveChangesAsync();
        }
    }
}