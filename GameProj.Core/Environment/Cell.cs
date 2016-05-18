using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using GameProj.Core.Core;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameProj.Lib.Templates;
using GameProj.Lib.Enums;
using GameProj.Core.Entities;

namespace GameProj.Core.Environment
{
    public class Cell
    {
        public const sbyte ELEMENTS_SIZE_PERCENTAGE = 50;

        Dictionary<MapElementTemplate, LayerType> Elements = new Dictionary<MapElementTemplate, LayerType>();

        public Cell(int id)
        {
            Id = id;
            Walkable = false;
        }
        public void LoadElements(Dictionary<MapElementTemplate, LayerType> elements)
        {
            this.Elements = elements;
        }
        public bool HasProjectilStopper()
        {
            return Elements.Keys.FirstOrDefault(x => x.StopProjectiles == true) != null;
        }
        public void AddElement(MapElementTemplate template, LayerType type)
        {
            this.Elements.Add(template, type);
        }
        public void RemoveLastElement()
        {
            if (this.Elements.Count > 0)
                this.Elements.Remove(this.Elements.Last().Key);
        }
        public void RefreshRect()
        {
            int x = Points.Min(entry => entry.X);
            int y = Points.Min(entry => entry.Y);
            int width = Points.Max(entry => entry.X) - x;
            int height = Points.Max(entry => entry.Y) - y;
            Rectangle = new Rectangle(x, y, width, height);
        }
        /// <summary>
        /// by David Loiseaux
        /// </summary>
        public void EvaluateArea()
        {
            for (int X = -24; X < 24; X++)
            {
                for (int Y = -24; Y < 24; Y++)

                    if (Y <= (-12 / 23.5) * X + 12 && Y <= 12 / 23.5 * X + 12
                    && Y >= 12 / 23.5 * X - 12 && Y >= -12 / 23.5 * X - 12)
                    {
                        AreaPoints.Add(new Point(X + Center.X, Y + Center.Y));
                    }
            }
        }
        public List<Point> AreaPoints = new List<Point>();

        public void DrawBorders(Color color)
        {
            GameCore.Batch.DrawPolygon(Points, color);
          
        }
        public void DrawId()
        {
            GameCore.DrawString("miniFont", Id.ToString(), Center, Color.White);
        }
        public void DrawArea(Color color)
        {
            foreach (var point in AreaPoints)
            {
                GameCore.Batch.Draw(GameCore.Batch.Pixel, new Rectangle(point.X, point.Y, 1, 1), color);
            }

        }
        public void DrawCenter()
        {
            GameCore.Batch.FillRectangle(new Rectangle(Center.X - 5, Center.Y - 5, 10, 10), Color.Red);
        }
        public void DrawString(string str, SpriteFont font, ExtendedSpriteBatch batch)
        {
            batch.Begin();
            batch.DrawString(font, str, new Vector2(Center.X / 2, Center.X / 2), Color.Black);
            batch.End();
        }
        public int Id;

        private Point[] m_points;


        public bool Walkable;

        public Point[] VerticalDiagonal { get { return new Point[] { Points[1], Points[3] }; } }
        public Point[] HorizontalDiagonal { get { return new Point[] { Points[0], Points[2] }; } }

        public Point[] Points
        {
            get { return m_points; }
            set
            {
                m_points = value;
                RefreshRect();
            }
        }
        /// <summary>
        /// Compare HitBox
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public List<T> GetEntities<T>(Map map) where T : Entity
        {
            if (map.World != null)
            {
                return map.World.GetEntities<T>(x => Rectangle.Intersects(x.HitBox));
                //    return map.World.GetEntities<T>(x => AreaPoints.Contains(x.GetCellPoint()));
            }
            else
                return new List<T>();
        }
        /// <summary>
        /// Compare CellId
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="map"></param>
        /// <param name="elementHitBox"></param>
        /// <returns></returns>
        public List<T> GetEntities<T>(Map map, Rectangle elementHitBox) where T : Entity
        {
            return map.World.GetEntities<T>(x =>x.Cell != null && x.Cell.Id == Id);
        }
        public List<Entity> GetEntities(Map map)
        {
            if (map.World != null)
            {
                return map.World.GetEntities().FindAll((x => Rectangle.Intersects(x.HitBox)));
            }
            else
                return new List<Entity>();
        }
        public void DrawElements(Map map, ExtendedSpriteBatch batch, MapElementType elementType)
        {

            if (Elements == null)
                return; // Aucun element sur la cell
            var elements = Elements.ToList().FindAll(x => x.Key.Type == elementType);
            elements = elements.OrderBy(x => x.Value).ToList();
            foreach (var element in elements)
            {
                var elementSprite = GameCore.GetSprite(element.Key.ElementId.ToString());
                ResizedSprite resized = elementSprite.Resize(ELEMENTS_SIZE_PERCENTAGE);
                Color color = Color.White;

                if (GetEntities<Player>(map).Count > 0 && element.Value != LayerType.Ground)
                {
                    color = color * 0.5f;
                }
                batch.Draw(resized.Sprite.Gfx, resized.NewBounds.Calibrate(Center.X, Center.Y), color);
            }

        }

        public bool Visible()
        {

            if ((CameraManager.Cam.Position.X - Rectangle.Width) > Center.X * CameraManager.Cam.Zoom || (CameraManager.Cam.Position.X + Rectangle.Width + CameraManager.Cam.ViewportRectangle.Width) < Center.X * CameraManager.Cam.Zoom)
                return false;
            if (CameraManager.Cam.Position.Y - Rectangle.Height > Center.Y * CameraManager.Cam.Zoom || CameraManager.Cam.Position.Y + Rectangle.Height + CameraManager.Cam.ViewportRectangle.Height < Center.Y * CameraManager.Cam.Zoom)
                return false;
            return true;
        }
        public Point Center
        {
            get { return new Point((Points[0].X + Points[2].X) / 2, (Points[1].Y + Points[3].Y) / 2); }
        }
        public Rectangle Rectangle = new Rectangle();

        public bool IsInRectange(Rectangle rect)
        {
            return Rectangle.Intersects(rect);
        }


    }
}
