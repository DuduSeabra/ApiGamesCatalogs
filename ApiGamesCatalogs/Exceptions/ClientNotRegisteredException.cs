using System;

namespace ApiGamesCatalogs.Exceptions
{
    public class ClientNotRegisteredException : Exception
    {
        public ClientNotRegisteredException()
            : base("This client is not registered")
        { }
    }
}
