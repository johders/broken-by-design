using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Contracts.V1.AppEvent.Requests;
using RendezVoulns.Contracts.V1.AppEvent.Responses;
using RendezVoulns.Contracts.V1.Tag.Requests;
using RendezVoulns.Contracts.V1.Tag.Responses;
using RendezVoulns.Contracts.V1.Group.Requests;
using RendezVoulns.Contracts.V1.Group.Responses;
using RendezVoulns.Contracts.V1.User.Requests;
using RendezVoulns.Contracts.V1.User.Responses;
using RendezVoulns.Contracts.V1.Rsvp.Responses;
using RendezVoulns.Contracts.V1.Rsvp.Requests;
using RendezVoulns.Application.Models.Enums;
using RendezVoulns.Contracts.V1.Membership.Responses;
using RendezVoulns.Contracts.V1.Membership.Requests;

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
            CreatedOn = appEvent.CreatedOn,
            Tags = appEvent.Tags.Select(t => t.MapToResponse()),
            Rsvps = appEvent.Rsvps.Select(r => r.MapToResponse())
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

    public static User MapToUser(this CreateUserRequest request)
    {
        return new User
        {
            Username = request.Username,
            Email = request.Email,
            ProfileImageUrl = request.ProfileImageUrl
        };
    }

    public static User MapToUser(this UpdateUserRequest request, User user)
    {
        return new User
        {
            Id = user.Id,
            Username = request.Username,
            Email = request.Email,
            ProfileImageUrl = request.ProfileImageUrl,
            CreatedOn = user.CreatedOn,
            UpdatedOn = DateTimeOffset.UtcNow
        };
    }

    public static UserResponse MapToResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            ProfileImageUrl = user.ProfileImageUrl,
            CreatedOn = user.CreatedOn,
        };
    }
    public static UsersResponse MapToResponse(this IEnumerable<User> users)
    {
        return new UsersResponse
        {
            Items = users.Select(u => u.MapToResponse())
        };
    }

    public static Rsvp MapToRsvp(this Guid eventId, Guid userId, string status)
    {
        var parsed = Enum.TryParse<RsvpStatus>(status, ignoreCase: true, out var parsedStatus);

        return new Rsvp
        {
            UserId = userId,
            EventId = eventId,
            Status = parsed ? parsedStatus : RsvpStatus.Maybe
        };
    }

    public static RsvpResponse MapToResponse(this Rsvp rsvp)
    {
        return new RsvpResponse
        {
            UserId = rsvp.UserId,
            EventId = rsvp.EventId,
            Status = rsvp.Status.ToString(),
            RespondedOn = rsvp.RespondedOn
        };
    }

    public static RsvpsResponse MapToResponse(this IEnumerable<Rsvp> rsvps)
    {
        return new RsvpsResponse
        {
            Items = rsvps.Select(r => r.MapToResponse())
        };
    }

    public static Membership MapToMemberShip(this UpdateMembershipRequest request, Guid groupId)
    {
        var parsed = Enum.TryParse<GroupRole>(request.Role, ignoreCase: true, out var parsedRole);

        return new Membership
        {
            UserId = request.UserId,
            GroupId = groupId,
            Role = parsed ? parsedRole : GroupRole.Member
        };
    }

    public static MembershipResponse MapToResponse(this Membership membership)
    {
        return new MembershipResponse
        {
            UserId = membership.UserId,
            GroupId = membership.GroupId,
            Role = membership.Role.ToString(),
            JoinedOn = membership.JoinedOn
        };
    }
    
        public static MembershipsResponse MapToResponse(this IEnumerable<Membership> memberships)
    {
        return new MembershipsResponse
        {
            Items = memberships.Select(m => m.MapToResponse())
        };
    }
}

