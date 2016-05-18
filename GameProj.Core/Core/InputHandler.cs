using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Core
{
    public class InputHandler
    {
        /// <summary>
        /// A faire mieux
        /// </summary>
        /// <param name="world"></param>
        /// <param name="mstate"></param>
        /// <param name="kstate"></param>
        public static void Handle(World world,MouseState mstate,KeyboardState kstate)
        {
            if (kstate.IsKeyDown(Keys.LeftShift))
                world.Map.Renderer.ViewGrid = true;
            else
                world.Map.Renderer.ViewGrid = false;
        }
    }
}
