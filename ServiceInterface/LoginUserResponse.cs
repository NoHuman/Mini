using ServiceStack.ServiceInterface.ServiceModel;

namespace ServiceInterface
{
    public class LoginUserResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}