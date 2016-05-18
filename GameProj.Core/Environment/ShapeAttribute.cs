using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProj.Core.Environment
{
    public class Shape : Attribute
    {
        public char ShapeIdentifier { get; set; }
        public Shape(char identifier)
        {
            this.ShapeIdentifier = identifier;
        }
    }
}
