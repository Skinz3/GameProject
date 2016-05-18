using GameProj.Core.Animations;
using GameProj.Core.Controls;
using GameProj.Core.Core;
using GameProj.Core.Effects;
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Core.UIRoot;
using GameProj.Core.WorldEvents;
using GameProj.Lib.Enums;
using GameProj.Lib.Managers;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Entities
{
    public abstract class StatsOwnerEntity : Entity
    {
        public bool IsInvulnerable { get; set; }

        public StatsTemplate Stats { get; set; }

        public List<SpellTemplate> Spells { get; set; }

        public EntityPopup Popup { get; set; }

        public const sbyte CRITICAL_PERCENTAGE = 20;

        public StatsOwnerEntity(World world, string spriteName, int cellid, StatsTemplate stats, List<SpellTemplate> spells)
            : base(world, spriteName, cellid)
        {
            this.Stats = stats;
            this.Spells = spells;
            this.Popup = new EntityPopup(this);
        }
        public override void OnMouseOver(GameTime time)
        {
            if (!Dead)
            {
                Popup.Update(time);
                Popup.Draw(time);
                base.OnMouseOver(time);
            }
        }
        public void UnsetInvunerable()
        {
            this.RemoveTransparency(50);
            this.IsInvulnerable = false;
        }
        public virtual void AddSpell(int id)
        {
            var template = GSXManager.GetElement<SpellTemplate>(x => x.Id == id);
            Spells.Add(template);
        }
        public void Invulnerable(int ms)
        {
            this.AddTransparency(50);
            this.IsInvulnerable = true;
            CooldownHandler.New(UnsetInvunerable, ms);
        }
        public virtual void OnDamagesDodged() { }

        public virtual void OnDamagesTaken(int amount)
        {

        }
        public virtual void Die(Entity killer)
        {
            Dead = true;
        }
        public int CalculateMagicDamages(int min, int max)
        {
            AsyncRandom random = new AsyncRandom();
            int percentage = random.Next(0, 100);
            if (percentage <= CRITICAL_PERCENTAGE)
            {
                return Stats.MagicPower * max;
            }
            else
            {
                return Stats.MagicPower * min;
            }
        }
        public int CalculateStrenghtDamages(int min, int max)
        {
            AsyncRandom random = new AsyncRandom();
            int percentage = random.Next(0, 100);
            if (percentage <= 5)
            {
                return Stats.StrenghtPower * max;
            }
            else
            {
                return Stats.StrenghtPower * min;
            }
        }
        public void TakesDamages(Entity source, int amount, bool magic)
        {
            if (IsInvulnerable)
            {
                OnDamagesDodged();
                return;
            }

            if (Stats.LifePoints - amount < 0)
                Stats.LifePoints = 0;
            else
                Stats.LifePoints -= amount;

            if (magic)
                TemporaryString("-" + amount, Color.DarkViolet);
            else
                TemporaryString("-" + amount, Color.DarkRed);

            if (Stats.LifePoints <= 0)
            {
                Die(source);
                return;
            }
            OnDamagesTaken(amount);
        }
        public bool CheckEnergy(short cost)
        {
            if (Stats.Mana - cost < 0)
            {
                TemporaryString("Energy (" + cost + ")", Color.White * 0.5f);
                return false;
            }
            Stats.Mana -= cost;
            return true;
        }



        public virtual void Cast(SpellTemplate template, Point castPoint)
        {
            if (CheckEnergy(template.ManaCost))
            {
                DirectCast(template, Position.GetDirectionBetwenn(castPoint), castPoint);
            }
        }
        Rectangle AddSpellAnimation(SpellTemplate template, Point castPoint)
        {
            if (template.IndependantAnimationId != 0)
            {
                AnimatorDefinition definition = AnimatorDefinition.FromTemplate(template.IndependantAnimationId);


                if (!template.UseMouseCalibration)
                {
                    WObjectLinkedAnimation animation = new WObjectLinkedAnimation(definition, this, World, null);
                    World.AddEvent(animation, template.AnimationDuration);
                    return new Rectangle(castPoint, definition.Dimension);
                }
                else
                {
                    var animationPos = castPoint.SubCalibrate(definition.Dimension.X, definition.Dimension.Y);
                    IndependantAnimation animation = new IndependantAnimation(definition, animationPos, World, null);
                    World.AddEvent(animation, template.AnimationDuration);
                    return new Rectangle(animationPos, definition.Dimension);
                }
            }

            return new Rectangle();
        }
        public override void Draw(GameTime time)
        {
            base.Draw(time);
        }
        public void DirectCast(SpellTemplate template, DirectionsType direction, Point castPoint)
        {

            Rectangle hitShape = AddSpellAnimation(template, castPoint);
            foreach (var effect in template.Effects)
            {
                EffectsHandler.Handle(this, template, effect, GetAffecteds(hitShape, template.AffectSelf).ToArray(), castPoint, direction);
            }
        }

        public List<StatsOwnerEntity> GetAffecteds(Rectangle hitShape, bool affectself)
        {
            List<StatsOwnerEntity> results = new List<StatsOwnerEntity>();
            foreach (var entity in World.GetEntities<StatsOwnerEntity>().FindAll(x => !x.Dead))
            {
                if (hitShape.Intersects(entity.HitBox))
                    results.Add(entity);
            }
            if (!affectself)
                results.Remove(this);
            return results;
        }
        public void AddMana(int amount)
        {
            if (Stats.Mana + amount > Stats.MaxMana)
            {
                int rest = Stats.MaxMana - Stats.Mana;
                Stats.Mana = Stats.MaxMana;
                if (rest != 0)
                    TemporaryString("+" + rest, Color.CornflowerBlue);
            }
            else
            {
                Stats.Mana += amount;
                TemporaryString("+" + amount, Color.CornflowerBlue);
            }
        }
        public void AddHealth(int amount)
        {                      
            if (Stats.LifePoints + amount > Stats.MaxLifePoints)
            {
                int rest = Stats.MaxLifePoints - Stats.LifePoints;
                Stats.LifePoints = Stats.MaxLifePoints;
                if (rest != 0)
                    TemporaryString("+" + rest, Color.Green);
            }
            else
            {
                Stats.LifePoints += amount;
                TemporaryString("+" + amount, Color.Green);
            }

        }
    }
}
