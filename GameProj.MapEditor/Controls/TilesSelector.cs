using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using GameProj;
using System.Text;
using GameProj.Core.Core;
using GameProj.Core;

namespace GameProj.MapEditor.Controls
{
    public class TilesSelector : Control
    {
        public Dictionary<Rectangle, Sprite> Sprites = new Dictionary<Rectangle, Sprite>();

        public int SelectedElementId = -1;

        public TilesSelector(List<string> sprites,Point position)
        {
            this.Rectangle = new Rectangle(position.X, position.Y, 799, 100);
            Build(sprites);
        }
        public void Build(List<string> sprites)
        {
            GameCore.Load(sprites);
            int x = 2;
            foreach (var spriteName in sprites)
            {
                var sprite = GameCore.GetSprite(spriteName);
                Sprites.Add(new Rectangle(x, 401, 77, 77), sprite);
                x += 79;
            }
        }

        public override void UpdateEvents(MouseState state,GameTime time)
        {
            if (state.LeftButton == ButtonState.Pressed)
            {
                var data = Sprites.FirstOrDefault(x => x.Key.Intersects(new Rectangle(state.Position, new Point(10, 10))));
                if (data.Value != null)
                {
                    SelectedElementId = int.Parse(data.Value.Name);
                }
            }
            base.UpdateEvents(state, time);
        }
        public void Draw()
        {
            if (SelectedElementId != -1)
            {
                var state = Mouse.GetState();
                var sprite = GameCore.GetSprite(SelectedElementId.ToString());
                var realSprite =sprite.Resize(50);
                var rectangle  = new Rectangle(state.Position,new Point(realSprite.NewBounds.Width,realSprite.NewBounds.Height));
                GameCore.Batch.Draw(sprite.Gfx,rectangle.Calibrate(state.X,state.Y), Color.White * 0.5f);
            }
            GameCore.Batch.DrawRectangle(Rectangle, Color.Black);
            foreach (var sprite in Sprites)
            {
                GameCore.Batch.DrawRectangle(sprite.Key, Color.Black);
                GameCore.Batch.Draw(sprite.Value.Gfx, sprite.Key, Color.White);
            }
        }
    }
}
