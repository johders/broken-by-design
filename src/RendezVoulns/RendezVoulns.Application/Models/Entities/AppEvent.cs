using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace RendezVoulns.Application.Models.Entities;

public partial class AppEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid GroupId { get; init; }
    public required string Title { get; set; }
    public string Slug => GenerateSlug();
    public required string Description { get; set; }
    public required string Location { get; set; }
    public required DateTimeOffset StartTime { get; set; }
    public required DateTimeOffset EndTime { get; set; }
    public required Guid CreatedByUserId { get; init; }
    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;

    private string GenerateSlug()
    {
        var normalizedTitle = string.Concat(Title.Normalize(NormalizationForm.FormD)
                                                    .Where(c => CharUnicodeInfo
                                                    .GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

        var titlePart = SlugRegex().Replace(normalizedTitle, "-").ToLowerInvariant().Trim('-');
        var datePart = StartTime.ToString("yyyy-MM-dd");
        var groupIdPart = GroupId.ToString("N")[^3..];

        return $"{titlePart}-{datePart}-{groupIdPart}";
    }

    [GeneratedRegex(@"[^a-zA-Z0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase)]   
    private static partial Regex SlugRegex();
}