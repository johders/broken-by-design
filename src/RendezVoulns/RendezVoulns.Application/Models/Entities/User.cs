using System.Text.RegularExpressions;
using RendezVoulns.Application.Models.Common;

namespace RendezVoulns.Application.Models.Entities;

public partial class User : TrackableEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Username { get; init; }
    public required string Email { get; init; }
    public string? ProfileImageUrl { get; set; }
    public string Slug => GenerateSlug();
    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;

    private string GenerateSlug()
    {
        var slug = Username.Trim().ToLowerInvariant();
        slug = SlugRegex().Replace(slug, "-");
        slug = slug.Trim('-');

        if (string.IsNullOrEmpty(slug))
        {
            return Id.ToString("N")[..8];
        }

        return slug;
    }

    [GeneratedRegex(@"[^a-zA-Z0-9]+", RegexOptions.Compiled)]   
    private static partial Regex SlugRegex();
}