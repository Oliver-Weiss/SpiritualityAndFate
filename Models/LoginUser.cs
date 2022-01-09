using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpiritualityAndFate.Models
{
    public class LoginUser
    {
        [Required]
        public string LogUsername {get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string LogPassword {get; set;}
    }
}