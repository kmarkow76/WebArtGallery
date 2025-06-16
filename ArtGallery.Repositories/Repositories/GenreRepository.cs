using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

    public class GenreRepository : IGenreRepository
    {
        private readonly GalleryDbContext _context;

        public GenreRepository(GalleryDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres
                .Include(g => g.Paintings)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres
                .Include(g => g.Paintings)
                .ToListAsync();
        }

        public async Task AddAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
