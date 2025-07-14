using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApi.Domain.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public ExternalServiceException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
