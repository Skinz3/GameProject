using GameProj.Lib.Core;
using GameProj.Lib.Enums;
using GameProj.Lib.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Lib.Templates
{
    public class SpellTemplate : GSXSerializable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<EffectTemplate> Effects = new List<EffectTemplate>();

        public EntityAnimationType AnimationType { get; set; }

        public int IndependantAnimationId { get; set; }

        public int AnimationDuration { get; set; }

        public bool AffectSelf { get; set; }
        /// <summary>
        /// En secondes
        /// </summary>
        public double Cooldown { get; set; }

        public bool UseMouseCalibration { get; set; }

        public short ManaCost { get; set; }

        public string IconSpriteName = string.Empty;

        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(Id);
            writer.WriteUTF(Name);
            writer.WriteInt(Effects.Count);
            Effects.ForEach(x => x.Serialize(writer));
            writer.WriteInt((int)AnimationType);
            writer.WriteInt(IndependantAnimationId);
            writer.WriteInt(AnimationDuration);
            writer.WriteBoolean(AffectSelf);
            writer.WriteDouble(Cooldown);
            writer.WriteBoolean(UseMouseCalibration);
            writer.WriteShort(ManaCost);
            writer.WriteUTF(IconSpriteName);
        }

        public void Deserialize(BigEndianReader reader)
        {
            this.Id = reader.ReadInt();
            this.Name = reader.ReadUTF();
            int num = reader.ReadInt();
            for (int i = 0; i < num; i++)
            {
                Effects.Add(EffectTemplate.FromReader(reader));
            }
            this.AnimationType = (EntityAnimationType)reader.ReadInt();
            this.IndependantAnimationId = reader.ReadInt();
            this.AnimationDuration = reader.ReadInt();
            this.AffectSelf = reader.ReadBoolean();
            this.Cooldown = reader.ReadDouble();
            this.UseMouseCalibration = reader.ReadBoolean();
            this.ManaCost = reader.ReadShort();
            this.IconSpriteName = reader.ReadUTF();
        }

        public static List<SpellTemplate> ConvertAll(List<int> spellids)
        {
            return spellids.ConvertAll<SpellTemplate>(x => GSXManager.GetElement<SpellTemplate>(w => w.Id == x));
        }
        public string GetFileName()
        {
            return Id.ToString()+"-"+Name;
        }
    }
}
