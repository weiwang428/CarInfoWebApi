using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CarInfoWebApplication.Models
{
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