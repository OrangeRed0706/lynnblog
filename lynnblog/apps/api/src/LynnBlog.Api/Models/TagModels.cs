namespace LynnBlog.Api.Models;

public record TagCreateRequest(string Name, string Slug);

public record TagUpdateRequest(string Name, string Slug);

public record TagResponse(Guid Id, string Name, string Slug, int UsageCount);
