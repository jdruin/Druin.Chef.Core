using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net;

namespace Druin.Chef.Core.Exceptions
{
    public class ChefExceptionBuilder : Exception
    {
        public ChefExceptionBuilder(string username, WebException exception)
        {
            Build(username, exception);
        }
        private void Build(string username, WebException exception)
        {
            HttpWebResponse res = (HttpWebResponse)exception.Response;
            string message;
            switch (res.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    message = "Bad request. The contents of the request are not formatted correctly.";
                    throw new ChefException(message);
                case HttpStatusCode.Unauthorized:
                    message = "Unauthorized. The user or client (" + username + ") could not be authenticated. Verify the user/client name, and that the correct key was used to sign the request.";
                    throw new ChefUnauthorizedException(message);
                case HttpStatusCode.Forbidden:
                    message = "Forbidden. " + username + " is not authorized to perform the action.";
                    throw new ChefForbiddenException(message);
                case HttpStatusCode.NotFound:
                    message = "Not found. The requested object does not exist.";
                    throw new ChefNotFoundException(message);
                case HttpStatusCode.Conflict:
                    message = "Conflict. The object already exists.";
                    throw new ChefConflictException(message);
                case HttpStatusCode.Gone:
                    message = "The object is gone";
                    throw new ChefGoneException(message);
                case HttpStatusCode.RequestEntityTooLarge:
                    message = "Request entity too large. A request may not be larger than 1000000 bytes.";
                    throw new ChefTooLargeException(message);
                case HttpStatusCode.InternalServerError:
                    message = "Internal Server Error";
                    throw new ChefException(message, exception);
                default:
                    message = res.StatusDescription;
                    throw new ChefException(message, exception);
            }
        }
    }
}
