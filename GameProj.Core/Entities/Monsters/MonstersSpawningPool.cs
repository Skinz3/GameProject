using GameProj.Core.Core;
using GameProj.Core.Environment;
using GameProj.Core.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GameProj.Core.Entities.Monsters
{
    class MonstersSpawningPool
    {
        static List<Type> MonstersType = new List<Type>();

        static MonstersSpawningPool()
        {
            foreach (var type in Assembly.GetAssembly(typeof(MonstersSpawningPool)).GetTypes())
            {
                if (type.BaseType == typeof(Monster))
                {
                    MonstersType.Add(type);
                }
            }
        }
        public const int SPAWN_INTERVAL = 100;

        static int ActualFrames = SPAWN_INTERVAL;

        static AsyncRandom Random = new AsyncRandom();

        public static void Update(World world, GameTime time)
        {
            ActualFrames -= 1;
            if (ActualFrames == 0)
            {
                Spawn(world);
                ActualFrames = SPAWN_INTERVAL;
            }
        }
        static void Spawn(World world)
        {
            int cellId = Random.Next(0, world.Map.Cells.Count());
            var index = Random.Next(0, MonstersType.Count);
            world.AddEntity((Monster)Activator.CreateInstance(MonstersType[index], new object[] { world, cellId }));
        }
    }
}
