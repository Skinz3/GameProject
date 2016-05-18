using GameProj.Core.Effects;
using GameProj.Core.Environment;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Entities.Projectiles
{
    public abstract class Projectile : Entity
    {
        public List<int> TouchedEntityId = new List<int>();

        public Projectile(World world, string spriteName, Point position, DirectionsType direction, StatsOwnerEntity master, SpellTemplate impactSpell, int speed, bool @break)
            : base(world, spriteName, position)
        {
            this.Direction = direction;
            this.Master = master;
            this.ImpactSpell = impactSpell;
            this.Speed = speed;
            this.Break = @break;

           
        }
        public void AdaptView()
        {
            var dimensions = GetDimensions();
            switch (Direction)
            {
                case DirectionsType.UP:
                    RotationOrigin = new Vector2(dimensions.X, 0);
                    Rotation = -((float)Math.PI / 2f);
                    break;
                case DirectionsType.LEFT:
                    this.SpriteEffect = SpriteEffects.FlipHorizontally;
                    break;
                case DirectionsType.DOWN:
                    RotationOrigin = new Vector2(0, dimensions.X);
                    Rotation = ((float)Math.PI / 2f);
                    break;
                case DirectionsType.RIGHT:
                    break;
                case DirectionsType.UPRIGHT:
                    break;
                case DirectionsType.UPLEFT:
                    break;
                case DirectionsType.DOWNRIGHT:
                    break;
                case DirectionsType.DOWNLEFT:
                    break;
                default:
                    break;
            }
        }

        public StatsOwnerEntity Master { get; set; }

        public SpellTemplate ImpactSpell { get; set; }

        public int Speed { get; set; }

        public bool Break { get; set; }

        public override Point GetCellPoint()
        {
            return Position;
        }
        public abstract Point GetDimensions();

        public override void Update(GameTime time)
        {
            if (!World.Map.Renderer.IsPointOnBounds(Position))
            {
                World.RemoveEntity(this);
                return;
            }
            switch (Direction)
            {
                case DirectionsType.UP:
                      Position.Y -= Speed;
                    break;
                case DirectionsType.LEFT:
                    Position.X -= Speed;
                    break;
                case DirectionsType.DOWN:
                    Position.Y += Speed;
                    break;
                case DirectionsType.RIGHT:
                    Position.X += Speed;
                    break;
                case DirectionsType.UPLEFT:
                    Position.X -= Speed;
                    Position.Y -= Speed;
                    break;
                case DirectionsType.UPRIGHT:
                    Position.X += Speed;
                    Position.Y -= Speed;
                    break;
                case DirectionsType.DOWNLEFT:
                    Position.Y += Speed;
                    Position.X -= Speed;
                    break;
                case DirectionsType.DOWNRIGHT:
                    Position.Y += Speed;
                    Position.X += Speed;
                    break;
            }
            if (Cell != null && Cell.HasProjectilStopper() && Break)
            {
                World.RemoveEntity(this);
                return;
            }
            foreach (var entity in World.GetEntities<LCPAnimatedEntity>(x => !x.Dead))
            {
                if (entity != Master)
                {
                    if (entity.SafeHitBox.Intersects(HitBox))
                    {
                        if (!TouchedEntityId.Contains(entity.Id))
                        {
                            EffectsHandler.DirectHandle(Master, ImpactSpell, entity, Position,0);
                            TouchedEntityId.Add(entity.Id);
                        }
                        if (Break)
                            World.RemoveEntity(this);
                        break;
                    }
                }
            }

            base.Update(time);
        }
        public override void Draw(GameTime time)
        {
            base.Draw(time);
        }
    }
}
