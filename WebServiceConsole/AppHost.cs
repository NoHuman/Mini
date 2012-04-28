namespace WebServiceConsole
{
    using System;
    using Funq;
    using Infrastructure;
    using ServiceInterface;
    using ServiceModel;
    using ServiceStack.ServiceHost;
    using ServiceStack.Text;
    using ServiceStack.WebHost.Endpoints;

    /// <summary>
    /// An example of a AppHost to have your services running inside a webserver.
    /// </summary>
    public class AppHost
        : AppHostHttpListenerBase
    {
        private string _cookieValue;

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

            var AllowedUser = "morten";
            //this.RequestFilters.Add((req, res, dto) =>
            //{
            //    var userPass = req.GetBasicAuthUserAndPassword();
            //    if (userPass == null)
            //    {
            //        res.ReturnAuthRequired();
            //        return;
            //    }

            //    var userName = userPass.Value.Key;
            //    string AllowedPass = "pass";
            //    if (userName == AllowedUser && userPass.Value.Value == AllowedPass)
            //    {
            //        var sessionKey = userName + "/" + Guid.NewGuid().ToString("N");

            //        //set session for this request (as no cookies will be set on this request)
            //        req.Items["ss-session"] = sessionKey;
            //        res.SetPermanentCookie("ss-session", sessionKey);
            //    }
            //    else
            //    {
            //        res.ReturnAuthRequired();
            //    }

            //});
            this.RequestFilters.Add((req, res, dto) =>
                                        {
                                            Console.WriteLine(string.Format("{0}: {1}", "Request", dto.Dump()));

                                            var cookieValue = req.GetItemOrCookie("token");
                                            if (cookieValue != null && cookieValue == _cookieValue)
                                            {
                                                Console.WriteLine("Token approved");
                                            }
                                            else
                                            {
                                                Console.WriteLine(req.GetItemOrCookie("token"));
                                            }
                                            if (dto is LoginUser)
                                            {
                                                var sessionId = req.GetItemOrCookie("ss-session");
                                                if (sessionId == null || sessionId.SplitOnFirst('/')[0] != AllowedUser)
                                                {
                                                    //res.ReturnAuthRequired();
                                                }
                                            }
                                        });
            this.ResponseFilters.Add((req, res, dto) =>
                                         {
                                             Console.WriteLine(string.Format("{0}: {1}", "Response", dto.Dump()));
                                             if (dto is LoginUserResponse)
                                             {
                                                 var user = dto as LoginUserResponse;
                                                 if (user.Successful)
                                                 {
                                                     _cookieValue = Guid.NewGuid().ToString();
                                                     res.SetSessionCookie("token", _cookieValue);
                                                 }
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
}