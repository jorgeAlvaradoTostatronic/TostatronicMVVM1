using System;
using System.Collections.Generic;
using System.Text;

namespace TostatronicMVVM.Models
{
    class SaleProduct : ProductModel
    {
        float priceAtSale;
        int quantity;

        public SaleProduct(float priceAtSale, int quantity)
        {
            this.priceAtSale = priceAtSale;
            this.quantity = quantity;
        }
        public float PriceAtSale { get => priceAtSale; set => priceAtSale = value; }
        public int Quantiy { get => quantity; set => quantity = value; }
    }
}
