namespace RendezVoulns.Application.Common.Exceptions;

public static class Messages
{
    public static class AppEvents
    {
        public const string Duplicate = "An event with these details already exists";
        public const string DuplicateSlug = "An event with this title already exists for this group";
        public const string InvalidGroup = "Invalid group reference";
        public const string InvalidUser = "Invalid user reference";
        public const string InvalidReference = "Invalid reference";
    }

    public static class Groups
    {
        public const string DuplicateName = "A group with this name already exists";
    }

    public static class Tags
    {
        public const string DuplicateName = "A tag with this name already exists";
    }

    public static class Users
    {
        public const string Duplicate = "A user with these details already exists.";
        public const string DuplicateSlug = "A user with this slug already exists";
        public const string DuplicateUsername = "A user with this username already exists";
        public const string DuplicateEmail = "A user with this email already exists";
    }
}