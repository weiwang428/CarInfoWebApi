using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarInfoWebApplication.Models
{
    /// <summary>
    /// Car Model, represents a Car Object. One car can have many descriptions.
    /// </summary>
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public List<Description> Descriptions { get; private set; } = new List<Description>();
    }
}