using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetEscapades.AspNetCore.Identity.Validators
{
    /// <inheritdoc />
    public abstract class PasswordChangeValidatorBase<TUser>
        : ChangePasswordOnlyValidatorBase<TUser, string>
            where TUser : IdentityUser<string>
    { }

    /// <summary>
    /// A base class that only applies validations when the user is changing their password
    /// </summary>
    public abstract class ChangePasswordOnlyValidatorBase<TUser, TKey>
        : IPasswordValidator<TUser>
            where TUser : IdentityUser<TKey>
            where TKey : IEquatable<TKey>
    {
        /// <inheritdoc />
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            if (password == null) { throw new ArgumentNullException(nameof(password)); }
            if (manager == null) { throw new ArgumentNullException(nameof(manager)); }

            var isNewUser = (user == null 
                || user.Id == null
                || user.Id.Equals(default(TKey)) 
                || string.IsNullOrEmpty(user.PasswordHash));

            if (isNewUser)
            {
               return Task.FromResult(IdentityResult.Success);
            }
            return ValidatePasswordChangeAsync(manager, user, password);

        }

        public abstract Task<IdentityResult> ValidatePasswordChangeAsync(UserManager<TUser> manager, TUser user, string password);
    }
}
