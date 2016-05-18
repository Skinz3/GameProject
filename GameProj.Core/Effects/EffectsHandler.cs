using GameProj.Core.Entities;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GameProj.Core;
using Microsoft.Xna.Framework;

namespace GameProj.Core.Effects
{
    public static class EffectsHandler
    {
        public delegate void SpellHandlerDelegate(StatsOwnerEntity source, SpellTemplate spell, EffectTemplate effect,StatsOwnerEntity[] affecteds, Point castPoint,DirectionsType direction);

        public static Dictionary<EffectsEnum, SpellHandlerDelegate> Handlers = new Dictionary<EffectsEnum, SpellHandlerDelegate>();


        public static void DirectHandle(StatsOwnerEntity source,SpellTemplate spell,StatsOwnerEntity target,Point castPoint,DirectionsType direction)
        {
            StatsOwnerEntity[] affecteds = new StatsOwnerEntity[]{ target};
            foreach (var effect in spell.Effects)
            {
                Handle(source, spell, effect, affecteds, castPoint,direction);
            }
        }
        public static void Handle(StatsOwnerEntity source, SpellTemplate spell, EffectTemplate effect, StatsOwnerEntity[] affecteds, Point castPoint,DirectionsType direction)
        {
            var handler = Handlers.FirstOrDefault(x => x.Key == effect.EffectType);
            if (handler.Value != null)
            {
                handler.Value(source,spell,effect,affecteds,castPoint,direction);
            }
            else
            {
                Console.WriteLine(effect.EffectType + " not handled...");
            }
        }
        public static void Initialize()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(EffectsHandler));

            foreach (var type in assembly.GetTypes())
            {
                List<MethodInfo> methods = type.GetMethods().ToList().FindAll(x => x.GetCustomAttribute<EffectHandler>() != null);

                foreach (var method in methods)
                {
                    EffectHandler attribute = method.GetCustomAttribute<EffectHandler>(); 
                    Handlers.Add(attribute.Effect, (SpellHandlerDelegate)method.CreateDelegate<SpellHandlerDelegate>());
                }
            }
        }
    }
}
