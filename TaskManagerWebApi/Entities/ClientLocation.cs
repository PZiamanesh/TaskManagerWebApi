using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebApi.Entities
{
    public class ClientLocation
    {
        [Key]
        public int ClientLocationID { get; set; }

        public string ClientLocationName { get; set; }
    }
}
