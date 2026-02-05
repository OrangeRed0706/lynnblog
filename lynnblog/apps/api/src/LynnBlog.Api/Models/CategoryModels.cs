namespace LynnBlog.Api.Models;

public record CategoryCreateRequest(string Name, string Slug, string? Description, Guid? ParentId);

public record CategoryUpdateRequest(string Name, string Slug, string? Description, Guid? ParentId);

public record CategoryResponse(Guid Id, string Name, string Slug, string? Description, Guid? ParentId);
