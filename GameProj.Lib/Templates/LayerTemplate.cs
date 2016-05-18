using GameProj.Lib.Core;
using GameProj.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Templates
{
    public class LayerTemplate : GSXSerializable
    {
        public LayerTemplate() { }

        public LayerType LayerType { get; set; }

        public List<MapElementTemplate> Elements = new List<MapElementTemplate>();

        public LayerTemplate(LayerType type,List<MapElementTemplate> elements)
        {
            this.LayerType = type;
            this.Elements = elements;
        }
        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt((int)LayerType);
            writer.WriteInt(Elements.Count);
            Elements.ForEach(x => x.Serialize(writer));
        }

        public void Deserialize(BigEndianReader reader)
        {
            this.LayerType = (LayerType)reader.ReadInt();
            int num = reader.ReadInt();
            for (int i = 0; i < num; i++)
            {
                Elements.Add(MapElementTemplate.FromReader(reader));
            }
        }
        public static LayerTemplate FromReader(BigEndianReader reader)
        {
            LayerTemplate template = new LayerTemplate();
            template.Deserialize(reader);
            return template;
        }


        public string GetFileName()
        {
            throw new NotImplementedException();
        }
    }
}
