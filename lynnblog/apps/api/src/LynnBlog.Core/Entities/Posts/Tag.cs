namespace LynnBlog.Core.Entities.Posts;

/// <summary>
/// Tag Entity
/// </summary>
public class Tag
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Slug { get; set; } = string.Empty;
    
    /// <summary>
    /// AI suggested count
    /// </summary>
    public int UsageCount { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}

/// <summary>
/// Junction entity for Post and Tag
/// </summary>
public class PostTag
{
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
    
    public Guid TagId { get; set; }
    public Tag Tag { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
