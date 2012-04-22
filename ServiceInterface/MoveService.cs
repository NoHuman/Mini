using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceModel;
using ServiceStack.ServiceHost;

namespace ServiceInterface
{
    public class MoveService : IService<MoveCommand> 
    {
        static Vector2 vector = new Vector2{X=0, Y=0};
        public object Execute(MoveCommand request)
        {
            vector.X += request.Vector.X;
            vector.Y += request.Vector.Y;
            Console.WriteLine("User is now at [{0}, {1}]", vector.X, vector.Y);
            return true;
        }
    }
}
