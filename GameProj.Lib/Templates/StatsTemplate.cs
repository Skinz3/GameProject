using GameProj.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Templates
{
    public class StatsTemplate : GSXSerializable
    {

        public string Name;

        public int Level;

        public int LifePoints;

        public int MaxLifePoints;

        public int Mana;

        public int MaxMana;

        public int MagicPower;

        public int StrenghtPower;

        public void Serialize(BigEndianWriter writer)
        {
          
        }

        public void Deserialize(BigEndianReader reader)
        {
      
        }
        public static StatsTemplate Default(string name,int level,int lifepoints,int magic,int strenght)
        {
            return new StatsTemplate() { Name = name, Mana = 0, MaxMana = 0, Level = level,  LifePoints = lifepoints, MagicPower = magic, StrenghtPower = strenght, MaxLifePoints = lifepoints };
        }
        public static StatsTemplate FromStream(BigEndianReader reader)
        {
            var t = new StatsTemplate();
            t.Deserialize(reader);
            return t;
        }
        public StatsTemplate Clone()
        {
            return new StatsTemplate() { LifePoints = LifePoints, StrenghtPower = StrenghtPower, MaxLifePoints = MaxLifePoints, Mana = Mana, MaxMana = MaxMana, Level = Level, MagicPower = MagicPower, Name = Name };
        }
        public string GetFileName()
        {
            return Name;
        }
    }
}
