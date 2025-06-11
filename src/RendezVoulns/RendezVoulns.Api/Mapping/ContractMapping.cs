using Microsoft.AspNetCore.CookiePolicy;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Repositories;
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

    public static AppEvent MapToAppEvent(this UpdateAppEventRequest request, Guid id)
    {
        return new AppEvent
        {
            Id = id,
            GroupId = request.GroupId,
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            CreatedByUserId = Guid.Parse("00000000-0000-0000-0000-000000000001")
        };
    }

    public static AppEventResponse MapToResponse(this AppEvent appEvent)
    {
        return new AppEventResponse
        {
            Id = appEvent.Id,
            GroupId = appEvent.GroupId,
            Title = appEvent.Title,
            Description = appEvent.Description,
            Location = appEvent.Location,
            StartTime = appEvent.StartTime,
            EndTime = appEvent.EndTime,
            CreatedByUserId = appEvent.CreatedByUserId,
            CreatedOn = appEvent.CreatedOn
        };
    }
}

