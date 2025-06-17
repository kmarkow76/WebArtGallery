using ArtGallery.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ArtGallery.DTO;
using ArtGallery.DTO.Paintings;
using ArtGallery.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Core.Controllers;

/// <summary>
/// Контроллер для управления картинами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PaintingsController : ControllerBase
{
    private readonly IPaintingService _paintingService;

    /// <summary>
    /// Инициализирует контроллер.
    /// </summary>
    /// <param name="paintingService">Сервис картин.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="paintingService"/> null.</exception>
    public PaintingsController(IPaintingService paintingService)
    {
        _paintingService = paintingService ?? throw new ArgumentNullException(nameof(paintingService));
    }

    /// <summary>
    /// Получает все картины с их статусом.
    /// </summary>
    /// <returns>Список картин.</returns>
    [HttpGet]
    public async Task<IActionResult> GetPaintings()
    {
        var paintings = await _paintingService.GetPaintingsWithStatusAsync();
        return Ok(paintings);
    }

    /// <summary>
    /// Создает картину.
    /// </summary>
    /// <param name="paintingDto">Данные картины.</param>
    /// <returns>Созданная картина или ошибка.</returns>
    [HttpPost]
    public async Task<IActionResult> CreatePainting([FromBody] CreatePaintingDto paintingDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var painting = new Painting
            {
                GenreId = paintingDto.GenreId,
                ArtistId = paintingDto.ArtistId,
                Title = paintingDto.Title,
                CreationDate = paintingDto.CreationDate.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(paintingDto.CreationDate, DateTimeKind.Utc)
                    : paintingDto.CreationDate.ToUniversalTime()
            };

            await _paintingService.AddWithInitialMovementAsync(painting);
            return CreatedAtAction(nameof(GetPaintings), new { id = painting.Id }, painting);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Ошибка при сохранении данных: {ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Удаляет картину.
    /// </summary>
    /// <param name="id">Идентификатор картины.</param>
    /// <returns>Код 204 или ошибка.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePainting(int id)
    {
        var painting = await _paintingService.GetByIdAsync(id);
        if (painting == null)
            return NotFound();

        await _paintingService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Получает перемещения картины.
    /// </summary>
    /// <param name="id">Идентификатор картины.</param>
    /// <param name="page">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Список перемещений или ошибка.</returns>
    [HttpGet("{id}/movements")]
    public async Task<IActionResult> GetPaintingMovements(int id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var movements = await _paintingService.GetMovementsByPaintingIdAsync(id);
        if (!movements.Any())
            return NotFound("No movements found for this painting.");
        return Ok(movements);
    }

    /// <summary>
    /// Получает картины по жанру.
    /// </summary>
    /// <param name="genreId">Идентификатор жанра.</param>
    /// <returns>Список картин.</returns>
    [HttpGet("by-genre/{genreId}")]
    public async Task<IActionResult> GetPaintingsByGenre(int genreId)
    {
        var paintings = await _paintingService.GetPaintingsByGenreAsync(genreId);
        return Ok(paintings);
    }
}