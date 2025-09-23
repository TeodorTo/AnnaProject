using Infrastructure.Entities;
namespace Web.ViewModel;


public class ProductsViewModel
{
    public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
    public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
    public Guid? SelectedCategoryId { get; set; }
}
