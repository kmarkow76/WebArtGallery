namespace ArtGallery.Data.Models;

/// <summary>
/// Представляет перемещение картины (продажа, аренда, передача и т.д.).
/// </summary>
public class PaintingMovement
{
    /// <summary>
    /// Уникальный идентификатор перемещения.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор картины, которая была перемещена.
    /// </summary>
    public int PaintingId { get; set; }
    
    /// <summary>
    /// Картина, которая была перемещена.
    /// </summary>
    public Painting Painting { get; set; }
    
    /// <summary>
    /// Тип перемещения (продажа, аренда, передача и т.д.).
    /// </summary>
    public string MovementType { get; set; }
    
    /// <summary>
    /// Дата перемещения.
    /// </summary>
    public DateTime MovementDate { get; set; }
    
    /// <summary>
    /// Идентификатор контрагента (может быть null).
    /// </summary>
    public int? CounterpartyId { get; set; }
    
    /// <summary>
    /// Контрагент, участвовавший в перемещении.
    /// </summary>
    public Counterparty Counterparty { get; set; }
    
    /// <summary>
    /// Дополнительные заметки о перемещении.
    /// </summary>
    public string Notes { get; set; }
}