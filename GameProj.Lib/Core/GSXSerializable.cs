using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Core
{
    public interface GSXSerializable
    {
        void Serialize(BigEndianWriter writer);
        void Deserialize(BigEndianReader reader);

        string GetFileName();

     
    }
}
