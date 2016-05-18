using GameProj.Core.Entities;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Effects.Spells
{
    public class DamagesEffects
    {
        [EffectHandler(EffectsEnum.MagicDamages)]
        public static void MagicDamages(StatsOwnerEntity source, SpellTemplate spell, EffectTemplate effect,StatsOwnerEntity[] affecteds,Point castPoint,DirectionsType direction)
        {  
            foreach (var affected in affecteds)
            {
                int data =  source.CalculateMagicDamages(effect.Min, effect.Max);
                affected.TakesDamages(source,data,true);
            }
        }
        public static void StrenghtDamages(StatsOwnerEntity source, SpellTemplate spell, EffectTemplate effect,StatsOwnerEntity[] affecteds,Point castPoint,DirectionsType direction)
        {
            foreach (var affected in affecteds)
            {
                int data = source.CalculateStrenghtDamages(effect.Min, effect.Max);
                affected.TakesDamages(source, data, false);
            }
        }
    }
}
