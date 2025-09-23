namespace Infrastructure.Entities;

public class Category
{
    public Guid Id                          { get; set; } = Guid.NewGuid();
    public string Name                      { get; set; } = null!;
    public string Slug                      { get; set; } = null!;
    
    public Guid? ParentCategoryId           { get; set; }

    public Category? ParentCategory         { get; set; }

    public ICollection<Category>? Children  { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
