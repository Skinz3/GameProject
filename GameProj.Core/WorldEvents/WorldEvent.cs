using GameProj.Core.Entities;
using GameProj.Core.Environment;
using GameProj.Core.Interface;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.WorldEvents
{
    public abstract class WorldEvent : WObject
    {
        public World World { get; set; }

        public WorldEvent(World world) // Duration Here
        {
            this.World = world;
        }
        public virtual void OnRemoved() { }
        public void Remove()
        {
            OnRemoved();
            World.RemoveEvent(this);
        }
       
    }
}
