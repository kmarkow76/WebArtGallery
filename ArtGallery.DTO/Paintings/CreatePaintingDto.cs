namespace ArtGallery.DTO.Paintings;

public class CreatePaintingDto
{
    public int GenreId { get; set; }
    public int ArtistId { get; set; }
    public string Title { get; set; }
    public DateTime CreationDate { get; set; }
}