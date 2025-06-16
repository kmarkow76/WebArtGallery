using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtGallery.Data.Configurations;

/// <summary>
/// Класс конфигурации для сущности PaintingMovement.
/// Определяет схему базы данных и ограничения для модели PaintingMovement.
/// </summary>
public class PaintingMovementConfiguration : IEntityTypeConfiguration<PaintingMovement>
{
    /// <summary>
    /// Настраивает схему и ограничения для сущности PaintingMovement.
    /// </summary>
    /// <param name="builder">Объект для настройки сущности PaintingMovement.</param>
    public void Configure(EntityTypeBuilder<PaintingMovement> builder)
    {
        // Настройка связи один-ко-многим с сущностью Painting через внешний ключ PaintingId.
        // Отсутствует коллекция движений в Painting, используется ограничение на каскадное удаление.
        builder
            .HasOne(m => m.Painting)
            .WithMany() 
            .HasForeignKey(m => m.PaintingId)
            .OnDelete(DeleteBehavior.Restrict); 

        // Настройка связи один-ко-многим с сущностью Counterparty через внешний ключ CounterpartyId.
        // Отсутствует коллекция движений в Counterparty, при удалении Counterparty значение устанавливается в null.
        builder
            .HasOne(m => m.Counterparty)
            .WithMany() 
            .HasForeignKey(m => m.CounterpartyId)
            .OnDelete(DeleteBehavior.SetNull); 
    }
}