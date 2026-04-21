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

        private int turningIndex = 0;

        internal int BreakingDistance { get; set; }

        internal int PufferDistance { get; set; } = 15;

        internal Rectangle PufferArea { get; set; }

        internal Rectangle CheckLineArea;

        internal int MaxSpeed { get; set; } = 8;
        
        internal CclContLane Lane { get; set; } 
                                                                                                                                                     
        //internal bool InCenter { get { return Area.Contains(Crossroad.Center.Position); } }

        internal bool InCenter { get { return Crossroad.Center.Area.Contains(Position); } }

        internal CarDirection Direction { get; set; }

        private CclContCrossroad Crossroad { get; set; }

        internal int SpeedLimit { get { return Math.Min(MaxSpeed, Lane.Road.SpeedLimit); } }

        internal int deceleration { get; set; }

        internal int acceleration { get; set; }

        private int TimeFaktor { get; }

        private int ActualSpeed { get { return CurrentSpeed * TimeFaktor; } }


        public CclSvcCar(CclContCrossroad crossroad, CclContLane lane, int timeFaktor)
        {
            Direction = (CarDirection)CclRandom.Random.Next(0, 3);
            TimeFaktor = timeFaktor;
            Crossroad = crossroad;  
            Lane = lane;
            Size = new Size(20, 20); 
            Position = lane.StartPoint;
            deceleration = 1 * (int)TimeFaktor;
            acceleration = 1 * (int)TimeFaktor;
        }
  
        public void HandleSimulationStep(object sender, CeaNextStepData e)
        {  
            if (Direction == CarDirection.Right && InCenter)
            {
                TurnRight();
            }
            else
            {
                MoveBasedOnLane();
            }
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
                    Position = new Point(Position.X, Position.Y + ActualSpeed);
                    break;
                case RoadDirection.SouthToNorth:
                    Position = new Point(Position.X, Position.Y - ActualSpeed);
                    break;
                case RoadDirection.WestToEast:
                    Position = new Point(Position.X + ActualSpeed, Position.Y);
                    break;
                case RoadDirection.EastToWest:
                    Position = new Point(Position.X - ActualSpeed, Position.Y);
                    break;
            }
        }


        private bool CheckCarCanDrive()
        {
            //ToDo: Entscheidet, ob das Auto beschleunigen oder verzögern soll+
            //ToDo: Bremsweg wird anhand von currentSpeed und deceleration berechnet+
            BreakingDistance = (ActualSpeed * ActualSpeed) / (2 * deceleration);

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
            bool isTrafficLighYellow = Lane.Road.TrafficLight.CurrentState == TrafficLightState.YellowGreen || Lane.Road.TrafficLight.CurrentState == TrafficLightState.YellowRed;

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

        public void TurnRight()
        {
            //if (turningIndex <= Crossroad.Center.South.Count)
            //{
            //    Position = Crossroad.Center.South[turningIndex];
            //    turningIndex++;
            //}

            // Funktioniert, ist aber nicht so gut, weil der sich zur letzten position hinteleportiert, besser wäre, wenn er sich merken würde wo er aufgehört hat
            foreach (var position in Crossroad.Center.South)
            {
                Position = position;
            }

            //Lane wird übergeben und nicht ermittelt, auch nicht perfekt
            Lane = Crossroad.Roads.SelectMany(r => r.Lanes).FirstOrDefault(l => l.Road.Direction == RoadDirection.WestToEast);
            
        }

 



    }
}
