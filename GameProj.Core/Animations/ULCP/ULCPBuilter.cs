using GameProj.Core.Entities;
using GameProj.Core.Graphics;
using GameProj.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProj.Core.Animations.ULCP
{
    public class ULCPBuilter
    {
        public const int FRAME_RATE = 14;

        public static void Build(ULPCEntityAnimation eanimation, LCPAnimatedEntity entity)
        {
            eanimation.Animations.Add(GetLCPFullAnimation(EntityAnimationType.WALK, entity, eanimation.SpriteSheet,9, 8,true,null));
            eanimation.Animations.Add(GetLCPFullAnimation(EntityAnimationType.SPELL_CAST, entity, eanimation.SpriteSheet, 7, 0, false,entity.OnSpellCast,1));
            eanimation.Animations.Add(GetLCPFullAnimation(EntityAnimationType.SHOOT, entity, eanimation.SpriteSheet, 13, 16,false, entity.OnShoot,1));
            eanimation.Animations.Add(GetLCPFullAnimation(EntityAnimationType.THRUST, entity, eanimation.SpriteSheet, 8, 4, false,entity.OnThrust,1));
            eanimation.Animations.Add(GetLCPFullAnimation(EntityAnimationType.SLASH, entity, eanimation.SpriteSheet, 6, 12, false, entity.OnSlash, 1));
            eanimation.Animations.Add(GetDieAnimation(entity,eanimation.SpriteSheet,entity.OnDie));
        }
        static ULCPFullAnimations GetDieAnimation(LCPAnimatedEntity entity,Sprite spritesheet,Action onEnded)
        {
            Dictionary<DirectionsType, Animator> dictionnary = new Dictionary<DirectionsType, Animator>();
            DirectionsType[] directions = Enum.GetValues(typeof(DirectionsType)) as DirectionsType[];
            foreach (var direction in directions)
            {
                dictionnary.Add(direction, new Animator(entity, new AnimatorDefinition(spritesheet,AnimatorDefinition.ULPC_FRAME_DIMENSION,FRAME_RATE, 6, false, 20), onEnded, 80));
            }
            return new ULCPFullAnimations(EntityAnimationType.DIE, dictionnary);
          
        }
        static ULCPFullAnimations GetLCPFullAnimation(EntityAnimationType type, LCPAnimatedEntity entity, Sprite spritesheet, int framesnumber, int startFrameLine, bool loop,Action onEnded, int onEndedDelay = 0)
        {
            Dictionary<DirectionsType, Animator> dictionnary = new Dictionary<DirectionsType, Animator>();
            DirectionsType[] directions = Enum.GetValues(typeof(DirectionsType)) as DirectionsType[];
            for (int i = startFrameLine; i < startFrameLine + 4; i++)
			{

                dictionnary.Add(directions[i - startFrameLine], new Animator(entity, new AnimatorDefinition(spritesheet, AnimatorDefinition.ULPC_FRAME_DIMENSION, framesnumber,FRAME_RATE, loop, i), onEnded, onEndedDelay));
			}
            return new ULCPFullAnimations(type, dictionnary);
        }
 
    }
}
