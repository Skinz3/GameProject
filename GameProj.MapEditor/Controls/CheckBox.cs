using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.MapEditor.Controls
{
    public class CheckBox : Control
    {
        public bool Checked = false;
        public string Text { get; set; }
        Action<bool> OnClicked { get; set; }
        public CheckBox(Action<bool> onClicked,string text,Point position)
        {
            this.OnClicked = onClicked;
            this.OnMouseClick += CheckBox_OnMouseClick;
            this.Rectangle = new Rectangle(position, new Point(10, 10));
            this.Text = text;
        }

        void CheckBox_OnMouseClick()
        {
            Checked = Checked == true ? false : true;
          
            OnClicked(Checked);
        }
        public void Draw()
        {
            GameCore.Batch.DrawRectangle(Rectangle, Color.Black);
            if (Checked)
                GameCore.Batch.FillRectangle(Rectangle, Color.Black);

            GameCore.DrawString("miniFont",Text, new Vector2(Rectangle.X + 15, Rectangle.Y-4).ToPoint(), Color.Black);
                
        }

    }
}
