using ArtGallery.DTO.Painters;
using ArtGallery.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Core.Controllers;

/// <summary>
/// Контроллер для управления художниками.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PaintersController : ControllerBase
{
    private readonly IPainterService _painterService;

    /// <summary>
    /// Инициализирует контроллер.
    /// </summary>
    /// <param name="painterService">Сервис художников.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="painterService"/> null.</exception>
    public PaintersController(IPainterService painterService)
    {
        _painterService = painterService ?? throw new ArgumentNullException(nameof(painterService));
    }

    /// <summary>
    /// Создает художника.
    /// </summary>
    /// <param name="painterDto">Данные художника.</param>
    /// <returns>Созданный художник или ошибка.</returns>
    [HttpPost]
    public async Task<IActionResult> CreatePainter([FromBody] CreatePainterDto painterDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = await _painterService.CreatePainterAsync(painterDto);
            return CreatedAtAction(nameof(GetPainterById), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Получает художника по ID.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    /// <returns>Художник или ошибка.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPainterById(int id)
    {
        try
        {
            var painter = await _painterService.GetPainterByIdAsync(id);
            return Ok(painter);
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
    /// Получает всех художников.
    /// </summary>
    /// <returns>Список художников.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllPainters()
    {
        try
        {
            var painters = await _painterService.GetAllPaintersAsync();
            return Ok(painters);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Обновляет художника.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    /// <param name="painterDto">Обновленные данные.</param>
    /// <returns>Код 204 или ошибка.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePainter(int id, [FromBody] UpdatePainterDto painterDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _painterService.UpdatePainterAsync(id, painterDto);
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
    /// Удаляет художника.
    /// </summary>
    /// <param name="id">Идентификатор художника.</param>
    /// <returns>Код 204 или ошибка.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePainter(int id)
    {
        try
        {
            await _painterService.DeletePainterAsync(id);
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
    /// Получает художника по имени и фамилии.
    /// </summary>
    /// <param name="firstname">Имя художника.</param>
    /// <param name="lastname">Фамилия художника.</param>
    /// <returns>Художник или ошибка.</returns>
    [HttpGet("by-full-name")]
    public async Task<IActionResult> GetPainterByFullName([FromQuery] string firstname, [FromQuery] string lastname)
    {
        try
        {
            var painter = await _painterService.GetPainterByFullNameAsync(firstname, lastname);
            return Ok(painter);
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
    /// Получает художников по диапазону лет.
    /// </summary>
    /// <param name="startYear">Начальный год.</param>
    /// <param name="endYear">Конечный год.</param>
    /// <returns>Список художников.</returns>
    [HttpGet("by-year-range")]
    public async Task<IActionResult> GetPaintersByYearRange([FromQuery] string startYear, [FromQuery] string endYear)
    {
        try
        {
            var painters = await _painterService.GetPaintersByYearRangeAsync(startYear, endYear);
            return Ok(painters);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }
}