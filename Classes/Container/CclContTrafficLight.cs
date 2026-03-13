using Ampel__2._0.Classes.Container._base;
using Ampel__2._0.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;




namespace Ampel__2._0.Classes.Container
{
    internal class CclContTrafficLight: CclContGeometrieBase
    {
        internal CclContRoad Road { get; }
        internal TrafficLightState CurrentState { get; set; }
        internal int RedLightSeconds { get; set; }
        public CclContTrafficLight(CclContRoad road) 
        {
            Road = road;
            CalculateArea();
        }


        private void CalculateArea()
        {
            switch (Road.Direction)
            {
                case RoadDirection.NorthToSouth:
                    Position = new Point(Road.Position.X - Road.Size.Width/2, Road.Position.Y + Road.Size.Height/2);
                    Size = new Size(10,10);
                    break;
                case RoadDirection.SouthToNorth:
                    Position = new Point(Road.Position.X + Road.Size.Width / 2, Road.Position.Y - Road.Size.Height / 2);
                    Size = new Size(10, 10);
                    break;
                case RoadDirection.EastToWest:
                    Position = new Point(Road.Position.X - Road.Size.Width / 2, Road.Position.Y - Road.Size.Height / 2);
                    Size = new Size(10, 10);
                    break;
                case RoadDirection.WestToEast:
                    Position = new Point(Road.Position.X + Road.Size.Width / 2, Road.Position.Y + Road.Size.Height / 2);
                    Size = new Size(10, 10);
                    break;
            }
        }
    }
}
