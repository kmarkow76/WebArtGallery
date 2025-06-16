using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

public class PaintingRepository : IPaintingRepository
{
    private readonly GalleryDbContext _context;

    public PaintingRepository(GalleryDbContext context)
    {
        _context = context;
    }

    public async Task<Painting> GetByIdAsync(int id)
    {
        return await _context.Paintings
            .Include(p => p.Genre)
            .Include(p => p.Artist)
            .Include(p => p.Rentals)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Painting>> GetAllAsync()
    {
        return await _context.Paintings
            .Include(p => p.Genre)
            .Include(p => p.Artist)
            .Include(p => p.Rentals)
            .ToListAsync();
    }

    public async Task AddAsync(Painting painting)
    {
        _context.Paintings.Add(painting);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Painting painting)
    {
        _context.Paintings.Update(painting);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var painting = await _context.Paintings.FindAsync(id);
        if (painting != null)
        {
            _context.Paintings.Remove(painting);
            await _context.SaveChangesAsync();
        }
    }
}