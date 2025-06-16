using ArtGallery.DTO.Rentals;
using ArtGallery.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Core.Controllers;

/// <summary>
/// Контроллер для управления арендой.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _rentalService;

    /// <summary>
    /// Инициализирует контроллер.
    /// </summary>
    /// <param name="rentalService">Сервис аренды.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="rentalService"/> null.</exception>
    public RentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService ?? throw new ArgumentNullException(nameof(rentalService));
    }

    /// <summary>
    /// Создает аренду.
    /// </summary>
    /// <param name="rentalDto">Данные аренды.</param>
    /// <returns>Созданная аренда или ошибка.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateRental([FromBody] CreateRentalDto rentalDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = await _rentalService.CreateRentalAsync(rentalDto);
            return CreatedAtAction(nameof(GetRentalById), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Получает аренду по ID.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    /// <returns>Аренда или ошибка.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRentalById(int id)
    {
        try
        {
            var rental = await _rentalService.GetRentalByIdAsync(id);
            return Ok(rental);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Получает все аренды.
    /// </summary>
    /// <returns>Список аренд.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllRentals()
    {
        try
        {
            var rentals = await _rentalService.GetAllRentalsAsync();
            return Ok(rentals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Удаляет аренду.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    /// <returns>Код 204 или ошибка.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRental(int id)
    {
        try
        {
            await _rentalService.DeleteRentalAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Получает аренды по названию картины.
    /// </summary>
    /// <param name="paintingTitle">Название картины.</param>
    /// <returns>Список аренд.</returns>
    [HttpGet("by-painting-title")]
    public async Task<IActionResult> GetRentalsByPaintingTitle([FromQuery] string paintingTitle)
    {
        try
        {
            var rentals = await _rentalService.GetRentalsByPaintingTitleAsync(paintingTitle);
            return Ok(rentals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }
}