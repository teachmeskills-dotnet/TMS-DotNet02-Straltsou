using System.Collections.Generic;
using System.Security.Claims;

namespace LearnApp.Common.Interfaces
{
    public interface IJwtAuthenticationManager<T,Z>
        where T: class
        where Z : class

    {
        T Authenticate(Z parameters, string ipAddress);
        T RefreshToken(string token, string ipAddress);
    }
}
