using GameProj.Core.Entities;
using GameProj.Core.Stats;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.WorldEvents
{
    public class TemporaryBuffEvent : WorldEvent
    {
        ReflectedField Stat { get; set; }

        public int AddedValue { get; set; }

        StatsOwnerEntity Entity { get; set; }

        public TemporaryBuffEvent(StatsOwnerEntity entity,Type classType,object host,string fieldName, int addedValue)
            : base(entity.World)
        {
            this.Stat = new ReflectedField(fieldName,classType,host);
            this.AddedValue = addedValue;
            this.Stat.AddValue(addedValue);
            this.Entity = entity;
            Entity.TemporaryString(Stat.FieldName + "+ "+addedValue, Color.Purple);
        }

        public override void OnRemoved()
        {
            Stat.RemoveValue(AddedValue);
            base.OnRemoved();
        }
        public override void Update(GameTime time)
        {
            // no update needed
        }
        public override void Draw(GameTime time)
        {
           
        }
    }
}
