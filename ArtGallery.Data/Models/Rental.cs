namespace ArtGallery.Data.Models;

/// <summary>
/// Представляет аренду картины.
/// </summary>
public class Rental
{
    /// <summary>
    /// Уникальный идентификатор аренды.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор контрагента (арендатора).
    /// </summary>
    public int CounterpartyId { get; set; }
    
    /// <summary>
    /// Идентификатор арендуемой картины.
    /// </summary>
    public int PaintingId { get; set; }
    
    /// <summary>
    /// Дата начала аренды.
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Дата окончания аренды.
    /// </summary>
    public DateTime EndDate { get; set; }
    
    /// <summary>
    /// Стоимость аренды.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Контрагент (арендатор).
    /// </summary>
    public Counterparty Counterparty { get; set; }
    
    /// <summary>
    /// Арендуемая картина.
    /// </summary>
    public Painting Painting { get; set; }
}