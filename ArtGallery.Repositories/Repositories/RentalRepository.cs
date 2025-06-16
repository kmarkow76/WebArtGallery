using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly GalleryDbContext _context;

    public RentalRepository(GalleryDbContext context)
    {
        _context = context;
    }

    public async Task<Rental> GetByIdAsync(int id)
    {
        return await _context.Rentals
            .Include(r => r.Counterparty)
            .Include(r => r.Painting)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Rental>> GetAllAsync()
    {
        return await _context.Rentals
            .Include(r => r.Counterparty)
            .Include(r => r.Painting)
            .ToListAsync();
    }

    public async Task AddAsync(Rental rental)
    {
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Rental rental)
    {
        _context.Rentals.Update(rental);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental != null)
        {
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
        }
    }
}