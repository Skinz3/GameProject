using GameProj.Core.Entities;
using GameProj.Core.Environment;
using GameProj.Core.Interface;
using GameProj.Lib.Managers;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Effects.Triggers
{
    public class Trigger : IObject
    {
        public List<Cell> Cells = new List<Cell>();

        public Cell CenterCell { get; set; }

        public Color Color { get; set; }

        public SpellTemplate ExplodeSpellTemplate { get; set; }

        public World World { get { return Master.World; } }

        public StatsOwnerEntity Master { get; set; }

        public Trigger(StatsOwnerEntity master,Cell cell, char shapeType, short radius, Color color, int explodeTemplateId)
        {
            this.Color = color;
            this.Master = master;
            this.CenterCell = cell;

            var cells = ShapesProvider.Handle(shapeType, cell.Id, cell.Id, radius);
            cells.ForEach(x => Cells.Add(World.Map.Renderer.GetCell(x)));
            this.ExplodeSpellTemplate = GSXManager.GetElement<SpellTemplate>(x => x.Id == explodeTemplateId);

        }

        public void Remove()
        {
            World.RemoveTrigger(this);
        }
        public void Draw(GameTime time)
        {
            Cells.ForEach(x => x.DrawArea(Color * 0.5f));
            Cells.ForEach(x => x.DrawBorders(Color.White * 0.5f));
        }

        public void Update(GameTime time)
        {
            foreach (var entity in World.GetEntities<StatsOwnerEntity>())
            {
                if (entity.Cell != null)
                {
                    if (Cells.Contains(entity.Cell))
                    {
                        EffectsHandler.DirectHandle(Master, ExplodeSpellTemplate, entity, entity.Cell.Center, 0);
                        this.Remove();
                    }
                }
            }
        }
    }
}
