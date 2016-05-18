using GameProj.Core.Entities;
using GameProj.Core.WorldEvents;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Effects.Spells
{
    public class BuffEffects
    {
        [EffectHandler(EffectsEnum.AddSpeed)]
        public static void AddSpeed(StatsOwnerEntity source, SpellTemplate spell, EffectTemplate effect, StatsOwnerEntity[] affecteds, Point castPoint, DirectionsType direction)
        {
            TemporaryBuffEvent buff = new TemporaryBuffEvent(source, typeof(LCPAnimatedEntity), source, "Speed", effect.DiceRandom());
            source.World.AddEvent(buff, spell.AnimationDuration);
        }

        [EffectHandler(EffectsEnum.AddMagic)]
        public static void AddMagic(StatsOwnerEntity source, SpellTemplate spell, EffectTemplate effect, StatsOwnerEntity[] affecteds, Point castPoint, DirectionsType direction)
        {
            TemporaryBuffEvent buff = new TemporaryBuffEvent(source,typeof(StatsTemplate),source.Stats,"MagicPower",effect.DiceRandom());
            source.World.AddEvent(buff, spell.AnimationDuration);
        }
    }
}
