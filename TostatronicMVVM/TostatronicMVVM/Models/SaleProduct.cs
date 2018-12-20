using System;
using System.Collections.Generic;
using System.Text;

namespace TostatronicMVVM.Models
{
    class SaleProduct : ProductModel
    {
        float priceAtSale;
        int quantity;

        public SaleProduct(string codigo, string nombre, string imagen, int existencia, float precio_compra, float precio_publico, float precio_distribuidor, float precio_minimo,float priceAtSale, int quantity)
            :base(codigo, nombre, imagen, existencia, precio_compra, precio_publico, precio_distribuidor, precio_minimo)
        {
            this.priceAtSale = priceAtSale;
            this.quantity = quantity;
        }
        public float PriceAtSale { get => priceAtSale; set => priceAtSale = value; }
        public int Quantiy { get => quantity; set => quantity = value; }
    }
}
