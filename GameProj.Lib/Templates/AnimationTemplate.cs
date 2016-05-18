using GameProj.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Templates
{
    public class AnimationTemplate : GSXSerializable
    {
        public int Id { get; set; }

        public string SpriteName { get; set; }

        public int FramesNumber { get; set; }

        public int FrameWidth { get; set; }

        public int FrameHeight { get; set; }

        public int FrameRate { get; set; }

        public bool Loop { get; set; }

        public int StartYLine { get; set; }

        public AnimationTemplate() { }

        public AnimationTemplate(int id,string spriteName,int framesNumber,int frameWidth,int frameHeight,int frameRate,bool loop,int startYLine)
        {
            this.Id = id;
            this.SpriteName = spriteName;
            this.FramesNumber = framesNumber;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.FrameRate = frameRate;
            this.Loop = loop;
            this.StartYLine = startYLine;
        }
        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(Id);
            writer.WriteUTF(SpriteName);
            writer.WriteInt(FramesNumber);
            writer.WriteInt(FrameWidth);
            writer.WriteInt(FrameHeight);
            writer.WriteInt(FrameRate);
            writer.WriteBoolean(Loop);
            writer.WriteInt(StartYLine);
        }

        public void Deserialize(BigEndianReader reader)
        {
            this.Id = reader.ReadInt();
            this.SpriteName = reader.ReadUTF();
            this.FramesNumber = reader.ReadInt();
            this.FrameWidth = reader.ReadInt();
            this.FrameHeight = reader.ReadInt();
            this.FrameRate = reader.ReadInt();
            this.Loop = reader.ReadBoolean();
            this.StartYLine = reader.ReadInt();
        }




        public string GetFileName()
        {
            return Id.ToString();
        }
    }
}
