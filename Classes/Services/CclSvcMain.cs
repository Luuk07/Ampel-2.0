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
        private int _timeFactor = 2;
        CclContLane Lane { get;  set; }

        List<CclContLane> Lanes { get { return Crossroad.Roads.SelectMany(r => r.LanesToCenter).ToList(); }  }

        private DateTime LastTickTime;
        private DateTime CurrentSimTime { get; set; }
        internal int TimeFactor
        {
            get => _timeFactor;
            set
            {
                if (_timeFactor != value)
                {
                    _timeFactor = value;
                    TimeFactorChanged?.Invoke(this, _timeFactor);
                }
            }
        }

        public event EventHandler <int> TimeFactorChanged;

        internal event EventHandler<CeaNextStepData> NextStep;

        private static System.Timers.Timer _timer;

        private int intervalTimer = 10;


        internal int TimeToChange = 0;

        internal CclRandom Random = new CclRandom();
        internal CclContCrossroad Crossroad { get; set; }





        public CclSvcMain(Size size)
        {
            CurrentSimTime = DateTime.Now;

            //Problem: TimeFakto hier zu übergeben brint nichts, da es einmal gesetzt wird und dann nicht mehr upgedatet wird
            //deswegen -> mit Event arbeiten
            Crossroad = new CclContCrossroad(size, Random);
           
            foreach (var car in Crossroad.l_AllCars)
            {
                TimeFactorChanged += car.HandleTimeFactor;
            }
            
            foreach (var lane in Lanes)
            {
                TimeFactorChanged += lane.SpawnPoint.HandleTimeFactor;
            }
         
            NextStep += Crossroad.TrafficLightManager.HandleSimulationStep;  
            NextStep += (sender, e) => 
            {       
                    var lane = Lanes[CclRandom.Random.Next(Lanes.Count)];
                    lane.SpawnPoint.HandleSimulationStep(sender, e);
            };

       

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
