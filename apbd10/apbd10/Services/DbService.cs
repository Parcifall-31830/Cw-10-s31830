using apbd10.Data;
using apbd10.DTOs;
using apbd10.Exceptions;
using apbd10.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Services;

public interface IDbService
{
    public Task<ICollection<TripResponseDto>> GetTrips();
    public Task RemoveClient(int idClienta);
    public Task SignClient(int idTrip,Client client);
}
public class DbService(ApbdContext data): IDbService
{
    public async Task<ICollection<TripResponseDto>> GetTrips()
    {
        return await data.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .Select(t => new TripResponseDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryDto
                {
                    Name = c.Name
                }).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDto
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName
                }).ToList()
            })
            .ToListAsync();
        
    }

    public async Task RemoveClient(int idClienta)
    {
        var existance = await data.ClientTrips.FirstOrDefaultAsync(s => s.IdClient == idClienta);

        if (existance is not null)
        {
            var affectedRows = await data.Clients.FirstOrDefaultAsync(s => s.IdClient == idClienta);

            if (affectedRows is not null)
            {
                throw new NotFoundException($"Client {idClienta} not found");
            }
        }
        else
        {
            throw new ClientTripException($"Client {idClienta} is signed to a trip");
        }
    }

    public async Task SignClient(int idTrip,Client client)
    {
        var clientExistance = await data.Clients.FirstOrDefaultAsync(s=>s.Pesel==client.Pesel);
        if (clientExistance is not null)
        {
            
            var clientTripConnection = await data.ClientTrips.FirstOrDefaultAsync(s => s.IdClient == idTrip && s.IdClient==client.IdClient);

            if (clientTripConnection is not null)
            {
                await data.ClientTrips.AddAsync(new ClientTrip
                {
                    IdClient = client.IdClient,
                    IdTrip = idTrip,
                    RegisteredAt = DateTime.Now,
                    PaymentDate = null
                    
                });
            }
            else
            {
                throw new ClientTripException($"Client {idTrip} is signed to a trip");
            }

        }
        else
        {
            throw new NotFoundException($"Client with pesel :{client.Pesel} does not exist");
        }
        
    }
    
    
}