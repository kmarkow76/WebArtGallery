using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Data;

/// <summary>
/// Контекст базы данных для художественной галереи.
/// Предоставляет доступ к таблицам и сущностям системы.
/// </summary>
public class GalleryDbContext : DbContext
{
    /// <summary>
    /// Набор данных для работы с жанрами.
    /// </summary>
    public DbSet<Genre> Genres { get; set; }

    /// <summary>
    /// Набор данных для работы с художниками.
    /// </summary>
    public DbSet<Painter> Painters { get; set; }

    /// <summary>
    /// Набор данных для работы с картинами.
    /// </summary>
    public DbSet<Painting> Paintings { get; set; }

    /// <summary>
    /// Набор данных для работы с контрагентами (может содержать null-значения).
    /// </summary>
    public DbSet<Counterparty?> Counterparties { get; set; }

    /// <summary>
    /// Набор данных для работы со статьями расходов.
    /// </summary>
    public DbSet<ExpenseArticle> ExpenseArticles { get; set; }

    /// <summary>
    /// Набор данных для работы с денежными расходами (может содержать null-значения).
    /// </summary>
    public DbSet<MoneyExpense?> MoneyExpenses { get; set; }

    /// <summary>
    /// Набор данных для работы с денежными доходами.
    /// </summary>
    public DbSet<MoneyIncome> MoneyIncomes { get; set; }

    /// <summary>
    /// Набор данных для работы с арендами картин.
    /// </summary>
    public DbSet<Rental> Rentals { get; set; }

    /// <summary>
    /// Набор данных для работы с билетами.
    /// </summary>
    public DbSet<Ticket> Tickets { get; set; }

    /// <summary>
    /// Набор данных для работы с перемещениями картин.
    /// </summary>
    public DbSet<PaintingMovement> PaintingMovements { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр контекста базы данных галереи.
    /// </summary>
    /// <param name="options">Параметры конфигурации для контекста базы данных.</param>
    public GalleryDbContext(DbContextOptions<GalleryDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Конфигурирует модель базы данных при ее создании.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GalleryDbContext).Assembly);
    }
}