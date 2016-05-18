using GameProj.Core.Entities;
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
    public class ULPCAnimation
    {
        public Entity Entity { get; set; }
        public Point Position
        {
            get { return Entity.Position; }
        }
        protected ULPCAnimationDefinition Definition;
        protected ExtendedSpriteBatch spriteBatch;
        protected Texture2D sprite;
        protected Point CurrentFrame;
        public bool FinishedAnimation = false;

        public Action OnEnded;

        private int _Framerate = 60;
        protected double TimeBetweenFrame = 16; // 60 fps
        protected double lastFrameUpdatedTime = 0;
        public int Framerate
        {
            get { return this._Framerate; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Framerate can't be less or equal to 0");
                if (this._Framerate != value)
                {
                    this._Framerate = value;
                    this.TimeBetweenFrame = 1000.0d / (double)this._Framerate;
                }
            }
        }


        public ULPCAnimation(Entity entity, ULPCAnimationDefinition definition,Action onEnded = null)
        {
            this.Entity = entity;
            this.Definition = definition;
            this.CurrentFrame = Point.Zero;
            this.OnEnded = onEnded;
        }
        public void Initialize()
        {
            this.Framerate = this.Definition.FrameRate;
        }

       

        public void Reset()
        {
            this.CurrentFrame = new Point();
            this.FinishedAnimation = false;
            this.lastFrameUpdatedTime = 0;
        }

        public void Update(GameTime time)
        {
            if (FinishedAnimation) return;
            this.lastFrameUpdatedTime += time.ElapsedGameTime.Milliseconds;
            if (this.lastFrameUpdatedTime > this.TimeBetweenFrame)
            {
                this.lastFrameUpdatedTime = 0;
                if (this.Definition.Loop)
                {
                    this.CurrentFrame.X++;
                    if (this.CurrentFrame.X >= this.Definition.FramesNumber)
                    {
                        this.CurrentFrame.X = 0;
                        this.CurrentFrame.Y++;
                        if (this.CurrentFrame.Y >= 1)
                            this.CurrentFrame.Y = 0;
                    }
                }
                else
                {
                    this.CurrentFrame.X++;
                    if (this.CurrentFrame.X >= this.Definition.FramesNumber)
                    {
                        this.CurrentFrame.X = 0;
                        this.CurrentFrame.Y++;
                        if (this.CurrentFrame.Y >= 1)
                        {
                            this.CurrentFrame.X = this.Definition.FramesNumber - 1;
                            this.CurrentFrame.Y = 1 - 1;
                            if (OnEnded != null)
                                OnEnded();
                            this.FinishedAnimation = true;
                        }
                    }
                }
            }
        }

        public void Draw(GameTime time)
        {

            this.spriteBatch.Draw(this.sprite,
                                  new Rectangle(this.Position.X, this.Position.Y, ULPCAnimationDefinition.ULPC_FRAME_DIMENSION.X, ULPCAnimationDefinition.ULPC_FRAME_DIMENSION.Y),
                                  new Rectangle(this.CurrentFrame.X * ULPCAnimationDefinition.ULPC_FRAME_DIMENSION.X, this.CurrentFrame.Y * ULPCAnimationDefinition.ULPC_FRAME_DIMENSION.Y, 
                                      ULPCAnimationDefinition.ULPC_FRAME_DIMENSION.X, ULPCAnimationDefinition.ULPC_FRAME_DIMENSION.Y),
                                  Color.White * (Entity.ContrastPercent / 100));
        }
    }
}
