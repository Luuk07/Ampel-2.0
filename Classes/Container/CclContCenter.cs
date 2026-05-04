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
    internal class CclContCenter : CclContGeometrieBase
    {
        internal Size WindowSize { get; }
        // Rechts abbiegen
        internal List<Point> SouthEast { get; set; }
        internal List<Point> WestSouth { get; set; }
        internal List<Point> EastNorth { get; set; }
        internal List<Point> NorthWest { get; set; }


        // Links abbiegen

        internal List<Point> SouthWest { get; set; }
        internal List<Point> WestNorth { get; set; }
        internal List<Point> EastSouth { get; set; }
        internal List<Point> NorthEast { get; set; }


        public CclContCenter(Size windowSize)
        {
            WindowSize = windowSize;
            SouthEast = new List<Point>();
            EastNorth = new List<Point>();
            NorthWest = new List<Point>();
            WestSouth = new List<Point>();
            SouthWest = new List<Point>();
            EastSouth = new List<Point>();
            NorthEast = new List<Point>();
            WestNorth = new List<Point>();
        
          
        }

        public void CreatArea(int lanesInCrossroadNorthSouth, int lanesInCrossroadSouthWest)
        {
            Position = new Point(WindowSize.Width / 2, WindowSize.Height / 2);
            Size = new Size(CstConstants.C_iLaneWidth * lanesInCrossroadSouthWest, CstConstants.C_iLaneWidth * lanesInCrossroadNorthSouth);
        }

        public void CalculateCurveRight()
        {
            //Süd-Ost
            SouthEast.Clear();
            SouthEast.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2, Position.Y + CstConstants.C_iLaneWidth));
            SouthEast.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2, Position.Y + CstConstants.C_iLaneWidth - 5));
            SouthEast.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 + 5, Position.Y + CstConstants.C_iLaneWidth - 10));
            SouthEast.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 + 10, Position.Y + CstConstants.C_iLaneWidth - 15));
            SouthEast.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 + 15, Position.Y + CstConstants.C_iLaneWidth - 15));
            SouthEast.Add(new Point(Position.X + CstConstants.C_iLaneWidth, Position.Y + CstConstants.C_iLaneWidth / 2));
            SouthEast.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 5, Position.Y + CstConstants.C_iLaneWidth / 2));
            SouthEast.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 10, Position.Y + CstConstants.C_iLaneWidth / 2));
            SouthEast.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 15, Position.Y + CstConstants.C_iLaneWidth / 2));
           
           

            //Ost-Nord
            EastNorth.Clear();
            EastNorth.Add(new Point(Position.X + CstConstants.C_iLaneWidth, Position.Y - CstConstants.C_iLaneWidth / 2));
            EastNorth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 5, Position.Y - CstConstants.C_iLaneWidth / 2));
            EastNorth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 10, Position.Y - CstConstants.C_iLaneWidth / 2 - 5));
            EastNorth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 15, Position.Y - CstConstants.C_iLaneWidth / 2 - 10));
            EastNorth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 15));
            EastNorth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 20));
            EastNorth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 25));
            EastNorth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 30));

            //Nord-West
            NorthWest.Clear();
            NorthWest.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2, Position.Y - CstConstants.C_iLaneWidth));
            NorthWest.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2, Position.Y - CstConstants.C_iLaneWidth + 5));
            NorthWest.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 5, Position.Y - CstConstants.C_iLaneWidth + 10));
            NorthWest.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 10, Position.Y - CstConstants.C_iLaneWidth + 15));
            NorthWest.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 15, Position.Y - CstConstants.C_iLaneWidth + 20));
            NorthWest.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 20, Position.Y - CstConstants.C_iLaneWidth + 20));
            NorthWest.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 25, Position.Y - CstConstants.C_iLaneWidth + 20));
            NorthWest.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 30, Position.Y - CstConstants.C_iLaneWidth + 20));

            //West-Süd
            WestSouth.Clear();
            WestSouth.Add(new Point(Position.X - CstConstants.C_iLaneWidth, Position.Y + CstConstants.C_iLaneWidth / 2));
            WestSouth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 5, Position.Y + CstConstants.C_iLaneWidth / 2));
            WestSouth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 10, Position.Y + CstConstants.C_iLaneWidth / 2 + 5));
            WestSouth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 15, Position.Y + CstConstants.C_iLaneWidth / 2 + 10));
            WestSouth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 20, Position.Y + CstConstants.C_iLaneWidth / 2 + 15));
            WestSouth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 20, Position.Y + CstConstants.C_iLaneWidth / 2 + 20));
            WestSouth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 20, Position.Y + CstConstants.C_iLaneWidth / 2 + 25));
            WestSouth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 20, Position.Y + CstConstants.C_iLaneWidth / 2 + 30));

        }
        public void CalculateCurveLeft()
        {
            //Süd-West
            SouthWest.Clear();
            SouthWest.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2, Position.Y + CstConstants.C_iLaneWidth));
            SouthWest.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 - 2, Position.Y + CstConstants.C_iLaneWidth - 2));
            SouthWest.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 - 4, Position.Y + CstConstants.C_iLaneWidth - 4));
            SouthWest.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 - 5, Position.Y + CstConstants.C_iLaneWidth - 5));
            SouthWest.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 - 5, Position.Y + CstConstants.C_iLaneWidth - 10));
            SouthWest.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 - 10, Position.Y + CstConstants.C_iLaneWidth - 15));
            SouthWest.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 - 10, Position.Y + CstConstants.C_iLaneWidth - 20));
            SouthWest.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 - 15, Position.Y + CstConstants.C_iLaneWidth - 25));
            SouthWest.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 - 15, Position.Y + CstConstants.C_iLaneWidth - 30));
            SouthWest.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 - 15, Position.Y + CstConstants.C_iLaneWidth - 35));
            SouthWest.Add(new Point(Position.X , Position.Y));
            SouthWest.Add(new Point(Position.X - 5, Position.Y - 5));
            SouthWest.Add(new Point(Position.X - 10, Position.Y - 7));
            SouthWest.Add(new Point(Position.X - 15, Position.Y - 10));
            SouthWest.Add(new Point(Position.X - 20, Position.Y - 12));
            SouthWest.Add(new Point(Position.X - 25, Position.Y - 14));
            SouthWest.Add(new Point(Position.X - 30, Position.Y - 16));
            SouthWest.Add(new Point(Position.X - 35, Position.Y - 18));
            SouthWest.Add(new Point(Position.X - 40, Position.Y - 20));
            SouthWest.Add(new Point(Position.X - 45, Position.Y - 20));
            SouthWest.Add(new Point(Position.X - 50, Position.Y - 20));

            //Ost-Süd
            EastSouth.Clear();
            EastSouth.Add(new Point(Position.X + CstConstants.C_iLaneWidth, Position.Y - CstConstants.C_iLaneWidth / 2));
            EastSouth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 5, Position.Y - CstConstants.C_iLaneWidth / 2));
            EastSouth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 10, Position.Y - CstConstants.C_iLaneWidth / 2 + 2));
            EastSouth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 15, Position.Y - CstConstants.C_iLaneWidth / 2 + 4));
            EastSouth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 + 6));
            EastSouth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 25, Position.Y - CstConstants.C_iLaneWidth / 2 + 10));
            EastSouth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 30, Position.Y - CstConstants.C_iLaneWidth / 2 + 14));
            EastSouth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 35, Position.Y - CstConstants.C_iLaneWidth / 2 + 18));
            EastSouth.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 40, Position.Y - CstConstants.C_iLaneWidth / 2 + 20));
            EastSouth.Add(new Point(Position.X, Position.Y));
            EastSouth.Add(new Point(Position.X - 2, Position.Y + 5));
            EastSouth.Add(new Point(Position.X - 4, Position.Y + 10));
            EastSouth.Add(new Point(Position.X - 7, Position.Y + 15));
            EastSouth.Add(new Point(Position.X - 10, Position.Y + 20));
            EastSouth.Add(new Point(Position.X - 12, Position.Y + 25));
            EastSouth.Add(new Point(Position.X - 14, Position.Y + 30));
            EastSouth.Add(new Point(Position.X - 17, Position.Y + 35));
            EastSouth.Add(new Point(Position.X - 20, Position.Y + 40));
            EastSouth.Add(new Point(Position.X - 20, Position.Y + 45));
            EastSouth.Add(new Point(Position.X - 20, Position.Y + 50));

            //Nord-Ost
            NorthEast.Clear();
            NorthEast.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2, Position.Y - CstConstants.C_iLaneWidth));
            NorthEast.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 + 2, Position.Y - CstConstants.C_iLaneWidth + 5));
            NorthEast.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 + 4, Position.Y - CstConstants.C_iLaneWidth + 10));
            NorthEast.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 + 6, Position.Y - CstConstants.C_iLaneWidth + 15));
            NorthEast.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 + 8, Position.Y - CstConstants.C_iLaneWidth + 20));
            NorthEast.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 + 10, Position.Y - CstConstants.C_iLaneWidth + 25));
            NorthEast.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 + 13, Position.Y - CstConstants.C_iLaneWidth + 30));
            NorthEast.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 + 16, Position.Y - CstConstants.C_iLaneWidth + 35));
            NorthEast.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 + 20, Position.Y - CstConstants.C_iLaneWidth + 40));
            NorthEast.Add(new Point(Position.X, Position.Y));
            NorthEast.Add(new Point(Position.X + 5, Position.Y + 2));
            NorthEast.Add(new Point(Position.X + 10, Position.Y + 4));
            NorthEast.Add(new Point(Position.X + 15, Position.Y + 7));
            NorthEast.Add(new Point(Position.X + 20, Position.Y + 10));
            NorthEast.Add(new Point(Position.X + 25, Position.Y + 12));
            NorthEast.Add(new Point(Position.X + 30, Position.Y + 14));
            NorthEast.Add(new Point(Position.X + 35, Position.Y + 17));
            NorthEast.Add(new Point(Position.X + 40, Position.Y + 20));
            NorthEast.Add(new Point(Position.X + 45, Position.Y + 20));
            NorthEast.Add(new Point(Position.X + 50, Position.Y + 20));

            //West-Nord
            WestNorth.Clear();
            WestNorth.Add(new Point(Position.X - CstConstants.C_iLaneWidth, Position.Y + CstConstants.C_iLaneWidth / 2));
            WestNorth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 5, Position.Y + CstConstants.C_iLaneWidth / 2));
            WestNorth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 10, Position.Y + CstConstants.C_iLaneWidth / 2 - 2));
            WestNorth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 15, Position.Y + CstConstants.C_iLaneWidth / 2 - 4));
            WestNorth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 20, Position.Y + CstConstants.C_iLaneWidth / 2 - 7));
            WestNorth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 25, Position.Y + CstConstants.C_iLaneWidth / 2 - 10));
            WestNorth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 30, Position.Y + CstConstants.C_iLaneWidth / 2 - 13));
            WestNorth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 35, Position.Y + CstConstants.C_iLaneWidth / 2 - 17));
            WestNorth.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 40, Position.Y + CstConstants.C_iLaneWidth / 2 - 20));
            WestNorth.Add(new Point(Position.X, Position.Y));
            WestNorth.Add(new Point(Position.X + 2, Position.Y - 5));
            WestNorth.Add(new Point(Position.X + 4, Position.Y - 10));
            WestNorth.Add(new Point(Position.X + 7, Position.Y - 15));
            WestNorth.Add(new Point(Position.X + 10, Position.Y- 20));
            WestNorth.Add(new Point(Position.X + 12, Position.Y- 25));
            WestNorth.Add(new Point(Position.X + 14, Position.Y- 30));
            WestNorth.Add(new Point(Position.X + 17, Position.Y- 35));
            WestNorth.Add(new Point(Position.X + 20, Position.Y- 40));
            WestNorth.Add(new Point(Position.X + 20, Position.Y- 45));
            WestNorth.Add(new Point(Position.X + 20, Position.Y - 50));
        }
    }
}
