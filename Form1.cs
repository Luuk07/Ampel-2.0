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

        private float scaleX;

        private float scaleY;



        //ToDo: Dynamisch größe anpassen+
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // verhindert flackern
            WindowRectangle = this.ClientRectangle;
            WindowSize = this.ClientSize;
            Main = new CclSvcMain(this.WindowSize);
            this.Paint += Form1_PaintCenter;
            this.Paint += Form1_PaintRoads;
            this.Paint += Form1_PaintLanes;
            this.Paint += Form1_PaintTrafficLights;
            this.Paint += Form1_PaintCar;
            //this.Paint += Form1_PaintPoints;

            Main.TimeFactor = timeFaktorBar.Value/2;
            timeFaktorBar.ValueChanged += TimeFaktorBar_ValueChanged;
            timeFaktorBar.Anchor =  AnchorStyles.Top;


            this.SizeChanged += (s, e) => this.Invalidate();
            this.Resize += (s, e) => this.Invalidate();

            Main.NextStep += Main_NextStep;


        }

        private void TimeFaktorBar_ValueChanged(object sender, EventArgs e)
        {
            Main.TimeFactor = timeFaktorBar.Value;
        }


        private void Main_NextStep(object sender, CeaNextStepData e)
        {
            Invalidate();
            // 800 und 600 sind die Größen des Fensters
            scaleX = this.ClientSize.Width / 800f;
            scaleY = this.ClientSize.Height / 600f;
         
        }

        public void Form1_PaintLanes(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            foreach (var Lane in Main.Crossroad.Roads.FirstOrDefault(r => r.Direction == Classes.Tools.RoadDirection.EastToWest).Lanes)
            {
                Rectangle baseRect = Lane.Area;

                Rectangle rect = new Rectangle(
                    (int)(baseRect.X * scaleX),
                    (int)(baseRect.Y * scaleY),
                    (int)(baseRect.Width * scaleX),
                    (int)(baseRect.Height * scaleY)
                );

                if (Lane.LeedsToTrafficlight)
                {
                    using (Brush brush = new SolidBrush(Color.Green))
                    {
                        //g.DrawRectangle(Pens.Blue, Lane.StopArea);
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
                Rectangle baseRect = Lane.Area;

                Rectangle rect = new Rectangle(
                    (int)(baseRect.X * scaleX),
                    (int)(baseRect.Y * scaleY),
                    (int)(baseRect.Width * scaleX),
                    (int)(baseRect.Height * scaleY)
                );
                if (Lane.LeedsToTrafficlight)
                {
                    using (Brush brush = new SolidBrush(Color.Green))
                    {
                       // g.DrawRectangle(Pens.Blue, Lane.StopArea);
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
                Rectangle baseRect = Lane.Area;

                Rectangle rect = new Rectangle(
                    (int)(baseRect.X * scaleX),
                    (int)(baseRect.Y * scaleY),
                    (int)(baseRect.Width * scaleX),
                    (int)(baseRect.Height * scaleY)
                );

                if (Lane.LeedsToTrafficlight)
                {
                    using (Brush brush = new SolidBrush(Color.Green))
                    {
                        //g.DrawRectangle(Pens.Blue, Lane.StopArea);
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
                Rectangle baseRect = Lane.Area;

                Rectangle rect = new Rectangle(
                    (int)(baseRect.X * scaleX),
                    (int)(baseRect.Y * scaleY),
                    (int)(baseRect.Width * scaleX),
                    (int)(baseRect.Height * scaleY)
                );

                if (Lane.LeedsToTrafficlight)
                {
                    using (Brush brush = new SolidBrush(Color.Green))
                    {
                        //g.DrawRectangle(Pens.Blue, Lane.StopArea);
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


            Rectangle baseRect = Main.Crossroad.Center.Area;

            Rectangle rect = new Rectangle(
                (int)(baseRect.X * scaleX),
                (int)(baseRect.Y * scaleY),
                (int)(baseRect.Width * scaleX),
                (int)(baseRect.Height * scaleY)
            );

            using (Brush brush = new SolidBrush(Color.Blue))
            {
                g.FillRectangle(brush, rect);
            };
        }

        public void Form1_PaintRoads(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var Road in Main.Crossroad.Roads)
            {
                Rectangle baseRect = Road.Area;

                Rectangle rect = new Rectangle(
                    (int)(baseRect.X * scaleX),
                    (int)(baseRect.Y * scaleY),
                    (int)(baseRect.Width * scaleX),
                    (int)(baseRect.Height * scaleY)
                );


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

                Rectangle baseRectCar = car.Area;

                Rectangle rect = new Rectangle(
                    (int)(baseRectCar.X * scaleX),
                    (int)(baseRectCar.Y * scaleY),
                    (int)(baseRectCar.Width * scaleX),
                    (int)(baseRectCar.Height * scaleY)
                );


                Rectangle rectCorner = new Rectangle(
                       0,0,
                       (int)(4*scaleX), (int)(4 * scaleY)
                );

                Rectangle baseRectCheckLineArea = car.CheckLineArea;

                Rectangle rectCheckLineAre = new Rectangle(
                   (int)(baseRectCheckLineArea.X * scaleX),
                   (int)(baseRectCheckLineArea.Y * scaleY),
                   (int)(baseRectCheckLineArea.Width * scaleX),
                   (int)(baseRectCheckLineArea.Height * scaleY)
                );

                using (Brush brush = new SolidBrush(Color.Black))
                {
                    g.FillRectangle(brush, rect);
                    g.DrawRectangle(Pens.Red, rectCheckLineAre);
                }

                // Der Mittelpunkt des Autos
                int centerX = rect.X + rect.Width / 2;
                int centerY = rect.Y + rect.Height / 2;

                //Blinkergröße
                int offsetX = rect.Width / 2;
                int offsetY = rect.Height / 2;


                if (car.Direction == CarDirection.Right)
                {
                    switch (car.Lane.Road.Direction)

                    {
                        case RoadDirection.NorthToSouth:
                            rectCorner.X = centerX - offsetX;
                            rectCorner.Y = centerY + offsetY;
                            break;

                        case RoadDirection.SouthToNorth:
                            rectCorner.X = centerX + offsetX;
                            rectCorner.Y = centerY - offsetY;
                            break;

                        case RoadDirection.WestToEast:
                            rectCorner.X = centerX + offsetX;
                            rectCorner.Y = centerY + offsetY;
                            break;

                        case RoadDirection.EastToWest:
                            rectCorner.X = centerX - offsetX;
                            rectCorner.Y = centerY - offsetY;
                            break;
                    }

                    using (Brush brush = new SolidBrush(Color.Orange))
                    {
                        g.FillRectangle(brush, rectCorner);
                    }
                }
                if (car.Direction == CarDirection.Left)
                {
                    switch (car.Lane.Road.Direction)
                    {

                        case RoadDirection.NorthToSouth:
                            rectCorner.X = centerX + offsetX;
                            rectCorner.Y = centerY + offsetY;
                            break;

                        case RoadDirection.SouthToNorth:
                            rectCorner.X = centerX - offsetX;
                            rectCorner.Y = centerY - offsetY;
                            break;

                        case RoadDirection.WestToEast:
                            rectCorner.X = centerX + offsetX;
                            rectCorner.Y = centerY - offsetY;
                            break;

                        case RoadDirection.EastToWest:
                            rectCorner.X = centerX - offsetX;
                            rectCorner.Y = centerY + offsetY;
                            break;
                    }


                    using (Brush brush = new SolidBrush(Color.Blue))
                    {
                        g.FillRectangle(brush, rectCorner);
                    }

                }
            }
        }

        public void Form1_PaintTrafficLights(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (var light in Main.Crossroad.Roads.Select(r => r.TrafficLight))
            {
                Rectangle baseRect = light.Area;

                Rectangle rect = new Rectangle(
                    (int)(baseRect.X * scaleX),
                    (int)(baseRect.Y * scaleY),
                    (int)(baseRect.Width * scaleX),
                    (int)(baseRect.Height * scaleY)
                );
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
