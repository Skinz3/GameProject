using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Effects
{
    public class AcknoSpell 
    {
        public DirectionsType Direction { get; set; }
        public SpellTemplate Template { get; set; }
        public Point CastPoint { get; set; }
        public AcknoSpell(SpellTemplate template,DirectionsType direction,Point castPoint)
        {
            this.Template = template;
            this.Direction = direction;
            this.CastPoint = castPoint;
        }
    }
}
