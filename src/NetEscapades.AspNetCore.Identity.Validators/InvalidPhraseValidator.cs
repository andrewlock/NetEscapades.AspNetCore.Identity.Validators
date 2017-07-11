using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetEscapades.AspNetCore.Identity.Validators
{
    public class InvalidPhraseValidator<TUser> : IPasswordValidator<TUser>
        where TUser : class
    {
        private readonly HashSet<string> _invalidPhrases;
        public InvalidPhraseValidator(IEnumerable<string> invalidPhrases)
        {
            if (invalidPhrases == null) { throw new ArgumentNullException(nameof(invalidPhrases)); }
            _invalidPhrases = new HashSet<string>(invalidPhrases, StringComparer.OrdinalIgnoreCase);
        }

        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            if (password == null) { throw new ArgumentNullException(nameof(password)); }
            if (manager == null) { throw new ArgumentNullException(nameof(manager )); }
            
            var result = _invalidPhrases.Contains(password)
            ? IdentityResult.Failed(new IdentityError
            {
                Code = "InvalidPhrase",
                Description = $"You cannot use '{password}' as your password"
            })
            : IdentityResult.Success;

            return Task.FromResult(result);
        }
    }
}