using Ampel__2._0.Classes.Container._base;
using Ampel__2._0.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ampel__2._0.Classes.Container
{
    internal class CclContCenter: CclContGeometrieBase
    {
        internal Size WindowSize { get; }

        internal List<Point> South { get; set; }
        internal List <Point> West   { get; set; }
        internal List <Point> East   { get; set; }
        internal List <Point> North  { get; set; }

        public CclContCenter(Size windowSize)
        {
            WindowSize = windowSize;
            South = new List<Point>();
            East = new List<Point>();
            North = new List<Point>();
            West = new List<Point>();
        }

        public void CreatArea(int lanesInCrossroadNorthSouth, int lanesInCrossroadSouthWest) 
        {
            Position = new Point(WindowSize.Width / 2, WindowSize.Height / 2);
            Size = new Size(CstConstants.C_iLaneWidth * lanesInCrossroadSouthWest, CstConstants.C_iLaneWidth * lanesInCrossroadNorthSouth);
        }
        //ToDo: Rechtskurvenkoordinaten berechnen

        public void CalculateCurveRight()
        {
            //Süd-Ost
            South.Clear();
            South.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2, Position.Y + CstConstants.C_iLaneWidth));
            South.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2, Position.Y + CstConstants.C_iLaneWidth -5 ));
            South.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 + 5, Position.Y + CstConstants.C_iLaneWidth-10));
            South.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 + 10, Position.Y + CstConstants.C_iLaneWidth - 15));
            South.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 + 15, Position.Y + CstConstants.C_iLaneWidth - 15));
            South.Add(new Point(Position.X + CstConstants.C_iLaneWidth, Position.Y + CstConstants.C_iLaneWidth/2));
            South.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 5, Position.Y + CstConstants.C_iLaneWidth / 2));
            South.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 10, Position.Y + CstConstants.C_iLaneWidth / 2));
            South.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 15, Position.Y + CstConstants.C_iLaneWidth / 2));
            //South.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 20, Position.Y + CstConstants.C_iLaneWidth / 2));
            //South.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 25, Position.Y + CstConstants.C_iLaneWidth / 2));
            //South.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 30, Position.Y + CstConstants.C_iLaneWidth / 2));
            //South.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 35, Position.Y + CstConstants.C_iLaneWidth / 2));

            //Ost-Nord
            East.Clear();
            East.Add(new Point(Position.X + CstConstants.C_iLaneWidth, Position.Y - CstConstants.C_iLaneWidth / 2));
            East.Add(new Point(Position.X + CstConstants.C_iLaneWidth -5, Position.Y - CstConstants.C_iLaneWidth / 2 ));
            East.Add(new Point(Position.X + CstConstants.C_iLaneWidth -10, Position.Y - CstConstants.C_iLaneWidth / 2 -5));
            East.Add(new Point(Position.X + CstConstants.C_iLaneWidth -15, Position.Y - CstConstants.C_iLaneWidth / 2 -10));
            East.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 15));
            East.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 20));
            East.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 25));
            East.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 30));

            //Nord-West

            //West-Süd
        }

    }
}
