using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using TaskManagerWebApi.DataAccess;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Services
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskManagerDbContext _dbContext;

        public ProjectRepository(TaskManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TaskProject>> GetAllAsync()
        {
            return await _dbContext.TaskProjects.ToListAsync();
        }

        public async Task<IEnumerable<TaskProject>> GetProjectsAsync(ProjectForSearch parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.ProjectName) &&
                parameters.Date == null &&
                string.IsNullOrWhiteSpace(parameters.SearchText))
            {
                return await _dbContext.TaskProjects.ToListAsync();
            }

            var collection = _dbContext.TaskProjects.Include(p=>p.ClientLocation) as IQueryable<TaskProject>;

            if (!string.IsNullOrWhiteSpace(parameters.ProjectName))
            {
                var nameFilter = parameters.ProjectName.Trim();
                collection = collection.Where(tp => tp.ProjectName == nameFilter);
            }
            if (parameters.Date != null)
            {
                collection = collection.Where(tp => tp.DateOfStart == parameters.Date);
            }
            if (!string.IsNullOrWhiteSpace(parameters.SearchText))
            {
                var searchText = parameters.SearchText.Trim();
                collection = collection.Where(tp => tp.ProjectName.Contains(searchText));
            }

            return await collection.ToListAsync();
        }

        public async Task<TaskProject?> GetByAsync(int id)
        {
            return await _dbContext.TaskProjects.FindAsync(id);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _dbContext.TaskProjects.AsNoTracking().AnyAsync(p => p.ProjectId == id);
        }

        public async Task InsertAsync(TaskProject entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Update(TaskProject entity)
        {
            _dbContext.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.TaskProjects.FindAsync(id);
            if (entity != null)
            {
                _dbContext.TaskProjects.Remove(entity);
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
