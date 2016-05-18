using GameProj.Core.Animations;
using GameProj.Core.Entities;
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.WorldEvents
{
    public class WObjectLinkedAnimation : IndependantAnimation
    {
        public WObject WorldObject { get; set; }

        public WObjectLinkedAnimation(AnimatorDefinition definition,WObject worldObject,World world,Action onEnded,int onEndedDelay = 0):base(definition,worldObject.Position,world,onEnded,onEndedDelay)
        {
         
            this.WorldObject = worldObject;
        }
        public override void Draw(GameTime time)
        {
           
            base.Draw(time);
        }
        public override void Update(GameTime time)
        {

            this.Position = WorldObject.GetCellPoint().SubBottomCalibrate(Animator.Definition.Dimension.X, Animator.Definition.Dimension.Y);
            base.Update(time);
        }
    }
}
