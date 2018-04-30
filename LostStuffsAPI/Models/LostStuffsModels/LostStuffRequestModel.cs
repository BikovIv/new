﻿using LostStuffs.Entities;
using System.Collections.Generic;

namespace LostStuffs.Models
{
    public class LostStuffRequestModel : BaseRequestModel
    {
        // [Required]
        // [MinLength(10,ErrorMessage = "The title must be 8 characters long.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        // public string Category { get; set; }
        // [Required]
        //[StringLength(10 , ErrorMessage = "The phone number must be 10 characters long.")]
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}