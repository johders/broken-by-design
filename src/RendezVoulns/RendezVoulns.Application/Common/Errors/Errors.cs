using RendezVoulns.Application.Models.Entities;

namespace RendezVoulns.Application.Common.Errors;
public static class Errors
{
    public const string Duplicate = "Duplicate";
    public const string Invalid = "Invalid";
    public const string Slug = "Slug";
    public const string Name = "Name";
    public const string Title = "Title";
    public const string NotFound = "NotFound";
    public const string CreateFailed = "CreateFailed";
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
        public static readonly Error CreateFailedError = new(CreateFailedErrorCode, "The group could not be created.");
        public static readonly Error UpdateFailedError = new(UpdateFailedErrorCode, "The group could not be updated.");
        public static readonly Error DeleteFailedError = new(DeleteFailedErrorCode, "The group could not be deleted.");
    }

    public static class Users
    {
        private const string DuplicateCode = "Users.Duplicate";
        public static readonly Error DuplicateUsername = new(DuplicateCode, "A user with this username already exists.");
        public static readonly Error DuplicateEmail = new(DuplicateCode, "A user with this email already exists.");
        public static readonly Error DuplicateSlug = new(DuplicateCode, "A user with this slug already exists.");
        public static readonly Error NotFound = new("Users.NotFound", "User not found.");
    }

    public static class Tags
    {
        public static readonly Error Duplicate = new("Tags.Duplicate", "A tag with this name already exists.");
        public static readonly Error NotFound = new("Tags.NotFound", "Tag not found.");
    }

    public static class General
    {
        public static readonly Error None = Error.None;
        public static readonly Error Unexpected = new("General.Unexpected", "An unexpected error occurred.");
    }
}