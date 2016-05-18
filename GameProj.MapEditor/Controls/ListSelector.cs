using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.MapEditor.Controls
{
    public class ListSelector : Control
    {

        public Dictionary<Rectangle, string> Content = new Dictionary<Rectangle, string>();

        public int X { get; set; }
        public int Y { get; set; }
        public ListSelector(List<string> content, int x, int y)
        {
            this.X = x;
            this.Y = y;
            Build(content);
        }
        public void Build(List<string> items)
        {
            int y = Y;
            foreach (var item in items)
            {

                Content.Add(new Rectangle(X, y, 150, 30), item);
                y += 40;
            }
        }
        public override void UpdateEvents(MouseState state, GameTime time)
        {

            base.UpdateEvents(state, time);
        }
        public void Draw()
        {
            foreach (var content in Content)
            {
                GameCore.Batch.DrawRectangle(content.Key, Color.Black);
                GameCore.DrawString("miniFont",content.Value,new Point(content.Key.X, content.Key.Y),Color.Black);
              
            }
        }
    }
}
