using Ampel__2._0.Classes.Container._base;
using Ampel__2._0.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ampel__2._0.Classes.Container
{
    internal class CclContCenter: CclContGeometrieBase
    {
        internal Size WindowSize { get; }
        public CclContCenter(Size windowSize)
        {
            WindowSize = windowSize;
        }

        public void CreatArea(int lanesInCrossroadNorthSouth, int lanesInCrossroadSouthWest) 
        {
            Position = new Point(WindowSize.Width / 2, WindowSize.Height / 2);
            Size = new Size(CstConstants.C_iLaneWidth * lanesInCrossroadSouthWest, CstConstants.C_iLaneWidth * lanesInCrossroadNorthSouth);
        }
    }
}
