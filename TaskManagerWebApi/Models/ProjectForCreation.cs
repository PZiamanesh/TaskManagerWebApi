namespace TaskManagerWebApi.Models
{
    public class ProjectForCreation
    {
        public string? ProjectName { get; set; }

        public DateOnly? DateOfStart { get; set; }

        public int? TeamSize { get; set; }

        public bool Active { get; set; }

        public string Status { get; set; }

        public int ClientLocationID { get; set; }
    }
}
