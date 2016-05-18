using GameProj.Core.Core;
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Entities
{
    public abstract class Monster : LCPAnimatedEntity
    {
        public abstract int DefaultSpeed { get; }

        public Monster(World world,int cellid,string spriteName,StatsTemplate stats):base(world,spriteName,cellid,stats,new List<SpellTemplate>())
        {
            this.Speed = DefaultSpeed;
            EntityAnimation.PlayAnimation(EntityAnimationType.WALK, DirectionsType.DOWN);
         
        }
        public override void OnDamagesDodged()
        {
            TemporaryString("Miss", Color.Purple);
            base.OnDamagesDodged();
        }
        public override void Die(Entity killer)
        {
            var random = new AsyncRandom().Next(1 * Stats.Level,3 * Stats.Level);
            killer.AddXP(random);
            base.Die(killer);
        }
        public override void OnDamagesTaken(int amount)
        {
            SetColor(Color.DarkRed * 0.5f, 30);
            base.OnDamagesTaken(amount);
        }
        public override void OnDie()
        {
            World.RemoveEntity(this);
            base.OnDie();
        }
    }
}
