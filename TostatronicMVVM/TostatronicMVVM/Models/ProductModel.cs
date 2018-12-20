using System;
using System.Collections.Generic;
using System.Text;
using TostatronicMVVM.Common;

namespace TostatronicMVVM.Models
{
    public class ProductModel : BaseNotifyPropertyChanged
    {
        string codigo, nombre, imagen;
        int existencia, agregados, disponibles;
        int tipoPrecio;
        float precio_compra, precio_publico, precio_distribuidor, precio_minimo, precioMostrar;

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
        public float PrecioMostrar
        {
            get
            {
                switch (TipoPrecio)
                {
                    case 0:
                        return Precio_publico;
                    case 1:
                        return Precio_distribuidor;
                    case 2:
                        return Precio_minimo;
                    default:
                        return Precio_publico;
                }
            }

            set
            {
                precioMostrar = value;
            }
        }
        public int Existencia
        {
            get => existencia;
            set
            {
                SetValue(ref existencia, value);
                Agregados = 0;
            }
        }
        public int Agregados 
        {
            get
            {
                return agregados;
            }
            set
            {
                if (value > 0)
                {
                    if (value > Existencia)
                        value = Existencia;
                }
                else
                    value = 0;
                SetValue(ref agregados, value);

            }
        }
        public int Disponibles
        {
            get
            {
                return disponibles;
            }
            set
            {
                disponibles = existencia - agregados;
            }
        }
        public int TipoPrecio
        {
            get => tipoPrecio;
            set
            {
                SetValue(ref tipoPrecio, value);
                OnPropertyChanged(nameof(PrecioMostrar));
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