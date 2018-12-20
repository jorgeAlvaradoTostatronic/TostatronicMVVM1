using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TostatronicMVVM.Common
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task DisplayAlert(string titulo, string mensaje, string ok);
        Task<bool> DisplayAlert(string titulo, string mensaje, string ok, string cancel);
    }
}
