
namespace App.API.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Posts
        {
            public const string GetAll = Base + "/posts";
            public const string Create = Base + "/posts";
            public const string Get = Base + "/posts/{postId}";
            public const string Update = Base + "/posts/{postId}";

        }

        public static class Productos
        {
            public const string GetAll = Base + "/productos";
            public const string Create = Base + "/productos";
            public const string Get = Base + "/productos/{productoId}";
            public const string Update = Base + "/productos/{productoId}";
            public const string Delete = Base + "/productos/{productoId}";
        }

        public static class Identity
        {
            public const string Register = Base + "/identity/register";
            public const string Login = Base + "/identity/login";
        }

    }
}
