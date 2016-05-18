using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Graphics
{
    /// <summary>
    /// An extended version of the SpriteBatch class that supports line and
    /// rectangle drawing.
    /// </summary>
    public class ExtendedSpriteBatch : SpriteBatch
    {
        /// <summary>
        /// The texture used when drawing rectangles, lines and other 
        /// primitives. This is a 1x1 white texture created at runtime.
        /// </summary>
        public Texture2D Pixel { get; protected set; }

        public ExtendedSpriteBatch(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            this.Pixel = new Texture2D(this.GraphicsDevice, 1, 1);
            this.Pixel.SetData(new Color[] { Color.White });
        }

        /// <summary>
        /// Draw a line between the two supplied points.
        /// </summary>
        /// <param name="start">Starting point.</param>
        /// <param name="end">End point.</param>
        /// <param name="color">The draw color.</param>
        public void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            float length = (end - start).Length();
            float rotation = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            this.Draw(this.Pixel, start, null, color, rotation, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to draw.</param>
        /// <param name="color">The draw color.</param>
        public void DrawRectangle(Rectangle rectangle, Color color)
        {
            this.Draw(this.Pixel, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, 1), color);
            this.Draw(this.Pixel, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, 1), color);
            this.Draw(this.Pixel, new Rectangle(rectangle.Left, rectangle.Top, 1, rectangle.Height), color);
            this.Draw(this.Pixel, new Rectangle(rectangle.Right, rectangle.Top, 1, rectangle.Height + 1), color);
        }
        public void DrawPolygon(Point[] points,Color color)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (i != points.Length-1)
                  this.DrawLine(new Vector2(points[i].X,points[i].Y), new Vector2(points[i+1].X,points[i+1].Y), color);
                else
                    this.DrawLine(new Vector2(points[i].X, points[i].Y), new Vector2(points[0].X, points[0].Y), color);
            }
        }
        public void FillPolygon(Point[] points,Color color)
        {

            List<Color> colorData = new List<Color>();
            for (int i = 0; i < 10; i++)
            {
                colorData.Add(Color.White);
            }
        }
        public void FillCircle(Point position,int radius,Color color)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            Draw(texture, new Rectangle(position.X, position.Y, texture.Bounds.Width,
                texture.Bounds.Height), color);
        }
        /// <summary>
        /// Fill a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to fill.</param>
        /// <param name="color">The fill color.</param>
        public void FillRectangle(Rectangle rectangle, Color color)
        {
            this.Draw(this.Pixel, rectangle, color);
        }

    }
}

