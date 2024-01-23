namespace TaskManagerWebApi.Models
{
    public class ProjectForUpdate
    {
        public string? ProjectName { get; set; }

        public DateOnly? DateOfStart { get; set; }

        public int? TeamSize { get; set; }
    }
}
