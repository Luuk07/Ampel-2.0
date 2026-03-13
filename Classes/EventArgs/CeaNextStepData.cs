using Ampel__2._0.Classes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ampel__2._0.Classes.EventArgs
{
    internal class CeaNextStepData : System.EventArgs
    {
        internal double TimeSinceLastStep { get;}       
        internal bool IsTimeToChangeTrafficLight { get; }
        internal DateTime LastTickTime { get; }
        internal CclSvcMain Main { get; }

        internal CeaNextStepData(DateTime lastTickTime, double timeSinceLastStep, CclSvcMain main)
        {
            Main = main;
            TimeSinceLastStep = timeSinceLastStep;
            LastTickTime = lastTickTime;
            //IsTimeToChangeTrafficLight = CheckIfTimeToChangeTrafficLight();
            Main.TimeToChange ++;
        }

        //internal bool CheckIfTimeToChangeTrafficLight()
        //{
        //    if (Main.TimeToChange >= 700) 
        //    {
        //        Main.TimeToChange = 0;
        //        return true;
        //    }
        //    return false;
        //}
    }
}
