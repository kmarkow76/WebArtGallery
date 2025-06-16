using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtGallery.Data.Configurations;

/// <summary>
/// Класс конфигурации для сущности Genre.
/// Определяет схему базы данных и ограничения для модели Genre.
/// </summary>
public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    /// <summary>
    /// Настраивает схему и ограничения для сущности Genre.
    /// </summary>
    /// <param name="builder">Объект для настройки сущности Genre.</param>
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        // Настройка свойства Name: обязательное поле с максимальной длиной 100 символов.
        builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
    }
}