using AutoMapper;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Profiles
{
    public class ClientLocationProfile : Profile
    {
        public ClientLocationProfile()
        {
            CreateMap<ClientLocation, ClientLocationDto>();
        }
    }
}
