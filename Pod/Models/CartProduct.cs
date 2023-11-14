﻿namespace Pod.Models
{
    public class CartProduct
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
