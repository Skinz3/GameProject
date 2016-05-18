using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Interface
{
    public interface IObject
    {
        void Draw(GameTime time);
        void Update(GameTime time);
    }
}
