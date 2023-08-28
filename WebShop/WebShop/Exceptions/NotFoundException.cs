using System.Net;

namespace WebShop.Exceptions
{
    public class NotFoundException : BaseCustomException
    {
        public NotFoundException(string message) : base(message, null, HttpStatusCode.NotFound)
        {            
        }
    }
}
