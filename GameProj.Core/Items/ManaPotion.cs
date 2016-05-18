using GameProj.Core.Entities;
using GameProj.Core.Environment;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Items
{
   public class ManaPotion : Item
    {
       public const int MANA_REGEN = 50;
             
       public ManaPotion(World world, Point position) : base(world,position) { }

       public ManaPotion(World world,int cellid) : base(world,cellid) { }

       public override string SpriteName
        {
            get { return "potion2";}
        }
       public override void OnUsed(StatsOwnerEntity entity)
       {

           entity.AddMana(MANA_REGEN);
           base.OnUsed(entity);
       }
    }

}
