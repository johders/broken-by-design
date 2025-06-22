namespace RendezVoulns.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class AppEvents
    {
        private const string Base = $"{ApiBase}/events";
        public const string Create = Base;
        public const string Get = $"{Base}/{{idOrSlug}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string Rsvp = $"{Base}/{{id:guid}}/rsvps";
        public const string DeleteRsvp = $"{Base}/{{id:guid}}/rsvps";
        public const string Tag = $"{Base}/{{id:guid}}/tags";
        public const string DeleteTag = $"{Base}/{{id:guid}}/tags";
    }

    public static class Tags
    {
        private const string Base = $"{ApiBase}/tags";
        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class Groups
    {
        private const string Base = $"{ApiBase}/groups";
        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class Users
    {
        private const string Base = $"{ApiBase}/users";
        public const string Create = Base;
        public const string Get = $"{Base}/{{idOrSlug}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    public static class Rsvps
    {
        private const string Base = $"{ApiBase}/rsvps";
        public const string GetUserRsvps = $"{ApiBase}/rsvps/me";
    }
}