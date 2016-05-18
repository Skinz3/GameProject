using GameProj.Core.Graphics;
using GameProj.Core.Interface;
using GameProj.Lib.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Core.Environment
{
    public class IsometricRenderer
    {
        // https://www.desmos.com/calculator/waogzom0ym David Loiseaux  (AreaLeadingCoef = a)

        public const double AreaLeadingCoefficent = 0.510638297872;

        public static Color GridColor = Color.Black;

        ExtendedSpriteBatch SpriteBatch { get; set; }
        private int m_mapHeight;
        public int MapHeight
        {
            get { return m_mapHeight; }
            set
            {
                m_mapHeight = value;
                SetCellNumber();
            }
        }

        private int m_mapWidth;
        public int MapWidth
        {
            get { return m_mapWidth; }
            set
            {
                m_mapWidth = value;
                SetCellNumber();
            }
        }
        public int RealCellHeight
        {
            get;
            private set;
        }

        public int RealCellWidth
        {
            get;
            private set;
        }
        public bool ViewGrid { get; set; }
        /// <summary>
        /// Initialize Map variables
        /// </summary>
        /// <param name="device"></param>
        public IsometricRenderer(ExtendedSpriteBatch spritebatch, MapTemplate template, bool viewgrid)
        {
            this.MapHeight = template.Height; // 20
            this.MapWidth = template.Width; // 14
            this.SpriteBatch = spritebatch;
            this.SetCellNumber();
            this.DefineWalkable(template);
            this.BuildMap();
            this.ViewGrid = viewgrid;
        }
        private void DefineWalkable(MapTemplate template)
        {
            foreach (var cellid in template.WalkableCells)
            {
                var cell = GetCell(cellid);
                if (cell != null)
                    cell.Walkable = true;
            }
        }
        private void SetCellNumber()
        {
            Cells = new Cell[2 * MapHeight * MapWidth];
            int cellId = 0;
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth * 2; x++)
                {
                    var cell = new Cell(cellId++);
                    Cells[cell.Id] = cell;
                }
            }
        }
        private double GetMaxScaling()
        {
            double cellWidth = Width / (double)(MapWidth + 1);
            double cellHeight = Height / (double)(MapHeight + 1);
            cellWidth = Math.Min(cellHeight * 2, cellWidth);
            return cellWidth;
        }
        /// <summary>
        /// To SEE *2
        /// </summary>
        public int Width { get { return SpriteBatch.GraphicsDevice.Viewport.Width * 2; } }
        public int Height { get { return SpriteBatch.GraphicsDevice.Viewport.Height * 2; } }
        /// <summary>
        /// Build Cells 
        /// </summary>
        public void BuildMap()
        {

            int cellId = 0;
            double cellWidth = GetMaxScaling();
            double cellHeight = Math.Ceiling(cellWidth / 2);

            var offsetX = 0 +cellWidth; //(int)((Width - ((MapWidth + 0.5) * cellWidth)) / 2);
            var offsetY = 0 + cellHeight; //(int)((Height - ((MapHeight + 0.5) * cellHeight)) / 2);

            double midCellHeight = cellHeight / 2;
            double midCellWidth = cellWidth / 2;

            for (int y = 0; y < (2 * MapHeight); y++)
            {
                if (y % 2 == 0)
                    for (int x = 0; x < MapWidth; x++)
                    {
                        var left = new Point((int)(offsetX + x * cellWidth), (int)(offsetY + y * midCellHeight + midCellHeight));
                        var top = new Point((int)(offsetX + x * cellWidth + midCellWidth), (int)(offsetY + y * midCellHeight));
                        var right = new Point((int)(offsetX + x * cellWidth + cellWidth), (int)(offsetY + y * midCellHeight + midCellHeight));
                        var down = new Point((int)(offsetX + x * cellWidth + midCellWidth), (int)(offsetY + y * midCellHeight + cellHeight));
                        Cells[cellId++].Points = new[] { left, top, right, down };
                    }
                else
                    for (int x = 0; x < MapWidth; x++)
                    {
                        var left = new Point((int)(offsetX + x * cellWidth + midCellWidth), (int)(offsetY + y * midCellHeight + midCellHeight));
                        var top = new Point((int)(offsetX + x * cellWidth + cellWidth), (int)(offsetY + y * midCellHeight));
                        var right = new Point((int)(offsetX + x * cellWidth + cellWidth + midCellWidth), (int)(offsetY + y * midCellHeight + midCellHeight));
                        var down = new Point((int)(offsetX + x * cellWidth + cellWidth), (int)(offsetY + y * midCellHeight + cellHeight));
                        Cells[cellId++].Points = new[] { left, top, right, down };
                    }
            }
            RealCellHeight = (int)cellHeight;
            RealCellWidth = (int)cellWidth;

            CalculateCellsArea();

        }
        public void CalculateCellsArea()
        {
            foreach (var cell in Cells)
            {
                cell.EvaluateArea();
            }
        }
        public Cell GetCell(Point p,bool recalculate = true)
        {
            if (recalculate)
            p = Recalculate(p);
            int cellWidth = RealCellWidth;
            int cellHeight = RealCellHeight;
            var searchRect = new Rectangle(p.X - cellWidth, p.Y - cellHeight, cellWidth, cellHeight);
            return Cells.FirstOrDefault(cell => cell.IsInRectange(searchRect) && PointInPoly(p, cell.Points));
           
        }
        public static Point Recalculate(Point p)
        {
            int cameraPercent = (int)((double)CameraManager.Cam.Zoom * (double)100);
            double cameraRatio = ((double)cameraPercent / (double)100);
            int tX = (int)((double)p.X / (double)cameraRatio);
            int tY = (int)((double)p.Y / (double)cameraRatio);

            tX += (int)((double)CameraManager.Cam.Position.X / (double)cameraRatio);
            tY += (int)((double)CameraManager.Cam.Position.Y / (double)cameraRatio);
            return new Point(tX, tY);
        }
        public static Point RecalculateWhileLocked(Point p)
        {
            return new Point(p.X - (int)CameraManager.Cam.Position.X, p.Y - (int)CameraManager.Cam.Position.Y);
        }
        bool PointInPoly(Point p, Point[] poly)
        {
            int xnew, ynew;
            int xold, yold;
            int x1, y1;
            int x2, y2;
            bool inside = false;

            if (poly.Length < 3)
                return false;

            xold = poly[poly.Length - 1].X;
            yold = poly[poly.Length - 1].Y;

            foreach (Point t in poly)
            {
                xnew = t.X;
                ynew = t.Y;

                if (xnew > xold)
                {
                    x1 = xold;
                    x2 = xnew;
                    y1 = yold;
                    y2 = ynew;
                }
                else
                {
                    x1 = xnew;
                    x2 = xold;
                    y1 = ynew;
                    y2 = yold;
                }

                if ((xnew < p.X) == (p.X <= xold) && (p.Y - (long)y1) * (x2 - x1) < (y2 - (long)y1) * (p.X - x1))
                {
                    inside = !inside;
                }
                xold = xnew;
                yold = ynew;
            }
            return inside;
        }
        public Cell GetCell(int id)
        {
            return Cells.FirstOrDefault(cell => cell.Id == id);
        }
        public bool IsPointOnBounds(Point p)
        {
            if (p.X <= 0 || p.Y <= 0 || p.X >= Width || p.Y >= Height) // en dehors des bounds
                return false;
            return true;
        }
        public Cell GetCell(Rectangle rect)
        {
            return Cells.FirstOrDefault(x => x.Rectangle.Intersects(rect));
        }
        public Cell[] Cells
        {
            get;
            set;
        }
        public void Draw()
        {

            if (ViewGrid)
            {
                foreach (var cell in Cells) // enlever condi follow
                {
                    if (CameraManager.Cam.CameraMode == CameraMode.Follow)
                    {
                        if (cell.Visible())
                            cell.DrawBorders(GridColor);
                    }
                    else
                        cell.DrawBorders(GridColor);

                }

            }

        }

    }
}
