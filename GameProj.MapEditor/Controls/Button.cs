using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.MapEditor.Controls
{
    public class Button : Control
    {
        public Color BordersColor { get; set; }
        public Color ContentColor { get; set; }
        public string Text { get; set; }
        public int X { get; set; }
        public Button(Rectangle rectangle,Color borderscolor,Color contentcolor,string text,int x)
        {
            this.Rectangle = rectangle;
            this.BordersColor = borderscolor;
            this.ContentColor = contentcolor;
            this.Text = text;
            this.X = x;
        }
        public void Draw(GameTime time)
        {
           
            GameCore.Batch.DrawRectangle(Rectangle, BordersColor);
            GameCore.Batch.FillRectangle(new Rectangle(Rectangle.X+1,Rectangle.Y+1,Rectangle.Width-1,Rectangle.Height-1), ContentColor);
            GameCore.DrawString("miniFont", Text, new Point(X,Rectangle.Y), Color.Black);
        }

        public void Update(MouseState state,GameTime time)
        {
            this.UpdateEvents(state,time);
        }
    }
}
