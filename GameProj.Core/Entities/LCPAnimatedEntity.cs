using GameProj.Core.Animations.ULCP;
using GameProj.Core.Core;
using GameProj.Core.Effects;
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Core.WorldEvents;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Entities
{
    public class LCPAnimatedEntity : StatsOwnerEntity
    {
        public ULPCEntityAnimation EntityAnimation { get; set; }

        public bool CanExecuteAction { get; set; }

        public bool Walking { get; set; }

        public AcknoSpell AcknolegementSpell { get; set; }

        public int Speed;

        public override Rectangle HitBox
        {
            get
            {
                return new Rectangle(Position.X, Position.Y, 64, 64);
            }
        }
        // 32  = LCPWidth /2 64 = LCPHeight
        public Point FootPoint { get { return new Point(HitBox.X + 32, HitBox.Y + 60); } }

        public Point ArmsPoint { get { return new Point(HitBox.X + 32, HitBox.Y + 45); } }

        public Point EyesPoint { get { return new Point(HitBox.X + 32, HitBox.Y + 30); } }

        public Rectangle SafeHitBox { get { return new Rectangle(HitBox.X + 16, HitBox.Y + 16, HitBox.Width / 2, HitBox.Height - 16); } }




        // List<SpellTemplate>

        public LCPAnimatedEntity(World world, string spriteName, int cellid, StatsTemplate stats, List<SpellTemplate> spells) // liste spell template
            : base(world, spriteName, cellid, stats, spells)
        {
            this.EntityAnimation = new ULPCEntityAnimation(this, Sprite);
            this.EntityAnimation.PlayAnimation(EntityAnimationType.WALK, DirectionsType.DOWN);
            CanExecuteAction = true;
            IsInvulnerable = false;

        }
        public override void Draw(GameTime time)
        {
            base.Draw(time);
            this.EntityAnimation.Draw(time);
        }
        public override void Update(GameTime time)
        {
            if (Dead)
                AddTransparency(1);
            this.EntityAnimation.Update(time);
            base.Update(time);
        }
        public override void Cast(SpellTemplate template, Point castPoint)
        {
            if (CheckEnergy(template.ManaCost))
            {
                this.Direction = castPoint.GetDirectionBetwenn(Position);
                EntityAnimation.PlayAnimation(template.AnimationType, Direction);
                CanExecuteAction = false;
                AcknolegementSpell = new AcknoSpell(template, Direction,castPoint);
            }

        }
        public override void Die(Entity killer)
        {
            EntityAnimation.PlayAnimation(EntityAnimationType.DIE, DirectionsType.DOWN);
            CanExecuteAction = false;
            base.Die(killer);
        }
        public override Point GetCellPoint()
        {
            return FootPoint;
        }
        public virtual void OnThrust()
        {
            EntityAnimation.Reset(EntityAnimationType.THRUST);
            EndAnimAndCast();

        }
        public virtual void OnSlash()
        {
            EntityAnimation.Reset(EntityAnimationType.SLASH);
            EndAnimAndCast();

        }
        public virtual void OnShoot()
        {
            EntityAnimation.Reset(EntityAnimationType.SHOOT);
            EndAnimAndCast();
        }
        void EndAnimAndCast()
        {
            CanExecuteAction = true;
            DirectCast(AcknolegementSpell.Template, AcknolegementSpell.Direction,AcknolegementSpell.CastPoint);
            AcknolegementSpell = null;
        }
        public virtual void OnSpellCast()
        {
            EntityAnimation.Reset(EntityAnimationType.SPELL_CAST);
            EndAnimAndCast();

        }
        public virtual void OnDie()
        {
            //     System.Environment.Exit(0);
        }
        public override string GetName()
        {
            throw new NotImplementedException();
        }

    }
}
