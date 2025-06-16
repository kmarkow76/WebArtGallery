using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtGallery.Data.Configurations;

/// <summary>
/// Класс конфигурации для сущности Ticket.
/// Определяет схему базы данных и ограничения для модели Ticket.
/// </summary>
public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    /// <summary>
    /// Настраивает схему и ограничения для сущности Ticket.
    /// </summary>
    /// <param name="builder">Объект для настройки сущности Ticket.</param>
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        
    }
}