using ArtGallery.DTO.Rentals;

namespace ArtGallery.Interfaces.IServices;

/// <summary>
/// Сервис для работы с арендой.
/// </summary>
public interface IRentalService
{
    /// <summary>
    /// Создает аренду.
    /// </summary>
    /// <param name="rentalDto">Данные аренды.</param>
    /// <returns>Созданная аренда.</returns>
    Task<RentalResponseDto> CreateRentalAsync(CreateRentalDto rentalDto);

    /// <summary>
    /// Получает аренду по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    /// <returns>Аренда.</returns>
    Task<RentalResponseDto> GetRentalByIdAsync(int id);

    /// <summary>
    /// Получает все аренды.
    /// </summary>
    /// <returns>Список аренд.</returns>
    Task<IEnumerable<RentalResponseDto>> GetAllRentalsAsync();

    /// <summary>
    /// Удаляет аренду.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    Task DeleteRentalAsync(int id);

    /// <summary>
    /// Получает аренды по названию картины.
    /// </summary>
    /// <param name="paintingTitle">Название картины.</param>
    /// <returns>Список аренд.</returns>
    Task<IEnumerable<RentalResponseDto>> GetRentalsByPaintingTitleAsync(string paintingTitle);
}