using System.Threading.Tasks;
using Bash.Session.Configuration;
using Bash.Session.Http;
using Bash.Session.SessionState;
using Bash.Session.Storage;

namespace Bash.Session.Internal
{
    internal class SessionLoader : ISessionLoader
    {
        private readonly ISessionStorage _sessionStorage;
        
        private readonly CookieName _cookieName;

        public SessionLoader(
            ISessionStorage sessionStorage,
            CookieName cookieName)
        {
            _sessionStorage = sessionStorage;
            _cookieName = cookieName;
        }

        public async Task<RawSession?> LoadFromRequest(IRequest request)
        {
            var sessionIdString = request.GetCookie(_cookieName);

            if (string.IsNullOrWhiteSpace(sessionIdString))
            {
                return null;
            }

            var sessionId = new SessionId(sessionIdString);
            var sessionData = await _sessionStorage.ReadSessionData(sessionId);

            if (sessionData is null)
            {
                return null;
            }

            return new RawSession(
                new Existing(sessionId),
                sessionData);
        }
    }
}