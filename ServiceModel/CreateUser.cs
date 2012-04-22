using ServiceStack.ServiceInterface.ServiceModel;

namespace ServiceModel
{
    public class CreateUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}