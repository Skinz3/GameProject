using GameProj.Core.Controls;
using GameProj.Core.Entities;
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using GameProj.Lib.Controls;
using GameProj.Lib.Managers;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.UIRoot
{
    public class UIManager : IObject
    {
        List<Control> Controls = new List<Control>();

        public UIManager()
        {
            Build();
        }
        public void Build()
        {
            Point barPosition = new Point(150, GameCore.Batch.GraphicsDevice.Viewport.Height - 45);
            Point energyBarPosition = new Point(150, GameCore.Batch.GraphicsDevice.Viewport.Height - 30);
            Controls.Add(new PlayerInformationControl(new Point(10, GameCore.Batch.GraphicsDevice.Viewport.Height - 60)));
            Controls.Add(new BarContent("EnergyBar", energyBarPosition, 500, 15, Color.CornflowerBlue, Color.CornflowerBlue, Color.CornflowerBlue));
            Controls.Add(new BarContent("LifeBar",barPosition, 500, 15, Color.LimeGreen, Color.Orange, Color.Red,true));

        }
        public void Draw(GameTime time)
        {
            Controls.ForEach(x => x.Draw(time));
        }
        public void Update(GameTime time)
        {
            Controls.ForEach(x => x.Update(time));
        }
        public T GetControl<T>(string name) where T : Control
        {
            return Controls.OfType<T>().FirstOrDefault(x => x.Name == name);
        }
    }
}
