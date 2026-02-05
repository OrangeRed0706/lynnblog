namespace LynnBlog.Core.Entities.Posts;

/// <summary>
/// Blog Post Entity
/// </summary>
public class Post
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// URL-friendly slug
    /// </summary>
    public string Slug { get; set; } = string.Empty;
    
    /// <summary>
    /// Post title
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Markdown content
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// SEO description (AI generated or manual)
    /// </summary>
    public string? Summary { get; set; }
    
    /// <summary>
    /// Featured image URL
    /// </summary>
    public string? FeaturedImage { get; set; }
    
    /// <summary>
    /// Publication status
    /// </summary>
    public PostStatus Status { get; set; } = PostStatus.Draft;
    
    /// <summary>
    /// Publication date
    /// </summary>
    public DateTime? PublishedAt { get; set; }
    
    /// <summary>
    /// Creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Last update timestamp
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Reading time in minutes (AI calculated)
    /// </summary>
    public int? ReadingTime { get; set; }
    
    /// <summary>
    /// View count
    /// </summary>
    public int ViewCount { get; set; } = 0;
    
    // Navigation properties
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    public ICollection<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
    public PostEmbedding? Embedding { get; set; }
}

public enum PostStatus
{
    Draft = 0,
    Published = 1,
    Archived = 2
}
