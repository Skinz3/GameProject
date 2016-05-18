using GameProj.Core.Animations;
using GameProj.Core.Environment;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.WorldEvents
{
    public class IndependantAnimation : WorldEvent
    {
        public Animator Animator { get; set; }

        public IndependantAnimation(AnimatorDefinition definition,Point position, World world,Action onEnded,int onEndedDelay = 1):base(world)
        {
            this.Position = position;
            Animator = new Animator(this, definition, onEnded, onEndedDelay);
        }
        public override void Update(GameTime time)
        {
            Animator.Update(time);
        }

        public override void Draw(GameTime time)
        {
            Animator.Draw(time);
        }
    }
}
