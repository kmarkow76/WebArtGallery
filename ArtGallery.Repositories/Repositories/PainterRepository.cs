using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

public class PainterRepository : IPainterRepository
{
    private readonly GalleryDbContext _context;

    public PainterRepository(GalleryDbContext context)
    {
        _context = context;
    }

    public async Task<Painter> GetByIdAsync(int id)
    {
        return await _context.Painters
            .Include(p => p.Paintings)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Painter>> GetAllAsync()
    {
        return await _context.Painters
            .Include(p => p.Paintings)
            .ToListAsync();
    }

    public async Task AddAsync(Painter painter)
    {
        _context.Painters.Add(painter);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Painter painter)
    {
        _context.Painters.Update(painter);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var painter = await _context.Painters.FindAsync(id);
        if (painter != null)
        {
            _context.Painters.Remove(painter);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<Painter?> GetByFullNameAsync(string firstname, string lastname)
    {
        return await _context.Painters
            .FirstOrDefaultAsync(p => p.Firstname == firstname && p.Lastname == lastname);
    }
}