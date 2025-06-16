using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtGallery.Data.Configurations;

/// <summary>
/// Класс конфигурации для сущности Rental.
/// Определяет схему базы данных и ограничения для модели Rental.
/// </summary>
public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    /// <summary>
    /// Настраивает схему и ограничения для сущности Rental.
    /// </summary>
    /// <param name="builder">Объект для настройки сущности Rental.</param>
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        // Настройка связи один-ко-многим с сущностью Counterparty через внешний ключ CounterpartyId.
        builder.HasOne(r => r.Counterparty)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CounterpartyId);

        // Настройка связи один-ко-многим с сущностью Painting через внешний ключ PaintingId.
        builder.HasOne(r => r.Painting)
            .WithMany(p => p.Rentals)
            .HasForeignKey(r => r.PaintingId);
    }
}