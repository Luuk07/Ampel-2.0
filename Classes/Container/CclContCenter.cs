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

        //internal List<Point> South { get; set; }
        //internal List <Point> West   { get; set; }
        //internal List <Point> East   { get; set; }
        //internal List <Point> North  { get; set; }

        internal List <Point> Queue { get; set; }

        public CclContCenter(Size windowSize)
        {
            WindowSize = windowSize;
            //South = new List<Point>();
            //East = new List<Point>();
            //North = new List<Point>();
            //West = new List<Point>();
            Queue = new List<Point>();
        }

        public void CreatArea(int lanesInCrossroadNorthSouth, int lanesInCrossroadSouthWest) 
        {
            Position = new Point(WindowSize.Width / 2, WindowSize.Height / 2);
            Size = new Size(CstConstants.C_iLaneWidth * lanesInCrossroadSouthWest, CstConstants.C_iLaneWidth * lanesInCrossroadNorthSouth);
        }

        public List<Point> CalculateCurveRight(CarSpawnPoint spawnPoint)
        {
            //Süd-Ost
            Queue.Clear();
            switch (spawnPoint)
            {
                case CarSpawnPoint.South:
                    //Süd-Ost
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2, Position.Y + CstConstants.C_iLaneWidth));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2, Position.Y + CstConstants.C_iLaneWidth - 5));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 + 5, Position.Y + CstConstants.C_iLaneWidth - 10));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 + 10, Position.Y + CstConstants.C_iLaneWidth - 15));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth / 2 + 15, Position.Y + CstConstants.C_iLaneWidth - 15));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth, Position.Y + CstConstants.C_iLaneWidth / 2));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 5, Position.Y + CstConstants.C_iLaneWidth / 2));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 10, Position.Y + CstConstants.C_iLaneWidth / 2));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth + 15, Position.Y + CstConstants.C_iLaneWidth / 2));
                    break;
                case CarSpawnPoint.East:
                    //Ost-Nord
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth, Position.Y - CstConstants.C_iLaneWidth / 2));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 5, Position.Y - CstConstants.C_iLaneWidth / 2));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 10, Position.Y - CstConstants.C_iLaneWidth / 2 - 5));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 15, Position.Y - CstConstants.C_iLaneWidth / 2 - 10));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 15));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 20));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 25));
                    Queue.Add(new Point(Position.X + CstConstants.C_iLaneWidth - 20, Position.Y - CstConstants.C_iLaneWidth / 2 - 30));
                    break;
                case CarSpawnPoint.West:
                    //West-Süd
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth, Position.Y + CstConstants.C_iLaneWidth / 2));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 5, Position.Y + CstConstants.C_iLaneWidth / 2));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 10, Position.Y + CstConstants.C_iLaneWidth / 2 + 5));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 15, Position.Y + CstConstants.C_iLaneWidth / 2 + 10));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 20, Position.Y + CstConstants.C_iLaneWidth / 2 + 15));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 20, Position.Y + CstConstants.C_iLaneWidth / 2 + 20));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 20, Position.Y + CstConstants.C_iLaneWidth / 2 + 25));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth + 20, Position.Y + CstConstants.C_iLaneWidth / 2 + 30));
                    break;
                case CarSpawnPoint.North:
                    //Nord-West
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2, Position.Y - CstConstants.C_iLaneWidth));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2, Position.Y - CstConstants.C_iLaneWidth + 5));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 5, Position.Y - CstConstants.C_iLaneWidth + 10));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 10, Position.Y - CstConstants.C_iLaneWidth + 15));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 15, Position.Y - CstConstants.C_iLaneWidth + 20));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 20, Position.Y - CstConstants.C_iLaneWidth + 20));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 25, Position.Y - CstConstants.C_iLaneWidth + 20));
                    Queue.Add(new Point(Position.X - CstConstants.C_iLaneWidth / 2 - 30, Position.Y - CstConstants.C_iLaneWidth + 20));
                    break;


            }

            return Queue;




        }

    }
}
