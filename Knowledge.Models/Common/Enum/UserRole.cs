using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge.Models.Common.Enum
{
    [Flags]
    public enum UserRole
    {
        Admin = ~0,
        Viewer = 1 << 0,
        Editer = 1 << 1,
        
    }
    public static class UserRoleExtensions
    {
        public static bool IsAdmin(this IEnumerable<UserRole> userRoles) =>
            userRoles.Contains(UserRole.Admin);

        public static int CombineRoles(this IEnumerable<UserRole> userRoles) =>
            userRoles.Aggregate(0, (aggr, role) => aggr | (int)role);

        public static bool IsEnrolled(IEnumerable<UserRole> rolesWithAccess, int rolesToCheck)
        {
            foreach (var roleWithAccess in rolesWithAccess)
            {
                var roleWithAccessCast = (int)roleWithAccess;
                if ((roleWithAccessCast & rolesToCheck) == roleWithAccessCast) return true;
            }

            return false;
        }

        public static bool HasRole(this IEnumerable<UserRole> userRoles, params UserRole[] rolesWithAccess) =>
            IsEnrolled(rolesWithAccess, userRoles.CombineRoles());
    }
}
