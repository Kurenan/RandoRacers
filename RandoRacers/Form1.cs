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

        public int StopValue { get; set; }
        public string FirstPlace { get; set; }
        public int Incrementer { get; set; }
        public int Ticker { get; set; }
        public bool IsInit { get; set; }
        public Random rnd { get; set; }
        public RacerResult racerResult { get; set; }
        public Timer Timer1 { get; set; }

        public Form1()
        {
            //Initialize the values that are made available across the rest of the form
            //Feel free to play around with some of these.

            StopValue = 12; //This is the same as the X value of the racers. Should stay the same.
            FirstPlace = "Red"; //This is a dummy initializer. The first place racer will change very shortly after the race has begun
            Incrementer = 2; //Governs how many pixels the racer will go if it is chose to move
            Ticker = 1; //Governs how many milliseconds will elapse every time the Timer object ticks

            IsInit = false; 
            rnd = new Random();

            InitializeComponent();

            //Initialize timer object and set interval
            Timer1 = new Timer();
            Timer1.Tick += new EventHandler(timer1_tick);
            Timer1.Interval = Ticker;

        }

        private void timer1_tick(object sender, EventArgs e)
        {
            if (IsInit)
            {
                if (StopValue < 676)
                {
                    racerResult = runRace(rnd);
                    StopValue = racerResult.ReturnValue;
                    FirstPlace = racerResult.SourceRacer;
                }
                else
                {
                    IsInit = false;
                    Timer1.Stop();
                    label1.Text = FirstPlace + " is the WINNER!";
                }
            }
        }

        private Racers getStats()
        {
            Racers racers = new RandoRacers.Racers();
            racers.racerRed_x = racerRed.Location.X;
            racers.racerBlue_x = racerBlue.Location.X;
            racers.racerYellow_x = racerYellow.Location.X;
            racers.racerPurple_x = racerPurple.Location.X;
            racers.racerOrange_x = racerOrange.Location.X;
            racers.racerGreen_x = racerGreen.Location.X;
            racers.racerPink_x = racerPink.Location.X;
            racers.racerBrown_x = racerBrown.Location.X;
            return racers;
        }

        private Racers getInitialStats()
        {
            Racers racers = new RandoRacers.Racers();
            racers.racerRed_x = 12;
            racers.racerBlue_x = 12;
            racers.racerYellow_x = 12;
            racers.racerPurple_x = 12;
            racers.racerOrange_x = 12;
            racers.racerGreen_x = 12;
            racers.racerPink_x = 12;
            racers.racerBrown_x = 12;
            return racers;
        }

        public void postStats(Racers racers)
        {
            Point pointRed = new Point(racers.racerRed_x, racerRed.Location.Y);
            racerRed.Location = pointRed;
            Point pointBlue = new Point(racers.racerBlue_x, racerBlue.Location.Y);
            racerBlue.Location = pointBlue;
            Point pointYellow = new Point(racers.racerYellow_x, racerYellow.Location.Y);
            racerYellow.Location = pointYellow;
            Point pointPurple = new Point(racers.racerPurple_x, racerPurple.Location.Y);
            racerPurple.Location = pointPurple;
            Point pointOrange = new Point(racers.racerOrange_x, racerOrange.Location.Y);
            racerOrange.Location = pointOrange;
            Point pointGreen = new Point(racers.racerGreen_x, racerGreen.Location.Y);
            racerGreen.Location = pointGreen;
            Point pointPink = new Point(racers.racerPink_x, racerPink.Location.Y);
            racerPink.Location = pointPink;
            Point pointBrown = new Point(racers.racerBrown_x, racerBrown.Location.Y);
            racerBrown.Location = pointBrown;

            label1.Text = FirstPlace + " is in the lead.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsInit = true;
            Timer1.Start();
            Racers racers = getInitialStats();
            postStats(racers);
            StopValue = 12;
        }

        public RacerResult runRace(Random rnd)
        {
            Racers racers = getStats();
            RacerResult racerResult = new RacerResult();
            int returnValue = StopValue;
            var rndMove = rnd.Next(1, 9);

            switch (rndMove)
            {
                case 1:
                    racers.racerRed_x+=Incrementer;
                    postStats(racers);
                    racerResult.ReturnValue = racers.racerRed_x;
                    racerResult.SourceRacer = "Red";
                    break;
                case 2:
                    racers.racerBlue_x+=Incrementer;
                    postStats(racers);
                    racerResult.ReturnValue = racers.racerBlue_x;
                    racerResult.SourceRacer = "Blue";
                    break;
                case 3:
                    racers.racerYellow_x+=Incrementer;
                    postStats(racers);
                    racerResult.ReturnValue = racers.racerYellow_x;
                    racerResult.SourceRacer = "Yellow";
                    break;
                case 4:
                    racers.racerPurple_x+=Incrementer;
                    postStats(racers);
                    racerResult.ReturnValue = racers.racerPurple_x;
                    racerResult.SourceRacer = "Purple";
                    break;
                case 5:
                    racers.racerOrange_x+=Incrementer;
                    postStats(racers);
                    racerResult.ReturnValue = racers.racerOrange_x;
                    racerResult.SourceRacer = "Orange";
                    break;
                case 6:
                    racers.racerGreen_x+=Incrementer;
                    postStats(racers);
                    racerResult.ReturnValue = racers.racerGreen_x;
                    racerResult.SourceRacer = "Green";
                    break;
                case 7:
                    racers.racerPink_x+=Incrementer;
                    postStats(racers);
                    racerResult.ReturnValue = racers.racerPink_x;
                    racerResult.SourceRacer = "Pink";
                    break;
                case 8:
                    racers.racerBrown_x+=Incrementer;
                    postStats(racers);
                    racerResult.ReturnValue = racers.racerBrown_x;
                    racerResult.SourceRacer = "Brown";
                    break;
            }

            //If the overall X value of all racers has been pushed forward, then return that value plus the car that caused the push
            //Otherwise, return the same value (that is, if a racer in an inferior position proɡressed)
            if (racerResult.ReturnValue > StopValue)
            {                
                return racerResult;
            }

            racerResult.ReturnValue = StopValue;
            racerResult.SourceRacer = FirstPlace;
            return racerResult;
        }
    }
}
