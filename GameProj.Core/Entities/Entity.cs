using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameProj.Core.Core;
using System.Linq;
using System.Text;
using GameProj.Lib.Enums;
using GameProj.Core.WorldEvents;

namespace GameProj.Core.Entities
{
    public abstract class Entity : WObject
    {
        public int Id { get; set; }

        public DirectionsType Direction { get; set; }

        public World World { get; set; }

        public virtual Rectangle HitBox { get { return new Rectangle(Position.X, Position.Y, Sprite.Gfx.Bounds.Width, Sprite.Gfx.Bounds.Height); } }

        public bool Dead = false;

        public Sprite Sprite { get; set; }

        public Entity(World world, string spriteName, int cellid)
        {
            Init(world, spriteName);

            this.Cell = world.Map.Renderer.GetCell(cellid);
            this.Position = Cell.Center;
        }
        public Entity(World world, string spriteName, Point position)
        {
            Init(world, spriteName);
            this.Position = position;
            this.Cell = world.Map.Renderer.GetCell(position);
        }
        void Init(World world, string spriteName)
        {
            this.World = world;
            GameCore.Load(spriteName);
            this.Sprite = GameCore.GetSprite(spriteName);
            this.Id = World.EntityUIDProvider.Pop();

        }
       

        public virtual void OnMouseOver(GameTime time)
        {
          
        }
        public void SetColor(Color color, int ms)
        {
            this.Color = color;
            CooldownHandler.New(UnSetColor, ms);
        }
        public void UnSetColor()
        {
            this.Color = Color.White;
        }
        public void TemporaryString(string content, Color color, float transparencyDecr = 1)
        {
            World.AddEvent(new TemporaryString(World, Position, "Smart", content, color, transparencyDecr));
        }
        public virtual void AddXP(int amount)
        {

        }

        public override void Draw(GameTime time)
        {
            if (Extensions.GetRecalculatedMouseHitBox().Intersects(HitBox))
                OnMouseOver(time);
        }
        public override void Update(GameTime time)
        {
            Point cellPoint = GetCellPoint();
            Point pos = IsometricRenderer.RecalculateWhileLocked(cellPoint);
            var cell = World.Map.Renderer.GetCell(pos);
            if (cell != null)
                this.Cell = cell;
        }
        public abstract string GetName();



    }
}
