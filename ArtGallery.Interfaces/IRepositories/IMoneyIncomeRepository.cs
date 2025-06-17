using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.IRepositories;

/// <summary>
/// Репозиторий для работы с доходами.
/// </summary>
public interface IMoneyIncomeRepository
{
    /// <summary>
    /// Получает доход по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор дохода.</param>
    /// <returns>Доход или null.</returns>
    Task<MoneyIncome?> GetByIdAsync(int id);

    /// <summary>
    /// Получает все доходы.
    /// </summary>
    /// <returns>Список доходов.</returns>
    Task<IEnumerable<MoneyIncome>> GetAllAsync();

    /// <summary>
    /// Добавляет доход.
    /// </summary>
    /// <param name="moneyIncome">Данные дохода.</param>
    Task AddAsync(MoneyIncome moneyIncome);

    /// <summary>
    /// Обновляет доход.
    /// </summary>
    /// <param name="moneyIncome">Обновленные данные дохода.</param>
    Task UpdateAsync(MoneyIncome moneyIncome);

    /// <summary>
    /// Удаляет доход.
    /// </summary>
    /// <param name="id">Идентификатор дохода.</param>
    Task DeleteAsync(int id);
}