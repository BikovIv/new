using System.Collections.Generic;

namespace LostStuffs.Entities
{
    public class LostStuff : BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }


        public string  PhoneNumber { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}