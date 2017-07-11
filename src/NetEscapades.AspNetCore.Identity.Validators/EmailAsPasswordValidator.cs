using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NetEscapades.AspNetCore.Identity.Validators
{
    /// <inheritdoc />
    public class EmailAsPasswordValidator<TUser> 
        : EmailAsPasswordValidator<TUser, string>
            where TUser : IdentityUser<string> {}
    
    /// <summary>
    /// Validates that the supplied password is not the same as the user's Email
    /// </summary>
    public class EmailAsPasswordValidator<TUser, TKey> 
        : IPasswordValidator<TUser> 
            where TUser : IdentityUser<TKey>
            where TKey : IEquatable<TKey>
    {
        /// <inheritdoc />
        public Task<IdentityResult> ValidateAsync(
            UserManager<TUser> manager, 
            TUser user, 
            string password)
        {

            if (password == null) { throw new ArgumentNullException(nameof(password)); }
            if (manager == null) { throw new ArgumentNullException(nameof(manager )); }
            
            if (user != null && string.Equals(user.Email, password, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailAsPassword",
                    Description = "You cannot use your Email as your password"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}