
using GameProj.Core.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.MapEditor.Controls
{
    public abstract class Control
    {
        public const sbyte CURSOR_SIZE = 10;

        public Rectangle Rectangle { get; set; }
        public event Action OnMouseOver;
        public event Action OnMouseClick;

        public virtual void UpdateEvents(MouseState state, GameTime time)
        {
            if (Rectangle.Intersects(new Rectangle(state.Position.X, state.Position.Y,(int)CURSOR_SIZE,(int)CURSOR_SIZE)))
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
    }
}
