using System;
using System.Collections.Generic;
using System.Text;
using TostatronicMVVM.Common;

namespace TostatronicMVVM.Models
{
    public class ShowProductModel : ProductModel
    {
        int agregados, disponibles;
        float precioMostrar;
        int tipoPrecio;
        bool isEnabled;
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
        public new int Existencia
        {
            get => base.Existencia;
            set
            {
                base.Existencia = value;
                Agregados = 0;
                OnPropertyChanged(nameof(IsEnabled));
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
                disponibles = base.Existencia - agregados;
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

        public bool IsEnabled
        {
            get
            {
                return (base.Existencia > 0) ? true : false;
            }
            
        }
    }
}
