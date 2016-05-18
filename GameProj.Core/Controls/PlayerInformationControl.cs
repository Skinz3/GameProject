using GameProj.Core.Entities;
using GameProj.Core.Graphics;
using GameProj.Lib.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Controls    
{
    public class PlayerInformationControl : Control
    {
        public int Xp { get; set; }
        public int Level { get; set; }
        public int MP { get; set; }
        public int ST { get; set; }

        public PlayerInformationControl(Point position):base("playerInfo",true)
        {
            this.Rectangle = new Rectangle(position, new Point(120, 50));

        }
        public override void Draw(GameTime time)
        {
            DrawInformations();
            base.Draw(time);
        }
        public void UpdateInformations(Player player)
        {
            this.Xp = player.XP;
            this.Level = player.Stats.Level;
            this.MP = player.Stats.MagicPower;
            this.ST = player.Stats.StrenghtPower;
        }
        void DrawInformations()
        {
            GameCore.DrawString("overmini", "Level : " + Level, new Point(Rectangle.X + 2,Rectangle.Y ), Color.Black);
            GameCore.DrawString("overmini", "Exp :" +Xp +"/"+Level*320, new Point(Rectangle.X+2,Rectangle.Y + 12), Color.Black);
            GameCore.DrawString("overmini", "MagicPower :" + MP, new Point(Rectangle.X + 2, Rectangle.Y + 24), Color.Black);
            GameCore.DrawString("overmini", "StrenghtPower :" + ST, new Point(Rectangle.X + 2, Rectangle.Y + 38), Color.Black);
        }
    }
}
