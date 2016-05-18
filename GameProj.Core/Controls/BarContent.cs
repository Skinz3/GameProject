using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Lib.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Controls
{
    public class BarContent : Control
    {

        Color Full { get; set; }
        Color Mid { get; set; }
        Color Down { get; set; }

        Rectangle ContentRectangle = new Rectangle();

        sbyte ActualPercentage { get; set; }

        public bool DrawValues { get; set; }

        public BarContent(string name,Point position, int width, int height, Color fullLifeColor, Color midlifeColor, Color downColor,bool drawvalues = false)
            : base(name,true)
        {
            this.Rectangle = new Rectangle(position, new Point(width, height));
            this.Full = fullLifeColor;
            this.Mid = midlifeColor;
            this.Down = downColor;
            this.ContentRectangle = new Rectangle(Rectangle.X + 1, Rectangle.Y + 1, Rectangle.Width - 2, Rectangle.Height - 2);
            ActualPercentage = 100;
            this.Name = name;
            this.DrawValues = drawvalues;
        }
        public int Max { get; set; }

        public int Current { get; set; }

        public void UpdateContent(int current,int max)
        {

            this.Current = current;
            this.Max = max;
            this.ActualPercentage = (sbyte)((double)current / (double)max * (double)100);
         

        }
        public override void Draw(GameTime time)
        {
            base.Draw(time);
            if (ActualPercentage == 0)
                return;
            ContentRectangle.Width = ((int)((double)ActualPercentage / (double)100 * (double)Rectangle.Width)) - 2;
            Color color = Full;
            if (ActualPercentage <= 50)
                color = Mid;
            if (ActualPercentage <= 25)
                color = Down;
            GameCore.Batch.FillRectangle(ContentRectangle, color);

            if (DrawValues)
            {
                string str = Current + "/" + Max;

                GameCore.DrawString("miniFont",str, new Point((Rectangle.X + Rectangle.Width / 2) - str.Length * 5, Rectangle.Y), Color.Black);
            }
    
        }
    }
}
