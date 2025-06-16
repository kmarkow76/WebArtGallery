using ArtGallery.Data.Models;
using ArtGallery.DTO.Painters;

namespace ArtGallery.Interfaces.ServicesInterfaces;

public interface IPainterService
{
    Task<PainterResponseDto> CreatePainterAsync(CreatePainterDto painterDto);
    Task<PainterResponseDto> GetPainterByIdAsync(int id);
    Task<IEnumerable<PainterResponseDto>> GetAllPaintersAsync();
    Task UpdatePainterAsync(int id, UpdatePainterDto painterDto);
    Task DeletePainterAsync(int id);
    Task<PainterResponseDto> GetPainterByFullNameAsync(string firstname, string lastname);
    Task<IEnumerable<PainterResponseDto>> GetPaintersByYearRangeAsync(string startYear, string endYear);
    Task<Painter?> GetByFullNameAsync(string firstname, string lastname); 
}