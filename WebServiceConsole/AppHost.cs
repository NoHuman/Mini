using System;
using Funq;
using ServiceInterface;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

namespace WebServiceConsole
{
    /// <summary>
    /// An example of a AppHost to have your services running inside a webserver.
    /// </summary>
    public class AppHost
        : AppHostHttpListenerBase
    {
        public AppHost()
            : base("Ludum Dare", typeof (GetPositionService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            //Signal advanced web browsers what HTTP Methods you accept
            SetConfig(new EndpointHostConfig
                               {
                                   GlobalResponseHeaders =
                                       {
                                           {"Access-Control-Allow-Origin", "*"},
                                           {"Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS"},
                                       },
                                   WsdlServiceNamespace = "http://www.servicestack.net/types",
                               });

            container.Register<IUserRepository>(new UserRepository());

            string AllowedUser = "morten";
            this.RequestFilters.Add((req, res, dto) =>
            {
                var userPass = req.GetBasicAuthUserAndPassword();
                if (userPass == null)
                {
                    res.ReturnAuthRequired();
                    return;
                }

                var userName = userPass.Value.Key;
                string AllowedPass = "pass";
                if (userName == AllowedUser && userPass.Value.Value == AllowedPass)
                {
                    var sessionKey = userName + "/" + Guid.NewGuid().ToString("N");

                    //set session for this request (as no cookies will be set on this request)
                    req.Items["ss-session"] = sessionKey;
                    res.SetPermanentCookie("ss-session", sessionKey);
                }
                else
                {
                    res.ReturnAuthRequired();
                }

            });
            //this.RequestFilters.Add((req, res, dto) =>
            //{
            //    if (dto is Secure)
            //    {
            //        var sessionId = req.GetItemOrCookie("ss-session");
            //        if (sessionId == null || sessionId.SplitOnFirst('/')[0] != AllowedUser)
            //        {
            //            res.ReturnAuthRequired();
            //        }
            //    }
            //});
        }
    }

    public class UserRepository : IUserRepository
    {
        public bool NameInUse(string name)
        {
            return false;
        }
    }
}