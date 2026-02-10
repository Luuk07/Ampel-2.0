using Ampel__2._0.Classes.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Ampel__2._0
{
    public partial class Form1 : Form
    {
        internal CclSvcMain Main { get; set; }

        internal Rectangle WindowRectangle { get; }

        internal Size WindowSize { get; }
        public Form1()
        {
            InitializeComponent();
            WindowRectangle = this.ClientRectangle;
            WindowSize = this.ClientSize;
            Main = new CclSvcMain(this.WindowSize);//ToDo: Nur Size übergeben
            this.Paint += Form1_PaintLanes;
            this.Paint += Form1_PaintCenter;
            this.Paint += Form1_PaintRoads;
            //ToDo: Mittelpunkt des Fensters der Mainklasse geben++
        }

        //ToDo: Wenn alles Fertig -> alle recs zeichnen

        public void Form1_PaintLanes(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var Lane in Main.Crossroad.Roads.FirstOrDefault(r => r.Direction == Classes.Tools.RoadDirection.EastToWest).Lanes)
            {
                Rectangle rect = Lane.Area;
                if (Lane.LeedsToTrafficlight)
                {
                    using (Brush brush = new SolidBrush(Color.Green))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
                else 
                {
                    using (Brush brush = new SolidBrush(Color.Red))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
            }
            foreach (var Lane in Main.Crossroad.Roads.FirstOrDefault(r => r.Direction == Classes.Tools.RoadDirection.WestToEast).Lanes)
            {
                Rectangle rect = Lane.Area;

                if (Lane.LeedsToTrafficlight)
                {
                    using (Brush brush = new SolidBrush(Color.Green))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
                else
                {
                    using (Brush brush = new SolidBrush(Color.Red))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
            }
            foreach (var Lane in Main.Crossroad.Roads.FirstOrDefault(r => r.Direction == Classes.Tools.RoadDirection.NorthToSouth).Lanes)
            {
                Rectangle rect = Lane.Area;

                if (Lane.LeedsToTrafficlight)
                {
                    using (Brush brush = new SolidBrush(Color.Green))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
                else
                {
                    using (Brush brush = new SolidBrush(Color.Red))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
            }
            foreach (var Lane in Main.Crossroad.Roads.FirstOrDefault(r => r.Direction == Classes.Tools.RoadDirection.SouthToNorth).Lanes)
            {
                Rectangle rect = Lane.Area;

                if (Lane.LeedsToTrafficlight)
                {
                    using (Brush brush = new SolidBrush(Color.Green))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
                else
                {
                    using (Brush brush = new SolidBrush(Color.Red))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
            }
        }

        public void Form1_PaintCenter(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            Rectangle rect = Main.Crossroad.Center.Area;

            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.FillRectangle(brush, rect);
            };
        }

        public void Form1_PaintRoads(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var Road in Main.Crossroad.Roads)
            {
                Rectangle rect = Road.Area;

                using (Brush brush = new SolidBrush(Color.Black))
                {
                    g.FillRectangle(brush, rect);
                }
            }
        }
    }
}
