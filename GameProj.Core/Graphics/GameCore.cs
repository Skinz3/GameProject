using GameProj.Core.Effects;
using GameProj.Core.Environment;
using GameProj.Core.Interface;
using GameProj.Core.UIRoot;
using GameProj.Lib.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameProj.Core.Graphics
{
    public class GameCore
    {
        public static string[] SpriteExtensions = new string[] { ".gif", ".png", ".jpg", ".jpeg" };

        public static ContentManager ContentManager { get; set; }

        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static ExtendedSpriteBatch Batch { get; set; }

        public static List<Sprite> Sprites = new List<Sprite>();

        static List<Sprite> LoadedSprites = new List<Sprite>();

        public static UIManager UIManager { get; set; }

        static Color TeintColor = Color.White;

        public static void Initialize(ContentManager content, ExtendedSpriteBatch batch)
        {
            ContentManager = content;
            GSXManager.InitializeManagers();
            Batch = batch;
            LoadFonts();
            LoadSprites();
            UIManager = new UIManager();
            ShapesProvider.Initialize();
            EffectsHandler.Initialize();
        }
        static void LoadSprites()
        {
            foreach (var file in Directory.GetFiles(ContentManager.RootDirectory))
            {
                AddSprite(file);
            }
            foreach (var directory in Directory.GetDirectories(ContentManager.RootDirectory))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    AddSprite(file);
                }
            }
        }
        static void LoadFonts()
        {
            Fonts.Add("miniFont", ContentManager.Load<SpriteFont>("miniFont"));
            Fonts.Add("Smart", ContentManager.Load<SpriteFont>("Smart"));
            Fonts.Add("SmartBig", ContentManager.Load<SpriteFont>("SmartBig"));
            Fonts.Add("overmini", ContentManager.Load<SpriteFont>("overmini"));
        }
        public static SpriteFont GetFont(string name)
        {
            return Fonts.FirstOrDefault(x => x.Key == name).Value;
        }
        public static List<string> GetSubfoldersNames()
        {
            List<string> results = new List<string>();
            foreach (var sprite in Sprites)
            {
                string dirName = Path.GetDirectoryName(sprite.Path);
                if (dirName != "Content")
                    results.Add(dirName.Split('\\').Last());
            }
            return results.Distinct().ToList();
        }
        public static void DrawString(string fontName, string text, Point position, Color color)
        {
            Batch.DrawString(Fonts.First(x => x.Key == fontName).Value, text, position.ToVector2(), color);
        }
        public static void Unload(string name)
        {
            Sprite sprite = Sprites.Find(x => x.Name == name);
            sprite.Unload();
            LoadedSprites.Remove(sprite);
        }
        public static Sprite Load(string name)
        {
            Sprite sprite = Sprites.Find(x => x.Name == name);
            if (sprite == null)
                return null;
            sprite.TryLoad();
            LoadedSprites.Add(sprite);
            return sprite;
        }
        public static void Load(IEnumerable<string> spritesNames)
        {
            foreach (string name in spritesNames)
            {
                Sprite sprite = Sprites.Find(x => x.Name == name);
                sprite.TryLoad();
                LoadedSprites.Add(sprite);
            }
        }
        public static void Unload(IEnumerable<string> spritesNames)
        {
            foreach (string name in spritesNames)
            {
                Sprite sprite = Sprites.Find(x => x.Name == name);
                sprite.Unload();
                LoadedSprites.Remove(sprite);
            }

        }
        static void AddSprite(string file)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            string extension = Path.GetExtension(file);

            if (SpriteExtensions.Contains(extension))
            {
                Sprite sprite = new Sprite(name, extension, file);
                Sprites.Add(sprite);
            }
        }
        public static void OnExit(World world)
        {
            var player = world.GetPlayer();
            GSXManager.SaveData.MapName = world.Map.Template.MapName;
            GSXManager.SaveData.CellId = player.Cell.Id;
            GSXManager.SaveData.Stats = player.Stats;
            GSXManager.SaveData.Spells = player.Spells.ConvertAll<int>(x => x.Id);
            GSXManager.SaveData.Save();
        }
        public static Texture2D ReadTexture(string file)
        {
            return Texture2D.FromStream(Batch.GraphicsDevice, new MemoryStream(File.ReadAllBytes(file)));
        }
        public static Sprite GetSprite(string name)
        {
            return LoadedSprites.Find(x => x.Name == name);
        }
        public static void SetTeint(Color color)
        {
            TeintColor = color;
        }
        public static void Draw(GameTime time)
        {
            Batch.Begin();
            if (TeintColor != Color.White)
                Batch.FillRectangle(Batch.GraphicsDevice.Viewport.Bounds, TeintColor * 0.1f);
            UIManager.Draw(time);
          
            Batch.End();
        }
        public static void Update(GameTime time)
        {
            UIManager.Update(time);
        }
    }
}
