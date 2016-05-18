using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalLPC
{
    public class ULPCAnimationDefinition
    {
        public const Point ULPC_FRAME_DIMENSION = new Point(64,64);

        public Sprite Sprite { get; set; }
        public int FramesNumber { get; set; }
        public int FrameRate { get; set; }
        public bool Loop { get; set; }
        public int StartFrameLine { get; set; }

        public ULPCAnimationDefinition(Sprite sprite,int framesnumber,int frameRate,bool loop,int startframeline)
        {
            this.Sprite = sprite;
            this.StartFrameLine = startframeline;
            this.FramesNumber = framesnumber;
            this.FrameRate = frameRate;
            this.Loop = loop;
        }
    }
}
