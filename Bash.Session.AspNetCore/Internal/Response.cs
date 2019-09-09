using System;
using Bash.Session.Configuration;
using Bash.Session.Http;
using Microsoft.AspNetCore.Http;

namespace Bash.Session.AspNetCore.Internal
{
    internal class Response : IResponse
    {
        private readonly HttpContext _httpContext;

        public Response(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public void SetCookie(Cookie cookie)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = cookie.Settings.HttpOnly,
                Secure = MapSecurePreferenceToBool(cookie.Settings.SecurePreference),
            };

            _httpContext.Response.Cookies.Append(
                cookie.Settings.Name.Value,
                cookie.Value,
                cookieOptions);
        }

        public void SetHeader(string name, string value)
        {
            _httpContext.Response.Headers.Append(name, value);
        }

        private bool MapSecurePreferenceToBool(CookieSecurePreference securePreference)
        {
            return securePreference switch
            {
                CookieSecurePreference.Always => true,
                CookieSecurePreference.Never => false,
                CookieSecurePreference.MatchingRequest => _httpContext.Request.IsHttps,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}