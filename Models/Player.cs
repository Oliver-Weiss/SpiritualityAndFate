using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpiritualityAndFate.Models
{
    public class Player
    {
        [Key]
        public int PlayerId {get; set;}
        public string Name {get; set;}
        public string Species {get; set;}
        public string AgeRange {get; set;}
        public List<Item> Inventory {get; set;}

        [Required]
        [MinLength(2)]
        public string Username {get; set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get; set;}
    }
}