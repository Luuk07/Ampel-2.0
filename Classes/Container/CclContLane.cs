using Ampel__2._0.Classes.Container._base;
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
    internal class CclContLane : CclContGeometrieBase
    {
        internal Rectangle StopArea { get; } 
        internal Point StartPoint { get; } 

        // Außerhalb vom Fenster
        internal CclSvcSpawnPoint SpawnPoint { get; }

        //Bisschen übers Fenster
        internal CclContRoad Road { get; }
        internal bool LeedsToTrafficlight { get { return SpawnPoint != null; } }
        internal List<CclSvcCar> l_carsInLane { get; } = new List<CclSvcCar>();
        internal double SpawnChance { get; set; }

        private int TimeFaktor { get; }
        public CclContLane(bool createSpawnPoint, bool leedsToTrafficLight, CclContRoad road, CclContCrossroad crossroad, CclRandom random, int timeFaktor)
        {
            //LeedsToTrafficlight = leedsToTrafficLight;
            Road = road;
            TimeFaktor = timeFaktor;


            if (createSpawnPoint)
            {
                SpawnChance = 0.02;
                switch (Road.Direction)
                {
                    case RoadDirection.NorthToSouth:
                        Size = new Size(CstConstants.C_iLaneWidth, Road.Size.Height);
                        Position = new Point(Road.Position.X - CstConstants.C_iLaneWidth/2, Road.Position.Y);
                        SpawnPoint = new CclSvcSpawnPoint(this, crossroad, random, SpawnChance);
                        break;
                    case RoadDirection.SouthToNorth:
                        Size = new Size(CstConstants.C_iLaneWidth, Road.Size.Height);
                        Position = new Point(Road.Position.X + CstConstants.C_iLaneWidth / 2, Road.Position.Y);
                        SpawnPoint = new CclSvcSpawnPoint(this, crossroad, random, SpawnChance);
                        break;
                    case RoadDirection.EastToWest:
                        Size = new Size(Road.Size.Width, CstConstants.C_iLaneWidth);
                        Position = new Point(Road.Position.X, Road.Position.Y - CstConstants.C_iLaneWidth / 2);
                        SpawnPoint = new CclSvcSpawnPoint(this, crossroad, random, SpawnChance);
                        break;
                    case RoadDirection.WestToEast:
                        Size = new Size(Road.Size.Width, CstConstants.C_iLaneWidth);
                        Position = new Point(Road.Position.X, Road.Position.Y + CstConstants.C_iLaneWidth / 2);
                        SpawnPoint = new CclSvcSpawnPoint(this, crossroad, random, SpawnChance);
                        break;
                }
                StopArea = CalculateStopArea();
                StartPoint = CalculateStartPoint();
            }
            else
            {
                switch (Road.Direction)
                {
                    case RoadDirection.NorthToSouth:
                        Size = new Size(CstConstants.C_iLaneWidth, Road.Size.Height);
                        Position = new Point(Road.Position.X + CstConstants.C_iLaneWidth / 2, Road.Position.Y);
                        break;
                    case RoadDirection.SouthToNorth:
                        Size = new Size(CstConstants.C_iLaneWidth, Road.Size.Height);
                        Position = new Point(Road.Position.X - CstConstants.C_iLaneWidth / 2, Road.Position.Y);
                        break;
                    case RoadDirection.EastToWest:
                        Size = new Size(Road.Size.Width, CstConstants.C_iLaneWidth);
                        Position = new Point(Road.Position.X, Road.Position.Y + CstConstants.C_iLaneWidth / 2);
                        break;
                    case RoadDirection.WestToEast:
                        Size = new Size(Road.Size.Width, CstConstants.C_iLaneWidth);
                        Position = new Point(Road.Position.X, Road.Position.Y - CstConstants.C_iLaneWidth / 2);
                        break;
                }
            }
         
        }

        private Rectangle CalculateStopArea()
        {   
            if (!LeedsToTrafficlight)
            {
                return Rectangle.Empty;
            }
            var stopArea = new Rectangle();
            double stopAreaPercentage = 0.20; 


            switch (Road.Direction)
            {
                case RoadDirection.NorthToSouth:
                    stopArea = new Rectangle(Area.X, Area.Y + Area.Height - (int)(Area.Height * stopAreaPercentage), Area.Width, (int)(Area.Height * stopAreaPercentage));
                    break;

                case RoadDirection.SouthToNorth:
                    stopArea = new Rectangle(Area.X, Area.Y, Area.Width, (int)(Area.Height * stopAreaPercentage));
                    break;

                case RoadDirection.EastToWest:
                    stopArea = new Rectangle(Area.X, Area.Y, (int)(Area.Height * stopAreaPercentage),Area.Height);
                    break;

                case RoadDirection.WestToEast:
                    stopArea = new Rectangle(Area.X + Area.Width - (int)(Area.Height * stopAreaPercentage),Area.Y,(int)(Area.Height * stopAreaPercentage), Area.Height);
                    break;
            }
            return stopArea;
        }

     
        private Point CalculateStartPoint()
        {
            if (!LeedsToTrafficlight)
            {
                return Point.Empty;
            }

            var StartPoint = new Point();
            switch (Road.Direction)
            {
                case RoadDirection.NorthToSouth:
                    StartPoint = new Point(Position.X, Position.Y - Size.Height / 2 - 20 );
                    break;
                case RoadDirection.SouthToNorth:
                    StartPoint = new Point(Position.X, Position.Y + Size.Height / 2 + 20 );
                    break;
                case RoadDirection.EastToWest:
                    StartPoint = new Point(Position.X + Size.Width / 2 + 20 , Position.Y);
                    break;
                case RoadDirection.WestToEast:
                    StartPoint = new Point(Position.X - Size.Width / 2 - 20, Position.Y);
                    break;
            }
            return StartPoint;
        }
    }
}
