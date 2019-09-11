using System;
using Messerli.Session.Configuration;
using Xunit;

namespace Messerli.Session.Test.Configuration
{
    public class CookieSettingsBuilderTest
    {
        [Fact]
        public void BuilderHasDefaultValues()
        {
            var _ = new CookieSettingsBuilder().Build();
        }

        [Theory]
        [MemberData(nameof(InvalidCookieNames))]
        public void ThrowsIfCookieNameIsInvalid(string invalidCookieName)
        {
            var cookieName = new CookieName(invalidCookieName);
            var builder = new CookieSettingsBuilder().Name(cookieName);
            Assert.Throws<InvalidOperationException>(() =>
            {
                var _ = builder.Build();
            });
        }

        [Fact]
        public void UsesProvidedValues()
        {
            var cookieName = new CookieName("foo");
            const bool httpOnly = false;
            const CookieSecurePreference secureOnly = CookieSecurePreference.Never;
            var expectedCookieSettings = new CookieSettings(cookieName, httpOnly, secureOnly);

            var cookieSettings = new CookieSettingsBuilder()
                .Name(cookieName)
                .HttpOnly(httpOnly)
                .SecurePreference(secureOnly)
                .Build();
            Assert.Equal(expectedCookieSettings, cookieSettings);
        }

        public static TheoryData<string> InvalidCookieNames() => Constant.WhitespaceValues;
    }
}
