using GameProj.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalLPC.LCP
{
    public class LCPAnimationType
    {
        public LCPAnimationType(EntityAnimationType type,Dictionary<DirectionsType,ULPCAnimation> anims)
        {
            this.AnimationType = type;
            this.Anims = anims;
        }
        public EntityAnimationType AnimationType { get; set; }
        public Dictionary<DirectionsType, ULPCAnimation> Anims = new Dictionary<DirectionsType, ULPCAnimation>();
    }
}
