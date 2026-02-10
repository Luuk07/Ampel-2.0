using Ampel__2._0.Classes.Container;
using Ampel__2._0.Classes.Container._base;
using Ampel__2._0.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampel__2._0.Classes.Services
{
    internal class CclSvcCar : CclContGeometrieBase
    {
        //
 
        //
        internal double Speed { get; private set; } = 0;
        internal Point SpawnPoint { get; set; } 

        internal CclContLane Lane { get{ return Crossroad.Roads.SelectMany(r => r.Lanes).FirstOrDefault(l => l.Area.IntersectsWith(this.Area));}} // ToDo: Automatisch aus Position berechnen++

        internal bool InCenter { get { return Area.Contains(Crossroad.Center.Position); } } // ToDo: Automatisch aus Position berechnen, am Punkt ++
                                                                                            
        internal CarDirection Direction { get; set; } = CarDirection.Straight; //ToDoI: Muss später beim setzen des Autos festgelegt werden

        private CclContCrossroad Crossroad { get; set; }

        public CclSvcCar(CclContCrossroad crossroad)
        {
            Crossroad = crossroad;
            SpawnPoint = new Point(0, 0);

            //Hier werden die Anfangswerte für Position und Größe des Autos festgelegt
            Size = new Size(10, 20);
            Position = new Point(SpawnPoint.X + Size.Width / 2,SpawnPoint.Y + Size.Height / 2);
        }

        public void CalculateActions()
        {

            double speedLimit = Lane.Road.SpeedLimit;
            Speed = speedLimit; 
            CheckIfCarCanMove();


            switch (Direction)
            {
                case CarDirection.Left:
                    
                    break;
                case CarDirection.Right:
                    
                    break;
                case CarDirection.Straight:
                    
                    break;
            }
        }

        public void CheckIfCarCanMove()
        {
            // Methode prüft, ob das Auto sich bewegen kann oder ob es anhalten muss (z.B. wegen eines anderen Autos oder einer roten Ampel)
            foreach (var otherCar in Crossroad.l_AllCars)
            { 
                if (otherCar != this) // Prüfen, ob es sich nicht um das gleiche auto handelt und ob ein anderes Auto im Weg ist
                                      // Wie Prüfe ich jetzt, ob ein anderes Auto im Weg ist? Soll ich in alle richtungen prüfen oder basierend auf der Lane ?? 
                {
                    Speed = 0; 
                    return;
                }
            }
        }



    }
}
