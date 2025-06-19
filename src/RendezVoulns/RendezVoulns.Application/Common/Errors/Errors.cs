namespace RendezVoulns.Application.Common.Errors;
public static class Errors
{
    public static class AppEvents
    {
        public static readonly Error Duplicate = new("AppEvents.Duplicate", "An event with this name already exists.");
        public static readonly Error NotFound = new("AppEvents.NotFound", "Event not found.");
    }

    public static class Users
    {
        private const string DuplicateCode = "Users.Duplicate";
        public static readonly Error DuplicateUsername = new(DuplicateCode, "A user with this username already exists.");
        public static readonly Error DuplicateEmail = new(DuplicateCode, "A user with this email already exists.");
        public static readonly Error DuplicateSlug = new(DuplicateCode, "A user with this slug already exists.");
        public static readonly Error NotFound = new("Users.NotFound", "User not found.");
    }

    public static class Groups
    {
        public static readonly Error Duplicate = new("Groups.Duplicate", "A group with this name already exists.");
        public static readonly Error NotFound = new("Groups.NotFound", "Group not found.");
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