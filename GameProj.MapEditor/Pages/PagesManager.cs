using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.MapEditor.Pages
{
    public class PagesManager : IObject
    {
        public IPage LoadedPage { get; set; }

        public void LoadPage(IPage page)
        {
            this.LoadedPage = page;
        }
        public void Draw(GameTime time)
        {
            LoadedPage.Draw(time);
        }

        public void Update(GameTime time)
        {
            LoadedPage.Update(time);
        }
    }
}
