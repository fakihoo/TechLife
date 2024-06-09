﻿namespace TechLife.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int ShopStoreId { get; set; }
        public int Quantity { get; set; }

        public ShopStore? ShopStore { get; set; }
    }

}