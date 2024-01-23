using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.DataAccess;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Services
{
    public class ClientLocationRepository : IClientLocationRepository
    {
        private readonly TaskManagerDbContext _context;

        public ClientLocationRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientLocation>> GetClientLocationsAsync()
        {
            return await _context.ClientLocations.ToListAsync();
        }
    }
}
