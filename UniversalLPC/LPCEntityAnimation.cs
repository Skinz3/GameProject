using GameProj.Core.Entities;
using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using GameProj.Lib.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalLPC.LCP;

namespace UniversalLPC
{
    public class LPCEntityAnimation : IObject
    {
        public Sprite SpriteSheet { get; set; }

        public List<LCPAnimationType> Animations = new List<LCPAnimationType>();

        public ULPCAnimation Die { get; set; }


        public ULPCAnimation CurrentAnimation { get; set; }



        public LPCEntityAnimation(Entity entity, string spriteName)
        {
            GameCore.Load(spriteName);
            this.SpriteSheet = GameCore.GetSprite(spriteName);
            ULCPBuilter.Build(this,entity);
        }

        public void PlayAnimation(EntityAnimationType type, DirectionsType direction)
        {
            if (type == EntityAnimationType.DIE)
                CurrentAnimation = Die;
            else
            {
                var anim = Animations.Find(x => x.AnimationType == type);
                CurrentAnimation = anim.Anims.First(x => x.Key == direction).Value;
            }
        }

        public void Draw(GameTime time)
        {
            CurrentAnimation.Draw(time);
        }

        public void Update(GameTime time)
        {
            CurrentAnimation.Update(time);
        }
    }
}
