using System;
using System.Threading.Tasks;

namespace Bash.Session.Infrastructure.Writer
{
    internal interface ISessionWriter
    {
        Task WriteSession(RawSession session, DateTime idleExpirationDate);
    }
}
