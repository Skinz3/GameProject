using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Entities.Projectiles
{
    public class BoomrangProjectile : AnimatedProjectile
    {
        public BoomrangProjectile(StatsOwnerEntity master,Point position,DirectionsType direction,SpellTemplate spell,SpellTemplate impactSpell,int animationId,int speed,bool @break):base(master,position,direction,spell,impactSpell,animationId,speed,@break)
        {

        }
        public override void Update(GameTime time)
        {
           
            base.Update(time);
        }
    }
}
