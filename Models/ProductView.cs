﻿using Microsoft.AspNetCore.Identity;

namespace TechLife.Models
{
    public class ProductView
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
