using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebApi.Models
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }

        public string? ProjectName { get; set; }

        public DateOnly? DateOfStart { get; set; }

        public int? TeamSize { get; set; }
    }
}
