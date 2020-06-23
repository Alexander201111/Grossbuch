using Grossbuch.Models;

namespace Grossbuch.ViewModels
{
    public class CategoryDetailVM : BaseViewModel
    {
        public Category Category { get; set; }
        public CategoryDetailVM(Category category = null)
        {
            Title = category?.Title;
            Category = category;
        }
    }
}
