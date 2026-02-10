using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampel__2._0.Classes.Container._base
{
    internal class CclContGeometrieBase
    {
        internal Rectangle Area { get { return new Rectangle(Position.X - Size.Width / 2, Position.Y - Size.Height / 2, Size.Width, Size.Height); }} //ToDo: Automatisch aus position und größe berechnen++

        internal Point Position { get; set; } // ToDo: MIttelpunkt des Autos++

        public Size Size { get; protected set; } 


    }
}
