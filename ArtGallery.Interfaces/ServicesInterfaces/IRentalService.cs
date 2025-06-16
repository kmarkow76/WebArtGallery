using ArtGallery.DTO.Rentals;

namespace ArtGallery.Interfaces.ServicesInterfaces;

public interface IRentalService
{
    Task<RentalResponseDto> CreateRentalAsync(CreateRentalDto rentalDto);
    Task<RentalResponseDto> GetRentalByIdAsync(int id);
    Task<IEnumerable<RentalResponseDto>> GetAllRentalsAsync();
    Task DeleteRentalAsync(int id);
    Task<IEnumerable<RentalResponseDto>> GetRentalsByPaintingTitleAsync(string paintingTitle);
}