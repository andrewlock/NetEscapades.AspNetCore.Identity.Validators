using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Test;
using Xunit;

namespace NetEscapades.AspNetCore.Identity.Validators.Test
{
    public class UsernameAsPasswordValidatorTests
    {
        const string _error = "You cannot use your username as your password";

        [Fact]
        public async Task ValidateThrowsWithNullTest()
        {
            // Setup
            var validator = new UsernameAsPasswordValidator<IdentityUser>();

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>("password", () => validator.ValidateAsync(null, null, null));
            await Assert.ThrowsAsync<ArgumentNullException>("manager", () => validator.ValidateAsync(null, null, "foo"));
        }

        [Fact]
        public async Task ValidateDoesNotThrowWithNullUserTest()
        {
            // Setup
            var validator = new UsernameAsPasswordValidator<IdentityUser>();
            var manager = MockHelpers.TestUserManager<IdentityUser>();
            var password = "foo";

            // Act
            // Assert
            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, null, password));
        }

        [Theory]
        [InlineData("test@test.com","test@test.com")]
        [InlineData("test@test.com","TEST@test.com")]
        public async Task FailsIfSame(string username, string password)
        {
            var manager = MockHelpers.TestUserManager<IdentityUser>();
            var validator = new UsernameAsPasswordValidator<IdentityUser>();
            var user = new IdentityUser { UserName = username };

            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password), _error);
        }

        [Theory]
        [InlineData("test@test.com","something else")]
        public async Task SuccessIfDifferent(string username, string password)
        {
            var manager = MockHelpers.TestUserManager<IdentityUser>();
            var validator = new UsernameAsPasswordValidator<IdentityUser>();
            var user = new IdentityUser { UserName = username };

            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, user, password));
        }
    }
}