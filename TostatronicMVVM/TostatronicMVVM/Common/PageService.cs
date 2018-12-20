using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TostatronicMVVM.Common
{
    class PageService : IPageService
    {
        public async Task DisplayAlert(string titulo, string mensaje, string ok)
        {
            await Application.Current.MainPage.DisplayAlert(titulo, mensaje, ok);
        }

        public async Task<bool> DisplayAlert(string titulo, string mensaje, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(titulo, mensaje, ok, cancel);
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
