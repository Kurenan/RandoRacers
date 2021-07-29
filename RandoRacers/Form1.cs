using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandoRacers
{
    public partial class Form1 : Form
    {
        public List<Point> RacerPoints = new List<Point>();
        public List<Racer> Racers = new List<Racer>();
        public int InitialValueX { get; set; } = 12; //This is the same as the X value of the racers. Should stay the same.
        public int LeadX { get; set; } = 12; //Represents the X value of the lead racer
        public PictureBox LeadRacer { get; set; } = new PictureBox();
        public string FirstPlace { get; set; } = "Red"; //This is a dummy initializer. The first place racer will change very shortly after the race has begun
        public int SpdMinVal { get; set; } = 3;
        public int SpdMaxVal { get; set; } = 10;
        public int DefaultSpeed { get; set; } = 5; //Governs how many pixels the racer will go if it is chose to move
        public int Ticker { get; set; } = 10; //Governs how many milliseconds will elapse every time the Timer object ticks
        public bool RandomizeSpeed { get; set; } = false;
        public bool IsInit { get; set; }
        public Random rnd { get; set; }
        public RacerResult racerResult { get; set; }
        public Timer Timer1 { get; set; }

        public Form1()
        {
            IsInit = false; 
            rnd = new Random();

            InitializeComponent();

            GetStats();

            //Initialize timer object and set interval
            Timer1 = new Timer();
            Timer1.Tick += new EventHandler(timer1_tick);
            Timer1.Interval = Ticker;

        }

        private void timer1_tick(object sender, EventArgs e)
        {
            if (IsInit)
            {
                if (LeadX < 676)
                {
                    RunRace(rnd);
                }
                else
                {
                    IsInit = false;
                    LeadX = InitialValueX;
                    Timer1.Stop();
                    label1.Text = $"{LeadRacer.BackColor.Name} is the WINNER!";
                    button1.Enabled = true;
                    randomizeSpeedChk.Enabled = true;
                }
            }
        }

        private void GetStats()
        {
            //Blank out racers so we can start fresh
            Racers.Clear();

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control.Name.Contains("racer"))
                {
                    //Resets the racers to their initial values if we are starting the race again.
                    if (IsInit)
                    {
                        Point point = new Point(InitialValueX, control.Location.Y);
                        control.Location = point;
                    }

                    Racers.Add(new Racer { RacerBox = (PictureBox)control, RacerSpeed = RandomizeSpeed ? rnd.Next(SpdMinVal, SpdMaxVal) : DefaultSpeed });
                }
            }
        }

        public void PostStats(Racer racer)
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control.Name == racer.RacerBox.Name)
                {
                    control.Location = racer.RacerBox.Location;
                    if (control.Location.X > LeadX)
                    {
                        LeadX = control.Location.X;
                        LeadRacer = (PictureBox)control;
                        label1.Text = $"{control.BackColor.Name} is in the lead.";
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            IsInit = true;
            button1.Enabled = false;
            randomizeSpeedChk.Enabled = false;
            GetStats();
            Timer1.Start();
        }

        public void RunRace(Random rnd)
        {
            Racer racerToAdvance = Racers[rnd.Next(0, Racers.Count)];                  
            Point locPoint = new Point(racerToAdvance.RacerBox.Location.X + racerToAdvance.RacerSpeed, racerToAdvance.RacerBox.Location.Y);
            racerToAdvance.RacerBox.Location = locPoint;
            PostStats(racerToAdvance);
        }

        private void randomizeSpeedChk_CheckedChanged(object sender, EventArgs e)
        {
            if (RandomizeSpeed)
            {
                RandomizeSpeed = false;
            }
            else
            {
                RandomizeSpeed = true;
            }
        }
    }
}
