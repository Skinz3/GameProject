using GameProj.Core.Core;
using GameProj.Core.Environment;
using GameProj.Core.Interface;
using GameProj.Lib.Controls;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GameProj.Core
{
    public static class Extensions
    {
        public static void Draw<T>(this List<T> obj, GameTime time) where T : IObject
        {
            obj.ForEach(x => x.Draw(time));
        }
        public static void Update<T>(this List<T> obj, GameTime time) where T : IObject
        {
            obj.ForEach(x => x.Update(time));
        }
        public static Rectangle NoCoords(this Point size)
        {
            return new Rectangle(Point.Zero, size);
        }
        public static Rectangle GetMouseHitBox()
        {
            return new Rectangle(Mouse.GetState().Position, new Point(Control.CursorSize.Width, Control.CursorSize.Height));
        }
        public static Rectangle GetRecalculatedMouseHitBox()
        {
            return new Rectangle(IsometricRenderer.Recalculate(Mouse.GetState().Position), new Point(Control.CursorSize.Width, Control.CursorSize.Height));
        }
        /// <summary>
        /// Obtenir l'angle, pi/4 etc pour les autres directions
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public static DirectionsType GetDirectionBetwenn(this Point pos1, Point pos2)
        {
            DirectionsType actualX = 0;
            DirectionsType actualY = 0;
            var biX = pos1.X - pos2.X;
            var biY = pos1.Y - pos2.Y;
            if (pos1.X < pos2.X)
                actualX = DirectionsType.LEFT;
            else
                actualX = DirectionsType.RIGHT;

            if (pos1.Y < pos2.Y)
                actualY = DirectionsType.UP;
            else
                actualY = DirectionsType.DOWN;
            biX = Math.Abs(biX);
            biY = Math.Abs(biY);
            if (actualX == DirectionsType.RIGHT && actualY == DirectionsType.UP || actualX == DirectionsType.LEFT && actualY == DirectionsType.UP)
            {
                if (biX > biY)
                    return actualX;
                else
                    return actualY;
            }
            if (biX < biY)
            {
                if (actualX == DirectionsType.LEFT && actualY == DirectionsType.UP || actualX == DirectionsType.RIGHT && actualY == DirectionsType.UP)
                    return actualX;
                else
                    return actualY;
            }
            else
            {
                if (actualX == DirectionsType.LEFT && actualY == DirectionsType.UP || actualX == DirectionsType.RIGHT && actualY == DirectionsType.UP)
                    return actualY;
                else
                    return actualX;

                
            }
           
         
      
            





        }

        public static bool IsMultiple(this int number,int mulof)
        {
            double source = (double)number;
            double multipleof = (double)mulof;

            var rest = source / multipleof;

           var test= Math.Ceiling(rest);
           if (rest == test)
               return true;
           else
               return false;
 
        }
        /// <summary>
        /// du nimporte quoi ça
        /// </summary>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Point AddCalibrate(this Point position, int width, int height)
        {
            return new Point(position.X + width / 2, position.Y + height / 2);
        }
        public static Point SubCalibrate(this Point position, int width, int height)
        {
            return new Point(position.X - width / 2, position.Y - height / 2);
        }
        public static Point SubBottomCalibrate(this Point position, int width, int height)
        {
            return new Point(position.X - width / 2, position.Y -height);
        }
        public static Rectangle Calibrate(this Rectangle bounds, int x, int y)
        {
            return new Rectangle(x - bounds.Width / 2, y - bounds.Height / 2, bounds.Width, bounds.Height);
        }
        public static Rectangle BottomCalibrate(this Rectangle bounds, int x, int y)
        {
            return new Rectangle(x - bounds.Width / 2, y - bounds.Height, bounds.Width, bounds.Height);
        }
        /// <summary>
        /// 4.5 .net Framework Utility
        /// </summary>
        public static T GetCustomAttribute<T>(this MethodInfo method) where T : Attribute
        {
            object[] attributes = method.GetCustomAttributes(typeof(T), false);
            if (attributes.Length > 0)
                return attributes[0] as T;
            else
                return default(T);
        }
        public static T CreateDelegate<T>(this MethodInfo method)
        {
            return (T)Convert.ChangeType(Delegate.CreateDelegate(typeof(T), method), typeof(T));
        }
        public static int DiceRandom(this EffectTemplate effect)
        {
            return new AsyncRandom().Next(effect.Min, effect.Max+1);
        }
    }
}
