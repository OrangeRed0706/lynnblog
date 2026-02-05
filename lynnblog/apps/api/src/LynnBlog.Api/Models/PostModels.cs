using LynnBlog.Core.Entities.Posts;

namespace LynnBlog.Api.Models;

public record PostCreateRequest(
    string Title,
    string Slug,
    string Content,
    string? Summary,
    string? FeaturedImage,
    PostStatus Status
);

public record PostUpdateRequest(
    string Title,
    string Slug,
    string Content,
    string? Summary,
    string? FeaturedImage,
    PostStatus Status
);

public record PostResponse(
    Guid Id,
    string Title,
    string Slug,
    string Content,
    string? Summary,
    string? FeaturedImage,
    PostStatus Status,
    DateTime? PublishedAt,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
