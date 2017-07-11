using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Test;
using Xunit;

namespace NetEscapades.AspNetCore.Identity.Validators.Test
{
    public class EmailAsPasswordValidatorTests
    {
        const string _error = "You cannot use your Email as your password";

        [Fact]
        public async Task ValidateThrowsWithNullTest()
        {
            // Setup
            var validator = new EmailAsPasswordValidator<IdentityUser>();

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>("password", () => validator.ValidateAsync(null, null, null));
            await Assert.ThrowsAsync<ArgumentNullException>("manager", () => validator.ValidateAsync(null, null, "foo"));
        }

        [Fact]
        public async Task ValidateDoesNotThrowWithNullUserTest()
        {
            // Setup
            var validator = new EmailAsPasswordValidator<IdentityUser>();
            var manager = MockHelpers.TestUserManager<IdentityUser>();
            var password = "foo";

            // Act
            // Assert
            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, null, password));
        }

        [Theory]
        [InlineData("test@test.com","test@test.com")]
        [InlineData("test@test.com","TEST@test.com")]
        public async Task FailsIfSame(string email, string password)
        {
            var manager = MockHelpers.TestUserManager<IdentityUser>();
            var validator = new EmailAsPasswordValidator<IdentityUser>();
            var user = new IdentityUser { Email = email };

            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password), _error);
        }

        [Theory]
        [InlineData("test@test.com","something else")]
        public async Task SuccessIfDifferent(string email, string password)
        {
            var manager = MockHelpers.TestUserManager<IdentityUser>();
            var validator = new EmailAsPasswordValidator<IdentityUser>();
            var user = new IdentityUser { Email = email };

            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, user, password));
        }
    }
}