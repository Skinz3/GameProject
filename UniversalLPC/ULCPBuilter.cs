using GameProj.Core.Entities;
using GameProj.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalLPC.LCP;

namespace UniversalLPC
{
    public class ULCPBuilter
    {
        public const int FrameRate = 14;

        public static void Build(LPCEntityAnimation eanimation,Entity entity)
        {
            eanimation.Animations.Add(GetSpellCast(eanimation,entity));
        }

        public static LCPAnimationType GetSpellCast(LPCEntityAnimation eanim,Entity entity)
        {
            var dic = new Dictionary<DirectionsType, ULPCAnimation>();
            dic.Add(DirectionsType.DOWN, new ULPCAnimation(entity, new ULPCAnimationDefinition(eanim.SpriteSheet, 7, FrameRate, true, 0)));
            return new LCPAnimationType(EntityAnimationType.SPELL_CAST, dic);
        }
    }
}
