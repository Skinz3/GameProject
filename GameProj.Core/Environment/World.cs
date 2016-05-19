using GameProj.Core.Animations;
using GameProj.Core.Core;
using GameProj.Core.Effects.Triggers;
using GameProj.Core.Entities;
using GameProj.Core.Entities.Monsters;
using GameProj.Core.Entities.Projectiles;
using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using GameProj.Core.Items;
using GameProj.Core.WorldEvents;
using GameProj.Lib.Controls;
using GameProj.Lib.Enums;
using GameProj.Lib.Managers;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Environment
{
    public class World : IObject
    {
        public ExtendedSpriteBatch Batch { get; set; }
        public Map Map { get; set; }

        List<Entity> Entities = new List<Entity>();
        public UIDProvider EntityUIDProvider = new UIDProvider();

        List<WorldEvent> Events = new List<WorldEvent>();

        List<Trigger> Triggers = new List<Trigger>();

        List<Item> Items = new List<Item>();

        public Player GetPlayerFromSaveData()
        {
            return new Player(this, "sheet", GSXManager.SaveData.CellId, TestStock.GetStats(), SpellTemplate.ConvertAll(GSXManager.SaveData.Spells));
        }

        public World(ExtendedSpriteBatch batch)
        {
            this.Batch = batch;
            Map = new Map(this,TestStock.GetMap()); //  GSXManager.GetElement<MapTemplate>(x => x.MapName == GSXManager.SaveData.MapName)
            AddItem(new LifePotion(this,new Point(50, 400)));
            Map.OnCellOver += Map_OnCellOver;

            var player = GetPlayerFromSaveData();
            AddEntity(player);
            //AsyncRandom random = new AsyncRandom();
            //for (int i = 0; i < 2; i++)
            //{
            //   AddEntity(new Monster(this, "sheet2",random.Next(0,Map.Cells.Count()),TestStock.GetStats()));
            //}
            //AddEntity(new Monster(this, "sheet3", random.Next(0, Map.Cells.Count()), TestStock.GetStats()));


        }
        public void Reset()
        {
            Entities.Clear();
            Entities.Add(GetPlayerFromSaveData());
            Events.ForEach(x => x.Remove());
        }
        public void TemporaryString(string content, Color color, float transparencyDecr = 1)
        {
            Point position = Point.Zero;
            position.X = (int)CameraManager.Cam.Position.X + GameCore.Batch.GraphicsDevice.Viewport.Width / 2;
            position.Y = (int)CameraManager.Cam.Position.Y + GameCore.Batch.GraphicsDevice.Viewport.Height / 2;
            Vector2 size = GameCore.GetFont("SmartBig").MeasureString(content);
            AddEvent(new TemporaryString(this, position.SubCalibrate((int)size.X, (int)size.Y), "SmartBig", content, color, transparencyDecr),1);
        }
        void Map_OnCellOver(Cell obj)
        {
            obj.DrawArea(Color.Red * 0.5f);
            obj.DrawBorders(Color.Red);
            Console.WriteLine(obj.Id);
        }

        public void Draw(GameTime time)
        {

            GameCore.Batch.Begin(
             SpriteSortMode.Deferred,
             BlendState.AlphaBlend,
             SamplerState.PointClamp,
             null,
             null,
             null,
             CameraManager.Cam.Transformation);

            Map.DrawBackground(time);

            Triggers.Draw(time);
            Entities.Draw(time);
            Items.Draw(time);
            Map.DrawForeground();
            Events.Draw(time);

            GameCore.Batch.End();

        }
        /// <summary>
        /// Rafraichit le monde (Fonction appelée 60 fois par secondes)
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            Entities.Update(time); // Met a jour les entités du jeu
            Items.Update(time);  // Met a jour la position, et le status des items sur la carte
            ItemSpawningPool.Update(this, time); // Met a jour le spawn automatique d'item
            MonstersSpawningPool.Update(this, time); // Met a jour le spawn automatique de monstres
            CooldownHandler.Update(time); // Rafraîchit les actions en attente
        }
        public void AddEntity(Entity entity)
        {
            this.Entities.Add(entity);
        }
        public void RemoveEntity(Entity entity)
        {
            this.Entities.Remove(entity);
        }
        public List<Entity> GetEntities()
        {
            return Entities.FindAll(x => !x.Dead);
        }
        public List<Entity> GetEntities(int cellid, bool deaths = false)
        {
            return Entities.FindAll(x => x.Cell != null && x.Cell.Id == cellid && x.Dead == deaths);
        }
        public List<T> GetEntities<T>(Predicate<T> predicate = null) where T : Entity
        {
            return predicate == null ? Entities.OfType<T>().ToList() : Entities.OfType<T>().ToList().FindAll(predicate);
        }
        /// <summary>
        /// Add a world event
        /// </summary>
        /// <param name="event"></param>
        /// <param name="duration">en millisecondes ( -1 = infinie)</param>
        public void AddEvent(WorldEvent @event, int duration = -1)
        {
            if (duration >= 0)
                CooldownHandler.New(@event.Remove, duration);

            this.Events.Add(@event);
        }
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
        public void AddTrigger(Trigger trigger)
        {
            this.Triggers.Add(trigger);
        }
        public void AddItem(Item item)
        {
            this.Items.Add(item);
        }
        public void RemoveTrigger(Trigger trigger)
        {
            Triggers.Remove(trigger);
        }
        public void RemoveEvent(WorldEvent @event)
        {
            this.Events.Remove(@event);
        }
        public Player GetPlayer()
        {
            return Entities.Find(x => x is Player) as Player;
        }
    }
}
