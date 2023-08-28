using System.Net;

namespace WebShop.Exceptions
{
    public class InternalServerException : BaseCustomException
    {
        public InternalServerException(string message) : base(message, null, HttpStatusCode.InternalServerError)
        {
        }
    }
}
