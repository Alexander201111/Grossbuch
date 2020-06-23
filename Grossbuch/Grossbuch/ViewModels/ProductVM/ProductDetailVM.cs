using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Grossbuch.Models;
using Grossbuch.Repositories;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Grossbuch.ViewModels
{
    public class ProductDetailVM : BaseViewModel
    {
        public Product Product { get; set; }
        public ProductDetailVM(Product product = null)
        {
            Title = product?.Title;
            Product = product;
        }
    }
}
