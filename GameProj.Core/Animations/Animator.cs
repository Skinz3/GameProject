using GameProj.Core.Entities;
using GameProj.Core.Graphics;
using GameProj.Lib.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProj.Core.Animations
{
    public class Animator
    {
        public WObject WorldObject { get; set; }
        public Point Position
        {
            get { return WorldObject.Position; }
        }
        public AnimatorDefinition Definition;
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


        public Animator(WObject @object, AnimatorDefinition definition, Action onEnded = null,int onEndedDelay = 0)
        {
            this.WorldObject = @object;
            this.Definition = definition;
            this.sprite = definition.Sprite.Gfx;
            this.OnEnded = onEnded;
            this.Framerate = this.Definition.FrameRate;
            this.CurrentFrame = new Point(0,Definition.StartFrameLine);
            this.OnEndedDelay = onEndedDelay;
            this.MaxOnEndedDelay = onEndedDelay;
        }
        int MaxOnEndedDelay { get; set; }
        int OnEndedDelay { get; set; }

        public void Reset()
        {
            this.CurrentFrame = new Point(0,Definition.StartFrameLine);
            this.FinishedAnimation = false;
            this.lastFrameUpdatedTime = 0;
            this.OnEndedDelay = MaxOnEndedDelay;
        }

        public void Update(GameTime time)
        {
            if (FinishedAnimation)
            {
                if (!Definition.Loop && OnEnded != null)
                {
                    if (OnEndedDelay == 0)
                        return;
                        OnEndedDelay--;
                    if (OnEndedDelay == 0)
                    {
                        OnEnded();
                    }
                }
                return;
            }
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
                            this.CurrentFrame.Y = Definition.StartFrameLine;
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
                            this.CurrentFrame.Y = Definition.StartFrameLine;
                            this.FinishedAnimation = true;
                        }
                    }
                }
            }
        }

        public void Draw(GameTime time)
        {
             GameCore.Batch.Draw(this.sprite,
                                  new Rectangle(this.Position.X, this.Position.Y, Definition.Dimension.X, Definition.Dimension.Y),
                                  new Rectangle(this.CurrentFrame.X * Definition.Dimension.X, this.CurrentFrame.Y * Definition.Dimension.Y,
                                      Definition.Dimension.X, Definition.Dimension.Y),
                                  WorldObject.Color* (WorldObject.ContrastPercent / 100),WorldObject.Rotation,WorldObject.RotationOrigin,WorldObject.SpriteEffect,0);
        }
    }
}
