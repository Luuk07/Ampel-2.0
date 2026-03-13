using Ampel__2._0.Classes.Container;
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
    internal class CclSvcSpawnPoint
    {
        internal Point LanePosition { get; }

        private DateTime CarCreated;

        private double intervall = 5000;
        internal CclContCrossroad Crossroad {  get;  }
        internal CclContLane ParentLane { get; }
        internal bool IsSpawningCar { get; set; }
        internal double SpawnChance { get; set; }


        public CclSvcSpawnPoint(CclContLane parentLane, CclContCrossroad crossroad, CclRandom random, double spawnChance) 
        {
            //ToDo: Wahrscheinlichkeit, dass ein Auto je Timertick erzeugt wird, als parameter übergeben++
            //ToDo: object der Randomklasse als Parameter übergeben++
            //ToDo: 3 Lanes bekommen wahrscheinlichkeit von 0 die andere von ca 20 prozent++

            SpawnChance = spawnChance;
            LanePosition = parentLane.Position;
            Crossroad = crossroad;  
            ParentLane = parentLane;
            CarCreated = DateTime.Now;
        }
        public void HandleSimulationStep(object sender, CeaNextStepData e)
        {
          
           IsSpawningCar = CclRandom.Random.NextDouble() < SpawnChance;
           //ToDo: Mithilfe des Random Objektes bestimmen ob ein Auto erzeugt werden soll oder nicht, abhängig von der übergebenen Wahrscheinlichkeit++

           
              if (!Crossroad.l_AllCars.Any(car => car.Area.Contains(ParentLane.StartPoint)))
              {
                 if ((DateTime.Now - CarCreated).TotalMilliseconds >= intervall)
                 {
                       CclSvcCar Car = new CclSvcCar(Crossroad, ParentLane);
                       Crossroad.l_AllCars.Add(Car);
                       CarCreated = DateTime.Now;
                       e.Main.NextStep += Car.HandleSimulationStep;
                 }
              }

            // Bin mir nicht sicher, ob hier der richtige Ort ist, um die Methode aufzurufen
            e.Main.NextStep += Crossroad.TrafficLightManager.HandleSimulationStep;

        }
    }
}
