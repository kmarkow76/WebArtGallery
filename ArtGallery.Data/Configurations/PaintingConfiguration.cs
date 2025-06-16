using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtGallery.Data.Configurations;

/// <summary>
/// Класс конфигурации для сущности Painting.
/// Определяет схему базы данных и ограничения для модели Painting.
/// </summary>
public class PaintingConfiguration : IEntityTypeConfiguration<Painting>
{
    /// <summary>
    /// Настраивает схему и ограничения для сущности Painting.
    /// </summary>
    /// <param name="builder">Объект для настройки сущности Painting.</param>
    public void Configure(EntityTypeBuilder<Painting> builder)
    {
        // Настройка связи один-ко-многим с сущностью Genre через внешний ключ GenreId.
        builder.HasOne(p => p.Genre)
            .WithMany(g => g.Paintings)
            .HasForeignKey(p => p.GenreId);

        // Настройка связи один-ко-многим с сущностью Artist через внешний ключ ArtistId.
        builder.HasOne(p => p.Artist)
            .WithMany(p => p.Paintings)
            .HasForeignKey(p => p.ArtistId);
    }
}