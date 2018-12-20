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
        //ObservableCollection<ProductModel> listOfProducts;
        public ObservableCollection<ProductModel> listOfProductsAction { get; private set; }
        List<ProductModel> productsAddesToShoppingCar = new List<ProductModel>();

        public ProductViewModel(PageService pageService)
        {
            this.pageService = pageService;
            UpdateData();
            this.ReduceQuantityCommand = new Command<ProductModel>(producto => ReduceQuantity(producto));
            this.IncrementQuantityCommand = new Command<ProductModel>(producto => IncrementQuantity(producto));
            this.AddToCartCommand = new Command<ProductModel>(producto=> AddToCart(producto));
            this.SeeMyCartCommand = new Command(this.SeeMyCart);
            this.PriceKindChangedCommand = new Command<int>(kindOfPrice => ChangePrice(kindOfPrice));
        }

        private void ChangePrice(int kindOfPrice)
        {
            foreach (var product in listOfProductsAction)
                product.TipoPrecio = kindOfPrice;
        }

        private async void SeeMyCart()
        {
            string message = $"Numero de productos: {productsAddesToShoppingCar.Count}";
            string detailMessage = "";
            foreach (ProductModel p in productsAddesToShoppingCar)
                detailMessage += @"\nNombre: "+p.Nombre+"--> "+p.Agregados;
            await pageService.DisplayAlert("elementos agregados", message+detailMessage, "ok");
        }

        private void AddToCart(ProductModel product)
        {
            if (product.Agregados > 0)
            {
                int index;
                if (productsAddesToShoppingCar.Contains(product))
                {
                    index = productsAddesToShoppingCar.IndexOf(product);
                    productsAddesToShoppingCar[index].Agregados += product.Agregados;
                }
                else
                {
                    index = listOfProductsAction.IndexOf(product);
                    ProductModel newProdct = new ProductModel()
                    {
                        Nombre = product.Nombre,
                        Agregados = product.Agregados,
                        Codigo = product.Codigo,
                        PrecioMostrar = product.PrecioMostrar,
                        Imagen = product.Imagen
                    };
                    productsAddesToShoppingCar.Add(newProdct);
                }
                listOfProductsAction[index].Existencia -= product.Agregados;
            }
            //await pageService.DisplayAlert("Not", productsAddesToShoppingCar[productsAddesToShoppingCar.Count - 1].PrecioMostrar.ToString("$0.00"),"ok");
        }

        private void IncrementQuantity(ProductModel product)
        {
            int index = listOfProductsAction.IndexOf(product);
            if(index>-1)
            {
                listOfProductsAction[index].Agregados++;
            }
        }

        private void ReduceQuantity(ProductModel product)
        {
            int index = listOfProductsAction.IndexOf(product);
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
                var productList = JsonConvert.DeserializeObject<List<ProductModel>>(result);
                listOfProductsAction = new ObservableCollection<ProductModel>(productList);
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
    }
}
