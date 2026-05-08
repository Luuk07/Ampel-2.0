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
    internal class CclContRoad: CclContGeometrieBase
    {
        internal List<CclContLane> LanesToCenter {get { return Lanes.Where(l => l.LeedsToTrafficlight).ToList();}}
        internal List<CclContLane> Lanes { get; }
        internal int SpeedLimit { get; set; }
        internal CclContCrossroad Crossroad { get; }
        internal Size WindowSize { get; set; }
        internal CclContCenter Center { get; }
        internal RoadDirection Direction { get; }

        private int TimeFaktor { get; }
        internal int LaneCount { get; }
        internal bool HasSpawnPoint { get; }
        internal CclRandom Random { get; }

        internal CclContTrafficLight TrafficLight { get; }


        public CclContRoad(int speedLimit, CclContCrossroad crossroad, RoadDirection direction, CclContCenter center, Size windowSize, CclRandom random, int laneCount, bool hasSpawnPoint)
        {
            HasSpawnPoint = hasSpawnPoint;
            LaneCount = laneCount;
            SpeedLimit = speedLimit;
            Crossroad = crossroad;
            Direction = direction;
            Center = center;
            Random = random;
            WindowSize = windowSize;
            Lanes = new List<CclContLane>();

            CalculateArea(); 
            CreatLanes();
            TrafficLight = new CclContTrafficLight(this);
        }

        private void CalculateArea()
        {
            switch (Direction)
            {
                case RoadDirection.NorthToSouth:
                    Position = new Point(Center.Position.X, Center.Position.Y - (WindowSize.Height/4));
                    Size = new Size(CstConstants.C_iLaneWidth * LaneCount, WindowSize.Height / 2 - Center.Area.Width );
                    break;
                case RoadDirection.SouthToNorth:
                    Position = new Point(Center.Position.X, Center.Position.Y + WindowSize.Height / 4);
                    Size = new Size(CstConstants.C_iLaneWidth * LaneCount, WindowSize.Height / 2 - Center.Area.Width );
                    break;
                case RoadDirection.EastToWest:
                    Position = new Point(Center.Position.X + WindowSize.Width/4, Center.Position.Y);
                    Size = new Size(WindowSize.Width / 2 - Center.Area.Height, CstConstants.C_iLaneWidth * LaneCount);
                    break;
                case RoadDirection.WestToEast:
                    Position = new Point(Center.Position.X - WindowSize.Width/4, Center.Position.Y);
                    Size = new Size(WindowSize.Width/2 - Center.Area.Height , CstConstants.C_iLaneWidth * LaneCount);
                    break;
            }
        }
        private void CreatLanes()
        {
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    CclContLane Lane = new CclContLane(HasSpawnPoint, true, this, Crossroad, Random, TimeFaktor); 
                    Lanes.Add(Lane);
                }
                else
                {
                    CclContLane Lane = new CclContLane(false, false, this, Crossroad, Random, TimeFaktor);
                    Lanes.Add(Lane);
                } 
            }
        }
    }
}
