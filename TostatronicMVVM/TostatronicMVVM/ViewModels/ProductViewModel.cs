using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TostatronicMVVM.Common;
using TostatronicMVVM.Models;
using TostatronicMVVM.Services;
using Xamarin.Forms;

namespace TostatronicMVVM.ViewModels
{
    class ProductViewModel : BaseNotifyPropertyChanged
    {
        private IPageService pageService;
        public ICommand ReduceQuantityCommand { get; set; }
        public ICommand IncrementQuantityCommand { get;  set; }
        public ICommand AddToCartCommand { get; set; }
        public ICommand SeeMyCartCommand { get; set; }
        public ICommand PriceKindChangedCommand { get; set; }
        public ICommand EntryValueChangedCommand { get; set; }
        //ObservableCollection<ProductModel> listOfProducts;
        public ObservableCollection<ShowProductModel> listOfProductsAction { get; private set; }
        List<SaleProduct> productsAddesToShoppingCar = new List<SaleProduct>();

        public ProductViewModel(PageService pageService)
        {
            this.pageService = pageService;
            UpdateData();
            this.ReduceQuantityCommand = new Command<ShowProductModel>(producto => ReduceQuantity(producto));
            this.IncrementQuantityCommand = new Command<ShowProductModel>(producto => IncrementQuantity(producto));
            this.AddToCartCommand = new Command<ShowProductModel>(producto=> AddToCart(producto));
            this.SeeMyCartCommand = new Command(this.SeeMyCart);
            this.PriceKindChangedCommand = new Command<int>(kindOfPrice => ChangePrice(kindOfPrice));
            //this.EntryValueChangedCommand = new Command<string>(newQuantity => ChangeQuantity(newQuantity));
        }

        //private void ChangeQuantity(string newQuantity)
        //{
        //    if(!string.IsNullOrEmpty(newQuantity))
        //    {
        //        float newQuantityA = float.Parse(newQuantity);
        //        int finalQuantity = (int)Math.Round(newQuantityA, 0, MidpointRounding.AwayFromZero);

        //    }
        //}

        private void ChangePrice(int kindOfPrice)
        {
            foreach (var product in listOfProductsAction)
            {
                product.TipoPrecio = kindOfPrice;
                foreach(var productOfSale in productsAddesToShoppingCar)
                {
                    if (productOfSale.Codigo.Equals(product.Codigo))
                    {
                        productOfSale.PriceAtSale = product.PrecioMostrar;
                        break;
                    }   
                }
            }
                
        }

        private async void SeeMyCart()
        {
            string message = $"Numero de productos: {productsAddesToShoppingCar.Count}";
            string detailMessage = "";
            foreach (SaleProduct p in productsAddesToShoppingCar)
                detailMessage += $"\nNombre: "+p.Nombre+"-->Cantidad: "+p.Quantiy+" -->Precio: "+p.PriceAtSale;
            await pageService.DisplayAlert("elementos agregados", message+detailMessage, "ok");
        }

        private void AddToCart(ShowProductModel product)
        {
            if (product.Agregados > 0)
            {
                int index = GetIndexOfProduct(product.Codigo);
                var obj = productsAddesToShoppingCar.Where(x => x.Codigo.Equals(product.Codigo)).FirstOrDefault();
                if (obj!=null)
                {
                    int index2 = GetIndexOfProductOfSale(obj.Codigo);
                    productsAddesToShoppingCar[index2].Quantiy += product.Agregados;
                }
                else
                {
                    float priceAtSale = product.PrecioMostrar;
                    int qty = product.Agregados;
                    SaleProduct newProdct = new SaleProduct(product.Codigo, product.Nombre, product.Imagen, product.Existencia,
                        product.Precio_compra, product.Precio_publico, product.Precio_distribuidor, product.Precio_minimo, priceAtSale, qty);
                    productsAddesToShoppingCar.Add(newProdct);
                }
                listOfProductsAction[index].Existencia -= product.Agregados;
            }
            //await pageService.DisplayAlert("Not", productsAddesToShoppingCar[productsAddesToShoppingCar.Count - 1].PrecioMostrar.ToString("$0.00"),"ok");
        }

        private void IncrementQuantity(ShowProductModel product)
        {
            int index = GetIndexOfProduct(product.Codigo);
            if (index>-1)
            {
                listOfProductsAction[index].Agregados++;
            }
        }

        private void ReduceQuantity(ShowProductModel product)
        {
            int index = GetIndexOfProduct(product.Codigo);
            if (index > -1)
            {
                listOfProductsAction[index].Agregados--;
            }
        }

        private void UpdateData()
        {
            string result = Service.GetData("http://storeapp.tostatronic.com/webService.php");
            if (!string.IsNullOrEmpty(result))
            {
                var productList = JsonConvert.DeserializeObject<List<ShowProductModel>>(result);
                listOfProductsAction = new ObservableCollection<ShowProductModel>(productList);
                //await pageService.DisplayAlert("Acyualizacion", "Productos actualizados", "ok");
            }
        }

        private int GetIndexOfProduct(string code)
        {
            for (int i = 0; i < listOfProductsAction.Count; i++)
                if (listOfProductsAction[i].Codigo.Equals(code))
                    return i;
            return -1;
        }
        private int GetIndexOfProductOfSale(string code)
        {
            for (int i = 0; i < listOfProductsAction.Count; i++)
                if (productsAddesToShoppingCar[i].Codigo.Equals(code))
                    return i;
            return -1;
        }
    }
}
