using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NetEscapades.AspNetCore.Identity.Validators;

namespace NetEscapades.AspNetCore.Identity.Validators.NetCore3
{
    public class PreventPasswordChangeValidator<TUser> : PasswordChangeValidatorBase<TUser> where TUser : IdentityUser<string>
    {
        public override Task<IdentityResult> ValidatePasswordChangeAsync(UserManager<TUser> manager, TUser user, string password)
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityError
            {
                Code = "PasswordChange",
                Description = "You cannot change your password"
            }));
        }
    }
}
