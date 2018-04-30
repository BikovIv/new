﻿namespace LostStuffs.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public int LostStuffId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public virtual LostStuff LostStuff { get; set; }
    }
}