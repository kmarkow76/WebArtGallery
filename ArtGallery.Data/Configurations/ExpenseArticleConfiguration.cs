using ArtGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtGallery.Data.Configurations;

/// <summary>
/// Класс конфигурации для сущности ExpenseArticle.
/// Определяет схему базы данных и ограничения для модели ExpenseArticle.
/// </summary>
public class ExpenseArticleConfiguration : IEntityTypeConfiguration<ExpenseArticle>
{
    /// <summary>
    /// Настраивает схему и ограничения для сущности ExpenseArticle.
    /// </summary>
    /// <param name="builder">Объект для настройки сущности ExpenseArticle.</param>
    public void Configure(EntityTypeBuilder<ExpenseArticle> builder)
    {
        
    }
}