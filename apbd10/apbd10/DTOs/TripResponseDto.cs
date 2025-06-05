namespace apbd10.DTOs;

public class TripResponseDto {
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<CountryDto> Countries { get; set; }
    public List<ClientDto> Clients { get; set; }
}


public class CountryDto {
    public String Name {get; set;}
}

public class ClientDto {
    public String FirstName { get; set; }
    public String LastName{get; set;}

}