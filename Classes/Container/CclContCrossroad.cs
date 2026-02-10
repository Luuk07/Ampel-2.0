using Ampel__2._0.Classes.Container._base;
using Ampel__2._0.Classes.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampel__2._0.Classes.Container
{
    internal class CclContCrossroad
    {
        public Size WindowSize { get; set; }
        public List<CclContRoad> Roads { get; } = new List<CclContRoad>();

        public CclContCenter Center { get; }

        public List<CclSvcCar> l_AllCars { get; set; } = new List<CclSvcCar>();

        public CclContCrossroad(Size windowSize)
        {
            WindowSize = windowSize;
            Center = new CclContCenter(windowSize);
            Center.CreatArea(2, 2);
            CreatRoads();
        }

        public void CreatRoads()
        {
            for (int i = 1; i <= 4; i++)
            {
                CclContRoad Road;
                switch (i)
                {
                    case 1:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.SouthToNorth,Center, WindowSize);
                        break;
                    case 2:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.EastToWest, Center, WindowSize);
                        break;
                    case 3:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.NorthToSouth, Center, WindowSize);
                        break;
                    case 4:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.WestToEast, Center, WindowSize);
                        break;
                    default:
                        Road = null;
                        break;
                }
                Roads.Add(Road);
            }
        }
    }
}

//Roads.FirstOrDefault(r => r.Direction == Tools.RoadDirection.NorthToSouth).Lanes.Count(), 
//Roads.FirstOrDefault(r => r.Direction == Tools.RoadDirection.EastToWest).Lanes.Count())