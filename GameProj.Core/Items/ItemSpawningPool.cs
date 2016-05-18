using GameProj.Core.Core;
using GameProj.Core.Environment;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GameProj.Core.Items
{
    public class ItemSpawningPool
    {
        static List<Type> ItemsType = new List<Type>();

        static ItemSpawningPool()
        {
            foreach (var type in Assembly.GetAssembly(typeof(ItemSpawningPool)).GetTypes())
            {
                if (type.BaseType == typeof(Item))
                {
                    ItemsType.Add(type);
                }
            }
        }
        public const int SPAWN_INTERVAL = 400;

        static int ActualFrames = SPAWN_INTERVAL;

        static AsyncRandom Random = new AsyncRandom();

        public static void Update(World world,GameTime time)
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
            var index = Random.Next(0,ItemsType.Count);
            world.AddItem((Item)Activator.CreateInstance(ItemsType[index], new object[] { world, cellId }));
        }
    }
}
