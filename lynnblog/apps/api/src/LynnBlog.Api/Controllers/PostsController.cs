using LynnBlog.Api.Models;
using LynnBlog.Core.Entities.Posts;
using LynnBlog.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LynnBlog.Api.Controllers;

[ApiController]
[Route("api/v1/posts")]
public class PostsController : ControllerBase
{
    private readonly LynnBlogDbContext _db;

    public PostsController(LynnBlogDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostResponse>>> GetPosts()
    {
        var posts = await _db.Posts
            .OrderByDescending(p => p.PublishedAt ?? p.CreatedAt)
            .Select(p => new PostResponse(
                p.Id,
                p.Title,
                p.Slug,
                p.Content,
                p.Summary,
                p.FeaturedImage,
                p.Status,
                p.PublishedAt,
                p.CreatedAt,
                p.UpdatedAt
            ))
            .ToListAsync();

        return Ok(posts);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<PostResponse>> GetPost(string slug)
    {
        var post = await _db.Posts.FirstOrDefaultAsync(p => p.Slug == slug);
        if (post == null) return NotFound();

        return Ok(new PostResponse(
            post.Id,
            post.Title,
            post.Slug,
            post.Content,
            post.Summary,
            post.FeaturedImage,
            post.Status,
            post.PublishedAt,
            post.CreatedAt,
            post.UpdatedAt
        ));
    }

    [HttpPost]
    public async Task<ActionResult<PostResponse>> CreatePost([FromBody] PostCreateRequest request)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Slug = request.Slug,
            Content = request.Content,
            Summary = request.Summary,
            FeaturedImage = request.FeaturedImage,
            Status = request.Status,
            PublishedAt = request.Status == PostStatus.Published ? DateTime.UtcNow : null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Posts.Add(post);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPost), new { slug = post.Slug }, new PostResponse(
            post.Id,
            post.Title,
            post.Slug,
            post.Content,
            post.Summary,
            post.FeaturedImage,
            post.Status,
            post.PublishedAt,
            post.CreatedAt,
            post.UpdatedAt
        ));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PostResponse>> UpdatePost(Guid id, [FromBody] PostUpdateRequest request)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post == null) return NotFound();

        post.Title = request.Title;
        post.Slug = request.Slug;
        post.Content = request.Content;
        post.Summary = request.Summary;
        post.FeaturedImage = request.FeaturedImage;
        post.Status = request.Status;
        post.PublishedAt = request.Status == PostStatus.Published ? post.PublishedAt ?? DateTime.UtcNow : null;
        post.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return Ok(new PostResponse(
            post.Id,
            post.Title,
            post.Slug,
            post.Content,
            post.Summary,
            post.FeaturedImage,
            post.Status,
            post.PublishedAt,
            post.CreatedAt,
            post.UpdatedAt
        ));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post == null) return NotFound();

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
