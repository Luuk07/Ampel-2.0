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
        internal Rectangle StopArea { get; } //ToDo: Abhängig von der Straße, die Position des Stopbereichs festlegen ++
        internal Point StartPoint { get; } //ToDO: Abhängig von der Straße, die Position des Startpunkts festlegen++

        internal CclContRoad Road { get; }
        internal bool LeedsToTrafficlight { get; set; }

        internal List<CclSvcCar> l_carsInLane { get; } = new List<CclSvcCar>();



        public CclContLane(bool leedsToTrafficLight, CclContRoad road)
        {
            LeedsToTrafficlight = leedsToTrafficLight;
            Road = road;
           
            if (LeedsToTrafficlight)
            {
                switch (Road.Direction)
                {
                    case RoadDirection.NorthToSouth:
                        Size = new Size(CstConstants.C_iLaneWidth, Road.Size.Height);
                        Position = new Point(Road.Position.X - CstConstants.C_iLaneWidth/2, Road.Position.Y);
                        break;
                    case RoadDirection.SouthToNorth:
                        Size = new Size(CstConstants.C_iLaneWidth, Road.Size.Height);
                        Position = new Point(Road.Position.X + CstConstants.C_iLaneWidth / 2, Road.Position.Y);
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
                        Position = new Point(Road.Position.X, Road.Position.Y - CstConstants.C_iLaneWidth / 2);
                        break;
                    case RoadDirection.WestToEast:
                        Size = new Size(Road.Size.Width, CstConstants.C_iLaneWidth);
                        Position = new Point(Road.Position.X, Road.Position.Y + CstConstants.C_iLaneWidth / 2);
                        break;
                }
            }
            StopArea = CalculateStopArea();
            StartPoint = CalculateStartPoint();

            //ToDo: Berechnung der Area der Lane abhängig von der Straße ++
            //ToDo: Methode erstellen die Stop Area ++ und Start Point berechnet ++
        }


        private Rectangle CalculateStopArea()
        {   
            if (!LeedsToTrafficlight)
            {
                return Rectangle.Empty;
            }
            var stopArea = new Rectangle();

            switch (Road.Direction)
            {
                case RoadDirection.NorthToSouth:
                    stopArea = new Rectangle(Position.X - Size.Width / 2, Position.Y - Size.Height / 2, Size.Width, Size.Height / 2);
                    break;
                case RoadDirection.SouthToNorth:
                    stopArea = new Rectangle(Position.X - Size.Width / 2, Position.Y, Size.Width, Size.Height / 2);
                    break;
                case RoadDirection.EastToWest:
                    stopArea = new Rectangle(Position.X - Size.Width / 2, Position.Y + Size.Height / 2, Size.Height, Size.Width/2);
                    break;
                case RoadDirection.WestToEast:
                    stopArea = new Rectangle(Position.X, Position.Y + Size.Height / 2, Size.Width/2, Size.Height);
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
                    StartPoint = new Point(Position.X, Position.Y - Size.Height / 2);
                    break;
                case RoadDirection.SouthToNorth:
                    StartPoint = new Point(Position.X, Position.Y + Size.Height / 2);
                    break;
                case RoadDirection.EastToWest:
                    StartPoint = new Point(Position.X - Size.Width / 2, Position.Y);
                    break;
                case RoadDirection.WestToEast:
                    StartPoint = new Point(Position.X + Size.Width / 2, Position.Y);
                    break;
            }
            return StartPoint;
        }

        
    }
}
