using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtGallery.Data.Configurations;

/// <summary>
/// Класс конфигурации для сущности Counterparty.
/// Определяет схему базы данных и ограничения для модели Counterparty.
/// </summary>
public class CounterpartyConfiguration : IEntityTypeConfiguration<Counterparty>
{
    /// <summary>
    /// Настраивает схему и ограничения для сущности Counterparty.
    /// </summary>
    /// <param name="builder">Объект для настройки сущности Counterparty.</param>
    public void Configure(EntityTypeBuilder<Counterparty> builder)
    {
       
    }
}