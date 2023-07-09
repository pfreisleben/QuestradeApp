namespace Client.Infrastructure.Routes
{
    public static class UserEndpoints
    {
        public static string GetAll = "api/identity/user";
        public static string UpdateUser = "api/identity/user/updateUserAsync";
        public static string UpdateUserStatus = "api/identity/user/updateUserStatusAsync";

        public static string Get(string userId)
        {
            return $"api/identity/user/{userId}";
        }

        public static string GetUserRoles(string userId)
        {
            return $"api/identity/user/roles/{userId}";
        }

    }
}