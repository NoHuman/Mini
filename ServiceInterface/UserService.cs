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
            return null;
        }
    }
}
