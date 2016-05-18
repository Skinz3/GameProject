using GameProj.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProj.Core.Animations.ULCP
{
    public class ULCPFullAnimations
    {
        public ULCPFullAnimations(EntityAnimationType type,Dictionary<DirectionsType,Animator> anims)
        {
            this.AnimationType = type;
            this.Anims = anims;
        }

        public EntityAnimationType AnimationType { get; set; }

        public Dictionary<DirectionsType, Animator> Anims = new Dictionary<DirectionsType, Animator>();
    }
}
