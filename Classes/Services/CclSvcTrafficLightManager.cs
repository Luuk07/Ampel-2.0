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
        private DateTime NextPhaseChangeTime { get; set; }

        private int currentPhase = 1;
        internal CclContCrossroad Crossroad { get; }
        internal List<CclContTrafficLight> TrafficLights { get { return Crossroad.Roads.Select(clRoad => clRoad.TrafficLight).ToList(); } }


        public CclSvcTrafficLightManager(CclContCrossroad crossroad)
        {
            //ToDo: Ampelschaltung aus alter version nehmen und Autos sollen nur halten, wenn Ampel rot ist
            //-> danach mehrere Gelb Stati hinzufügen.
            
            Crossroad = crossroad;
        }

        public void HandleSimulationStep(object sender, CeaNextStepData e)
        {                                                                         
          if (e.CurrentSimTime >= NextPhaseChangeTime) 
          {
             
             SetNextTrafficLightState();
             CalculateNextStateChangeTime(e, e.CurrentSimTime, currentPhase);
             
               
             //TODO: Zeitpunkt in Simulationszeit für den nächsten Phasenwechsel berechnen. NextStateChangeTime = CalculateNextStateChangeTime();
          }

        }

  
        private void CalculateNextStateChangeTime(CeaNextStepData e, DateTime dtCurrentSimTime, int phase)
        {
            double phaseDurarion;
            switch (phase)
            {
                case 1:
                    phaseDurarion = 10;
                    break;
                case 2:
                    phaseDurarion = 20;
                    break;
                case 3:
                    phaseDurarion = 30;
                    break;
                case 4:
                    phaseDurarion = 40;
                    break;
                case 5:
                    phaseDurarion = 50;
                    break;
                case 6:
                    phaseDurarion = 60;
                    break;
                default:
                    throw new IndexOutOfRangeException("Unknown phase: " + phase);
            }

            NextPhaseChangeTime = dtCurrentSimTime.AddSeconds(phaseDurarion);
    
        }

       

        private void SetNextTrafficLightState()
        {
            //foreach (var l in TrafficLights)
            //{
            //    switch (l.CurrentState)
            //    {
            //        case TrafficLightState.Green:
            //            l.CurrentState = TrafficLightState.YellowRed;
            //            break;
            //        case TrafficLightState.YellowRed:
            //            l.CurrentState = TrafficLightState.Red;
            //            break;
            //        case TrafficLightState.Red:
            //            l.CurrentState = TrafficLightState.YellowGreen;
            //            break;
            //        case TrafficLightState.YellowGreen:
            //            l.CurrentState = TrafficLightState.Green;
            //            break;
            //        default:
            //            throw new IndexOutOfRangeException("Unknown TrafficLightState: " + l.CurrentState);
            //    }
            //}


            var ou = TrafficLights.Where(tl => tl.Road.Direction == RoadDirection.NorthToSouth ||
                                   tl.Road.Direction == RoadDirection.SouthToNorth).ToList();


            var lr = TrafficLights.Where(tl => tl.Road.Direction == RoadDirection.EastToWest ||
                                    tl.Road.Direction == RoadDirection.WestToEast).ToList();



            // Es gibt 6 Phasen und für jede Phase gibt es eine klare Bedingung

            // 1. OU Grün | LR Rot  --> OU Gelb | LR Rot
            if (ou[0].CurrentState == TrafficLightState.Green && lr[0].CurrentState == TrafficLightState.Red)
            {
                SetGroupState(ou, TrafficLightState.YellowRed);
                currentPhase = 1;
            }
            // 2. OU Gelb | LR Rot  --> OU Rot | LR Rot+Gelb
            else if (ou[0].CurrentState == TrafficLightState.YellowRed && lr[0].CurrentState == TrafficLightState.Red)
            {
                SetGroupState(ou, TrafficLightState.Red);
                SetGroupState(lr, TrafficLightState.YellowGreen);
                currentPhase = 1;
            }
            // 3. OU Rot | LR Rot+Gelb --> OU Rot | LR Grün
            else if (ou[0].CurrentState == TrafficLightState.Red && lr[0].CurrentState == TrafficLightState.YellowGreen)
            {
                SetGroupState(lr, TrafficLightState.Green);
                currentPhase = 1;
            }
            // 4. OU Rot | LR Grün --> OU Rot | LR Gelb
            else if (ou[0].CurrentState == TrafficLightState.Red && lr[0].CurrentState == TrafficLightState.Green)
            {
                SetGroupState(lr, TrafficLightState.YellowRed);
                currentPhase = 1;
            }
            // 5. OU Rot | LR Gelb --> OU Rot+Gelb | LR Rot
            else if (ou[0].CurrentState == TrafficLightState.Red && lr[0].CurrentState == TrafficLightState.YellowRed)
            {
                SetGroupState(ou, TrafficLightState.YellowGreen);
                SetGroupState(lr, TrafficLightState.Red);
                currentPhase = 1;
            }
            // 6. OU Rot+Gelb | LR Rot --> OU Grün | LR Rot
            else if (ou[0].CurrentState == TrafficLightState.YellowGreen && lr[0].CurrentState == TrafficLightState.Red)
            {
                SetGroupState(ou, TrafficLightState.Green);
                currentPhase = 1;
            }
            //Default
            else
            {
                SetGroupState(ou, TrafficLightState.Green);
                SetGroupState(lr, TrafficLightState.Red);
            }
          
        }

       private void SetGroupState(List<CclContTrafficLight> group, TrafficLightState newState)
       {
           foreach (var l in group)
           {
               l.CurrentState = newState;
           }
       }

    
}
}
