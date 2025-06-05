using System.ComponentModel.DataAnnotations;

namespace apbd10.DTOs;

public class ClientTripDto
{
    [MaxLength(50)]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    [MaxLength(11)]
    public string Pesel { get; set; }
    public int IdTrip { get; set; }
    public string TripName { get; set; }
    public DateTime PaymentDate { get; set; }
}