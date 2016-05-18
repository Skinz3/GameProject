using GameProj.Core.Core;
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.WorldEvents
{
    public class TemporaryString : WorldEvent
    {
        public string Content { get; set; }

        public float TransparencyDecr { get; set; }

        public string FontName { get; set; }

        public Vector2 StringSize { get; set; }

        public TemporaryString(World world,Point position,string fontName,string content,Color color,float transparencyDecr = 1f):base(world)
        {
            this.Content = content;
            this.Color = color;
            this.Position = position;
            this.TransparencyDecr = transparencyDecr;
            this.FontName = fontName;
            this.StringSize = GameCore.GetFont(fontName).MeasureString(content);
        }
        public override void Draw(GameTime time)
        {
            GameCore.DrawString(FontName,Content, Position, Color * (ContrastPercent / 100f));

        }
        public override void Update(GameTime time)
        {
            Position.Y--;
            ContrastPercent -= TransparencyDecr;
            if (ContrastPercent <= 0)
            {
                Remove();
                return;
            }
        }
    }
}
