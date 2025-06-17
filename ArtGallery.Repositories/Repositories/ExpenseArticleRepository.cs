using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы со статьями расходов.
/// </summary>
public class ExpenseArticleRepository : IExpenseArticleRepository
{
    private readonly GalleryDbContext _context;

    /// <summary>
    /// Инициализирует репозиторий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="context"/> null.</exception>
    public ExpenseArticleRepository(GalleryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Получает статью расходов по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор статьи расходов.</param>
    /// <returns>Статья расходов или null.</returns>
    public async Task<ExpenseArticle?> GetByIdAsync(int id)
    {
        return await _context.ExpenseArticles
            .Include(ea => ea.MoneyExpenses)
            .FirstOrDefaultAsync(ea => ea.Id == id);
    }

    /// <summary>
    /// Получает все статьи расходов.
    /// </summary>
    /// <returns>Список статей расходов.</returns>
    public async Task<IEnumerable<ExpenseArticle>> GetAllAsync()
    {
        return await _context.ExpenseArticles
            .Include(ea => ea.MoneyExpenses)
            .ToListAsync();
    }

    /// <summary>
    /// Добавляет статью расходов.
    /// </summary>
    /// <param name="expenseArticle">Данные статьи расходов.</param>
    public async Task AddAsync(ExpenseArticle expenseArticle)
    {
        _context.ExpenseArticles.Add(expenseArticle);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет статью расходов.
    /// </summary>
    /// <param name="expenseArticle">Обновленные данные статьи расходов.</param>
    public async Task UpdateAsync(ExpenseArticle expenseArticle)
    {
        _context.ExpenseArticles.Update(expenseArticle);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет статью расходов.
    /// </summary>
    /// <param name="id">Идентификатор статьи расходов.</param>
    public async Task DeleteAsync(int id)
    {
        var expenseArticle = await _context.ExpenseArticles.FindAsync(id);
        if (expenseArticle != null)
        {
            _context.ExpenseArticles.Remove(expenseArticle);
            await _context.SaveChangesAsync();
        }
    }
}