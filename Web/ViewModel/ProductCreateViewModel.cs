namespace Web.ViewModel;

public class ProductCreateViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public List<Guid> SelectedCategoryIds { get; set; } = new();
}
