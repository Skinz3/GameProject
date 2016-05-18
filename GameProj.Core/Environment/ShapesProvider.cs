
using GameProj.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameProj.Core.Environment
{
    /// <summary>
    /// Dofus 2.0 Shapes & MapCells Providers 
    /// </summary>
    public class ShapesProvider
    {
        #region Provider
        public delegate List<int> ShapeMethodDel(int startcell,int entitycell, short radius);
        public static Dictionary<char, ShapeMethodDel> Shapes = new Dictionary<char, ShapeMethodDel>();

        public static void Initialize()
        {
            foreach (var type in Assembly.GetAssembly(typeof(ShapesProvider)).GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    var attributes = method.GetCustomAttributes(typeof(Shape), false);
                    if (attributes.Count() > 0)
                    {
                        var attribute = attributes[0] as Shape;
                        if (attribute != null)
                        {
                            Shapes.Add(attribute.ShapeIdentifier, (ShapeMethodDel)Delegate.CreateDelegate(typeof(ShapeMethodDel), method));
                        }
                    }
                }
            }
        }
        public static List<int> Handle(char shapetype,int startcell,int entitycell, short radius)
        {
            var shape = Shapes.FirstOrDefault(x => x.Key == shapetype);
            if (shape.Value != null)
                return shape.Value(startcell, entitycell, radius);
            else
            {

                Console.WriteLine(shapetype + " shape is not handled");
                return new List<int>();
            }
        }
        #endregion
        #region Simple
        public static List<int> GetRightCells(int startcell, short movedcellamount)
        {
            var list = new List<int>();
            for (short i = 1; i < (movedcellamount + 1); i++)
            {
                list.Add((short)(startcell + i));
            }
          
            return list;
        }
        public static List<int> GetLeftCells(int startcell, short movedcellamount)
        {
            var list = new List<int>();
            for (short i = 1; i < (movedcellamount + 1); i++)
            {
                list.Add((short)(startcell - i));
            }
          
            return list;
        }
        public static List<int> GetUpCells(int startcell, short movedcellamount)
        {
            var list = new List<int>();
            for (short i = 1; i < (movedcellamount + 1); i++)
            {
                list.Add((short)(startcell - 28 * i));
            }
          
            return list;
        }
        public static List<int> GetDownCells(int startcell, short movedcellamount)
        {
            var list = new List<int>();
            for (short i = 1; i < (movedcellamount + 1); i++)
            {
                list.Add((short)(startcell + 28 * i));
            }
          
            return list;
        }
        #endregion
        #region FrontDownRight&Left
        public static List<int> GetFrontDownLeftCells(int startcell, short movedcellamout)
        {
            var list = new List<int>();
            var checker = Math.Truncate((decimal)startcell / 14); // on regarde si la rangée de la cell est paire ou non
            var iee = Math.IEEERemainder((short)checker, 2); // on regarde si il y a un reste au nombre
            if (iee == 0) // si le nombre est pair , alors.. je m'y perd..edit : ok trouvé x)
            {
                list.Add((short)(startcell + 13));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] + 14));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] + 13));
                        check = true;
                    }
                }
            }
            else // si il est impaire ,alors
            {
                list.Add((short)(startcell + 14 * 1));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] + 13));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] + 14));
                        check = true;
                    }
                }
            }

            list.Remove(list.Last());
          
            return list;
        }
        public static List<int> GetFrontDownRightCells(int startcell, short movedcellamout)
        {
            var list = new List<int>();
            var checker = Math.Truncate((decimal)startcell / 14); // on regarde si la rangée de la cell est paire ou non
            var iee = Math.IEEERemainder((short)checker, 2); // on regarde si il y a un reste au nombre
            if (iee == 0) // si le nombre est pair , alors
            {
                list.Add((short)(startcell + 14));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] + 15));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] + 14));
                        check = true;
                    }
                }
            }
            else // si il est impaire ,alors
            {
                list.Add((short)(startcell + 15 * 1));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] + 14));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] + 15));
                        check = true;
                    }
                }
            }

            list.Remove(list.Last());
            return list;
        }
        #endregion
        #region FrontUpRight&Left
        public static List<int> GetFrontUpLeftCells(int startcell, short movedcellamout)
        {
            var list = new List<int>();
            var checker = Math.Truncate((decimal)startcell / 14); // on regarde si la rangée de la cell est paire ou non
            var iee = Math.IEEERemainder((short)checker, 2); // on regarde si il y a un reste au nombre
            if (iee == 0) // si le nombre est pair , alors
            {
                list.Add((short)(startcell - 15));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] - 14));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] - 15));
                        check = true;
                    }
                }
            }
            else // si il est impaire ,alors
            {
                list.Add((short)(startcell - 14 * 1));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] - 15));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] - 14));
                        check = true;
                    }
                }
            }

            list.Remove(list.Last());
          
            return list;
        }
        public static List<int> GetFrontUpRightCells(int startcell, short movedcellamout) // youston on a probleme cell 500
        {
            var list = new List<int>();
            var checker = Math.Truncate((decimal)startcell / 14);
            var iee = Math.IEEERemainder((short)checker, 2);
            if (iee == 0)
            {

                list.Add((short)(startcell - 14));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] - 13));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] - 14));
                        check = true;
                    }
                }
            }
            else // si il est impaire ,alors
            {
                list.Add((short)(startcell - 13));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] - 14));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] - 13));
                        check = true;
                    }
                }
            }

            list.Remove(list.Last());
          
            return list;
        }
        #endregion
        #region Areas
        public static List<int> GetThe4thsDiagonal(int startcell, short movedcellamount)
        {
            var list = new List<int>();
            list.AddRange(GetDownCells(startcell, movedcellamount));
            list.AddRange(GetUpCells(startcell, movedcellamount));
            list.AddRange(GetRightCells(startcell, movedcellamount));
            list.AddRange(GetLeftCells(startcell, movedcellamount));
            return list;
        }
        public static List<int> GetSquare(int startcell, bool containstartcell)
        {
            var list = new List<int>();
            list.AddRange(GetFrontDownLeftCells(startcell, 1));
            list.AddRange(GetFrontDownRightCells(startcell, 1));
            list.AddRange(GetFrontUpLeftCells(startcell, 1));
            list.AddRange(GetFrontUpRightCells(startcell, 1));
            list.AddRange(GetDownCells(startcell, 1));
            list.AddRange(GetUpCells(startcell, 1));
            list.AddRange(GetRightCells(startcell, 1));
            list.AddRange(GetLeftCells(startcell, 1));
            if (containstartcell)
                list.Add(startcell);
            return list;
        }
        #endregion
        #region Utils
        //public static void Verifiy(List<int> cells)
        //{
        //    cells.RemoveAll(x => x < 0 || x > 560);
        //}
        public static List<int> GetLineFromOposedDirection(int startcell, short movecellamount, DirectionsType direction)
        {
            switch (direction) // all good, directionfinderrework
            {
                case DirectionsType.DOWNRIGHT:
                    return GetFrontUpLeftCells(startcell, movecellamount);
                case DirectionsType.DOWNLEFT: // good
                    return GetFrontUpRightCells(startcell, movecellamount);
                case DirectionsType.UPLEFT:
                    return GetFrontDownRightCells(startcell, movecellamount);
                case DirectionsType.UPRIGHT: // good
                    return GetFrontDownLeftCells(startcell, movecellamount);
                default:
                    return null;
            }
        }
        public static List<int> GetLineFromDirection(int startcell, short movecellamount, DirectionsType direction)
        {
            switch (direction)
            {
                case DirectionsType.RIGHT:
                    return GetRightCells(startcell, movecellamount);
                case DirectionsType.DOWNRIGHT:
                    return GetFrontDownRightCells(startcell, movecellamount);
                case DirectionsType.DOWN:
                    return GetDownCells(startcell, movecellamount);
                case DirectionsType.DOWNLEFT:
                    return GetFrontDownLeftCells(startcell, movecellamount);
                case DirectionsType.LEFT:
                    return GetLeftCells(startcell, movecellamount);
                case DirectionsType.UPLEFT:
                    return GetFrontUpLeftCells(startcell, movecellamount);
                case DirectionsType.UP:
                    return GetUpCells(startcell, movecellamount);
                case DirectionsType.UPRIGHT:
                    return GetFrontUpRightCells(startcell, movecellamount);
                default:
                    return null;
            }
        }
        #endregion
        #region DirectionHelper
        public static DirectionsType GetOposedDirection(DirectionsType direction)
        {
            switch (direction)
            {
                case DirectionsType.RIGHT:
                    return DirectionsType.LEFT;
                case DirectionsType.DOWNRIGHT:
                    return DirectionsType.UPLEFT;
                case DirectionsType.DOWN:
                    return DirectionsType.UP;
                case DirectionsType.DOWNLEFT:
                    return DirectionsType.UPRIGHT;
                case DirectionsType.LEFT:
                    return DirectionsType.RIGHT;
                case DirectionsType.UPLEFT:
                    return DirectionsType.DOWNRIGHT;
                case DirectionsType.UP:
                    return DirectionsType.DOWN;
                case DirectionsType.UPRIGHT:
                    return DirectionsType.DOWNLEFT;
                default:
                    throw new Exception("What is dat direction dude?");
            }
        }
        public static DirectionsType GetDirectionFromTwoCells(int firstcellid, int secondccellid) // first = caster
        {
            if (GetFrontDownLeftCells(firstcellid, 10).Contains(secondccellid))
                return DirectionsType.DOWNLEFT;
            if (GetFrontDownRightCells(firstcellid, 10).Contains(secondccellid))
                return DirectionsType.DOWNRIGHT;
            if (GetFrontUpLeftCells(firstcellid, 10).Contains(secondccellid))
                return DirectionsType.UPLEFT;
            if (GetFrontUpRightCells(firstcellid, 10).Contains(secondccellid))
                return DirectionsType.UPRIGHT;

            if (GetRightCells(firstcellid, 10).Contains(secondccellid))
                return DirectionsType.RIGHT;
            if (GetLeftCells(firstcellid, 10).Contains(secondccellid))
                return DirectionsType.LEFT;
            if (GetUpCells(firstcellid, 10).Contains(secondccellid))
                return DirectionsType.UP;
            if (GetDownCells(firstcellid, 10).Contains(secondccellid))
                return DirectionsType.DOWN;

            return 0;
        }
        #endregion
        #region Shapes
        [Shape('U')]
        public static List<int> GetUShape(int startcell,int entitycell, short radius)
        {
            List<int> results = new List<int>();
            var direction = GetDirectionFromTwoCells(startcell, entitycell);
            var line = GetLineFromDirection(startcell, 2, direction);
            line.Remove(line.Last());
            line = GetSquare(line[0], true);
            results.Add(startcell);
            if (direction == DirectionsType.DOWNLEFT || direction == DirectionsType.UPRIGHT)
            {
                results.Add(line[1]);
                results.Add(line[2]);

            }
            if (direction == DirectionsType.UPLEFT || direction == DirectionsType.DOWNRIGHT)
            {
                results.Add(line[0]);
                results.Add(line[3]);
            }
          
            return results;
        }
        [Shape('a')]
        public static List<int> GetAllShape(int startcell,int entitycell, short radius)
        {
            List<int> results = new List<int>();
            for (short i = 0; i < 559; i++)
            {
                results.Add(i);
            }
            return results;
        }
        [Shape('+')]
        public static List<int> GetPlusShape(int startcell,int entitycell, short radius)
        {
            List<int> results = new List<int>();
            results.Add(startcell);
            results.Add(GetLeftCells(startcell, 1)[0]);
            results.Add(GetRightCells(startcell, 1)[0]);
            results.Add(GetUpCells(startcell, 1)[0]);
            results.Add(GetDownCells(startcell, 1)[0]);
          
            return results;
        }
        [Shape('V')]
        public static List<int> GetVShape(int startcell,int entitycell, short radius)
        {
            List<int> results = new List<int>();
            results.Add(startcell);
            var direction = GetDirectionFromTwoCells(entitycell, startcell);
            var line = GetLineFromDirection(startcell, 1, direction);
            results.AddRange(line);
            switch (direction)
            {
                case DirectionsType.DOWNRIGHT:
                    results.AddRange(GetLineFromDirection(line[0], 1, DirectionsType.DOWNLEFT));
                    results.AddRange(GetLineFromDirection(line[0], 1, DirectionsType.UPRIGHT));
                    break;
                case DirectionsType.DOWNLEFT:
                    results.AddRange(GetLineFromDirection(line[0], 1, DirectionsType.DOWNRIGHT));
                    results.AddRange(GetLineFromDirection(line[0], 1, DirectionsType.UPLEFT));
                    break;
                case DirectionsType.UPLEFT:
                    results.AddRange(GetLineFromDirection(line[0], 1, DirectionsType.DOWNLEFT));
                    results.AddRange(GetLineFromDirection(line[0], 1, DirectionsType.UPRIGHT));
                    break;
                case DirectionsType.UPRIGHT:
                    results.AddRange(GetLineFromDirection(line[0], 1, DirectionsType.UPLEFT));
                    results.AddRange(GetLineFromDirection(line[0], 1, DirectionsType.DOWNRIGHT));
                    break;
            }

          
            return results;
        }
        //[Shape('*')]
        //public static List<int> GetStarShape(int startcell,int entitycell, short radius)
        //{
        //    var cells = GetCShape(startcell, entitycell, radius);
        //    cells.AddRange(GetUpCells(startcell, radius));
        //    cells.AddRange(GetDownCells(startcell, radius));
        //    cells.AddRange(GetRightCells(startcell, radius));
        //    cells.AddRange(GetLeftCells(startcell, radius));
        //  
        //    return cells.Distinct().ToList();
        //}
        [Shape('G')]
        public static List<int> GetGShape(int startcell,int entitycell, short radius)
        {
            short shift = 1;
            if (radius >= 3)
            {
                for (int i = 3; i < radius + 1; i++)
                {
                    shift++;
                }
            }
            List<int> results = new List<int>();
            for (int i = 1; i < radius + 1; i++)
            {
                results.Add(startcell);
                results.Add((short)(startcell + i));
                results.AddRange(GetFrontDownLeftCells((short)(startcell + i), (short)(shift + i)));
                results.AddRange(GetFrontUpLeftCells((short)(startcell + i), (short)(shift + i)));
                results.AddRange(GetFrontDownRightCells((short)(startcell - i), (short)(shift + i)));
                results.AddRange(GetFrontUpRightCells((short)(startcell - i), (short)(shift + i)));
                results.AddRange(GetLeftCells((short)(startcell), (short)(i)));
                results.AddRange(GetDownCells((short)(startcell), (short)(i)));
                results.AddRange(GetUpCells((short)(startcell), (short)(i)));
            }
          
            return results.Distinct().ToList();
        }
        [Shape('Q')]
        public static List<int> GetQShape(int startcell,int entitycell, short radius)
        {
            List<int> results = new List<int>();
            results.AddRange(GetFrontDownLeftCells(startcell, radius));
            results.AddRange(GetFrontDownRightCells(startcell, radius));
            results.AddRange(GetFrontUpLeftCells(startcell, radius));
            results.AddRange(GetFrontUpRightCells(startcell, radius));
          
            return results;
        }
        [Shape('X')]
        public static List<int> GetCrossCells(int startcell,int entitycell, short radius)
        {
            List<int> results = new List<int>();
            results.Add(startcell);
            results.AddRange(GetFrontDownLeftCells(startcell, radius));
            results.AddRange(GetFrontDownRightCells(startcell, radius));
            results.AddRange(GetFrontUpLeftCells(startcell, radius));
            results.AddRange(GetFrontUpRightCells(startcell, radius));
          
            return results;
        }
        [Shape('L')]
        public static List<int> GeetLShape(int startcell,int entitycell, short radius)
        {
            var line = GetLineFromDirection(startcell, radius, GetDirectionFromTwoCells(entitycell, startcell));
            line.Add(startcell);
           
            return line;
        }
        //[Shape('C')]
        //public static List<int> GetCShape(int startcell,int entitycell, short radius)
        //{
        //    return Pathfinding.GetCircleCells(startcell, radius);
        //}
        [Shape('T')]
        public static List<int> GetTShape(int startcell,int entitycell, short radius)
        {
            List<int> cells = new List<int>();
            cells.Add(startcell);
            var position = GetDirectionFromTwoCells(entitycell, startcell);
            switch (position)
            {
                case DirectionsType.RIGHT:
                    return cells;
                case DirectionsType.DOWNRIGHT:
                    cells.AddRange(GetFrontDownLeftCells(startcell, radius));
                    cells.AddRange(GetFrontUpRightCells(startcell, radius));
                    break;
                case DirectionsType.DOWN:
                    return cells;
                case DirectionsType.DOWNLEFT:
                    cells.AddRange(GetFrontUpLeftCells(startcell, radius));
                    cells.AddRange(GetFrontDownRightCells(startcell, radius));
                    break;
                case DirectionsType.LEFT:
                    break;
                case DirectionsType.UPLEFT:
                    cells.AddRange(GetFrontUpRightCells(startcell, radius));
                    cells.AddRange(GetFrontDownLeftCells(startcell, radius));
                    break;
                case DirectionsType.UP:
                    break;
                case DirectionsType.UPRIGHT:
                    cells.AddRange(GetFrontUpLeftCells(startcell, radius));
                    cells.AddRange(GetFrontDownRightCells(startcell, radius));
                    break;
            }
          
            return cells;
        }
        public static List<int> GetCross1RadiusCells(short baseposition)
        {
            List<int> results = new List<int>();
            results.Add(GetFrontDownLeftCells(baseposition, 1)[0]);
            results.Add(GetFrontDownRightCells(baseposition, 1)[0]);
            results.Add(GetFrontUpLeftCells(baseposition, 1)[0]);
            results.Add(GetFrontUpRightCells(baseposition, 1)[0]);
          
            return results;
        }
        [Shape('P')]
        public static List<int> GetPShape(int startcell,int basecell, short radius)
        {
            return new List<int>() { startcell };
        }
        #endregion
        #region MapBorder
        #endregion
    }
}
