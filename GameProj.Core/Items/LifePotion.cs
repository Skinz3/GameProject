using GameProj.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using GameProj.Core.Environment;

namespace GameProj.Core.Items
{
    public class LifePotion : Item
    {
        public const int HEALTH_REGEN = 50;

        public LifePotion(World world, Point position) : base(world,position) { }

        public LifePotion(World world,int cellid) : base(world,cellid) { }

        public override string SpriteName
        {
            get { return "potion1"; }
        }

        public override void OnUsed(StatsOwnerEntity entity)
        {

            entity.AddHealth(HEALTH_REGEN);
            base.OnUsed(entity);
        }
    }
}
