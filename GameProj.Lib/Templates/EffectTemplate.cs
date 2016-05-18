using GameProj.Lib.Core;
using GameProj.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Templates
{
    public class EffectTemplate : GSXSerializable
    {

        public int Id { get; set; }

        public EffectsEnum EffectType { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        /// <summary>
        /// Missile Explosion SpellId
        /// </summary>
        public int ConstantValue { get; set; }

        /// <summary>
        /// MissileAnimation
        /// </summary>
        public string Optional1 = string.Empty;

        /// <summary>
        /// MissileSpeed
        /// </summary>
        public string Optional2 = string.Empty;

        /// <summary>
        /// Missile Break?
        /// </summary>
        public string Optional3 = string.Empty;

        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(Id);
            writer.WriteInt((int)EffectType);
            writer.WriteInt(Min);
            writer.WriteInt(Max);
            writer.WriteInt(ConstantValue);
            writer.WriteUTF(Optional1);
            writer.WriteUTF(Optional2);
            writer.WriteUTF(Optional3);
        }

        public void Deserialize(BigEndianReader reader)
        {
            this.Id = reader.ReadInt();
            this.EffectType = (EffectsEnum)reader.ReadInt();
            this.Min = reader.ReadInt();
            this.Max = reader.ReadInt();
            this.ConstantValue = reader.ReadInt();
            this.Optional1 = reader.ReadUTF();
            this.Optional2 = reader.ReadUTF(); 
            this.Optional3 = reader.ReadUTF(); 
        }
        public static EffectTemplate FromReader(BigEndianReader reader)
        {
            EffectTemplate t = new EffectTemplate();
            t.Deserialize(reader);
            return t;
        }
        public string GetFileName()
        {
            throw new NotImplementedException();
        }
    }
}
