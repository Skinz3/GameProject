using GameProj.Core.Core;
using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using GameProj.Lib;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Environment
{
    public class Map : IDisposable
    {
        public const int DEFAULT_MAP_WIDTH = 14 * 2;
        public const int DEFAULT_MAP_HEIGHT = 20 * 2;

        public static LayerType[] LayerTypes = (LayerType[])Enum.GetValues(typeof(LayerType));

        public MapTemplate Template { get; set; }

        public UIDProvider ElementUIDProvider { get; set; }

        public World World { get; set; }

        public IsometricRenderer Renderer { get; set; }

        public Cell[] Cells { get { return Renderer.Cells; } }

        public event Action<Cell> OnCellOver;
        public event Action<Cell> OnCellClickedLeftButton;
        public event Action<Cell> OnCellClickedRightButton;

        List<Sprite> UsedSprites = new List<Sprite>();

        public Map(World world,MapTemplate template, Action<Cell> onCellOver = null, Action<Cell> onCellClickedLeftButton = null, Action<Cell> onCellClickedRightButton = null)
        {
            this.Template = template;
            this.ElementUIDProvider = new UIDProvider(GetMaxElementUID());
            this.Renderer = new IsometricRenderer(GameCore.Batch, template, Constants.ShowGrid);
            this.LinkElements();
            this.OnCellOver = onCellOver;
            this.OnCellClickedLeftButton = onCellClickedLeftButton;
            this.OnCellClickedRightButton = onCellClickedRightButton;
            GameCore.Load(Template.GetDistinctedElementsId().ToList().ConvertAll<string>(x => x.ToString()));
            this.World = world;
        }
        int GetMaxElementUID()
        {
            int max = 0;
            foreach (var layer in Template.Layers)
            {
                foreach (var element in layer.Elements)
                {
                    if (element.UID >= max)
                        max = element.UID;
                }
            }
            return max;
        }
        void LinkElements()
        {
            Dictionary<MapElementTemplate, LayerType> elements = new Dictionary<MapElementTemplate, LayerType>();
            foreach (var layer in Template.Layers)
            {
                foreach (var element in layer.Elements)
                {
                    elements.Add(element, layer.LayerType);
                }
            }
            foreach (var cell in Cells)
            {
                cell.LoadElements(elements.ToList().FindAll(x => x.Key.CellId == cell.Id).ToDictionary(x => x.Key, x => x.Value));
            }
        }
        public void DrawBackground(GameTime time)
        {

            DrawElements(MapElementType.Background); // 50% CPU, FIXED :D (Cell.Load()) SpriteBatch.Begin() End()!
            Renderer.Draw();
            var state = Mouse.GetState();
            var cell = Renderer.GetCell(state.Position);
            if (cell != null)
            {
                if (OnCellOver != null)
                    OnCellOver(cell);
                if (OnCellClickedLeftButton != null && state.LeftButton == ButtonState.Pressed)
                    OnCellClickedLeftButton(cell);
                if (OnCellClickedRightButton != null && state.RightButton == ButtonState.Pressed)
                    OnCellClickedRightButton(cell);

            }

        }
        public void DrawForeground()
        {
            DrawElements(MapElementType.Foreground);
        }
        public void Update(GameTime time)
        {

        }

        public void DrawElements(MapElementType elementsType)
        {
            foreach (var cell in Cells)
            {
                if (CameraManager.Cam.CameraMode == CameraMode.Follow)
                {
                    if (cell.Visible()) // baisse un peu l'UC utilisé
                        cell.DrawElements(this, GameCore.Batch, elementsType);
                }
                else
                    cell.DrawElements(this, GameCore.Batch, elementsType);

            }
        }
        public static Map New(World world)
        {
            return new Map(world,new MapTemplate("Undefined", "Undefined", new List<int>(), new List<LayerTemplate>(), DEFAULT_MAP_WIDTH, DEFAULT_MAP_HEIGHT));
        }

        public void Dispose()
        {
            GameCore.Unload(Template.GetDistinctedElementsId().ToList().ConvertAll<string>(x => x.ToString()));
        }
    }
}
