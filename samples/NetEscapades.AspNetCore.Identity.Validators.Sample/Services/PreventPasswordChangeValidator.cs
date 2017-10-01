using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetEscapades.AspNetCore.Identity.Validators.Sample.Services
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
