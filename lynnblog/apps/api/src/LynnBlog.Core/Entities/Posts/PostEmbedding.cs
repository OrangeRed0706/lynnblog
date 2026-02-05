using Microsoft.EntityFrameworkCore;

namespace LynnBlog.Core.Entities.Posts;

/// <summary>
/// Post Embedding Entity for AI Semantic Search (pgvector)
/// </summary>
public class PostEmbedding
{
    public Guid Id { get; set; }
    
    public Guid PostId { get; set; }
    
    public Post Post { get; set; } = null!;
    
    /// <summary>
    /// Embedding vector stored as bytea in PostgreSQL via pgvector
    /// </summary>
    public float[]? Vector { get; set; }
    
    /// <summary>
    /// Model used to generate the embedding (e.g., "text-embedding-3-small")
    /// </summary>
    public string Model { get; set; } = string.Empty;
    
    /// <summary>
    /// When the embedding was generated
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Content text used for embedding
    /// </summary>
    public string ContentHash { get; set; } = string.Empty;
}
