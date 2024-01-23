using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerWebApi.Entities
{
    public class TaskProject
    {
        [Key]
        public int ProjectId { get; set; }

        [MaxLength(100)]
        public string ProjectName { get; set; } = "NoProjectName";

        public DateOnly? DateOfStart { get; set; }

        [Range(1, 50)]
        public int? TeamSize { get; set; }

        public bool Active { get; set; }

        public string Status { get; set; }

        public int ClientLocationID { get; set; }
        public virtual ClientLocation ClientLocation { get; set; }
    }
}
