using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Contracts.V1.Requests;
using RendezVoulns.Contracts.V1.Responses;

namespace RendezVoulns.Api.Mapping;

public static class ContractMapping
{
    public static AppEvent MapToAppEvent(this CreateAppEventRequest request)
    {
        return new AppEvent
        {
            GroupId = request.GroupId,
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            CreatedByUserId = Guid.Parse("00000000-0000-0000-0000-000000000001")
        };
    }

    public static AppEvent MapToAppEvent(this UpdateAppEventRequest request, AppEvent appEvent)
    {
        return new AppEvent
        {
            Id = appEvent.Id,
            GroupId = request.GroupId,
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            CreatedByUserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            CreatedOn = appEvent.CreatedOn,
            UpdatedOn = DateTimeOffset.UtcNow
        };
    }

    public static AppEventResponse MapToResponse(this AppEvent appEvent)
    {
        return new AppEventResponse
        {
            Id = appEvent.Id,
            GroupId = appEvent.GroupId,
            Title = appEvent.Title,
            Slug = appEvent.Slug,
            Description = appEvent.Description,
            Location = appEvent.Location,
            StartTime = appEvent.StartTime,
            EndTime = appEvent.EndTime,
            CreatedByUserId = appEvent.CreatedByUserId,
            CreatedOn = appEvent.CreatedOn
        };
    }

    public static AppEventsResponse MapToResponse(this IEnumerable<AppEvent> appEvents)
    {
        return new AppEventsResponse
        {
            Items = appEvents.Select(e => e.MapToResponse())
        };
    }

    public static Tag MapToTag(this CreateTagRequest request)
    {
        return new Tag
        {
            Name = request.Name,
            ColorHex = request.ColorHex
        };
    }

    public static Tag MapToTag(this UpdateTagRequest request, Tag tag)
    {
        return new Tag
        {
            Id = tag.Id,
            Name = request.Name,
            ColorHex = request.ColorHex,
            CreatedOn = tag.CreatedOn,
            UpdatedOn = DateTimeOffset.UtcNow
        };
    }

    public static TagResponse MapToResponse(this Tag tag)
    {
        return new TagResponse
        {
            Id = tag.Id,
            Name = tag.Name,
            ColorHex = tag.ColorHex,
            CreatedOn = tag.CreatedOn
        };
    }
    public static TagsResponse MapToResponse(this IEnumerable<Tag> tags)
    {
        return new TagsResponse
        {
            Items = tags.Select(t => t.MapToResponse())
        };
    }
}

