using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

public class PaintingMovementRepository : IPaintingMovementRepository
{
    private readonly GalleryDbContext _context;

    public PaintingMovementRepository(GalleryDbContext context)
    {
        _context = context;
    }

    public async Task<PaintingMovement> GetByIdAsync(int id)
    {
        return await _context.PaintingMovements
            .Include(pm => pm.Painting)
            .Include(pm => pm.Counterparty)
            .FirstOrDefaultAsync(pm => pm.Id == id);
    }

    public async Task<IEnumerable<PaintingMovement>> GetAllAsync()
    {
        return await _context.PaintingMovements
            .Include(pm => pm.Painting)
            .Include(pm => pm.Counterparty)
            .ToListAsync();
    }

    public async Task AddAsync(PaintingMovement paintingMovement)
    {
        _context.PaintingMovements.Add(paintingMovement);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PaintingMovement paintingMovement)
    {
        _context.PaintingMovements.Update(paintingMovement);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var paintingMovement = await _context.PaintingMovements.FindAsync(id);
        if (paintingMovement != null)
        {
            _context.PaintingMovements.Remove(paintingMovement);
            await _context.SaveChangesAsync();
        }
    }
}