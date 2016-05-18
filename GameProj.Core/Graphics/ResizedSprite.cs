using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Graphics
{
    public class ResizedSprite
    {
        public ResizedSprite(Sprite sprite,int newWidth,int newHeight)
        {
            this.Sprite = sprite;
            this.NewBounds = new Rectangle(0, 0, newWidth, newHeight);
        }
        public Sprite Sprite { get; set; }

        public Rectangle NewBounds { get; set; }
    }
}
