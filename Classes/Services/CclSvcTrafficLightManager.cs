using Ampel__2._0.Classes.Container;
using Ampel__2._0.Classes.EventArgs;
using Ampel__2._0.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampel__2._0.Classes.Services
{
    internal class CclSvcTrafficLightManager
    {
        internal DateTime LastTrafficLightChangeTime { get; set; } 
        internal CclContCrossroad Crossroad { get; }
        internal List<CclContTrafficLight> TrafficLights { get { return Crossroad.Roads.Select(clRoad => clRoad.TrafficLight).ToList(); } }

        private int isTransitioning = 0;

        private int yellowLightMilliSeconds = 3000;

        public CclSvcTrafficLightManager(CclContCrossroad crossroad)
        {
            //ToDo: Ampelschaltung aus alter version nehmen und Autos sollen nur halten, wenn Ampel rot ist
            //-> danach mehrere Gelb Stati hinzufügen.
            LastTrafficLightChangeTime = DateTime.Now;
            Crossroad = crossroad;
        }

        public void HandleSimulationStep(object sender, CeaNextStepData e)
        {
            if ((e.LastTickTime - LastTrafficLightChangeTime).TotalMilliseconds >= 10000) // Irgendwie kommt es nicht so hin
            {
                ChangeColorOfTrafficLight().ConfigureAwait(false);
            }
        }
      
        //ToDo: Uhrzeit des letzten Phasenwechsels merken und von der Simulationzeit abziehen, wenn genug Zeit vergnangen ist, dann Ampelphase ändern
        public async Task ChangeColorOfTrafficLight()
        {
            if (System.Threading.Interlocked.Exchange(ref isTransitioning, 1) == 1)
            {
                return;
            }
            try
            {
                // Snapshot previous states
                var previous = TrafficLights.ToDictionary(l => l, l => l.CurrentState);

                // All traffic lights to Yellow
                foreach (var l in TrafficLights)
                {
                    SetState(l, TrafficLightState.Yellow);
                }

                // Duration of yellow light
                await Task.Delay((int)yellowLightMilliSeconds).ConfigureAwait(false);

                // Set new states based on previous
                foreach (var l in TrafficLights)
                {
                    var prev = previous[l];

                    if (prev == TrafficLightState.Green)
                    {
                        SetState(l, TrafficLightState.Red);
                    }
                    else if (prev == TrafficLightState.Red)
                    {
                        SetState(l, TrafficLightState.Green);
                    }
                    else
                    {
                        // Here we choose Red as a safe default
                        SetState(l, TrafficLightState.Red);
                    }
                }
            }
            finally
            {
                // Reset transitioning flag, so its possible to call the method again
                System.Threading.Interlocked.Exchange(ref isTransitioning, 0);
                LastTrafficLightChangeTime = DateTime.Now;
            }
        }
        private void SetState(CclContTrafficLight light, TrafficLightState newState)
        {
            if (light.CurrentState != newState)
            {
                light.CurrentState = newState;
                //StateChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}
