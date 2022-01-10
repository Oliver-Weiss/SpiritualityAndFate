using System;
using System.ComponentModel.DataAnnotations;

namespace SpiritualityAndFate.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public int PlayerId { get; set; }
        public Player Owner { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}