using GameProj.Core.Environment;
using GameProj.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Entities.Monsters
{
    class ActionsHelper
    {
        public static void MoveToPlayer(LCPAnimatedEntity entity,World world)
        {
            var player = world.GetPlayer();
            if (!entity.Dead)
            {
                if (player.Position.X <= entity.Position.X)
                {
                    if (player.Position.X == entity.Position.X)
                        entity.EntityAnimation.PlayAnimation(Lib.Enums.EntityAnimationType.WALK, DirectionsType.UP);
                    else
                        entity.EntityAnimation.PlayAnimation(Lib.Enums.EntityAnimationType.WALK, DirectionsType.LEFT);
                    entity.Position.X -= entity.Speed;
                }
                else
                {
                    if (player.Position.X == entity.Position.X)
                        entity.EntityAnimation.PlayAnimation(Lib.Enums.EntityAnimationType.WALK, Lib.Enums.DirectionsType.DOWN);
                    else
                        entity.EntityAnimation.PlayAnimation(Lib.Enums.EntityAnimationType.WALK, Lib.Enums.DirectionsType.RIGHT);
                    entity.Position.X += entity.Speed;
                }
                if (player.Position.Y <= entity.Position.Y)
                    entity.Position.Y -= entity.Speed;
                else
                    entity.Position.Y += entity.Speed;
            }
        }
    }
}
