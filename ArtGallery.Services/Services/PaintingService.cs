using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;
using ArtGallery.Interfaces.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Services.Services
{
   public class PaintingService : IPaintingService
    {
       private readonly IPaintingRepository _repository;
        private readonly GalleryDbContext _context;

        public PaintingService(IPaintingRepository repository, GalleryDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<Painting> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Painting>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(Painting painting)
        {
            await _repository.AddAsync(painting);
        }

        public async Task UpdateAsync(Painting painting)
        {
            await _repository.UpdateAsync(painting);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<object>> GetPaintingsWithStatusAsync()
        {
            var paintings = _context.Paintings
                .Include(p => p.Genre)
                .Include(p => p.Artist);
            var movements = _context.PaintingMovements
                .Include(m => m.Counterparty);

            var query = from p in paintings
                        join m in movements on p.Id equals m.PaintingId into pm
                        from m in pm.DefaultIfEmpty()
                        group m by new { p.Id, p.Title, GenreName = p.Genre.Name, ArtistName = p.Artist.Firstname + " " + p.Artist.Lastname, p.CreationDate } into g
                        select new
                        {
                            Id = g.Key.Id,
                            Title = g.Key.Title,
                            GenreName = g.Key.GenreName,
                            ArtistName = g.Key.ArtistName,
                            CreationDate = g.Key.CreationDate,
                            Status = g.Select(m => m.MovementType).FirstOrDefault()
                        };

            var result = await query.ToListAsync();
            return result.Select(x => new
            {
                x.Id,
                x.Title,
                x.GenreName,
                x.ArtistName,
                x.CreationDate,
                Status = x.Status ?? "Available"
            });
        }

        public async Task AddWithInitialMovementAsync(Painting painting)
        {
            // Добавление картины и сохранение для получения Id
            await _repository.AddAsync(painting);
            await _context.SaveChangesAsync();

            // Создание начального движения с полученным Id
            var initialMovement = new PaintingMovement
            {
                PaintingId = painting.Id,
                MovementType = "Available",
                MovementDate = DateTime.UtcNow,
                Notes = "Picture added to gallery"
            };
            _context.PaintingMovements.Add(initialMovement);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetMovementsByPaintingIdAsync(int id)
        {
            return await _context.PaintingMovements
                .Where(m => m.PaintingId == id)
                .Include(m => m.Counterparty)
                .OrderByDescending(m => m.MovementDate)
                .Select(m => new
                {
                    m.Id,
                    m.PaintingId,
                    m.MovementType,
                    m.MovementDate,
                    CounterpartyName = m.Counterparty.Name,
                    m.Notes
                }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetPaintingsByGenreAsync(int genreId)
        {
            return await _context.Paintings
                .Where(p => p.GenreId == genreId)
                .Include(p => p.Genre)
                .Include(p => p.Artist)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.CreationDate,
                    GenreName = p.Genre.Name,
                    ArtistName = p.Artist.Firstname + " " + p.Artist.Lastname
                }).ToListAsync();
        }
    }
}
