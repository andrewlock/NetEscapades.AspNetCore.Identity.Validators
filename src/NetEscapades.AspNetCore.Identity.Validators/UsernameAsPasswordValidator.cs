using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetEscapades.AspNetCore.Identity.Validators
{
    /// <inheritdoc />
    public class UsernameAsPasswordValidator<TUser> 
        : UsernameAsPasswordValidator<TUser, string>
            where TUser : IdentityUser<string> {}
    
    /// <summary>
    /// Validates that the supplied password is not the same as the user's UserName
    /// </summary>
    public class UsernameAsPasswordValidator<TUser, TKey> 
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
            
            if (user != null && string.Equals(user.UserName, password, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UsernameAsPassword",
                    Description = "You cannot use your username as your password"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}