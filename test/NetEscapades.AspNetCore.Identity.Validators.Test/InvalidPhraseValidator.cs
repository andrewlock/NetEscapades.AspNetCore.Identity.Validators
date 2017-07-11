using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Test;
using Xunit;

namespace NetEscapades.AspNetCore.Identity.Validators.Test
{
    public class InvalidPhraseValidatorTests
    {
        [Fact]
        public async Task ValidateThrowsWithNullTest()
        {
            // Setup
            var validator = new InvalidPhraseValidator<TestUser>(new [] {"phrase"});

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>("password", () => validator.ValidateAsync(null, null, null));
            await Assert.ThrowsAsync<ArgumentNullException>("manager", () => validator.ValidateAsync(null, null, "foo"));
        }

        [Fact]
        public async Task ValidateDoesNotThrowWithNullUserTest()
        {
            // Setup
            var validator = new InvalidPhraseValidator<TestUser>(new [] {"phrase"});
            var manager = MockHelpers.TestUserManager<TestUser>();
            var password = "foo";

            // Act
            // Assert
            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, null, password));
        }

        [Theory]
        [InlineData("test@test.com","a phrase", "test@test.com")]
        [InlineData("test@test.com","a phrase", "TEST@test.com")]
        [InlineData("something","something","something more")]
        [InlineData("something","SOMETHING","something more")]
        public async Task FailsIfContained(string password, params string[] phrases)
        {
            var manager = MockHelpers.TestUserManager<TestUser>();
            var validator = new InvalidPhraseValidator<TestUser>(phrases);
            var user = new TestUser();
            var error = $"You cannot use '{password}' as your password";

            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password), error);
        }

        [Theory]
        [InlineData("test@test.com","not that")]
        [InlineData("something","not something", "something that's not it")]
        public async Task SuccessIfNotContained(string password, params string[] phrases)
        {
            var manager = MockHelpers.TestUserManager<TestUser>();
            var validator = new InvalidPhraseValidator<TestUser>(phrases);
            var user = new TestUser();

            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, user, password));
        }
    }
}