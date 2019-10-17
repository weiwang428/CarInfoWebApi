using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CarInfoWebApplication.Models
{
    /// <summary>
    /// Description Model, represents a Description Object.
    /// </summary>
    public class Description
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        [Required]
        public Car Car { get; set; }
    }
}