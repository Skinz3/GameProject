using GameProj.Core.Entities;
using GameProj.Core.Entities.Projectiles;
using GameProj.Lib.Enums;
using GameProj.Lib.Managers;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Effects.Spells
{
    public class SpawnEffects
    {
        static Point GetProjectilePoint(Entity source)
        {
            Point position = Point.Zero;

            if (source is LCPAnimatedEntity)
            {
                var lcpsource = source as LCPAnimatedEntity;
                position = lcpsource.EyesPoint;
            }
            else
                position = source.Position;

            return position;
        }
        [EffectHandler(EffectsEnum.CastProjectile)]
        public static void AddProjectiles(StatsOwnerEntity source, SpellTemplate spell, EffectTemplate effect, StatsOwnerEntity[] affecteds, Point castPoint,DirectionsType direction)
        {

            var position = GetProjectilePoint(source);
            AnimatedProjectile projectile = new AnimatedProjectile(source, position,direction, spell,
               TestStock.GetMissileExplodeTemplate(),int.Parse(effect.Optional1), int.Parse(effect.Optional2), bool.Parse(effect.Optional3));

            source.World.AddEntity(projectile);

        }
        [EffectHandler(EffectsEnum.CastBoomrang)]
        public static void AddBoom(StatsOwnerEntity source, SpellTemplate spell, EffectTemplate effect, StatsOwnerEntity[] affecteds, Point castPoint, DirectionsType direction)
        {

            var position = GetProjectilePoint(source);

            BoomrangProjectile projectile = new BoomrangProjectile(source, position, direction, spell,
               TestStock.GetMissileExplodeTemplate(), int.Parse(effect.Optional1), int.Parse(effect.Optional2), bool.Parse(effect.Optional3));


            source.World.AddEntity(projectile);

        }
    }
}
