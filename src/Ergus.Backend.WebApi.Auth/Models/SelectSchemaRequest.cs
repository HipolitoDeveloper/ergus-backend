using System.ComponentModel.DataAnnotations;

namespace Ergus.Backend.WebApi.Auth.Models
{
    public class SelectSchemaRequest
    {
        [Required]
        public int SchemaId { get; set; }
    }
}
