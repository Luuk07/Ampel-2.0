using Ampel__2._0.Classes.Container;
using Ampel__2._0.Classes.Services;
using Ampel__2._0.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ampel__2._0.Classes.EventArgs
{
    internal class CeaNextStepData : System.EventArgs
    {
        internal double SimTimeSinceLastStep { get;}       
        internal bool IsTimeToChangeTrafficLight { get; }
        internal DateTime CurrentSimTime { get; }
        internal CclSvcMain Main { get; }

        internal CeaNextStepData(DateTime currentSimtTIme, double simTimeSinceLastStep, CclSvcMain main)
        {
            Main = main;
            SimTimeSinceLastStep = simTimeSinceLastStep;
            CurrentSimTime = currentSimtTIme;
            Main.TimeToChange ++;
        }

    }
}
