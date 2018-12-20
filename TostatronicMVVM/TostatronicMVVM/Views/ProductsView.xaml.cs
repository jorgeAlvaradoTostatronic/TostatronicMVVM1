using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TostatronicMVVM.Common;
using TostatronicMVVM.Models;
using TostatronicMVVM.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TostatronicMVVM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductsView : ContentPage
	{
		public ProductsView ()
		{
			InitializeComponent ();
            BindingContext = new ProductViewModel(new PageService());
		}

        private void ButtonAddToCar_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var selectedProduct = btn.CommandParameter as ProductModel;
            (BindingContext as ProductViewModel).AddToCartCommand.Execute(selectedProduct);
        }

        private void ButtonLess_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var selectedProduct = btn.CommandParameter as ProductModel;
            (BindingContext as ProductViewModel).ReduceQuantityCommand.Execute(selectedProduct);
        }

        private void ButtonMore_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var selectedProduct = btn.CommandParameter as ProductModel;
            (BindingContext as ProductViewModel).IncrementQuantityCommand.Execute(selectedProduct);
        }

        private void ChangePriceKind_IndexChanged(object sender, EventArgs e)
        {
            (BindingContext as ProductViewModel).PriceKindChangedCommand.Execute(((Picker)sender).SelectedIndex);
        }

        private void EntryAgregados_Unfocused(object sender, FocusEventArgs e)
        {
            //(BindingContext as ProductViewModel).EntryValueChangedCommand.Execute(((Entry)sender).Text);
        }
    }
}