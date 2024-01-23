using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Services
{
    public interface IClientLocationRepository
    {
        Task<IEnumerable<ClientLocation>> GetClientLocationsAsync();
    }
}
