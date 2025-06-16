using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.Repositories
{
    public interface IMoneyIncomeRepository
    {
        Task<MoneyIncome?> GetByIdAsync(int id);
        Task<IEnumerable<MoneyIncome>> GetAllAsync();
        Task AddAsync(MoneyIncome moneyIncome);
        Task UpdateAsync(MoneyIncome moneyIncome);
        Task DeleteAsync(int id);
    }
}