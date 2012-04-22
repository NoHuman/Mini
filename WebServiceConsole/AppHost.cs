using Funq;
using ServiceInterface;
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
        }
    }
}