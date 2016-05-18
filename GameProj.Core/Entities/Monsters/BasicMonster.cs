using GameProj.Core.Environment;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Entities.Monsters
{
    public class BasicMonster : Monster
    {
        public static string SPRITE_SHEET_NAME = "sheet2";

        public static StatsTemplate STATS = StatsTemplate.Default("BasicMonster",10,100,10,30);

        public override int DefaultSpeed
        {
            get { return 1; }
        }
        public BasicMonster(World world,int cellid) : base(world, cellid,SPRITE_SHEET_NAME,STATS.Clone()) { }

        public override void Update(GameTime time)
        {
            ActionsHelper.MoveToPlayer(this, World);
            if (!Dead)
            {
                foreach (var entity in World.GetEntities<Player>(x => !x.Dead))
                {

                    if (entity.SafeHitBox.Intersects(SafeHitBox))
                    {
                        entity.TakesDamages(this, 20, false);
                        break;
                    }

                }
            }
            base.Update(time);
        }
        public override string GetName()
        {
            return "Basic Monster";
        }
    }
}
