using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Services;

namespace TaskManagerWebApi.Controllers;

[Route("api/projects")]
[ApiController]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectController(IMapper mapper, IProjectRepository projectRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskProject>>> GetProjects([FromQuery] ProjectForSearch search)
    {
        var projects = await _projectRepository.GetProjectsAsync(search);
        return Ok(projects);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> AddProject(ProjectForCreation createModel)
    {
        var entity = _mapper.Map<TaskProject>(createModel);
        await _projectRepository.InsertAsync(entity);
        _projectRepository.SaveChanges();
        var modelDto = _mapper.Map<ProjectDto>(entity);
        return Ok(modelDto);
    }

    [HttpPut]
    public async Task<ActionResult<ProjectDto>?> UpdateProject(ProjectDto updateModel)
    {
        if (! await _projectRepository.IsExistAsync(updateModel.ProjectId))
        {
            return null;
        }
        var entity = _mapper.Map<TaskProject>(updateModel);
        _projectRepository.Update(entity);
        _projectRepository.SaveChanges();
        var modelDto = _mapper.Map(entity, updateModel);
        return Ok(modelDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteProject(int id)
    {
        if (!await _projectRepository.IsExistAsync(id))
        {
            return -1;
        }

        await _projectRepository.DeleteAsync(id);
        _projectRepository.SaveChanges();
        return Ok(id);
    }
}
