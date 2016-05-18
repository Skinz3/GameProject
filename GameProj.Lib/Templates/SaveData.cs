using GameProj.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Templates
{
    public class SaveData : GSXSerializable
    {
        public string MapName = "testMap";
        public int CellId = 0;
        public StatsTemplate Stats = new StatsTemplate();
        public List<int> Spells = new List<int>();

        // items etc

        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteUTF(MapName);
            writer.WriteInt(CellId);
            Stats.Serialize(writer);
            writer.WriteInt(Spells.Count);
            Spells.ForEach(x => writer.WriteInt(x));

        }

        public void Deserialize(BigEndianReader reader)
        {
            this.MapName = reader.ReadUTF();
            this.CellId = reader.ReadInt();
            this.Stats = StatsTemplate.FromStream(reader);
            int num = reader.ReadInt();
            for (int i = 0; i < num; i++)
            {
                Spells.Add(reader.ReadInt());
            }
        }
        

        public string GetFileName()
        {
            return "save";
        }
    }
}
