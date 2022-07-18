using System.Security.Claims;
using TimeTrakAPI.Entities;

namespace TimeTrakAPI.Helpers
{
    public static class UserContext
    {
        private static IHttpContextAccessor _hca;

        public static void Configure(IHttpContextAccessor hca) { _hca = hca; }

        private static List<Claim> Claims() { return new List<Claim>();  }

        public static User? UserData => (User)_hca.HttpContext.Items["User"];
    }
}
