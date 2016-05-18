using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Graphics
{
    public class CameraManager
    {
        public static Camera Cam { get; set; }
        public static void Init(Viewport port)
        {
            Cam = new Camera(port.Bounds,15);
        }
        public static void Lock(Point position)
        {
            Cam.LockToSprite(position);
        }
        public static void Update(GameTime time)
        {
            Cam.Update(time);
        }
    }
}
