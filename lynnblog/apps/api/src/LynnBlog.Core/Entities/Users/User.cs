using LynnBlog.Core.Entities.Posts;

namespace LynnBlog.Core.Entities.Users;

/// <summary>
/// User Entity for Admin Authentication
/// </summary>
public class User
{
    public Guid Id { get; set; }
    
    public string Email { get; set; } = string.Empty;
    
    public string PasswordHash { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public string? Avatar { get; set; }
    
    public UserRole Role { get; set; } = UserRole.Writer;
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? LastLoginAt { get; set; }
    
    // Navigation properties
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}

public enum UserRole
{
    Admin = 0,
    Writer = 1,
    Editor = 2
}
