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
        private int TimeFaktor { get; }


        public CclSvcSpawnPoint(CclContLane parentLane, CclContCrossroad crossroad, CclRandom random, double spawnChance, int timeFaktor) 
        {
            SpawnChance = spawnChance;
            LanePosition = parentLane.Position;
            Crossroad = crossroad;  
            ParentLane = parentLane;
            CarCreated = DateTime.Now;
            TimeFaktor = timeFaktor;
        }

        // ToDo: Nur ein Auto spawnen, jetzt wird momentan auf jeder Lane ein Auto gespawnt
        // Problem: Er geht in 4 verschiedene HandleSimulationStep rein, also bezieht sich das CarCreated immer nur auf diese eine Lane
        // bzw. SpawnPoint und nicht global auf alle
        public void HandleSimulationStep(object sender, CeaNextStepData e)
        {
              IsSpawningCar = CclRandom.Random.NextDouble() < SpawnChance;
           
              if (!Crossroad.l_AllCars.Any(car => car.Area.Contains(ParentLane.StartPoint)))
              {
                 if ((DateTime.Now - CarCreated).TotalMilliseconds * TimeFaktor >= intervall)
                 {
                       CarCreated = DateTime.Now;
                       CclSvcCar Car = new CclSvcCar(Crossroad, ParentLane, TimeFaktor);
                       Crossroad.l_AllCars.Add(Car);
                       e.Main.NextStep += Car.HandleSimulationStep;
                 }
              }
        }
    }
}
