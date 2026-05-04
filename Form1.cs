using Ampel__2._0.Classes.EventArgs;
using Ampel__2._0.Classes.Services;
using Ampel__2._0.Classes.Tools;
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

        //ToDo: Blinken
        public Form1()
        {
            InitializeComponent();
            WindowRectangle = this.ClientRectangle;
            WindowSize = this.ClientSize;
            Main = new CclSvcMain(this.WindowSize);
            Main.NextStep += Main_NextStep;
            this.Paint += Form1_PaintCenter;
            this.Paint += Form1_PaintRoads;
            this.Paint += Form1_PaintLanes;
            this.Paint += Form1_PaintTrafficLights;
            this.Paint += Form1_PaintCar;
            this.Paint += Form1_PaintPoints;

            Main.TimeFactor = timeFaktorBar.Value;
            timeFaktorBar.ValueChanged += TimeFaktorBar_ValueChanged;
         
        }

        private void TimeFaktorBar_ValueChanged(object sender, EventArgs e)
        {
            Main.TimeFactor = timeFaktorBar.Value;
        }


        private void Main_NextStep(object sender, CeaNextStepData e)
        {
            Invalidate();
            this.DoubleBuffered = true; // verhindert flackern
        }

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
                        g.DrawRectangle(Pens.Blue, Lane.StopArea);
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
                        g.DrawRectangle(Pens.Blue, Lane.StopArea);
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
                        g.DrawRectangle(Pens.Blue, Lane.StopArea);
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
                        g.DrawRectangle(Pens.Blue, Lane.StopArea);
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

            using (Brush brush = new SolidBrush(Color.Blue))
            {
                g.FillRectangle(brush, rect);
            }
            ;
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
        public void Form1_PaintCar(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (var car in Main.Crossroad.l_AllCars.ToList())
            {
                Rectangle rectLeftCorner = new Rectangle(
                         (int)car.Position.X,
                         (int)car.Position.Y,
                         4, 4
                );
                Rectangle rectRightCorner = new Rectangle(
                        (int)car.Position.X,
                        (int)car.Position.Y,
                        4, 4
                );
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    g.FillRectangle(brush, car.Area);
                    g.DrawRectangle(Pens.Red, car.CheckLineArea);
                }
                if (car.Direction == CarDirection.Right)
                {
                    switch (car.Lane.Road.Direction)
                    {
                        case RoadDirection.NorthToSouth:
                            rectRightCorner.Y += 6;
                            rectRightCorner.X -= 6;
                            break;
                        case RoadDirection.WestToEast:
                            rectRightCorner.Y += 6;
                            rectRightCorner.X += 6;
                            break;
                        case RoadDirection.SouthToNorth:
                            rectRightCorner.X += 6;
                            rectRightCorner.Y -= 6;
                            break;
                        case RoadDirection.EastToWest:
                            rectRightCorner.X -= 6;
                            rectRightCorner.Y -= 6;
                            break;
                        default:
                            break;
                    }

                    using (Brush brush = new SolidBrush(Color.Orange))
                    {
                        g.FillRectangle(brush, rectRightCorner);
                    }

                }
            }
        }

        public void Form1_PaintTrafficLights(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (var light in Main.Crossroad.Roads.Select(r => r.TrafficLight))
            {
                Rectangle rect = light.Area;
                using (Brush brush = new SolidBrush(light.CurrentState == Classes.Tools.TrafficLightState.Green ? Color.Green : light.CurrentState == Classes.Tools.TrafficLightState.YellowGreen || light.CurrentState == Classes.Tools.TrafficLightState.YellowRed ? Color.Yellow : Color.Red))
                {
                    g.FillRectangle(brush, rect);
                }
            }
        }

        public void Form1_PaintPoints(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //foreach (var point in Main.Crossroad.Center.South)
            //{
            //}
            //using (Brush brush = new SolidBrush(Color.Purple))
            //{
            //   g.FillPolygon(brush, Main.Crossroad.Center.South.ToArray());
            //}
            using (Brush brush = new SolidBrush(Color.Purple))
            {
                g.FillClosedCurve(brush, Main.Crossroad.Center.SouthEast.ToArray());
                g.FillClosedCurve(brush, Main.Crossroad.Center.EastNorth.ToArray());
                g.FillClosedCurve(brush, Main.Crossroad.Center.WestSouth.ToArray());
                g.FillClosedCurve(brush, Main.Crossroad.Center.NorthWest.ToArray());
            }
            using (Brush brush = new SolidBrush(Color.Yellow))
            {
                //g.FillClosedCurve(brush, Main.Crossroad.Center.SouthWest.ToArray());
                //g.FillClosedCurve(brush, Main.Crossroad.Center.EastSouth.ToArray());
                //g.FillClosedCurve(brush, Main.Crossroad.Center.NorthEast.ToArray());
                //g.FillClosedCurve(brush, Main.Crossroad.Center.WestNorth.ToArray());
            }
        }
    }
}
