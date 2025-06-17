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

    public static Group MapToGroup(this CreateGroupRequest request)
    {
        return new Group
        {
            Name = request.Name,
            Description = request.Description
        };
    }

    public static Group MapToGroup(this UpdateGroupRequest request, Group group)
    {
        return new Group
        {
            Id = group.Id,
            Name = request.Name,
            Description = request.Description,
            CreatedOn = group.CreatedOn,
            UpdatedOn = DateTimeOffset.UtcNow
        };
    }

    public static GroupResponse MapToResponse(this Group group)
    {
        return new GroupResponse
        {
            Id = group.Id,
            Name = group.Name,
            Description = group.Description,
            CreatedOn = group.CreatedOn
        };
    }
    public static GroupsResponse MapToResponse(this IEnumerable<Group> groups)
    {
        return new GroupsResponse
        {
            Items = groups.Select(g => g.MapToResponse())
        };
    }
}

