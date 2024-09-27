namespace GMS.Shared.Constants
{
    public class Router
    {
        public const string Root = "api";

        public class Account
        {
            public const string Prefix = Root + "/account";

            public const string Login = Prefix + "/login";
            public const string ChangePassword = Prefix + "/change-password";
            public const string RevokeRefreshToken = Prefix + "/revoke-refresh-token";
            public const string RequestJwtToken = Prefix + "/request-jwt-token";
        }

        public class Users
        {
            public const string Prefix = Root + "/users";

            public const string Create = Prefix;
            public const string Update = Prefix + "/{id}";
            public const string Delete = Prefix + "/{id}";
            public const string GetById = Prefix + "/{id}";
            public const string GetAll = Prefix;
        }
        
        public class Brands
        {
            public const string Prefix = Root + "/brands";

            public const string Create = Prefix;
            public const string Update = Prefix + "/{id}";
            public const string Delete = Prefix + "/{id}";
            public const string GetById = Prefix + "/{id}";
            public const string GetAll = Prefix;
        }     
        
        public class Owners
        {
            public const string Prefix = Root + "/owners";

            public const string Create = Prefix;
            public const string Update = Prefix + "/{id}";
            public const string Delete = Prefix + "/{id}";
            public const string GetById = Prefix + "/{id}";
            public const string GetAll = Prefix;
        }
        
        public class Vehicles
        {
            public const string Prefix = Root + "/vehicles";

            public const string Create = Prefix;
            public const string Update = Prefix + "/{id}";
            public const string UpdateStatus = Prefix + "/{id}/status/{status}";
            public const string Delete = Prefix + "/{id}";
            public const string GetById = Prefix + "/{id}";
            public const string GetAll = Prefix;
        }

        public class Errors
        {
            public const string Prefix = Root + "/errors";

            public const string GetErrorResponse = Prefix + "/{status}";
        }
    }
}
