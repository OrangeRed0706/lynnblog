using LynnBlog.Api.Models;
using LynnBlog.Core.Entities.Posts;
using LynnBlog.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LynnBlog.Api.Controllers;

[ApiController]
[Route("api/v1/tags")]
public class TagsController : ControllerBase
{
    private readonly LynnBlogDbContext _db;

    public TagsController(LynnBlogDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagResponse>>> GetTags()
    {
        var tags = await _db.Tags
            .OrderBy(t => t.Name)
            .Select(t => new TagResponse(t.Id, t.Name, t.Slug, t.UsageCount))
            .ToListAsync();

        return Ok(tags);
    }

    [HttpPost]
    public async Task<ActionResult<TagResponse>> CreateTag([FromBody] TagCreateRequest request)
    {
        var tag = new Tag
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Slug = request.Slug,
            CreatedAt = DateTime.UtcNow
        };

        _db.Tags.Add(tag);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTags), new TagResponse(tag.Id, tag.Name, tag.Slug, tag.UsageCount));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TagResponse>> UpdateTag(Guid id, [FromBody] TagUpdateRequest request)
    {
        var tag = await _db.Tags.FindAsync(id);
        if (tag == null) return NotFound();

        tag.Name = request.Name;
        tag.Slug = request.Slug;

        await _db.SaveChangesAsync();

        return Ok(new TagResponse(tag.Id, tag.Name, tag.Slug, tag.UsageCount));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTag(Guid id)
    {
        var tag = await _db.Tags.FindAsync(id);
        if (tag == null) return NotFound();

        _db.Tags.Remove(tag);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
