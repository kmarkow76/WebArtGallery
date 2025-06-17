using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.IRepositories;

/// <summary>
/// Репозиторий для работы со статьями расходов.
/// </summary>
public interface IExpenseArticleRepository
{
    /// <summary>
    /// Получает статью расходов по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор статьи расходов.</param>
    /// <returns>Статья расходов или null.</returns>
    Task<ExpenseArticle?> GetByIdAsync(int id);

    /// <summary>
    /// Получает все статьи расходов.
    /// </summary>
    /// <returns>Список статей расходов.</returns>
    Task<IEnumerable<ExpenseArticle>> GetAllAsync();

    /// <summary>
    /// Добавляет статью расходов.
    /// </summary>
    /// <param name="expenseArticle">Данные статьи расходов.</param>
    Task AddAsync(ExpenseArticle expenseArticle);

    /// <summary>
    /// Обновляет статью расходов.
    /// </summary>
    /// <param name="expenseArticle">Обновленные данные статьи расходов.</param>
    Task UpdateAsync(ExpenseArticle expenseArticle);

    /// <summary>
    /// Удаляет статью расходов.
    /// </summary>
    /// <param name="id">Идентификатор статьи расходов.</param>
    Task DeleteAsync(int id);
}