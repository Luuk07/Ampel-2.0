using Ampel__2._0.Classes.Container;
using Ampel__2._0.Classes.EventArgs;
using Ampel__2._0.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Ampel__2._0.Classes.Services
{
    internal class CclSvcMain
    {
        CclContLane Lane { get;  set; }

        List<CclContLane> Lanes { get { return Crossroad.Roads.SelectMany(r => r.LanesToCenter).ToList(); }  }

        private DateTime LastTickTime;
        private DateTime CurrentSimTime { get; set; }

        //Nicht nur Ampel, sondern auch Geschwindigkeit der Autos, Häufigkeit des Spawns++ 
        //-> Ist jetzt bei allen relevanten Werten 
        internal int TimeFactor { get; set; } = 2;

        internal event EventHandler<CeaNextStepData> NextStep;

        private static System.Timers.Timer _timer;

        private int intervalTimer = 10;


        internal int TimeToChange = 0;

        internal CclRandom Random = new CclRandom();
        internal CclContCrossroad Crossroad { get; set; } 

        
        public CclSvcMain(Size size)
        {
            CurrentSimTime = DateTime.Now;

            Crossroad = new CclContCrossroad(size, Random, TimeFactor);
            NextStep += Crossroad.TrafficLightManager.HandleSimulationStep;  
            NextStep += (sender, e) => 
                {       
                    var lane = Lanes[CclRandom.Random.Next(Lanes.Count)];
                    lane.SpawnPoint.HandleSimulationStep(sender, e);
                };

            NextStep += Crossroad.HandleSimulationStep;

            //NextStep += Lanes[CclRandom.Random.Next(Lanes.Count)].SpawnPoint.HandleSimulationStep;

            try
            {
                _timer = new System.Timers.Timer(intervalTimer);
                _timer.Elapsed += MainTick;
                _timer.AutoReset = true;
                _timer.Enabled = false;
            }
            finally
            {
                LastTickTime = DateTime.Now;
                _timer.Enabled = true;
            }
        }

      



        private void MainTick(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            try
            {
                double   dSimTimeSinceLastTick = (DateTime.Now - LastTickTime).TotalMilliseconds * TimeFactor;
                DateTime dtCurrentSimTime      = CurrentSimTime.AddMilliseconds(dSimTimeSinceLastTick * TimeFactor);

                NextStep?.Invoke(this, new CeaNextStepData(dtCurrentSimTime, dSimTimeSinceLastTick, this));
          

                LastTickTime   = DateTime.Now;
                CurrentSimTime = dtCurrentSimTime;
            }
            finally
            {
                _timer.Enabled = true;
            }
        }

   
    }
}
