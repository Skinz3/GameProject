using GameProj.Core.Interface;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Core
{
    public class CooldownHandler
    {
        static List<CooldownHandler> Cooldowns = new List<CooldownHandler>();

        public static void New(Action action, int ms)
        {
            Cooldowns.Add(new CooldownHandler(action, ms));
        }

        public Action Action { get; set; }

        public int MS { get; set; }

        public CooldownHandler(Action action, int ms)
        {
            this.Action = action;
            this.MS = ms;
        }
        public void UpdateCd()
        {
            MS--;
            if (MS == 0)
            {
                Action();
                Cooldowns.Remove(this);
            }
        }

        public static void Update(GameTime time)
        {
            Cooldowns.ForEach(x => x.UpdateCd());
        }
    }
}
