using Ampel__2._0.Classes.Container._base;
using Ampel__2._0.Classes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampel__2._0.Classes.Container
{
    internal class CclContCrossroad
    {
        public Form1 Form { get; set; }
        public List<CclContRoad> Roads { get; } = new List<CclContRoad>();

        public CclContCenter Center { get; }

        public List<CclSvcCar> l_AllCars { get; set; } = new List<CclSvcCar>();

        public CclContCrossroad(Form1 form)
        {
            Form = form;
            Center = new CclContCenter(form);
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
                        Road = new CclContRoad(50, this, Tools.RoadDirection.SouthToNorth,Center, Form);
                        break;
                    case 2:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.EastToWest, Center, Form);
                        break;
                    case 3:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.NorthToSouth, Center, Form);
                        break;
                    case 4:
                        Road = new CclContRoad(50, this, Tools.RoadDirection.WestToEast, Center, Form);
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