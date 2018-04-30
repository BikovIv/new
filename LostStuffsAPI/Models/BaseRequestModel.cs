using System;

namespace LostStuffs.Models
{
    public class BaseRequestModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}