using GameProj.Lib.Core;
using GameProj.Lib.Templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GameProj.Lib.Managers
{
    public static class GSXManager
    {
        [GSX("Maps")]
        public static List<MapTemplate> Maps = new List<MapTemplate>();

        [GSX("Gfx")]
        public static List<AnimationTemplate> Animations = new List<AnimationTemplate>();

        [GSX("Spells")]
        public static List<SpellTemplate> Spells = new List<SpellTemplate>();


        public static SaveData SaveData = new SaveData();

        static Dictionary<Type, string> GSXDirectories = new Dictionary<Type, string>();

 
        public static T GetElement<T>(Predicate<T> predicate) where T : GSXSerializable
        {
            var typeOfT = typeof(T);
            foreach (var field in typeof(GSXManager).GetFields())
            {
                if (ISGSXCacheField(field))
                {
                    Type genericType = field.FieldType.GetGenericArguments()[0];
                    if (genericType == typeOfT)
                    {
                        var cache = field.GetValue(null) as List<T>;
                        return cache.Find(predicate);
                    }
                }
            }
            return default(T);
        }
       
        public static string GetDirectory(GSXSerializable obj)
        {
            var data = GSXDirectories.FirstOrDefault(x => x.Key == obj.GetType());
            if (data.Value != null)
                return data.Value;
            else
                return Environment.CurrentDirectory;
        }
        static bool ISGSXCacheField(FieldInfo field)
        {
            return field.GetCustomAttributes(typeof(GSXAttribute), false).Count() > 0;
        }
        static void LoadSave()
        {
            string path = Environment.CurrentDirectory + "/" + SaveData.GetFileName() + Constants.GameFileExtension;
            if (!File.Exists(path))
                SaveData.Save();
            else
            SaveData = (SaveData)FromFile(typeof(SaveData), SaveData.GetFileName()+Constants.GameFileExtension);
        }
        public static void InitializeManagers()
        {
            Console.WriteLine("--GSX Manager--");

            LoadSave();

            foreach (var field in typeof(GSXManager).GetFields())
            {
                if (ISGSXCacheField(field))
                {
                    int count = 0;
                    var attribute = field.GetCustomAttributes(typeof(GSXAttribute), false)[0] as GSXAttribute;
                    Type genericType = field.FieldType.GetGenericArguments()[0];
                    string dir = Environment.CurrentDirectory+"/"+ attribute.Directory+"/";
                    GSXDirectories.Add(genericType,dir);
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    foreach (var file in Directory.GetFiles(dir))
                    {
                        if (Path.GetExtension(file) == Constants.GameFileExtension)
                        {
                            var method = field.FieldType.GetMethod("Add");
                            method.Invoke(field.GetValue(null), new object[] { FromFile(genericType, file) });
                            count++;
                        }
                    }
                    Console.WriteLine(count +" "+ attribute.Directory + " Loaded..");
                }
            }
        }
        public static void Save(this GSXSerializable obj,string path)
        {
            BigEndianWriter writer = new BigEndianWriter();
            obj.Serialize(writer);
            File.WriteAllBytes(path, writer.Data);
            Console.WriteLine(obj.GetType().Name + " saved");
        }
        public static void Save(this GSXSerializable obj)
        {
            Save(obj, GetDirectory(obj) +"/"+ obj.GetFileName() +Constants.GameFileExtension);
        }
        public static GSXSerializable FromFile(Type type,string path)
        {
            if (type.GetConstructor(new Type[0]) == null)
            {
                throw new Exception("A GSXSerializable object must have a empty constructor.");
            }
            var newObj = Activator.CreateInstance(type) as GSXSerializable;
            newObj.Deserialize(new BigEndianReader(File.ReadAllBytes(path)));
            return newObj;
        }
    }
}
