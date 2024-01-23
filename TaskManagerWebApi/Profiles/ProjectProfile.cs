using AutoMapper;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectForCreation, TaskProject>();
            CreateMap<TaskProject, ProjectDto>().ReverseMap();
        }
    }
}
