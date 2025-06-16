using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.Repositories
{
    public interface IMoneyExpenseRepository
    {
        Task<MoneyExpense?> GetByIdAsync(int id);
        Task<IEnumerable<MoneyExpense>> GetAllAsync();
        Task AddAsync(MoneyExpense? moneyExpense);
        Task UpdateAsync(MoneyExpense? moneyExpense);
        Task DeleteAsync(int id);
    }
}