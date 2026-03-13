using Ampel__2._0.Classes.Container;
using Ampel__2._0.Classes.Container._base;
using Ampel__2._0.Classes.EventArgs;
using Ampel__2._0.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ampel__2._0.Classes.Services
{
    internal class CclSvcCar : CclContGeometrieBase
    {

        //ToDo: Einen Ereignishändlermethode analog zum Spawnpoint erzeugen, welcher auf das NextStep erreignis reagiert ++
        internal int CurrentSpeed { get; private set; } = 0;

        internal int BreakingDistance { get; set; }

        internal int PufferDistance { get; set; } = 15;

        internal Rectangle PufferArea { get; set; }

        internal Rectangle CheckLineArea;

        internal int MaxSpeed { get; set; } = 8;
        
        internal CclContLane Lane { get; set; } 
                                                                                                                                                     
        internal bool InCenter { get { return Area.Contains(Crossroad.Center.Position); } }
                                                                                            
        internal CarDirection Direction { get; set; } = CarDirection.Straight; 

        private CclContCrossroad Crossroad { get; set; }

        internal int SpeedLimit { get { return Math.Min(MaxSpeed, Lane.Road.SpeedLimit); } }

        internal int deceleration { get; set; } = 1;

        internal int acceleration { get; set; } = 1;


        public CclSvcCar(CclContCrossroad crossroad, CclContLane lane)
        {
            Crossroad = crossroad;  
            Lane = lane;
            Size = new Size(20, 20); 
            Position = lane.StartPoint;
        }
        // Zur Eventhandler Methode
        public void HandleSimulationStep(object sender, CeaNextStepData e)
        {
            MoveBasedOnLane(); 
        }
        private void MoveBasedOnLane()
        {
            if (CheckCarCanDrive())
            {
                //ToDo: Basierend auf dem ob das Auto bremsen muss oder beschleunigen kann die geschwindigkeit anpassen+
                if(CurrentSpeed < SpeedLimit)
                {
                    CurrentSpeed += acceleration;   
                }
            }
            else
            {
                if (CurrentSpeed > 0)
                {
                    CurrentSpeed -= deceleration;
                }
            }
                switch (Lane.Road.Direction)
                {
                    case RoadDirection.NorthToSouth:
                        Position = new Point(Position.X, Position.Y + CurrentSpeed);
                        break;
                    case RoadDirection.SouthToNorth:
                        Position = new Point(Position.X, Position.Y - CurrentSpeed);
                        break;
                    case RoadDirection.WestToEast:
                        Position = new Point(Position.X + CurrentSpeed, Position.Y);
                        break;
                    case RoadDirection.EastToWest:
                        Position = new Point(Position.X - CurrentSpeed, Position.Y);
                        break;
                }
        }


        private bool CheckCarCanDrive()
        {
            //ToDo: Entscheidet, ob das Auto beschleunigen oder verzögern soll+
            //ToDo: Bremsweg wird anhand von currentSpeed und deceleration berechnet+
            BreakingDistance = (CurrentSpeed * CurrentSpeed) / (2 * deceleration);

            CheckLineArea = new Rectangle();

            int distance = BreakingDistance + PufferDistance;
            Point beforCarPosition = new Point();

            switch (Lane.Road.Direction)
            {

                case RoadDirection.NorthToSouth:
                    CheckLineArea = new Rectangle( Area.X, Area.Y + Area.Height, Area.Width, distance);
                    break;

                case RoadDirection.SouthToNorth:
                    CheckLineArea = new Rectangle( Area.X, Area.Y - distance, Area.Width, distance);
                    break;

                case RoadDirection.WestToEast:
                    CheckLineArea = new Rectangle(Area.X + Area.Width, Area.Y,distance, Area.Height);
                    break;

                case RoadDirection.EastToWest:
                    CheckLineArea = new Rectangle( Area.X - distance,Area.Y, distance, Area.Height);
                    break;

            }

            PufferArea = new Rectangle(beforCarPosition.X, beforCarPosition.Y, Size.Width, Size.Height);

            bool blockCar = Crossroad.l_AllCars.Where(car => !ReferenceEquals(car, this)).Any(car => CheckLineArea.IntersectsWith(car.Area)); //ToDo:Funktioniert noch nicht ganz
            bool blockArea = Crossroad.Roads.SelectMany(r => r.Lanes).Any(lane => CheckLineArea.IntersectsWith(lane.StopArea));
            bool isTrafficLightRed = Lane.Road.TrafficLight.CurrentState == TrafficLightState.Red;
            bool isTrafficLighYellow = Lane.Road.TrafficLight.CurrentState == TrafficLightState.Yellow;

            if (blockArea)
            {
                if (isTrafficLightRed || isTrafficLighYellow)
                {
                    return false;
                }
            }
            if (blockCar)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

 



    }
}
