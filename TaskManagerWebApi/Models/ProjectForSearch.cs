using Microsoft.AspNetCore.Mvc;

namespace TaskManagerWebApi.Models
{
    public class ProjectForSearch
    {
        // filters
        public string? ProjectName { get; set; }
        public DateOnly? Date { get; set; }

        // search query
        public string? SearchText { get; set; }
    }
}
