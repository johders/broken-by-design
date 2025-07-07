using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Common.Errors;

public static class Errors
{
    public const string Duplicate = "Duplicate";
    public const string Invalid = "Invalid";
    public const string Slug = "Slug";
    public const string Name = "Name";
    public const string Username = "Username";
    public const string Email = "Email";
    public const string Title = "Title";
    public const string NotFound = "NotFound";
    public const string CreateFailed = "CreateFailed";
    public const string TagEventFailed = "TagEventFailed";
    public const string UpdateFailed = "UpdateFailed";
    public const string DeleteFailed = "DeleteFailed";

    public static class AppEvents
    {
        private const string Domain = nameof(AppEvent);
        public const string DuplicateErrorCode = $"{Domain}.{Duplicate}";
        public const string DuplicateSlugErrorCode = $"{Domain}.{Duplicate}{Slug}";
        public const string DuplicateTitleErrorCode = $"{Domain}.{Duplicate}{Title}";
        public const string InvalidReferenceErrorCode = $"{Domain}.{Invalid}Reference";
        public const string InvalidGroupErrorCode = $"{Domain}.{Invalid}{nameof(Group)}";
        public const string InvalidUserErrorCode = $"{Domain}.{Invalid}{nameof(User)}";
        public const string CreateFailedErrorCode = $"{Domain}.{CreateFailed}";
        public const string UpdateFailedErrorCode = $"{Domain}.{UpdateFailed}";
        public const string DeleteFailedErrorCode = $"{Domain}.{DeleteFailed}";
        public const string NotFoundErrorCode = $"{Domain}.{NotFound}";
        public static readonly Error NotFoundError = new(NotFoundErrorCode, "Event not found.");
        public static readonly Error CreateFailedError = new(CreateFailedErrorCode, "The event could not be created.");
        public static readonly Error DuplicateTitleInGroupError = new(DuplicateTitleErrorCode, "An event with this title already exists for this group.");
        public static readonly Error UpdateFailedError = new(UpdateFailedErrorCode, "The event could not be updated.");
        public static readonly Error DeleteFailedError = new(DeleteFailedErrorCode, "The event could not be deleted.");
    }

    public static class Groups
    {
        private const string Domain = nameof(Group);
        public const string DuplicateNameErrorCode = $"{Domain}.{Duplicate}{Name}";
        public const string CreateFailedErrorCode = $"{Domain}.{CreateFailed}";
        public const string UpdateFailedErrorCode = $"{Domain}.{UpdateFailed}";
        public const string DeleteFailedErrorCode = $"{Domain}.{DeleteFailed}";
        public const string NotFoundErrorCode = $"{Domain}.{NotFound}";
        public static readonly Error NotFoundError = new(NotFoundErrorCode, "Group not found.");
        public static readonly Error DuplicateNameError = new(DuplicateNameErrorCode, "A group with this name already exists.");
        public static readonly Error CreateFailedError = new(CreateFailedErrorCode, "The group could not be created.");
        public static readonly Error UpdateFailedError = new(UpdateFailedErrorCode, "The group could not be updated.");
        public static readonly Error DeleteFailedError = new(DeleteFailedErrorCode, "The group could not be deleted.");
    }

    public static class Tags
    {
        private const string Domain = nameof(Tag);
        public const string DuplicateNameErrorCode = $"{Domain}.{Duplicate}{Name}";
        public const string CreateFailedErrorCode = $"{Domain}.{CreateFailed}";
        public const string TagEventFailedErrorCode = $"{Domain}.{TagEventFailed}";
        public const string UpdateFailedErrorCode = $"{Domain}.{UpdateFailed}";
        public const string DeleteFailedErrorCode = $"{Domain}.{DeleteFailed}";
        public const string NotFoundErrorCode = $"{Domain}.{NotFound}";
        public static readonly Error NotFoundError = new(NotFoundErrorCode, "Tag not found.");
        public static readonly Error DuplicateNameError = new(DuplicateNameErrorCode, "A tag with this name already exists.");
        public static readonly Error CreateFailedError = new(CreateFailedErrorCode, "The tag could not be created.");
        public static readonly Error TagEventFailedError = new(TagEventFailedErrorCode, "The tag could not be added to the event.");
        public static readonly Error UpdateFailedError = new(UpdateFailedErrorCode, "The tag could not be updated.");
        public static readonly Error DeleteFailedError = new(DeleteFailedErrorCode, "The tag could not be deleted.");
    }

    public static class Users
    {
        private const string Domain = nameof(User);
        public const string DuplicateErrorCode = $"{Domain}.{Duplicate}";
        public const string DuplicateSlugErrorCode = $"{Domain}.{Duplicate}{Slug}";
        public const string DuplicateUsernameErrorCode = $"{Domain}.{Duplicate}{Username}";
        public const string DuplicateEmailErrorCode = $"{Domain}.{Duplicate}{Email}";
        public const string CreateFailedErrorCode = $"{Domain}.{CreateFailed}";
        public const string UpdateFailedErrorCode = $"{Domain}.{UpdateFailed}";
        public const string DeleteFailedErrorCode = $"{Domain}.{DeleteFailed}";
        public const string NotFoundErrorCode = $"{Domain}.{NotFound}";
        public static readonly Error NotFoundError = new(NotFoundErrorCode, "User not found.");
        public static readonly Error CreateFailedError = new(CreateFailedErrorCode, "The user could not be created.");
        public static readonly Error UpdateFailedError = new(UpdateFailedErrorCode, "The user could not be updated.");
        public static readonly Error DeleteFailedError = new(DeleteFailedErrorCode, "The user could not be deleted.");
    }

    public static class Rsvps
    {
        private const string Domain = nameof(Rsvp);
        public const string DuplicateNameErrorCode = $"{Domain}.{Duplicate}{Name}";
        public const string CreateFailedErrorCode = $"{Domain}.{CreateFailed}";
        public const string UpdateFailedErrorCode = $"{Domain}.{UpdateFailed}";
        public const string DeleteFailedErrorCode = $"{Domain}.{DeleteFailed}";
        public const string NotFoundErrorCode = $"{Domain}.{NotFound}";
        public static readonly Error NotFoundError = new(NotFoundErrorCode, "Rsvp not found.");
        public static readonly Error CreateFailedError = new(CreateFailedErrorCode, "Rsvp could not be added.");
        public static readonly Error UpdateFailedError = new(UpdateFailedErrorCode, "Rsvp could not be updated.");
        public static readonly Error DeleteFailedError = new(DeleteFailedErrorCode, "Rsvp could not be deleted.");
    }
    
    public static class Memberships
    {
        private const string Domain = nameof(Membership);
        public const string CreateFailedErrorCode = $"{Domain}.{CreateFailed}";
        public const string UpdateFailedErrorCode = $"{Domain}.{UpdateFailed}";
        public const string DeleteFailedErrorCode = $"{Domain}.{DeleteFailed}";
        public const string NotFoundErrorCode = $"{Domain}.{NotFound}";
        public static readonly Error NotFoundError = new(NotFoundErrorCode, "Membership not found.");
        public static readonly Error CreateFailedError = new(CreateFailedErrorCode, "Membership could not be added.");
        public static readonly Error UpdateFailedError = new(UpdateFailedErrorCode, "Membership could not be updated.");
        public static readonly Error DeleteFailedError = new(DeleteFailedErrorCode, "Membership could not be deleted.");
    }    
}