using ArtGallery.DTO.Counterparties;
using ArtGallery.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Core.Controllers;

/// <summary>
/// Контроллер для управления контрагентами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CounterpartiesController : ControllerBase
{
    private readonly ICounterpartyService _counterpartyService;

    /// <summary>
    /// Инициализирует контроллер.
    /// </summary>
    /// <param name="counterpartyService">Сервис контрагентов.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="counterpartyService"/> null.</exception>
    public CounterpartiesController(ICounterpartyService counterpartyService)
    {
        _counterpartyService = counterpartyService ?? throw new ArgumentNullException(nameof(counterpartyService));
    }

    /// <summary>
    /// Создает контрагента.
    /// </summary>
    /// <param name="counterpartyDto">Данные контрагента.</param>
    /// <returns>Созданный контрагент или ошибка.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateCounterparty([FromBody] CreateCounterpartyDto counterpartyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = await _counterpartyService.CreateCounterpartyAsync(counterpartyDto);
            return CreatedAtAction(nameof(GetCounterpartyById), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Получает контрагента по ID.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    /// <returns>Контрагент или ошибка.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCounterpartyById(int id)
    {
        try
        {
            var counterparty = await _counterpartyService.GetCounterpartyByIdAsync(id);
            return Ok(counterparty);
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
    /// Получает всех контрагентов.
    /// </summary>
    /// <returns>Список контрагентов.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllCounterparties()
    {
        try
        {
            var counterparties = await _counterpartyService.GetAllCounterpartiesAsync();
            return Ok(counterparties);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Обновляет контрагента.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    /// <param name="counterpartyDto">Обновленные данные.</param>
    /// <returns>Код 204 или ошибка.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCounterparty(int id, [FromBody] UpdateCounterpartyDto counterpartyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _counterpartyService.UpdateCounterpartyAsync(id, counterpartyDto);
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
    /// Удаляет контрагента.
    /// </summary>
    /// <param name="id">Идентификатор контрагента.</param>
    /// <returns>Код 204 или ошибка.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCounterparty(int id)
    {
        try
        {
            await _counterpartyService.DeleteCounterpartyAsync(id);
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
    /// Получает контрагентов с доходом выше порога.
    /// </summary>
    /// <param name="threshold">Порог дохода.</param>
    /// <returns>Список контрагентов.</returns>
    [HttpGet("with-total-income-above")]
    public async Task<IActionResult> GetCounterpartiesWithTotalIncomeAbove([FromQuery] decimal threshold)
    {
        try
        {
            var counterparties = await _counterpartyService.GetCounterpartiesWithTotalIncomeAboveAsync(threshold);
            return Ok(counterparties);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Получает контрагентов с арендной стоимостью выше порога.
    /// </summary>
    /// <param name="threshold">Порог стоимости аренды.</param>
    /// <returns>Список контрагентов.</returns>
    [HttpGet("with-total-rental-cost-above")]
    public async Task<IActionResult> GetCounterpartiesWithTotalRentalCostAbove([FromQuery] decimal threshold)
    {
        try
        {
            var counterparties = await _counterpartyService.GetCounterpartiesWithTotalRentalCostAboveAsync(threshold);
            return Ok(counterparties);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }
}