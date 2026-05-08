using Ampel__2._0.Classes.Container._base;
using Ampel__2._0.Classes.EventArgs;
using Ampel__2._0.Classes.Services;
using Ampel__2._0.Classes.Tools;
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
        public CclContRoad CurrentRoad { get; private set; }
        public List<CclContRoad> Roads { get; } = new List<CclContRoad>();
        public CclContCenter Center { get; }
        public List<CclSvcCar> l_AllCars { get; set; } = new List<CclSvcCar>(); // ToDo: Irgendwann autos entfernen+
        public CclRandom Random { get; }
        public CclSvcTrafficLightManager TrafficLightManager { get; set; }

        public List<CclContLane> Lanes
        {
            get { return Roads.SelectMany(r => r.Lanes).ToList(); }
        }
        internal int TimeFaktor { get; set; } = 2;
        public CclContCrossroad(Size windowSize, CclRandom random)
        {
            WindowSize = windowSize;
            Random = random;
            Center = new CclContCenter(windowSize);
            TrafficLightManager = new CclSvcTrafficLightManager(this);
            Center.CreatArea(2, 2);
            Center.CalculateCurveRight();
            Center.CalculateCurveLeft();
            CreatRoads();
        }
        public void HandleTimeFactor(object sender, int newValue)
        {
            TimeFaktor = newValue;
            foreach (var car in l_AllCars)
            {
                car.updateSpeed();
            }
        }

        public void CreatRoads()
        {
            for (int i = 1; i <= 4; i++)
            {
                CclContRoad Road;
                switch (i)
                {
                    case 1:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.SouthToNorth,Center, WindowSize, Random, 2, true);
                        break;
                    case 2:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.WestToEast, Center, WindowSize, Random, 2, true);
                        break;
                    case 3:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.NorthToSouth, Center, WindowSize, Random, 2, true);
                        break;
                    case 4:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.EastToWest, Center, WindowSize, Random, 2, true);
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
