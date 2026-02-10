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
        internal List<CclContLane> Lanes { get; }
        internal double SpeedLimit { get; set; }
        internal CclContCrossroad Crossroad { get; }
        internal Size WindowSize { get; set; }
        internal CclContCenter Center { get; }
        internal RoadDirection Direction { get; }

        public CclContRoad(double speedLimit, CclContCrossroad crossroad, RoadDirection direction, CclContCenter center, Size windowSize)
        {
            SpeedLimit = speedLimit;
            Crossroad = crossroad;
            Direction = direction;
            Center = center;
            Lanes = new List<CclContLane>();
            WindowSize = windowSize;

            CalculateArea();
            CreatLanes();
            //ToDo: bekommen Center++
            //ToDo: Client Rectangle übergeben, um ihre Länge zu Berchen

        }

        //ToDo: Höhe und Breite anhand von Center und Client Rectangle des Windows berechnen ++
             // Habe das jetzt doch mit Client Size gemacht
        private void CalculateArea()
        {
           
            switch (Direction)
            {
                case RoadDirection.NorthToSouth:
                    Position = new Point(Center.Position.X, Center.Position.Y - WindowSize.Height/4);
                    Size = new Size(CstConstants.C_iLaneWidth * Lanes.Count, WindowSize.Height / 2 + 20);
                    break;
                case RoadDirection.SouthToNorth:
                    Position = new Point(Center.Position.X, Center.Position.Y + WindowSize.Height / 4);
                    Size = new Size(CstConstants.C_iLaneWidth * Lanes.Count, WindowSize.Height / 2 + 20);
                    break;
                case RoadDirection.EastToWest:
                    Position = new Point(Center.Position.X - WindowSize.Width/4, Center.Position.Y);
                    Size = new Size(WindowSize.Width / 2 - 20, CstConstants.C_iLaneWidth * Lanes.Count);
                    break;
                case RoadDirection.WestToEast:
                    Position = new Point(Center.Position.X + WindowSize.Width / 4, Center.Position.Y);
                    Size = new Size(WindowSize.Width/2 - 20, CstConstants.C_iLaneWidth * Lanes.Count);
                    break;
            }
        }

        // Lanes werden richtig erzeugt aber falsch gezeichnet, keine Ahnung wo der Fehler ist da
        private void CreatLanes()
        {
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    CclContLane Lane = new CclContLane(true, this);
                    Lanes.Add(Lane);
                }
                else
                {
                    CclContLane Lane = new CclContLane(false, this);
                    Lanes.Add(Lane);
                } 
            }
        }
    }
}
