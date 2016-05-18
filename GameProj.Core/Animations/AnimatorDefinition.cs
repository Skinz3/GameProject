using GameProj.Core.Graphics;
using GameProj.Lib.Managers;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProj.Core.Animations
{
    public class AnimatorDefinition
    {
        public static Point ULPC_FRAME_DIMENSION = new Point(64, 64);


        public Sprite Sprite { get; set; }
        public int FramesNumber { get; set; }
        public Point Dimension { get; set; }
        public int FrameRate { get; set; }
        public bool Loop { get; set; }
        public int StartFrameLine { get; set; }

        public AnimatorDefinition(Sprite sprite, Point dimension, int framesnumber, int frameRate, bool loop, int startframeline)
        {
            this.Sprite = sprite;
            this.StartFrameLine = startframeline;
            this.FramesNumber = framesnumber;
            this.Dimension = dimension;
            this.FrameRate = frameRate;
            this.Loop = loop;
        }
        public static AnimatorDefinition FromTemplate(int animationId)
        {
            AnimationTemplate template = GSXManager.GetElement<AnimationTemplate>(x => x.Id == animationId);
            return FromTemplate(template);
        }
        public static AnimatorDefinition FromTemplate(AnimationTemplate template)
        {
            return new AnimatorDefinition(GameCore.Load(template.SpriteName), new Point(template.FrameWidth, template.FrameHeight), template.FramesNumber, template.FrameRate,
                template.Loop, template.StartYLine);
        }
    }
}
