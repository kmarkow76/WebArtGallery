using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtGallery.Data.Configurations;

/// <summary>
/// Класс конфигурации для сущности Painter.
/// Определяет схему базы данных и ограничения для модели Painter.
/// </summary>
public class PainterConfiguration : IEntityTypeConfiguration<Painter>
{
    /// <summary>
    /// Настраивает схему и ограничения для сущности Painter.
    /// </summary>
    /// <param name="builder">Объект для настройки сущности Painter.</param>
    public void Configure(EntityTypeBuilder<Painter> builder)
    {
        
    }
}