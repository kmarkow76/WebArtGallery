using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

public class MoneyExpenseRepository : IMoneyExpenseRepository
{
    private readonly GalleryDbContext _context;

    public MoneyExpenseRepository(GalleryDbContext context)
    {
        _context = context;
    }

    public async Task<MoneyExpense?> GetByIdAsync(int id)
    {
        return await _context.MoneyExpenses
            .Include(me => me.Counterparty)
            .Include(me => me.ExpenseArticle)
            .FirstOrDefaultAsync(me => me.Id == id);
    }

    public async Task<IEnumerable<MoneyExpense>> GetAllAsync()
    {
        return await _context.MoneyExpenses
            .Include(me => me.Counterparty)
            .Include(me => me.ExpenseArticle)
            .ToListAsync();
    }

    public async Task AddAsync(MoneyExpense? moneyExpense)
    {
        _context.MoneyExpenses.Add(moneyExpense);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MoneyExpense? moneyExpense)
    {
        _context.MoneyExpenses.Update(moneyExpense);
        await _context.SaveChangesAsync();
    }

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