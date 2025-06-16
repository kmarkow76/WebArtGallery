namespace ArtGallery.Data.Models;

/// <summary>
/// Представляет жанр художественного произведения.
/// </summary>
public class Genre
{
    /// <summary>
    /// Уникальный идентификатор жанра.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название жанра.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Список картин, относящихся к данному жанру.
    /// </summary>
    public List<Painting> Paintings { get; set; }
}