using System;
using System.Collections.Generic;
using NetEscapades.AspNetCore.Identity.Validators;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityBuilderExtensions
    {
        /// <summary>
        /// Adds a password validator that checks the user's Username is not the same as the provided password
        /// </summary>
        /// <typeparam name="TUser">The type of the TUser</typeparam>
        /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
        /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
        public static IdentityBuilder AddUsernameAsPasswordValidator<TUser>(this IdentityBuilder builder)
            where TUser: IdentityUser
        {
            if (builder == null) { throw new ArgumentNullException(nameof(builder)); }
            return builder.AddPasswordValidator<UsernameAsPasswordValidator<TUser, string>>();
        }

        /// <summary>
        /// Adds a password validator that checks the user's Username is not the same as the provided password
        /// </summary>
        /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
        /// <typeparam name="TUser">The type of the TUser</typeparam>
        /// <typeparam name="TKey">The key type of the TUser</typeparam>
        /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
        public static IdentityBuilder AddUsernameAsPasswordValidator<TUser, TKey>(this IdentityBuilder builder)
            where TUser: IdentityUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (builder == null) { throw new ArgumentNullException(nameof(builder)); }
            return builder.AddPasswordValidator<UsernameAsPasswordValidator<TUser, TKey>>();
        }

        /// <summary>
        /// Adds a password validator that checks the user's Email is not the same as the provided password
        /// </summary>
        /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
        /// <typeparam name="TUser">The type of the TUser</typeparam>
        /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
        public static IdentityBuilder AddEmailAsPasswordValidator<TUser>(this IdentityBuilder builder)
            where TUser: IdentityUser
        {
            if (builder == null) { throw new ArgumentNullException(nameof(builder)); }
            return builder.AddPasswordValidator<EmailAsPasswordValidator<TUser, string>>();
        }

        /// <summary>
        /// Adds a password validator that checks the user's Email is not the same as the provided password
        /// </summary>
        /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
        /// <typeparam name="TUser">The type of the TUser</typeparam>
        /// <typeparam name="TKey">The key type of the TUser</typeparam>
        /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
        public static IdentityBuilder AddEmailAsPasswordValidator<TUser, TKey>(this IdentityBuilder builder)
            where TUser: IdentityUser<TKey>
            where TKey: IEquatable<TKey>
        {
            if (builder == null) { throw new ArgumentNullException(nameof(builder)); }
            return builder.AddPasswordValidator<EmailAsPasswordValidator<TUser, TKey>>();
        }

        /// <summary>
        /// Adds a password validator that checks the user's Email is not the same as the provided password
        /// </summary>
        /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
        /// <param name="invalidPhrases"></param>
        /// <typeparam name="TUser">The user type whose password will be validated.</typeparam>
        /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
        public static IdentityBuilder AddInvalidPhraseValidator<TUser>(this IdentityBuilder builder, IEnumerable<string> invalidPhrases)
            where TUser : IdentityUser
        {
            var validator = new InvalidPhraseValidator<TUser>(invalidPhrases);
            builder.Services.AddSingleton(typeof(IPasswordValidator<>).MakeGenericType(builder.UserType), validator);
            return builder;
        }
    }
}
