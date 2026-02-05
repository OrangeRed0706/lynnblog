namespace LynnBlog.Core.Entities.Media;

/// <summary>
/// Media Entity for uploaded files and images
/// </summary>
public class Media
{
    public Guid Id { get; set; }
    
    public string FileName { get; set; } = string.Empty;
    
    public string OriginalFileName { get; set; } = string.Empty;
    
    public string MimeType { get; set; } = string.Empty;
    
    public long FileSize { get; set; }
    
    /// <summary>
    /// Public URL to access the file
    /// </summary>
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// Storage path (relative to storage root)
    /// </summary>
    public string StoragePath { get; set; } = string.Empty;
    
    /// <summary>
    /// Alt text for images (AI generated or manual)
    /// </summary>
    public string? AltText { get; set; }
    
    /// <summary>
    /// Caption for the media
    /// </summary>
    public string? Caption { get; set; }
    
    /// <summary>
    /// User who uploaded the file
    /// </summary>
    public Guid? UploadedById { get; set; }
    
    public Core.Entities.Users.User? UploadedBy { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
