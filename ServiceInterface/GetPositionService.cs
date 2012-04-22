using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceModel;
using ServiceStack.ServiceHost;

namespace ServiceInterface
{
    public class GetPositionService : IService<GetPosition>
    {
        public object Execute(GetPosition request)
        {
            Console.WriteLine("Position of {0} was requested", request.Id);
            return new GetPositionResponse
                       {
                           Position = new Position{X = 0, Y = 0}
                       };
        }
    }
}
