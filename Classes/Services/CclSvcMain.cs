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
        public Form1 Form1;

        internal CclContCrossroad Crossroad {  get; set; } 
        public CclSvcMain(Form1 form) 
        {
            Form1 = form;
            Crossroad = new CclContCrossroad(form);

        }
    }
}
