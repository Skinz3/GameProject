using GameProj.Core;
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using GameProj.Lib.Enums;
using GameProj.Lib.Templates;
using GameProj.MapEditor.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.MapEditor.Pages
{
    public class EditorPage : IPage
    {
        Button AsBackgroundButton = new Button(new Rectangle(10, 20, 200, 30), Color.Black, Color.Transparent, "As Background", 30);
        TilesSelector TilesSelector { get; set; }
        ListSelector FolderSelector { get; set; }


        CheckBox groundLayer { get; set; }

        Map Map { get; set; }


        public void ToogleGroundLayer(bool select)
        {

        }
        public EditorPage()
        {
            CameraManager.Cam = new Camera(GameCore.Batch.GraphicsDevice.Viewport.Bounds, 15f);
            CameraManager.Cam.Zoom0();
            this.Map = Map.New(null);
            this.groundLayer = new CheckBox(ToogleGroundLayer, "GroundLayer", new Point(700, 30));
            this.Map.OnCellOver += Map_OnCellOver;
            this.Map.OnCellClickedLeftButton += Map_OnCellClicked;
            this.Map.OnCellClickedRightButton += Map_OnCellClickedRightButton;

            FolderSelector = new ListSelector(GameCore.GetSubfoldersNames(),10,80);
            AsBackgroundButton.OnMouseClick += AsBackgroundButton_OnMouseClick;
            TilesSelector = new TilesSelector(new List<string>() { "900","20432" }, new Point(0,399));
        }
        bool dd = false;
        void AsBackgroundButton_OnMouseClick()
        {
            if (dd == true)
                return;
            foreach (var cell in Map.Cells)
            {
                cell.AddElement(new MapElementTemplate(Map.ElementUIDProvider.Pop(),cell.Id, 900, MapElementType.Background,false), LayerType.Ground);
            }
            dd = true;
        }

        void Map_OnCellClickedRightButton(Cell obj)
        {
            obj.RemoveLastElement();
        }
        void Map_OnCellClicked(Cell obj)
        {
            var state = Mouse.GetState();
            if (state.Y >= 380 || state.X >= 700)
                return;
            if (TilesSelector.SelectedElementId == -1)
                return;
            obj.AddElement(new MapElementTemplate(Map.ElementUIDProvider.Pop(),obj.Id, TilesSelector.SelectedElementId, MapElementType.Background,false), LayerType.Ground);
        }

        void Map_OnCellOver(Cell obj)
        {
          //  obj.DrawCenter();
        }
        public void DrawUI(GameTime time)
        {
            GameCore.Batch.Begin();
            AsBackgroundButton.Draw(time);
            FolderSelector.Draw();
            TilesSelector.Draw();
            groundLayer.Draw();
            GameCore.Batch.End();
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
            Map.DrawForeground();
            GameCore.Batch.End();

            DrawUI(time);
        }
        public void Update(GameTime time)
        {
            CameraManager.Cam.Update(time);
            HandleInput(time);
        }
        int FramesUntilLastInput = 0;
        void OnInput()
        {
            FramesUntilLastInput = 10;
        }
        public void HandleInput(GameTime time)
        {
            MouseState mstate = Mouse.GetState();
            groundLayer.UpdateEvents(mstate, time);
            AsBackgroundButton.Update(mstate, time);
            FolderSelector.UpdateEvents(mstate, time);
            TilesSelector.UpdateEvents(mstate, time);
            if (FramesUntilLastInput > 0)
            {
                FramesUntilLastInput--;
                return;
            }
            var state = Keyboard.GetState();

            
            if (state.IsKeyDown(Keys.LeftShift))
            {
                Map.Renderer.ViewGrid = !Map.Renderer.ViewGrid;
                OnInput(); 
            }
            if (state.IsKeyDown(Keys.A))
            {
                CameraManager.Cam.ZoomIn();
                OnInput();
            }
            if (state.IsKeyDown(Keys.Z))
            {
                CameraManager.Cam.ZoomOut();
                OnInput();
            }

            if (state.IsKeyDown(Keys.D2))
            {
                MapEditorFrame.graphics.ToggleFullScreen();
                OnInput();
            }
            if (state.IsKeyDown(Keys.D4))
            {
                MapEditorFrame.PageManager.LoadPage(new HomePage());
            }

        }
    }
}
