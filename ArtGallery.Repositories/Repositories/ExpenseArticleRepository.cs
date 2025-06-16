using ArtGallery.Data;
using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories.Repositories;

public class ExpenseArticleRepository : IExpenseArticleRepository
{
    private readonly GalleryDbContext _context;

    public ExpenseArticleRepository(GalleryDbContext context)
    {
        _context = context;
    }

    public async Task<ExpenseArticle?> GetByIdAsync(int id)
    {
        return await _context.ExpenseArticles
            .Include(ea => ea.MoneyExpenses)
            .FirstOrDefaultAsync(ea => ea.Id == id);
    }

    public async Task<IEnumerable<ExpenseArticle>> GetAllAsync()
    {
        return await _context.ExpenseArticles
            .Include(ea => ea.MoneyExpenses)
            .ToListAsync();
    }

    public async Task AddAsync(ExpenseArticle expenseArticle)
    {
        _context.ExpenseArticles.Add(expenseArticle);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ExpenseArticle expenseArticle)
    {
        _context.ExpenseArticles.Update(expenseArticle);
        await _context.SaveChangesAsync();
    }

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