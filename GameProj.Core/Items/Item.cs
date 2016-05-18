using GameProj.Core.Entities;
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Items
{
    public abstract class Item : WObject
    {
        bool Active = false;

        bool Used = false;

        public abstract string SpriteName { get; }

        protected Sprite Sprite { get; set; }

        public Rectangle HitBox { get { return new Rectangle(Position.X, Position.Y, Sprite.Gfx.Width, Sprite.Gfx.Height); } }

        public Point DestinationPoint { get; set; }

        public World World { get; set; }

        public Item(World world,int cellid)
        {
            Cell cell = world.Map.Renderer.GetCell(cellid);
            Sprite = GameCore.Load(SpriteName);
            this.DestinationPoint = cell.Center;
            this.Position.X = cell.Center.X;
            this.World = world;
        }
        public Item(World world,Point position)
        {
            Sprite = GameCore.Load(SpriteName);
            this.DestinationPoint = position;
            this.Position.X = position.X;
            this.World = world;
        }
        public override void Update(GameTime time)
        {
            if (Used)
            {
                AddTransparency(5);
                if (ContrastPercent == 0)
                {
                    World.RemoveItem(this);
                }
            }
            else
            {

                if (!Active)
                {
                    if (Position.Y < DestinationPoint.Y)
                        Position.Y += 10;
                    else
                        Active = true;
                }
                else
                {
                    foreach (var entity in World.GetEntities<StatsOwnerEntity>())
                    {
                        if (entity.HitBox.Intersects(HitBox))
                            OnUsed(entity);
                        break;
                    }
                }
            }
        }

        public override void Draw(GameTime time)
        {
            GameCore.Batch.Draw(Sprite.Gfx, HitBox, Color.White * (ContrastPercent / 100));
        }
        public virtual void OnUsed(StatsOwnerEntity entity)
        {
            Used = true;
        }
    }
}
