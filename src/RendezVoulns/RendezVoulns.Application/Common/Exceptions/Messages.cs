namespace RendezVoulns.Application.Common.Exceptions;

public static class Messages
{
    public static class AppEvents
    {
        public const string Duplicate = "An event with these details already exists";
        public const string DuplicateSlug = "An event with this slug already exists";
        public const string DuplicateTitle = "An event with this title already exists";
        public const string InvalidGroup = "Invalid group reference";
        public const string InvalidUser = "Invalid user reference";
        public const string InvalidReference = "Invalid reference";
    }
    
    public static class Groups
    {
        public const string DuplicateName ="A group with this name already exists";
    }
}