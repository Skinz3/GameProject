using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using GameProj.Lib.Controls;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Entities
{
    /// <summary>
    /// a link direct au player, la c'est trop galère ^^'
    /// </summary>
    public class SpellBarItem
    {
        public SpellBarItem(Rectangle rectangle,SpellTemplate template,Sprite iconSprite)
        {
            this.Rectangle = rectangle;
            this.Template = template;
            this.IconSprite = iconSprite;
        }

        public Rectangle Rectangle { get; set; }
        public SpellTemplate Template { get; set; }
        public Sprite IconSprite { get; set; }
    }
    public class SpellBar : IObject
    {
        public const int MAX_SPELL_NUMBER = 8;


        Player Player { get; set; }
        
        public SpellBar(Player player)
        {
            if (player.Spells.Count > MAX_SPELL_NUMBER)
                throw new Exception("Max spell number overflow");

            this.Player = player;
            Build(player.Spells);
        }

        List<SpellBarItem> Content = new List<SpellBarItem>();

        public void Build(List<SpellTemplate> items)
        {
            int x = 150;
            foreach (var item in items)
            {
                Sprite sprite = GameCore.Load(item.IconSpriteName);
                Content.Add(new SpellBarItem(new Rectangle(x + 4, 390, 38, 38), item, sprite));
                x += 38+4;
            }
        }
        public void AddSpell(SpellTemplate template)
        {
            Rectangle newRect = new Rectangle(150+4, 390, 38, 38);

            if (Content.Count > 0)
            {
               
                newRect = Content.Last().Rectangle;
                newRect.X += 38 + 4;
               
            }

            Sprite iconSprite = GameCore.Load(template.IconSpriteName);

            Content.Add(new SpellBarItem(newRect, template, iconSprite));
        }
        public void Draw(GameTime time)
        {
          
            foreach (var item in Content)
            {
                GameCore.Batch.Draw(item.IconSprite.Gfx, item.Rectangle, Color.White);
            }
        }
      
        public void Update(GameTime time)
        {
          
        }
    }
}
