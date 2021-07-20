using System;

namespace ApiGamesCatalogs.Exceptions
{
    public class ClientAlreadyRegisteredException : Exception
    {
        public ClientAlreadyRegisteredException()
            : base("This client is already registered")
        { }
    }
}
