namespace ArtGallery.Data.Models;

/// <summary>
/// Представляет художественную картину.
/// </summary>
public class Painting
{
    /// <summary>
    /// Уникальный идентификатор картины.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор жанра картины.
    /// </summary>
    public int GenreId { get; set; }
    
    /// <summary>
    /// Идентификатор художника, создавшего картину.
    /// </summary>
    public int ArtistId { get; set; }
    
    /// <summary>
    /// Название картины.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Дата создания картины.
    /// </summary>
    public DateTime CreationDate { get; set; }
    
    /// <summary>
    /// Жанр, к которому относится картина.
    /// </summary>
    public Genre Genre { get; set; }
    
    /// <summary>
    /// Художник, создавший картину.
    /// </summary>
    public Painter Artist { get; set; }
    
    /// <summary>
    /// Список аренд/прокатов данной картины.
    /// </summary>
    public List<Rental> Rentals { get; set; }
}