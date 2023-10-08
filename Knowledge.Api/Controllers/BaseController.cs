using Knowledge.Api.Common;
using Knowledge.Models.Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Knowledge.Api.Controllers
{
    public static class IdentityExtensions
    {
        private static string? ClaimValue(ClaimsPrincipal? claimsPrincipal, string appClaimType)
        {
            return claimsPrincipal?.Claims.SingleOrDefault(x => x.Type == appClaimType)?.Value;
        }

        private static Guid GetClaimId(ClaimsPrincipal? claimsPrincipal, string appClaimType)
        {
            var claimValue = ClaimValue(claimsPrincipal, appClaimType);
            return Guid.TryParse(claimValue, out var id) ? id : Guid.Empty;
        }

        public static Guid UserId(this ClaimsPrincipal? claimsPrincipal) =>
            GetClaimId(claimsPrincipal, AppClaimType.UserId);

    

        public static string SessionId(this ClaimsPrincipal? claimsPrincipal) =>
            ClaimValue(claimsPrincipal, AppClaimType.SessionId) ?? string.Empty;

        

        private static string? GetUserRolesClaim(this ClaimsPrincipal? claimsPrincipal) =>
            ClaimValue(claimsPrincipal, AppClaimType.UserRoles);

        private static int GetUserRoles(this ClaimsPrincipal? claimsPrincipal)
        {
            return int.TryParse(claimsPrincipal.GetUserRolesClaim(), out var value)
                ? value
                : 0;
        }

        public static bool IsAdmin(this ClaimsPrincipal? claimsPrincipal) =>
            claimsPrincipal.GetUserRoles() == (int)UserRole.Admin;

        public static bool IsInRole(this ClaimsPrincipal? claimsPrincipal, params UserRole[] userRoles) =>
            UserRoleExtensions.IsEnrolled(userRoles, claimsPrincipal.GetUserRoles());
    }

    public class BaseController : Controller
    {
        protected string SessionId => User.SessionId();

        protected Guid SessionUserId => User.UserId();

        protected bool SessionIsAdmin => User.IsAdmin();
    }
}
