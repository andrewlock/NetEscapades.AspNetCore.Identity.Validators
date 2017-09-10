using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Test;
using Xunit;

namespace NetEscapades.AspNetCore.Identity.Validators.Test
{
    public class PasswordChangeValidatorBaseTests
    {
        const string _error = "You cannot change your password";

        [Fact]
        public async Task ValidateThrowsWithNullTest()
        {
            // Setup
            var validator = new PreventPasswordChangeValidator<IdentityUser>();

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>("password", () => validator.ValidateAsync(null, null, null));
            await Assert.ThrowsAsync<ArgumentNullException>("manager", () => validator.ValidateAsync(null, null, "foo"));
        }

        [Fact]
        public async Task SuccessIfNullUser()
        {
            // Setup
            var validator = new PreventPasswordChangeValidator<IdentityUser>();
            var manager = MockHelpers.TestUserManager<IdentityUser>();
            var password = "foo";

            // Act
            // Assert
            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, null, password));
        }

        [Fact]
        public async Task SuccessIfUserWithDefaultId()
        {
            var manager = MockHelpers.TestUserManager<IdentityUser>();
            var validator = new PreventPasswordChangeValidator<IdentityUser>();
            var user = new IdentityUser { UserName = "foo@foo.com", Id = default(string) };
            var password = "foo";

            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, user, password));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("", "1234-5678-9012-1234")]
        [InlineData("not null", default(string))]
        public async Task SuccessIfUserWithNoPasswordHash(string passwordHash, string id)
        {
            var manager = MockHelpers.TestUserManager<IdentityUser>();
            var validator = new PreventPasswordChangeValidator<IdentityUser>();
            var user = new IdentityUser
            {
                UserName = "foo@foo.com",
                Id = id,
                PasswordHash = passwordHash
            };
            var password = "foo";

            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, user, password));
        }

        [Fact]
        public async Task FailsIfUserWithIdAndPasswordHash()
        {
            var manager = MockHelpers.TestUserManager<IdentityUser>();
            var validator = new PreventPasswordChangeValidator<IdentityUser>();
            var user = new IdentityUser
            {
                UserName = "foo@foo.com",
                Id = Guid.NewGuid().ToString(),
                PasswordHash = "not null",
            };
            var password = "foo";

            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password), _error);
        }

        public class PreventPasswordChangeValidator<TUser> : PasswordChangeValidatorBase<TUser> where TUser : IdentityUser<string>
        {
            public override Task<IdentityResult> ValidatePasswordChangeAsync(UserManager<TUser> manager, TUser user, string password)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "PasswordChange",
                    Description = _error
                }));
            }
        }
    }
}