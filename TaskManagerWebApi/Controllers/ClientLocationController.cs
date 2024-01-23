using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Services;

namespace TaskManagerWebApi.Controllers
{
    [Route("api/ClientLocations")]
    [ApiController]
    public class ClientLocationController : ControllerBase
    {
        private readonly IClientLocationRepository _clientLocationRepository;
        private readonly IMapper _mapper;

        public ClientLocationController(IClientLocationRepository clientLocationRepository,
            IMapper mapper)
        {
            _clientLocationRepository = clientLocationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ClientLocationDto>> GetClientLocations()
        {
            var entities = await _clientLocationRepository.GetClientLocationsAsync();
            return Ok(_mapper.Map<IEnumerable<ClientLocationDto>>(entities));
        }
    }
}
