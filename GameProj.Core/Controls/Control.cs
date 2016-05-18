
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Controls
{
    public abstract class Control : IObject
    {
        public static System.Drawing.Size CursorSize = System.Windows.Forms.Cursor.Current.Size;

        public string Name { get; set; }

        public Rectangle Rectangle { get; set; }
        Color BoundsColor { get; set; }
        public bool DrawBounds { get; set; }
        public event Action OnMouseOver;
        public event Action OnMouseClick;

        public Control(string name,bool drawBounds = true) 
        {
            this.Name = name;
            this.DrawBounds = drawBounds;
            this.BoundsColor = Color.Black;
        }
        public virtual void UpdateEvents(MouseState state, GameTime time)
        {
            if (Rectangle.Intersects(new Rectangle(state.Position.X, state.Position.Y, CursorSize.Width, CursorSize.Height)))
            {
                if (OnMouseOver != null)
                    OnMouseOver();
                if (state.LeftButton == ButtonState.Pressed)
                {
                    if (OnMouseClick != null)
                        OnMouseClick();
                }
            }
        }
        public void SetBoundsColor(Color color)
        {
            BoundsColor = color;
        }
        public virtual void Draw(GameTime time)
        {
            if (DrawBounds)
                GameCore.Batch.DrawRectangle(Rectangle, BoundsColor);
        }

        public void Update(GameTime time)
        {
            var state = Mouse.GetState();
            UpdateEvents(state, time);
        }
    }
}
