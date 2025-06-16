using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

public class MoneyIncomeRepository : IMoneyIncomeRepository
{
    private readonly GalleryDbContext _context;

    public MoneyIncomeRepository(GalleryDbContext context)
    {
        _context = context;
    }

    public async Task<MoneyIncome> GetByIdAsync(int id)
    {
        return await _context.MoneyIncomes
            .Include(mi => mi.Counterparty)
            .FirstOrDefaultAsync(mi => mi.Id == id);
    }

    public async Task<IEnumerable<MoneyIncome>> GetAllAsync()
    {
        return await _context.MoneyIncomes
            .Include(mi => mi.Counterparty)
            .ToListAsync();
    }

    public async Task AddAsync(MoneyIncome moneyIncome)
    {
        _context.MoneyIncomes.Add(moneyIncome);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MoneyIncome moneyIncome)
    {
        _context.MoneyIncomes.Update(moneyIncome);
        await _context.SaveChangesAsync();
    }

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