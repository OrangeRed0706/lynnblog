using Microsoft.EntityFrameworkCore;
using LynnBlog.Core.Entities.Posts;
using LynnBlog.Core.Entities.Users;
using LynnBlog.Core.Entities.Media;

namespace LynnBlog.Infrastructure.Data;

/// <summary>
/// LynnBlog DbContext
/// </summary>
public class LynnBlogDbContext : DbContext
{
    public LynnBlogDbContext(DbContextOptions<LynnBlogDbContext> options)
        : base(options)
    {
    }
    
    // Posts
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<PostTag> PostTags => Set<PostTag>();
    public DbSet<PostCategory> PostCategories => Set<PostCategory>();
    public DbSet<PostEmbedding> PostEmbeddings => Set<PostEmbedding>();
    
    // Users
    public DbSet<User> Users => Set<User>();
    
    // Media
    public DbSet<Media> Media => Set<Media>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure Post
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.PublishedAt);
            entity.HasIndex(e => e.CreatedAt);
            
            entity.Property(e => e.Slug).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.Summary).HasMaxLength(1000);
        });
        
        // Configure Tag
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Slug).HasMaxLength(100);
        });
        
        // Configure Category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Slug).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            
            entity.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // Configure PostTag (composite key)
        modelBuilder.Entity<PostTag>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.TagId });
            
            entity.HasOne(e => e.Post)
                .WithMany(e => e.PostTags)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Tag)
                .WithMany(e => e.PostTags)
                .HasForeignKey(e => e.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Configure PostCategory (composite key)
        modelBuilder.Entity<PostCategory>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.CategoryId });
            
            entity.HasOne(e => e.Post)
                .WithMany(e => e.PostCategories)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Category)
                .WithMany(e => e.PostCategories)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Configure User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });
        
        // Configure Media
        modelBuilder.Entity<Media>(entity =>
        {
            entity.HasIndex(e => e.FileName);
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.OriginalFileName).HasMaxLength(255);
            entity.Property(e => e.MimeType).HasMaxLength(100);
            entity.Property(e => e.Url).HasMaxLength(500);
            entity.Property(e => e.AltText).HasMaxLength(500);
        });
        
        // Configure PostEmbedding
        modelBuilder.Entity<PostEmbedding>(entity =>
        {
            entity.HasIndex(e => e.PostId).IsUnique();
            
            // pgvector uses vector type - stored as bytea in EF
            entity.Property(e => e.Vector)
                .HasColumnType("vector(1536)"); // OpenAI ada-002 default dimension
        });
    }
}
