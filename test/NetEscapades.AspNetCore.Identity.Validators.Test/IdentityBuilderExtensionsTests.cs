using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Test;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace NetEscapades.AspNetCore.Identity.Validators.Test
{
    public class IdentityBuilderExtensionsTests
    {
        [Fact]
        public async Task ValidateThrowsWithNull()
        {
            // Setup

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                IdentityBuilderExtensions.AddEmailAsPasswordValidator<IdentityUser>(null);
                return Task.CompletedTask;
            });
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                IdentityBuilderExtensions.AddUsernameAsPasswordValidator<IdentityUser>(null);
                return Task.CompletedTask;
            });
        }

        [Fact]
        public void ValidateCanAddUserDerivedUserTypeForUsernameAsPasswordValidator()
        {
            // Setup
            var services = new ServiceCollection();
            var builder = services.AddIdentity<ApplicationUser, IdentityRole>();

            // Act
            builder.AddUsernameAsPasswordValidator<ApplicationUser>();
            // Assert
        }

        [Fact]
        public void ValidateCanAddUserDerivedUserTypeForEmailAsPasswordValidator()
        {
            // Setup
            var services = new ServiceCollection();
            var builder = services.AddIdentity<ApplicationUser, IdentityRole>();

            // Act
            builder.AddEmailAsPasswordValidator<ApplicationUser>();
            // Assert
        }

        public class ApplicationUser : IdentityUser {}
    }
}