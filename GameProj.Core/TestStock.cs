using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using GameProj.Lib.Managers;
using System.Text;

namespace GameProj.Core
{
    public class TestStock
    {
        public static MapTemplate GetMap()
        {
            var walkables = new List<int>();
            var layers = new List<LayerTemplate>();

            var ground = new List<MapElementTemplate>();
            for (int i = 0; i < 2240; i++)
            {
                ground.Add(new MapElementTemplate(i, i, 900, MapElementType.Background, false));
                walkables.Add(i);
            }
            for (int i = 0; i < 499; i+=40)
            {
                ground.Add(new MapElementTemplate(i + 2240, i, 17982, MapElementType.Foreground,true));
                walkables.Remove(i);
            }
            ground.Add(new MapElementTemplate(0, 743, 17969, MapElementType.Foreground, true));
            ground.Add(new MapElementTemplate(0, 1115, 19587, MapElementType.Foreground, true));

            ground.Add(new MapElementTemplate(0, 1351, 19934, MapElementType.Foreground, true));
            ground.Add(new MapElementTemplate(0, 955, 19922, MapElementType.Foreground, true));
            ground.Add(new MapElementTemplate(0, 1751, 32248, MapElementType.Foreground, true));
             for (int i = 1500; i < 1700; i+=30)
            {
                ground.Add(new MapElementTemplate(i + 2240, i, 17983, MapElementType.Foreground, true));
                walkables.Remove(i);
            }
           
            layers.Add(new LayerTemplate(LayerType.Ground, ground));
            
            return new MapTemplate("test2", "skinz", walkables, layers, 14*2 , 20*2 );
        }
        public static StatsTemplate GetStats()
        {
            return new StatsTemplate() { Mana = 300, Level = 1, LifePoints = 200, MaxLifePoints = 200, MagicPower = 10, MaxMana = 100, Name = "Freako", StrenghtPower = 10 };
        }

        public static SpellTemplate GetBIGBIGSpellTemplate()
        {
         
            var effs = new List<EffectTemplate>();
            effs.Add(new EffectTemplate() { Id = 5, EffectType = EffectsEnum.MagicDamages, Min = 12, Max = 40 });
            var ap = new SpellTemplate() { Id = 5, Name = "BigFire", UseMouseCalibration = true, IndependantAnimationId = 3, IconSpriteName = "spell1", ManaCost = 80, Effects = effs, Cooldown = 2.0, AnimationDuration = 60, AffectSelf = true, AnimationType = EntityAnimationType.SPELL_CAST };
            return ap;
        }
        public static AnimationTemplate GetBigFireAnimT()
        {
            return new AnimationTemplate(5,"slash", 4, 50, 37, 20, true, 0);
        }





        public static SpellTemplate GetFireSpellTemplate()
        {
            var effs = new List<EffectTemplate>();
            effs.Add(new EffectTemplate() { Id = 1, EffectType = EffectsEnum.MagicDamages, Min = 1, Max = 4 });
            var ap = new SpellTemplate() { Id = 1, Name = "LittleFire", UseMouseCalibration = true, IndependantAnimationId = 2, IconSpriteName = "spell1", ManaCost = 20, Effects = effs, Cooldown = 2.0, AnimationDuration = 60, AffectSelf = true, AnimationType = EntityAnimationType.SPELL_CAST };
            return ap;
        }

        public static SpellTemplate GetMissileSpellTemplate()
        { // optional1 = 1
            var effs = new List<EffectTemplate>();
            effs.Add(new EffectTemplate() { Id = 2, EffectType = EffectsEnum.CastProjectile, ConstantValue = 3, Optional1 = "1", Optional2 = "4", Optional3 = "False" });
            var ap = new SpellTemplate() { Id = 2, Name = "BananaRay", UseMouseCalibration = true, IndependantAnimationId = 0, IconSpriteName = "spell2", ManaCost = 100, Effects = effs, Cooldown = 10.0, AnimationDuration = 0, AffectSelf = false, AnimationType = EntityAnimationType.SPELL_CAST };

            return ap;
        }



        public static SpellTemplate GetBoomrangSpellTemplate()
        { // optional1 = 1
            var effs = new List<EffectTemplate>();
            effs.Add(new EffectTemplate() { Id = 5, EffectType = EffectsEnum.CastBoomrang, ConstantValue = 3, Min = 1, Max = 2, Optional1 = "5", Optional2 = "6", Optional3 = "False" });
            var ap = new SpellTemplate() { Id = 5, Name = "MagicBoomRang", UseMouseCalibration = true, IndependantAnimationId = 0, IconSpriteName = "spell2", ManaCost = 100, Effects = effs, Cooldown = 10.0, AnimationDuration = 0, AffectSelf = false, AnimationType = EntityAnimationType.SHOOT };

            return ap;
        }


        public static SpellTemplate GetMissileExplodeTemplate()
        {
            var effs = new List<EffectTemplate>();
            effs.Add(new EffectTemplate() { Id = 3, EffectType = EffectsEnum.MagicDamages, Min = 7, Max = 10 });
            var ap = new SpellTemplate() { Id = 3, Name = "BananaRayExplode", Effects = effs, AnimationDuration = 0, AffectSelf = false };

            return ap;
        }
        public static SpellTemplate MagicTEST()
        {
            var effs = new List<EffectTemplate>();
            effs.Add(new EffectTemplate() { Id = 4, EffectType = EffectsEnum.AddSpeed, Min = 1, Max = 3 });
            return new SpellTemplate() { Id = 4, Effects = effs, AnimationType = EntityAnimationType.THRUST, AffectSelf = true, AnimationDuration = 300, Cooldown = 0, ManaCost = 100, IconSpriteName = "spell2", IndependantAnimationId = 5, Name = "TestMagic", UseMouseCalibration = false };
        }


    }
}
