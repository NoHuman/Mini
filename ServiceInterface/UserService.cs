using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ServiceModel;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;

namespace ServiceInterface
{
    using ServiceStack.ServiceInterface.ServiceModel;

    class UserService : IService<CreateUser>, IService<LoginUser>
    {
        public IUserRepository UserRepository { get; set; } 

        public object Execute(CreateUser request)
        {
            if (UserRepository.NameInUse(request.Name))
            {
                throw new WebServiceException("Username in use") {StatusCode = 500};
            }
            return null;
        }

        public object Execute(LoginUser request)
        {
            if (UserRepository.NameInUse(request.Username))
            {
                if (UserRepository.Match(request.Username, request.Password))
                {
                    return new LoginUserResponse
                               {
                                   Successful = true
                               };
                }
            }else
            {
                var user = UserRepository.CreateUser(new User {Username = request.Username, Password = request.Password});
                return new LoginUserResponse {Successful = true};
            }
            return new LoginUserResponse
                       {
                           ResponseStatus = new ResponseStatus {ErrorCode = "Username and password does not match."},
                           Successful = false
                       };
        }
    }
}
