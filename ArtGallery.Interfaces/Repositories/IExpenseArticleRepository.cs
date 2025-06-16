using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.Repositories
{
    public interface IExpenseArticleRepository
    {
        Task<ExpenseArticle?> GetByIdAsync(int id);
        Task<IEnumerable<ExpenseArticle>> GetAllAsync();
        Task AddAsync(ExpenseArticle expenseArticle);
        Task UpdateAsync(ExpenseArticle expenseArticle);
        Task DeleteAsync(int id);
    }
}