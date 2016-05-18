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

namespace GameProj.Core.Animations.ULCP
{
    public class ULPCEntityAnimation : IObject
    {
        public Sprite SpriteSheet { get; set; }

        public List<ULCPFullAnimations> Animations = new List<ULCPFullAnimations>();

        public KeyValuePair<DirectionsType,Animator> CurrentAnimation { get; set; }


        public ULPCEntityAnimation(LCPAnimatedEntity entity, Sprite sprite)
        {
            this.SpriteSheet = sprite;
            ULCPBuilter.Build(this, entity);
        }

        public void PlayAnimation(EntityAnimationType type, DirectionsType direction)
        {
            var anim = Animations.Find(x => x.AnimationType == type);
            CurrentAnimation = anim.Anims.First(x => x.Key == direction);

        }
        public void Reset(EntityAnimationType type)
        {
            foreach (var animation in Animations.Find(x => x.AnimationType == type).Anims.Values)
            {
                animation.Reset();
            }

        }
        public void Draw(GameTime time)
        {
            CurrentAnimation.Value.Draw(time);
        }

        public void Update(GameTime time)
        {
            CurrentAnimation.Value.Update(time);
        }
    }
}
