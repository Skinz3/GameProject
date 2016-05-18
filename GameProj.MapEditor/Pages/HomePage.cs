using GameProj.Core.Graphics;
using GameProj.Core.Interface;
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
    public class HomePage : IPage
    {
        public System.Windows.Forms.OpenFileDialog OpenMapDialog = new System.Windows.Forms.OpenFileDialog();

        public Button NewMapButton { get; set; }

        public Button OpenMapButton { get; set; }

        public Button HelpButton { get; set; }

        public Button ExitButton { get; set; }


        public HomePage()
        {
            this.NewMapButton = new Button(new Rectangle(300, 100, 200, 40), Color.Black, Color.Transparent, "New Map", 310);
            this.OpenMapButton = new Button(new Rectangle(300, 150, 200, 40), Color.Black, Color.Transparent, "Open Map", 310);
            this.HelpButton = new Button(new Rectangle(300, 200, 200, 40), Color.Black, Color.Transparent, "Help", 310);
            this.ExitButton = new Button(new Rectangle(300, 250, 200, 40), Color.Black, Color.Transparent, "Exit", 310);
            this.NewMapButton.OnMouseClick += NewMapButton_OnMouseClick;
            this.OpenMapButton.OnMouseClick += OpenMapButton_OnMouseClick;
            this.ExitButton.OnMouseClick += ExitButton_OnMouseClick;
            this.HelpButton.OnMouseClick += HelpButton_OnMouseClick;
            OpenMapDialog.FileName = string.Empty;
            OpenMapDialog.InitialDirectory = Environment.CurrentDirectory + @"\Maps\";
            OpenMapDialog.Filter = "Map (*.gsx)|*.gsx";
            OpenMapDialog.Title = "Open Map";
        }

        void HelpButton_OnMouseClick()
        {
            System.Windows.Forms.MessageBox.Show("Aide: (Shift) to Show Grid (A) to zoom in (Z) to zoom out (1) to save map (2) to toogle full screen (3) to create new map. (4) to go to menu", "Help", 
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
        }

        void ExitButton_OnMouseClick()
        {
            Environment.Exit(0);
        }

        void OpenMapButton_OnMouseClick()
        {
            OpenMapDialog.ShowDialog();
            if (OpenMapDialog.FileName != null)
            {
           
            }
        }

        void NewMapButton_OnMouseClick()
        {
            MapEditorFrame.PageManager.LoadPage(new EditorPage());
        }
        public void Draw(GameTime time)
        {
            GameCore.Batch.Begin();
            NewMapButton.Draw(time);
            HelpButton.Draw(time);
            ExitButton.Draw(time);
            OpenMapButton.Draw(time);
     
            GameCore.Batch.End();
        }

        public void Update(GameTime time)
        {
            var state = Mouse.GetState();
            NewMapButton.Update(state, time);
            HelpButton.Update(state, time);
            ExitButton.Update(state, time);
            OpenMapButton.Update(state, time);
        }
    }
}
