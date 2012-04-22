using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceModel
{
    public class GetPosition
    {
        public int Id { get; set; }
    }

    public class GetPositionResponse
    {
        public Position Position { get; set; }
    }

    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
