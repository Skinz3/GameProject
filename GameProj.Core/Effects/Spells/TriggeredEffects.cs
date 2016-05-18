using GameProj.Core.Effects.Triggers;
using GameProj.Core.Entities;
using GameProj.Core.Environment;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Effects.Spells
{
    public class TriggeredEffects
    {
        [EffectHandler(EffectsEnum.Trap)]
        public static void Trap(StatsOwnerEntity source, SpellTemplate spell, EffectTemplate effect, StatsOwnerEntity[] affecteds, Point castPoint, DirectionsType direction)
        {
   
        }
    }
}
