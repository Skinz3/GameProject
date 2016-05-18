using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Managers
{
    public class GSXAttribute : Attribute
    {
        public string Directory { get; set; }

        public GSXAttribute(string directory)
        {
            this.Directory = directory;
        }
    }
}
