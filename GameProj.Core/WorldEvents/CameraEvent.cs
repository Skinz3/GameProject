using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.WorldEvents
{
    public enum CameraActionType
    {
        ZOOM_IN,
        ZOOM_OUT,
    }

    public class CameraEvent : WorldEvent
    {
        float Value { get; set; }

        CameraActionType ActionType { get; set; }

        public CameraEvent(World world,CameraActionType actionType,float value = 0.001f)
            : base(world)
        {
            this.ActionType = actionType;
            this.Value= value;
        }
        public override void OnRemoved()
        {
            CameraManager.Cam.RemoveZoom();
            base.OnRemoved();
        }
        public override void Update(GameTime time)
        {
            if (ActionType == CameraActionType.ZOOM_IN)
            {
                CameraManager.Cam.ProgressiveZoomIn(Value);
            }
            else 
            {
                 CameraManager.Cam.ProgressiveZoomOut(Value);
            }
        }

        public override void Draw(GameTime time)
        {
            // Nothing to draw here...
        }


    }
}
