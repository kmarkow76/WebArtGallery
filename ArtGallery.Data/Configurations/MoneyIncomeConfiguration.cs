using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtGallery.Data.Configurations;

/// <summary>
/// Класс конфигурации для сущности MoneyIncome.
/// Определяет схему базы данных и ограничения для модели MoneyIncome.
/// </summary>
public class MoneyIncomeConfiguration : IEntityTypeConfiguration<MoneyIncome>
{
    /// <summary>
    /// Настраивает схему и ограничения для сущности MoneyIncome.
    /// </summary>
    /// <param name="builder">Объект для настройки сущности MoneyIncome.</param>
    public void Configure(EntityTypeBuilder<MoneyIncome> builder)
    {
        // Настройка связи один-ко-многим с сущностью Counterparty через внешний ключ CounterpartyId.
        builder.HasOne(mi => mi.Counterparty)
            .WithMany(c => c.MoneyIncomes)
            .HasForeignKey(mi => mi.CounterpartyId);
    }
}