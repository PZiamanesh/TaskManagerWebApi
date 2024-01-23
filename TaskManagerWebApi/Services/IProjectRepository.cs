using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Services
{
    public interface IProjectRepository
    {
        Task<IEnumerable<TaskProject>> GetProjectsAsync(ProjectForSearch parameters);

        Task<TaskProject?> GetByAsync(int id);

        Task<bool> IsExistAsync(int id);

        Task InsertAsync(TaskProject project);

        void Update(TaskProject entity);

        Task DeleteAsync(int id);

        void SaveChanges();
    }
}
