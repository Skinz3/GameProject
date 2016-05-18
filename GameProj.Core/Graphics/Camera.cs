using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProj.Core.Graphics
{
    public enum CameraMode { Free, Follow }

    public class Camera
    {
        #region Field Region

        Vector2 position;
        float speed;
        float zoom;
        Rectangle viewportRectangle;
        CameraMode mode;

        #endregion

        #region Property Region

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Speed
        {
            get { return speed; }
            set
            {
                speed = (float)MathHelper.Clamp(speed, 1f, 16f);
            }
        }

        public float Zoom
        {
            get { return zoom; }
        }

        public CameraMode CameraMode
        {
            get { return mode; }
        }

        public Matrix Transformation
        {
            get
            {
                return Matrix.CreateScale(zoom) *
                    Matrix.CreateTranslation(new Vector3(-Position, 0f));
            }
        }

        public Rectangle ViewportRectangle
        {
            get
            {
                return new Rectangle(
                    viewportRectangle.X,
                    viewportRectangle.Y,
                    viewportRectangle.Width,
                    viewportRectangle.Height);
            }
        }

        #endregion

        #region Constructor Region
        public Camera(Rectangle viewportRect,float speed)
        {
            this.speed = speed;
            zoom = 1f;
            viewportRectangle = viewportRect;
            mode = CameraMode.Free;
        }

        public Camera(Rectangle viewportRect, Vector2 position,float speed)
        {
            this.speed = speed;
            zoom = 1f;
            viewportRectangle = viewportRect;
            Position = position;
            mode = CameraMode.Free;
        }

        #endregion

        #region Method Region

        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            if (mode == CameraMode.Follow)
                return;

            Vector2 motion = Vector2.Zero;

            if (state.IsKeyDown(Keys.Left))
                motion.X = -speed;
            else if (state.IsKeyDown(Keys.Right))
                motion.X = speed;

            if (state.IsKeyDown(Keys.Up))
                motion.Y = -speed;
            else if (state.IsKeyDown(Keys.Down))
                motion.Y = speed;

            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                position += motion * speed;
                LockCamera();
            }

        }
        public bool Visible(Rectangle rect)
        {
            if (new Rectangle((int)Position.X,(int)Position.Y,viewportRectangle.Width,viewportRectangle.Height).Intersects(rect))
                return true;
            else
                return false;
        }
        public void ZoomIn()
        {
            zoom += .25f;

            if (zoom > 2.5f)
                zoom = 2.5f;

            Vector2 newPosition = Position * zoom;
            SnapToPosition(newPosition);
        }
        public void ProgressiveZoomIn(float value)
        {
            zoom += value;
            Vector2 newPosition = Position * zoom;
        }
        public void ProgressiveZoomOut(float value)
        {
            zoom -= value;
            Vector2 newPosition = Position * zoom;
        }
        public void RemoveZoom()
        {
            zoom = 1f;
        }
        public void Zoom0()
        {
            zoom = 0.5f;
            Vector2 newPosition = Position * zoom;
            SnapToPosition(newPosition);

        }
        public void ZoomOut()
        {
            zoom -= .25f;

            if (zoom < .5f)
                zoom = .5f;

            Vector2 newPosition = Position * zoom;
            SnapToPosition(newPosition);
        }

        private void SnapToPosition(Vector2 newPosition)
        {
            position.X = newPosition.X - viewportRectangle.Width / 2;
            position.Y = newPosition.Y - viewportRectangle.Height / 2;
            LockCamera();
        }

        public void LockCamera()
        {
            position.X = MathHelper.Clamp(position.X,
                0,
                5000 * zoom - viewportRectangle.Width);
            position.Y = MathHelper.Clamp(position.Y,
                0,
                5000 * zoom - viewportRectangle.Height);
        }

        public void LockToSprite(Point pos)
        {
            position.X = (int)((pos.X) * zoom - (viewportRectangle.Width / 2));
            position.Y = (int)((pos.Y) * zoom - (viewportRectangle.Height / 2));
            LockCamera();
            this.mode = CameraMode.Follow;
        }


        #endregion
    }
}
