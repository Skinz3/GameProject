using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Graphics
{
    public class Sprite
    {
        public string Name { get; set; }

        public Texture2D Gfx { get; set; }

        public string Extension { get; set; }

        public string Path { get; set; }

        public bool Loaded { get { return Gfx != null; } }

        public Sprite(string name, string extension, string path)
        {
            this.Name = name;
            this.Extension = extension;
            this.Path = path;
        }
        public void Unload()
        {
            this.Gfx = null;
        }
        public void TryLoad()
        {
            if (!Loaded)
            this.Gfx = GameCore.ReadTexture(Path);
        }
        public ResizedSprite Resize(sbyte percent)
        {
            double ratio = (double)percent/ (double)100;
            var newWidth = (double)this.Gfx.Bounds.Width * (double)ratio;
            var newHeight = (double)this.Gfx.Bounds.Height * (double)ratio;
            ResizedSprite sprite = new ResizedSprite(this, (int)newWidth, (int)newHeight);
            return sprite;
        }

    }
}
