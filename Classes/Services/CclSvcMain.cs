using Ampel__2._0.Classes.Container;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ampel__2._0.Classes.Services
{
    internal class CclSvcMain
    {
       

        internal CclContCrossroad Crossroad {  get; set; } 
        public CclSvcMain(Size size)
        { 
            Crossroad = new CclContCrossroad(size);
            //ToDo: timer anlegen mit Ereignisshändler, im erreignishändler für jedes auto move METHODED AUFRUFEn

        }
    }
}
