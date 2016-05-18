using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Entities
{
    public class StaticEntity : Entity
    {
        public StaticEntity(World world,string spriteName,int cellid):base(world,spriteName,cellid)
        {

        }
        public StaticEntity(World world,string spriteName,Point position):base(world,spriteName,position)
        {

        }
        public override void Draw(GameTime time)
        {
            GameCore.Batch.Draw(Sprite.Gfx, new Rectangle(Position.X, Position.Y, Sprite.Gfx.Width, Sprite.Gfx.Height), Color * (ContrastPercent / 100));
            base.Draw(time);
        }
        public override Point GetCellPoint()
        {
            return Position;
        }
        public override string GetName()
        {
            throw new NotImplementedException();
        }

    }
}
