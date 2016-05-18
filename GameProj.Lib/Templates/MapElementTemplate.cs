using GameProj.Lib.Core;
using GameProj.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Templates
{
    public class MapElementTemplate : GSXSerializable
    {
        public int UID { get; set; }

        public int CellId { get; set; }

        public int ElementId { get; set; }

        public MapElementType Type { get; set; }

        public MapElementTemplate() { }

        public bool StopProjectiles { get; set; }

        public MapElementTemplate(int uid,int cellid,int elementid,MapElementType type,bool stopprojectiles)
        {
            this.UID = uid;
            this.CellId = cellid;
            this.ElementId = elementid;
            this.Type = type;
            this.StopProjectiles = stopprojectiles;
        }

        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(this.UID);
            writer.WriteInt(this.CellId);
            writer.WriteInt(this.ElementId);
            writer.WriteInt((int)Type);
            writer.WriteBoolean(StopProjectiles);
        }

        public void Deserialize(BigEndianReader reader)
        {
            this.UID = reader.ReadInt();
            this.CellId = reader.ReadInt();
            this.ElementId = reader.ReadInt();
            this.Type = (MapElementType)reader.ReadInt();
            this.StopProjectiles = reader.ReadBoolean();
        }
        public static MapElementTemplate FromReader(BigEndianReader reader)
        {
            MapElementTemplate template = new MapElementTemplate();
            template.Deserialize(reader);
            return template;
        }


        public string GetFileName()
        {
            throw new NotImplementedException();
        }
    }
}
