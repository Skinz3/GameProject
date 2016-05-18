using GameProj.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Templates
{
    public class MapTemplate : GSXSerializable
    {
        public const ushort CURRENT_MAP_VERSION = 1;

        public string MapName { get; set; }

        public string MapAuthor { get; set; }

        public ushort Version { get; set; }

        public List<int> WalkableCells = new List<int>();

        public List<LayerTemplate> Layers = new List<LayerTemplate>();

        public int Width { get; set; }

        public int Height { get; set; }

        public MapTemplate() { }
        public MapTemplate(string mapName,string mapAuthor,List<int> walkable,List<LayerTemplate> layers,int width,int height)
        {
            this.MapName = mapName;
            this.MapAuthor = mapAuthor;
            this.WalkableCells = walkable;
            this.Layers = layers;
            this.Width = width;
            this.Height = height;
            this.Version = CURRENT_MAP_VERSION;
        }

        public int[] GetDistinctedElementsId()
        {
            List<int> results = new List<int>();
            foreach (var layer in Layers)
            {
                foreach (var element in layer.Elements)
                {
                    results.Add(element.ElementId);
                }
            }
            return results.Distinct().ToArray();
        }
        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort(Version);
            writer.WriteUTF(MapName);
            writer.WriteUTF(MapAuthor);
            writer.WriteInt(WalkableCells.Count);
            WalkableCells.ForEach(x => writer.WriteInt(x));
            writer.WriteInt(Layers.Count);
            Layers.ForEach(x => x.Serialize(writer));
            writer.WriteInt(Width);
            writer.WriteInt(Height);
        }

        public void Deserialize(BigEndianReader reader)
        {
            this.Version = reader.ReadUShort();
            this.MapName = reader.ReadUTF();
            this.MapAuthor = reader.ReadUTF();
            int num = reader.ReadInt();
            for (int i = 0; i < num; i++)
            {
                WalkableCells.Add(reader.ReadInt());
            }
            num = reader.ReadInt();
            for (int i = 0; i < num; i++)
            {
                Layers.Add(LayerTemplate.FromReader(reader));
            }
            this.Width = reader.ReadInt();
            this.Height = reader.ReadInt();
        }
        public override string ToString()
        {

            return MapName;
        }


        public string GetFileName()
        {
            return MapName;
        }
    }
}
