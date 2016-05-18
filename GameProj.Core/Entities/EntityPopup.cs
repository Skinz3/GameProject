using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Entities
{
    public class EntityPopup : IObject
    {
        public const int POPUP_WIDTH = 100;
        public const int POPUP_HEIGHT = 20;

        public StatsOwnerEntity Entity { get; set; }

        public Rectangle Bounds = new Rectangle();

        public Rectangle LifeRectangle = new Rectangle();

        public EntityPopup(StatsOwnerEntity entity)
        {
            this.Entity = entity;
            Bounds = new Rectangle(Entity.Position, new Point(POPUP_WIDTH, POPUP_HEIGHT));
        }

        public void Draw(GameTime time)
        {
            GameCore.Batch.DrawRectangle(Bounds, Color.Black);
            GameCore.DrawString("overmini", Entity.GetName() + " (lvl "+Entity.Stats.Level+")", new Point(Bounds.X, Bounds.Y), Color.Black);

            LifeRectangle = Bounds;
            LifeRectangle.X += 1;
            LifeRectangle.Height = Bounds.Height / 2;
            LifeRectangle.Y += Bounds.Height / 2;
            int actualPercentage = (sbyte)((double)Entity.Stats.LifePoints / (double)Entity.Stats.MaxLifePoints * (double)100);
            LifeRectangle.Width = ((int)((double)actualPercentage / (double)100 * (double)Bounds.Width))-1;

            GameCore.Batch.FillRectangle(LifeRectangle, GetLifeColor(actualPercentage));
        }
        public Color GetLifeColor(int percentage)
        {
            Color color = Color.Green * 0.5f;
            if (percentage <= 50)
                color = Color.Orange * 0.5f;
            if (percentage <= 25)
                color = Color.Red * 0.5f;

            return color;
        }
        public void Update(GameTime time)
        {
            Bounds.X = Entity.GetCellPoint().X - POPUP_WIDTH / 2;
            Bounds.Y = Entity.Position.Y - POPUP_HEIGHT /2;
           
        }
    }
}
