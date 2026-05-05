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

        private CarSpawnPoint spawnPoint; 

        private Queue<Point> queueRight;  // Ist wie eine Warteschlange, erstes Element was drin ist wird als erstes genommen und gelöscht (mit der Methode Dequeue())

        private Queue<Point> queueLeft;
        internal int BreakingDistance { get; set; }

        internal int PufferDistance { get; set; } = 15;

        internal Rectangle PufferArea { get; set; }

        internal Rectangle CheckLineArea;

        internal int MaxSpeed { get; set; } = 3;
        
        internal CclContLane Lane { get; set; } 
                                                                                                                                                    
        internal bool InCenter { get { return Crossroad.Center.Area.IntersectsWith(Area); } }

        internal CarDirection Direction { get; set; }

        private CclContCrossroad Crossroad { get; set; }

        internal int SpeedLimit { get { return Math.Min(MaxSpeed, Lane.Road.SpeedLimit); } }

        internal int deceleration { get; set; }

        internal int acceleration { get; set; }

        private int TimeFaktor { get; }

        private int ActualSpeed { get { return CurrentSpeed * TimeFaktor; } }

        internal bool IsOut { get 
            {
                if (Position.X >= 700 || Position.X < 0 || Position.Y >= 700 || Position.Y < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //ToDo: Boolean der sagt er ist raus++
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
       
       
            switch (Lane.Road.Direction)
            {
                case RoadDirection.NorthToSouth:
                    queueRight = new Queue<Point>(Crossroad.Center.NorthWest);
                    spawnPoint = CarSpawnPoint.North;
                    break;
                case RoadDirection.SouthToNorth:
                    queueRight = new Queue<Point>(Crossroad.Center.SouthEast);
                    spawnPoint = CarSpawnPoint.South;
                    break;
                case RoadDirection.WestToEast:
                    queueRight = new Queue<Point>(Crossroad.Center.WestSouth);
                    spawnPoint = CarSpawnPoint.West;
                    break;
                case RoadDirection.EastToWest:
                    queueRight = new Queue<Point>(Crossroad.Center.EastNorth);
                    spawnPoint = CarSpawnPoint.East;
                    break;
            }
            switch (Lane.Road.Direction)
            {
                case RoadDirection.NorthToSouth:
                    queueLeft = new Queue<Point>(Crossroad.Center.NorthEast);
                    spawnPoint = CarSpawnPoint.North;
                    break;
                case RoadDirection.SouthToNorth:
                    queueLeft = new Queue<Point>(Crossroad.Center.SouthWest);
                    spawnPoint = CarSpawnPoint.South;
                    break;
                case RoadDirection.WestToEast:
                    queueLeft = new Queue<Point>(Crossroad.Center.WestNorth);
                    spawnPoint = CarSpawnPoint.West;
                    break;
                case RoadDirection.EastToWest:
                    queueLeft = new Queue<Point>(Crossroad.Center.EastSouth);
                    spawnPoint = CarSpawnPoint.East;
                    break;
            }


        }
  
        public void HandleSimulationStep(object sender, CeaNextStepData e)
        {
            if (IsOut)
            {
                Crossroad.l_AllCars.Remove(this);
            }

            if (Direction == CarDirection.Right && InCenter)
            {
                TurnRight();

            }
            else if (Direction == CarDirection.Left && InCenter)
            {
                TurnLeft();
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

         
            if (queueRight == null || queueRight.Count == 0 || !InCenter)
            {
                return;
            }

            

            Position = queueRight.Dequeue(); // Nimmt, das erste element und entfernt es danach dauerhaft


            switch (spawnPoint)
            {
                case CarSpawnPoint.North:
                    //ToDo: SelectMany für Roads in Crossroad
                    Lane = Crossroad.Roads.SelectMany(r => r.Lanes).FirstOrDefault(l => l.Road.Direction == RoadDirection.EastToWest);
                    break;
                case CarSpawnPoint.West:
                    Lane = Crossroad.Roads.SelectMany(r => r.Lanes).FirstOrDefault(l => l.Road.Direction == RoadDirection.NorthToSouth);
                    break;
                case CarSpawnPoint.South:
                    Lane = Crossroad.Roads.SelectMany(r => r.Lanes).FirstOrDefault(l => l.Road.Direction == RoadDirection.WestToEast);
                    break;
                case CarSpawnPoint.East:       
                    Lane = Crossroad.Roads.SelectMany(r => r.Lanes).FirstOrDefault(l => l.Road.Direction == RoadDirection.SouthToNorth);
                    break;
            }

        }

        public void TurnLeft()
        {
            if (queueLeft == null || queueLeft.Count == 0 || !InCenter)
            {
                return;
            }

            //ToDO: Soll die nächste Position angucken und prüfen, ob da auto ist
            Point NextPosition = queueLeft.Peek();

            Point[] Positions = queueLeft.ToArray();
            int counter = 0;
            foreach (var position in Positions)
            {
                if (Crossroad.l_AllCars.Where(c => c != this).Any(c => c.Area.Contains(position)))
                {
                    return;
                }
                if (counter >= 3)
                {
                    counter = 0;
                    break;
                }
                counter++;
            }
            //if (Crossroad.l_AllCars.Where(c => c != this).Any(c => c.Area.Contains(NextPosition)))
            //{
            //    return;
            //}

            Position = queueLeft.Dequeue();
            switch (spawnPoint)
            {
                case CarSpawnPoint.North:
                    //ToDo: SelectMany für Roads in Crossroad
                    Lane = Crossroad.Roads.SelectMany(r => r.Lanes).FirstOrDefault(l => l.Road.Direction == RoadDirection.WestToEast);
                    break;
                case CarSpawnPoint.West:
                    Lane = Crossroad.Roads.SelectMany(r => r.Lanes).FirstOrDefault(l => l.Road.Direction == RoadDirection.SouthToNorth);
                    break;
                case CarSpawnPoint.South:
                    Lane = Crossroad.Roads.SelectMany(r => r.Lanes).FirstOrDefault(l => l.Road.Direction == RoadDirection.EastToWest);
                    break;
                case CarSpawnPoint.East:
                    Lane = Crossroad.Roads.SelectMany(r => r.Lanes).FirstOrDefault(l => l.Road.Direction == RoadDirection.NorthToSouth);
                    break;
            }
        }


    }
}
