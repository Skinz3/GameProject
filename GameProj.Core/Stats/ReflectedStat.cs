using GameProj.Lib.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GameProj.Core.Stats
{
    public class ReflectedField
    {
        public ReflectedField(string fieldName,Type classType,object host)
        {
            this.Field = classType.GetField(fieldName);
            if (Field == null)
                throw new Exception("Field " + fieldName + " dosent exist in "+classType.Name+" class");
            this.Host = host;
        }
        public string FieldName { get { return Field.Name; } }
        public FieldInfo Field { get; set; }
        public Object Host { get; set; }

        public void AddValue(int value)
        {
            var added = (int)((int)(Field.GetValue(Host)) + value);
            Field.SetValue(Host, added);
        }
        public void RemoveValue(int value)
        {
            var added = (int)((int)(Field.GetValue(Host)) - value);
            Field.SetValue(Host, added);
        }
        public void SetValue(int value)
        {
            Field.SetValue(Host, value);
        }
        public int GetValue()
        {
            return (int)Field.GetValue(Host);
        }
    }
}
