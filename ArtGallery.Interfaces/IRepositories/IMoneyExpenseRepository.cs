using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.IRepositories;

/// <summary>
/// Репозиторий для работы с расходами.
/// </summary>
public interface IMoneyExpenseRepository
{
    /// <summary>
    /// Получает расход по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор расхода.</param>
    /// <returns>Расход или null.</returns>
    Task<MoneyExpense?> GetByIdAsync(int id);

    /// <summary>
    /// Получает все расходы.
    /// </summary>
    /// <returns>Список расходов.</returns>
    Task<IEnumerable<MoneyExpense>> GetAllAsync();

    /// <summary>
    /// Добавляет расход.
    /// </summary>
    /// <param name="moneyExpense">Данные расхода.</param>
    Task AddAsync(MoneyExpense? moneyExpense);

    /// <summary>
    /// Обновляет расход.
    /// </summary>
    /// <param name="moneyExpense">Обновленные данные расхода.</param>
    Task UpdateAsync(MoneyExpense? moneyExpense);

    /// <summary>
    /// Удаляет расход.
    /// </summary>
    /// <param name="id">Идентификатор расхода.</param>
    Task DeleteAsync(int id);
}