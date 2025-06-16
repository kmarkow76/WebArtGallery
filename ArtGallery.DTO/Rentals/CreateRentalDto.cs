namespace ArtGallery.DTO.Rentals;

public class CreateRentalDto
{
    public int CounterpartyId { get; set; }

    public int PaintingId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Price { get; set; }
}