using GameProj.Core.Animations;
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Core.WorldEvents;
using GameProj.Lib.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using GameProj.Core.Core;
using System.Text;
using GameProj.Lib.Templates;
using GameProj.Lib.Managers;

namespace GameProj.Core.Entities.Projectiles
{
    public class AnimatedProjectile : Projectile
    {
        public Animator Animation { get; set; }

        public override Rectangle HitBox
        {
            get
            {
                return new Rectangle(Position, Animation.Definition.Dimension);
            }
        }
        public AnimatedProjectile(StatsOwnerEntity master, Point position, DirectionsType direction, SpellTemplate spell, SpellTemplate impactSpell, int animationId, int speed, bool @break)
            : base(master.World, null, position, direction, master, impactSpell, speed, @break)
        {
            this.Position.Y -= master.HitBox.Height / 2;
            this.Position.X -= master.HitBox.Width / 2;
            AnimatorDefinition def = AnimatorDefinition.FromTemplate(GSXManager.GetElement<AnimationTemplate>(x => x.Id == animationId));
            this.Animation = new Animator(this, def);
            this.AdaptView();
        }
        public override Point GetCellPoint()
        {
            return Position;
        }

        public override void Draw(GameTime time)
        {
         //   GameCore.Batch.DrawRectangle(HitBox, Color.Red);
            Animation.Draw(time);
        }

        public override void Update(GameTime time)
        {
            Animation.Update(time);
            base.Update(time);
        }
        public override string GetName()
        {
            return Master.GetName();
        }

        public override Point GetDimensions()
        {
            return Animation.Definition.Dimension;
        }
    }
}
