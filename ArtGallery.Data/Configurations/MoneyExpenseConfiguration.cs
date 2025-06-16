using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtGallery.Data.Configurations;

/// <summary>
/// Класс конфигурации для сущности MoneyExpense.
/// Определяет схему базы данных и ограничения для модели MoneyExpense.
/// </summary>
public class MoneyExpenseConfiguration : IEntityTypeConfiguration<MoneyExpense>
{
    /// <summary>
    /// Настраивает схему и ограничения для сущности MoneyExpense.
    /// </summary>
    /// <param name="builder">Объект для настройки сущности MoneyExpense.</param>
    public void Configure(EntityTypeBuilder<MoneyExpense> builder)
    {
        // Настройка связи один-ко-многим с сущностью Counterparty через внешний ключ CounterpartyId.
        builder
            .HasOne(me => me.Counterparty)
            .WithMany(c => c.MoneyExpenses)
            .HasForeignKey(me => me.CounterpartyId);

        // Настройка связи один-ко-многим с сущностью ExpenseArticle через внешний ключ ExpenseArticleId.
        builder
            .HasOne(me => me.ExpenseArticle)
            .WithMany(ea => ea.MoneyExpenses)
            .HasForeignKey(me => me.ExpenseArticleId);
    }
}