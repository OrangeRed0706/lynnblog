namespace LynnBlog.Core.Entities.Posts;

/// <summary>
/// Category Entity
/// </summary>
public class Category
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Slug { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public Guid? ParentId { get; set; }
    
    public Category? Parent { get; set; }
    
    public int SortOrder { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public ICollection<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
    public ICollection<Category> Children { get; set; } = new List<Category>();
}

/// <summary>
/// Junction entity for Post and Category
/// </summary>
public class PostCategory
{
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
    
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
