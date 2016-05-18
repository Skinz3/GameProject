using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using GameProj.Core.Core;
using System.Linq;
using System.Text;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using GameProj.Core.Controls;
using GameProj.Core.Animations;
using GameProj.Core.WorldEvents;
using GameProj.Core.Entities.Projectiles;
using GameProj.Lib.Core;
using GameProj.Lib.Managers;
using GameProj.Core.Effects;
using GameProj.Core.Effects.Spells;
using GameProj.Lib;
using GameProj.Core.Items;
using GameProj.Core.Entities.Monsters;

namespace GameProj.Core.Entities
{
    public class Player : LCPAnimatedEntity
    {

        public int XP = 0;
        
      
        public Player(World world, string spriteName, int cellid, StatsTemplate stats,List<SpellTemplate> spells)
            : base(world, spriteName, cellid, stats,spells)
        {
            CameraManager.Lock(Position);
            this.Speed = 3;
            World.TemporaryString("Now Survive!", Color.Purple);
       
        }
      
        public override void Draw(GameTime time)
        {
           
            base.Draw(time);

        }
        public override void OnDie()
        {

            World.Reset();
            base.OnDie();
        }

        public override void Die(Entity killer)
        {
            World.TemporaryString("YOU ARE DEAD", Color.DarkRed,0.5f);
            base.Die(killer);
        }
        public void HandleLife()
        {
            var state = Keyboard.GetState();
            var mstate = Mouse.GetState();

            Walking = false;
            
            if (state.IsKeyDown(Keys.Q))
            {
                this.Direction = DirectionsType.LEFT;
                TryMove(-Speed, 0);

            }
            if (state.IsKeyDown(Keys.D))
            {
                this.Direction = DirectionsType.RIGHT;
                TryMove(Speed, 0);
            }
            if (state.IsKeyDown(Keys.Z))
            {
                this.Direction = DirectionsType.UP;
                TryMove(0, -Speed);
            }
            if (state.IsKeyDown(Keys.S))
            {
                this.Direction = DirectionsType.DOWN;
                TryMove(0, Speed);

            }
            if (state.IsKeyDown(Keys.A))
            {

                World.AddEntity(new BasicMonster(World, 555));
            }
            
            CheckSpellCast(mstate);

            if (CanExecuteAction)
                EntityAnimation.PlayAnimation(EntityAnimationType.WALK, Direction);

         
        }

        public SpellTemplate SelectedSpell = null;

        void CheckSpellCast(MouseState state)
        {
            var keyBoardState = Keyboard.GetState();
            
            if (keyBoardState.IsKeyDown(Keys.D1))
            {
                SelectedSpell = TestStock.GetFireSpellTemplate();
            }
            if (keyBoardState.IsKeyDown(Keys.D2))
            {
                SelectedSpell = Spells[1];
            }
            if (keyBoardState.IsKeyDown(Keys.D3))
            {
                SelectedSpell = TestStock.GetBIGBIGSpellTemplate();
            }


            if (state.LeftButton == ButtonState.Pressed && SelectedSpell != null)
            {
                if (CanExecuteAction)
                    Cast(SelectedSpell, IsometricRenderer.Recalculate(state.Position));
            }

         
        }
        public override void OnSpellCast()
        {
          
            //AnimatorDefinition def = new AnimatorDefinition(GameCore.Load("bananaRay"), new Point(65, 80), 4, 12, true, 0);
            ////IndependantAnimation inde = new IndependantAnimation(def, Position,World, null);
            ////World.Events.Add(inde);
            //AnimationTemplate template = new AnimationTemplate(1, "bananaRay", 4, 65, 80, 12, true, 0);
            //template.Save();

            //     World.AddEntity(new AnimatedProjectile(this,World, "bananaRay", EyesPoint, EntityAnimation.CurrentAnimation.Key, 5, 9999, false, AnimatorDefinition.FromTemplate(1)));
            base.OnSpellCast();
            
        }
        public override void Update(GameTime time)
        {
            CameraManager.Lock(Position);
            if (!Dead)
                HandleLife();// zoom increment a l'arrivée

            GameCore.UIManager.GetControl<BarContent>("LifeBar").UpdateContent(Stats.LifePoints, Stats.MaxLifePoints);
            GameCore.UIManager.GetControl<BarContent>("EnergyBar").UpdateContent(Stats.Mana, Stats.MaxMana);
            GameCore.UIManager.GetControl<PlayerInformationControl>("playerInfo").UpdateInformations(this);
            if (Walking)
                base.Update(time);
            else
            {
                EntityAnimation.Reset(EntityAnimationType.WALK);
                if (!CanExecuteAction)
                    base.Update(time);
            }

        }

        public override void AddXP(int amount)
        {
            XP += amount;
            if (XP > Stats.Level * 320)
            {
                World.TemporaryString("Level UP!", Color.White, 0.1f);
                Stats.Level++;
                Stats.MaxLifePoints += 50;
                Stats.StrenghtPower += 5;
                Stats.MagicPower += 5;
                Stats.Mana += 20;
            }
            else
            {
                TemporaryString(amount + " Xp", Color.Purple);
            }
            base.AddXP(amount);
        }
        public override void OnDamagesTaken(int amount)
        {
            Invulnerable(60);
            base.OnDamagesTaken(amount);

        }
        void TryMove(int xamount, int yamount)
        {
            Walking = true;
            var pos = new Point(Position.X + xamount, Position.Y + yamount);
            if (!World.Map.Renderer.IsPointOnBounds(pos))
                return;
            var cp = GetCellPoint();
            var collisionPoint = IsometricRenderer.RecalculateWhileLocked(new Point(cp.X + xamount, cp.Y + yamount));
            var cell = World.Map.Renderer.GetCell(collisionPoint);
            if (cell != null && !cell.Walkable)
            {
                Console.WriteLine("on ne marche pas sur " + cell.Id);
                return;

            }
            this.Position = pos;

        }
        public override string GetName()
        {
            return "Skinz";
        }


    }
}
