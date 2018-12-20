using System;
using System.Collections.Generic;
using System.Text;
using TostatronicMVVM.Common;

namespace TostatronicMVVM.Models
{
    public abstract class ProductModel : BaseNotifyPropertyChanged
    {
        string codigo, nombre, imagen;
        int existencia;
        float precio_compra, precio_publico, precio_distribuidor, precio_minimo;
        public ProductModel()
        {

        }
        public ProductModel(string codigo, string nombre, string imagen, int existencia, 
            float precio_compra, float precio_publico, float precio_distribuidor, float precio_minimo)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.imagen = imagen;
            this.existencia = existencia;
            this.precio_compra = precio_compra;
            this.precio_publico = precio_publico;
            this.precio_distribuidor = precio_distribuidor;
            this.precio_minimo = precio_minimo;
        }

        public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Imagen
        {
            get => imagen;
            set
            {
                imagen = "http://storeapp.tostatronic.com/Imagenes/"+value;
            }
        }
        public int Existencia
        { get => existencia;
            set
            {
                SetValue(ref existencia, value);
            }
        }
        
        
        public float Precio_compra { get => precio_compra; set => precio_compra = value; }
        public float Precio_publico { get => precio_publico; set => precio_publico = value; }
        public float Precio_distribuidor { get => precio_distribuidor; set => precio_distribuidor = value; }
        public float Precio_minimo { get => precio_minimo; set => precio_minimo = value; }
    }
}
public enum TipoPrecio
{
    Publico,
    Distribuidor,
    Minimo
}