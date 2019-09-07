namespace Bash.Session.Configuration
{
    public class CookieSettingsBuilder
    {
        private const CookieSecurePreference DefaultCookieSecurePreference =
            CookieSecurePreference.MatchingRequest;

        private static readonly CookieName DefaultCookieName =
            new CookieName("session_id");

        private readonly CookieName _name;

        private readonly bool _httpOnly;

        private readonly CookieSecurePreference _securePreference;

        public CookieSettingsBuilder()
        {
            _name = DefaultCookieName;
            _httpOnly = true;
            _securePreference = DefaultCookieSecurePreference;
        }

        private CookieSettingsBuilder(
            CookieName name,
            bool httpOnly,
            CookieSecurePreference securePreference)
        {
            _name = name;
            _httpOnly = httpOnly;
            _securePreference = securePreference;
        }

        public CookieSettingsBuilder Name(CookieName name)
        {
            return ShallowClone(name: name);
        }

        public CookieSettingsBuilder HttpOnly(bool httpOnly)
        {
            return ShallowClone(httpOnly: httpOnly);
        }

        public CookieSettingsBuilder SecurePreference(CookieSecurePreference securePreference)
        {
            return ShallowClone(securePreference: securePreference);
        }

        public CookieSettings Build()
        {
            return new CookieSettings(_name, _httpOnly, _securePreference);
        }

        private CookieSettingsBuilder ShallowClone(
            CookieName? name = null,
            bool? httpOnly = null,
            CookieSecurePreference? securePreference = null)
        {
            return new CookieSettingsBuilder(
                name ?? _name,
                httpOnly ?? _httpOnly,
                securePreference ?? _securePreference);
        }
    }
}