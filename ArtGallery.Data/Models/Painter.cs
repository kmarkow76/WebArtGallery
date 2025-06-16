namespace ArtGallery.Data.Models;

/// <summary>
/// Представляет художника/живописца.
/// </summary>
public class Painter
{
    /// <summary>
    /// Уникальный идентификатор художника.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Имя художника.
    /// </summary>
    public string Firstname { get; set; }
    
    /// <summary>
    /// Фамилия художника.
    /// </summary>
    public string Lastname { get; set; }
    
    /// <summary>
    /// Годы жизни художника в строковом формате (например, "1900-1950").
    /// </summary>
    public string Yearsoflife { get; set; }
    
    /// <summary>
    /// Историческая справка о художнике (может быть null).
    /// </summary>
    public string? Historicalbackground { get; set; }
    
    /// <summary>
    /// Список картин, созданных художником.
    /// </summary>
    public List<Painting> Paintings { get; set; }
}