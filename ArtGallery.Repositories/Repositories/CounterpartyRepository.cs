using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

public class CounterpartyRepository : ICounterpartyRepository
{
    private readonly GalleryDbContext _context;

    public CounterpartyRepository(GalleryDbContext context)
    {
        _context = context;
    }

    public async Task<Counterparty?> GetByIdAsync(int id)
    {
        return await _context.Counterparties
            .Include(c => c.MoneyExpenses)
            .Include(c => c.MoneyIncomes)
            .Include(c => c.Rentals)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Counterparty>> GetAllAsync()
    {
        return await _context.Counterparties
            .Include(c => c.MoneyExpenses)
            .Include(c => c.MoneyIncomes)
            .Include(c => c.Rentals)
            .ToListAsync();
    }

    public async Task AddAsync(Counterparty? counterparty)
    {
        _context.Counterparties.Add(counterparty);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Counterparty? counterparty)
    {
        _context.Counterparties.Update(counterparty);
        await _context.SaveChangesAsync();
    }

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