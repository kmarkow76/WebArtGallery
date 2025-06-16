namespace ArtGallery.Data.Models;

/// <summary>
/// Представляет билет в художественную галерею.
/// </summary>
public class Ticket
{
    /// <summary>
    /// Уникальный идентификатор билета.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Цена билета.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Количество билетов.
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Тип билета.
    /// </summary>
    public string TicketType { get; set; }
    
    /// <summary>
    /// Дата покупки билета.
    /// </summary>
    public DateTime PurchaseDate { get; set; }
}