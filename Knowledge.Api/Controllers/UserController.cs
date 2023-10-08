using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Knowledge.Api.Common;
using Knowledge.Api.Repositories;
using Knowledge.Models.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;

namespace Knowledge.Api.Controllers
{
    public static class UserRoutes
    {
        public const string V1 = "v1";
        // indicate to current user( use sessionUserId)
        public const string UserControllerV1 = "/" + V1 + "/user";
        public const string CurrentUserV1 = UserControllerV1;
        public const string ForgottenPasswordV1 = UserControllerV1 + "/forgotten-password";
        public const string PasswordModifyV1 = UserControllerV1 + "/forgotten-password/modification";
        public const string RegisterByInvitationV1 = UserControllerV1 + "/registration/invitation";
        public const string RegisterByCardV1 = UserControllerV1 + "/registration/device";
        public const string EmailV1 = UserControllerV1 + "/email";
        public const string ChangePassV1 = UserControllerV1 + "/password";
        public const string UpdateCurrentUserV1 = UserControllerV1;

        public const string ControllerV1 = V1 + "/" + "users";
        public const string GetUserRolesV1 = "{id}/roles";
        public const string DeleteUserV1 = "{id}";
        public const string GetUserV1 = "{id}";
        public const string GetUsersV1 = "";
        public const string GetUserSessionsV1 = "sessions";
        public const string UpdateUserRolesV1 = "{id}/roles";
        public const string ValidatePassword = UserControllerV1 + "/passwords/validity";
        public const string ForgottenPasswordValidate = ForgottenPasswordV1 + "/validity";
        public const string PasswordVerification = UserControllerV1 + "/password/verification";
        public const string RecoveryCodeVerification = UserControllerV1 + "/mfa-device/verification";
        public const string RecoveryCodeReset = UserControllerV1 + "/mfa/recovery-codes";
        public const string EnableMfa = UserControllerV1 + "/mfa-device";
        public const string DisableMfa = UserControllerV1 + "/mfa-device";

        public const string Sessions = UserControllerV1 + "/sessions/{id}";
    }

    [Route(UserRoutes.ControllerV1)]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet(UserRoutes.GetUsersV1)]
        public Task<PagedResults<UserEnumerable>> Get(int page = 0, string search = "", int limit = KnowledgeConst.MaxUserResultLimit)
        {
            if (limit > KnowledgeConst.MaxUserResultLimit) limit = KnowledgeConst.MaxUserResultLimit;
            return _userRepository.Get(search, page, limit);
        }
    }
}
