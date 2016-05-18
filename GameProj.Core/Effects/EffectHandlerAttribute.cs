using GameProj.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Effects
{
    public class EffectHandler : Attribute
    {
        public EffectsEnum Effect { get; set; }

        public EffectHandler(EffectsEnum effect)
        {
            this.Effect = effect;
        }
    }
}
