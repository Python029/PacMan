using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Highscore f2 = new Highscore();
        Form3 f3 = new Form3();
        Random rnd = new Random();
        int pink = 0;
        int orange = 0;
        int p = 0;
        int Speed = 3;
        int pSpeed = 3;
        int rSpeed = 3;
        bool pellet = false;
        int score;
        int life;
        bool u = false; bool d = false; bool l = false; bool r = false;
        bool pu = false; bool pd = false; bool pl = false; bool pr = false;
        bool ru = false; bool rd = false; bool rl = false; bool rr = false; bool rlr = false; bool rud = false;
        bool bu = false; bool bd = false; bool bl = false; bool br = false;
        bool ou = false; bool od = false; bool ol = false; bool or = false;
        bool pStart = false; bool rStart = false; bool bStart = false; bool oStart = false;
        bool pChange = false; bool rChange = false;
        int plx = 0; int ply = 0; int px = 0; int py = 0; int rx = 0; int ry = 0;
        PictureBox[] walls;
        PictureBox[] ghosts;
        List<PictureBox> pellets = new List<PictureBox>();
        List<PictureBox> Bpellets = new List<PictureBox>();


        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            ghosts = new PictureBox[] { Pinky, Blinky };
            walls = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20, pictureBox21, pictureBox22, pictureBox23, pictureBox24, pictureBox25, pictureBox26, pictureBox27, pictureBox28, pictureBox29, pictureBox30, pictureBox31, pictureBox32, pictureBox33, pictureBox34, pictureBox35, pictureBox36, pictureBox37, pictureBox38, pictureBox39, pictureBox40, pictureBox41, pictureBox42, pictureBox43, pictureBox44, pictureBox45, pictureBox46 };
            if (Properties.Settings.Default.Start == false)
            {
                Properties.Settings.Default.SScore = 0;
                Properties.Settings.Default.Lives = 2;
                score = 0;
                life = 2;
                tmrUpdate.Enabled = true;
                MoveAndAnimate.Enabled = true;
                Properties.Settings.Default.Start = true;

            }
            else if (Properties.Settings.Default.Start)
            {
                score = Properties.Settings.Default.SScore;
                life = Properties.Settings.Default.Lives;
                tmrUpdate.Enabled = true;
                MoveAndAnimate.Enabled = true;
                Properties.Settings.Default.Start = false;               
            }
            Pinky.Location = new Point(402, 302);
        }
        private void LoadPellets()
        {
            if (pellet == false)
            {
                for (int i = 49; i < 128; i++)
                {
                    PictureBox pb = (PictureBox)this.Controls["pictureBox"+i.ToString()];
                    pellets.Add(pb);
                }
                for (int i = 128; i < 132; i++)
                {
                    PictureBox pb = (PictureBox)this.Controls["pictureBox" + i.ToString()];
                    Bpellets.Add(pb);
                }
                pellet = true;
            }
        }
        private void Score()
        {
            for(int i = 0;i<pellets.Count;i++)
            {
                if(Player.Bounds.IntersectsWith(pellets[i].Bounds))
                {
                    score += 10;
                    pellets[i].Dispose();
                    pellets.RemoveAt(i);
                }
                
            }
            for (int i = 0; i < Bpellets.Count; i++)
            {
                if (Player.Bounds.IntersectsWith(Bpellets[i].Bounds))
                {
                    score += 50;
                    Bpellets[i].Dispose();
                    Bpellets.RemoveAt(i);
                }
            }
            #region Show Score
            if(score>=10&&score<100)
            {
                lblScore.Text = "0000" + score.ToString();
            }
            if (score >= 100 && score < 1000)
            {
                lblScore.Text = "000" + score.ToString();
            }
            if (score >= 1000 && score < 10000)
            {
                lblScore.Text = "00" + score.ToString();
            }
            if (score >= 10000 && score < 100000)
            {
                lblScore.Text = "0" + score.ToString();
            }
            #endregion
            #region High Score
            if (Properties.Settings.Default.High1 == 0)
            {
                lblHScore.Text = "000000";
            }
            if (Properties.Settings.Default.High1 > 0 && Properties.Settings.Default.High1 < 10)
            {
                lblHScore.Text = "00000" + Properties.Settings.Default.High1.ToString();
            }
            if (Properties.Settings.Default.High1 >= 10 && Properties.Settings.Default.High1 < 100)
            {
                lblHScore.Text = "0000" + Properties.Settings.Default.High1.ToString();
            }
            if (Properties.Settings.Default.High1 >= 100 && Properties.Settings.Default.High1 < 1000)
            {
                lblHScore.Text = "000" + Properties.Settings.Default.High1.ToString();
            }
            if (Properties.Settings.Default.High1 >= 1000 && Properties.Settings.Default.High1 < 10000)
            {
                lblHScore.Text = "00" + Properties.Settings.Default.High1.ToString();
            }
            if (Properties.Settings.Default.High1 >= 10000 && Properties.Settings.Default.High1 < 100000)
            {
                lblHScore.Text = "0" + Properties.Settings.Default.High1.ToString();
            }
            #endregion
        }
        private void MoveAndAnimate_Tick(object sender, EventArgs e)
        {
            plx = Player.Location.X;
            ply = Player.Location.Y;
            px = Pinky.Location.X;
            py = Pinky.Location.Y;
            rx = Blinky.Location.X;
            ry = Blinky.Location.Y;
            #region Animation
            if (u)
            {
                switch (p)
                {
                    case 3:
                        Player.Image = Properties.Resources.PacManU2;
                        break;
                    case 6:
                        Player.Image = Properties.Resources.PacManU1;
                        break;
                    case 9:
                        Player.Image = Properties.Resources.PacMan3;
                        break;
                    case 12:
                        Player.Image = Properties.Resources.PacManU1;
                        p = 0;
                        break;
                }
                    
            }
            else if (d)
            {
                switch (p)
                {
                    case 3:
                        Player.Image = Properties.Resources.PacManD2;
                        break;
                    case 6:
                        Player.Image = Properties.Resources.PacManD1;
                        break;
                    case 9:
                        Player.Image = Properties.Resources.PacMan3;
                        break;
                    case 12:
                        Player.Image = Properties.Resources.PacManD1;
                        p = 0;
                        break;
                }
            }
            else if (l)
            {
                switch (p)
                {
                    case 3:
                        Player.Image = Properties.Resources.PacManL2;
                        break;
                    case 6:
                        Player.Image = Properties.Resources.PacManL1;
                        break;
                    case 9:
                        Player.Image = Properties.Resources.PacMan3;
                        break;
                    case 12:
                        Player.Image = Properties.Resources.PacManL1;
                        p = 0;
                        break;
                }
            }
            else if (r)
            {
                switch (p)
                {
                    case 3:
                        Player.Image = Properties.Resources.PacManR2;
                        break;
                    case 6:
                        Player.Image = Properties.Resources.PacManR1;
                        break;
                    case 9:
                        Player.Image = Properties.Resources.PacMan3;
                        break;
                    case 12:
                        Player.Image = Properties.Resources.PacManR1;
                        p = 0;
                        break;
                }
            }
            if(p==12)
            {
                p = 0;
            }
            p++;
            #endregion
            #region Movement
            if (u) { Player.Top -= Speed; }
            else if (d) { Player.Top += Speed;}
            else if (l) { Player.Left -= Speed;}
            else if (r) { Player.Left += Speed;}
            #endregion
            #region Ghost Movement
            PinkGhost();
            RedGhost();
            #endregion
        }
        #region Movement
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { u = false; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { d = false; }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { l = false; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { r = false; }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { u = true; }
            if(e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { d = true; }
            if(e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { l = true; }
            if(e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { r = true; }
            if(e.KeyCode==Keys.Subtract)
            { Properties.Settings.Default.Reset(); }
        }
        #endregion
        private void WallCollision()
        {
            if (MoveAndAnimate.Enabled == true)
            {
                #region Player
                for (int i = 0; i < walls.Length; i++)
                {
                    if (Player.Bounds.IntersectsWith(walls[i].Bounds))
                    {
                        Player.Location = new Point(plx, ply);
                    }
                }
                if (Player.Bounds.IntersectsWith(GhostWall.Bounds))
                {
                    Player.Location = new Point(plx, ply);
                }
                if(Player.Bounds.IntersectsWith(Corridor1.Bounds) || Player.Bounds.IntersectsWith(Corridor2.Bounds))
                {
                    Speed = 4;
                }
                else
                {
                    Speed = 3;
                }
                //Lives
                for(int i=0;i<ghosts.Length; i++)
                {
                    if(Player.Bounds.IntersectsWith(ghosts[i].Bounds))
                    {
                        if(life ==0)
                        {
                            GameOver();
                        }
                        if (life > 0)
                        {
                            life -= 1;
                            Player.Location = new Point(402, 349);
                        }
                    }
                }
                #endregion
                #region Pink Ghost
                for (int i = 0; i < walls.Length; i++)
                {
                    if (Pinky.Bounds.IntersectsWith(walls[i].Bounds) && pStart == true)
                    {
                        Pinky.Location = new Point(px, py);
                        pChange = false;
                    }
                }
                if (Pinky.Bounds.IntersectsWith(GhostWall.Bounds) && pStart==true)
                {
                    Pinky.Location = new Point(px, py);
                    pChange=false;
                }
                if (Pinky.Bounds.IntersectsWith(Corridor1.Bounds) || Pinky.Bounds.IntersectsWith(Corridor2.Bounds))
                {
                    pSpeed = 2;
                }
                else
                {
                    pSpeed = 3;
                }
                #endregion
                #region Red Ghost
                for (int i = 0; i < walls.Length; i++)
                {
                    if (Blinky.Bounds.IntersectsWith(walls[i].Bounds))
                    {
                        Blinky.Location = new Point(rx, ry);
                        if (ru == true || rd == true)
                        {
                            if (rl) { Blinky.Left -= rSpeed; }
                            else if (rr) { Blinky.Left += rSpeed; }
                            ru = false; rd = false;
                            rChange = false;
                        }
                        if (rl == true || rr == true)
                        {
                            if (ru) { Blinky.Left -= rSpeed; }
                            else if (rd) { Blinky.Left += rSpeed; }
                            rl = false; rr = false;
                            rChange = false;
                        }
                    }
                }
                if (Blinky.Bounds.IntersectsWith(GhostWall.Bounds) && rStart == true)
                {
                    Blinky.Location = new Point(px, py);
                }
                if (Blinky.Bounds.IntersectsWith(Corridor1.Bounds) || Blinky.Bounds.IntersectsWith(Corridor2.Bounds))
                {
                    rSpeed = 2;
                }
                else
                {
                    rSpeed = 3;
                }
                
                #endregion
            }
        }
        private void GameOver()
        {
            if(score>Properties.Settings.Default.High1)
            {
                Properties.Settings.Default.SScore = score;
                tmrUpdate.Enabled = false;
                MoveAndAnimate.Enabled = false;
                f2.ShowDialog();                
            }
            else if(score < Properties.Settings.Default.High1)
            {
                Application.Exit();
            }
            Properties.Settings.Default.Start = false;
            Properties.Settings.Default.SScore = 0;
            Properties.Settings.Default.Lives = 2;
        }
        private void Teleport()
        {
            if(Player.Location.X<162)
            {
                px = 642;
                Player.Left = 642;
            }
            if (Player.Location.X > 643)
            {
                px = 163;
                Player.Left = 163;
            }
        }
        private void Reset()
        {       
            if (pellets.Count == 0 && Bpellets.Count == 0 && life >= 0)
            {
                Properties.Settings.Default.SScore = score;
                Properties.Settings.Default.Lives = life;                
                tmrUpdate.Enabled = false;
                MoveAndAnimate.Enabled = false;
                Properties.Settings.Default.Start = true;
                Properties.Settings.Default.Save();
                System.Diagnostics.Process.Start(Application.ExecutablePath); 
                Application.Exit();
            }

        }
        #region Ghost Movement
        private void PinkGhost()
        {
            #region AI
            
            if (rStart == true)
            {
                if (pStart == false)
                {
                    Pinky.Top -= pSpeed;
                    if (Pinky.Location.Y <= 252)
                    {
                        pStart = true;
                    }
                }
            }
            #endregion
            if (pStart == true && pChange == false)
            {
                orange = rnd.Next(1, 5);
                if (orange ==1) { Clyde.Top -= pSpeed; pChange = true; }
                else if (orange == 2) { Clyde.Top += pSpeed; pChange = true; }
                else if (orange == 3) { Clyde.Left -= pSpeed; pChange = true; }
                else if (orange == 4) { Clyde.Left += pSpeed; pChange = true; }
            }
        }
        private void OrangeGhost()
        {
            #region AI

            if (pStart == true)
            {
                if (oStart == false)
                {
                    Pinky.Top -= pSpeed;
                    if (Pinky.Location.Y <= 252)
                    {
                        pStart = true;
                    }
                }
            }
            #endregion
            if (pStart == true && pChange == false)
            {
                pink = rnd.Next(1, 5);
                if (pink == 1) { Pinky.Top -= pSpeed; pChange = true; }
                else if (pink == 2) { Pinky.Top += pSpeed; pChange = true; }
                else if (pink == 3) { Pinky.Left -= pSpeed; pChange = true; }
                else if (pink == 4) { Pinky.Left += pSpeed; pChange = true; }
            }
        }
        private void RedGhost()
        {
            if(rStart==false)
            {
                Blinky.Top -= rSpeed;
                if(Blinky.Location.Y <= 252)
                {
                    rStart = true;
                }
            }
            else if (rStart == true&&rChange==false)
            {
                #region AI
                if (Player.Location.Y < Blinky.Location.Y)
                {
                    ru = true;
                    rd = false;
                }
                else if (Player.Location.Y > Blinky.Location.Y)
                {
                    ru = false;
                    rd = true;
                }
                if (Player.Location.X < Blinky.Location.X)
                {
                    rl = true;
                    rr = false;
                }
                else if (Player.Location.X > Blinky.Location.X)
                {
                    rl = false;
                    rr = true;                    
                }
                #endregion
                #region Booleans
                    if (ru) { Blinky.Top -= rSpeed; rChange = true; }
                    else if (rd) { Blinky.Top += rSpeed; rChange = true; }
                    if (rl) { Blinky.Left -= rSpeed; rChange = true; }
                    else if (rr) { Blinky.Left += rSpeed; rChange = true; }               
                #endregion
            }
        }
        private void Start()
        {
            if(rStart==false && Blinky.Location.Y<=252)
            {
                rStart = true;
            }
        }
        #endregion
        private void Lives()
        {
            if(life==0)
            {
                Lives1.Image = null;
            }
            else if (life == 1)
            {
                Lives1.Image = Properties.Resources.PacManL1;
                Lives2.Image = null;
            }
            else if (life == 2)
            {
                Lives1.Image = Properties.Resources.PacManL1;
                Lives2.Image = Properties.Resources.PacManL1;
                Lives3.Image = null;
            }
            else if (life == 3)
            {
                Lives1.Image = Properties.Resources.PacManL1;
                Lives2.Image = Properties.Resources.PacManL1;
                Lives3.Image = Properties.Resources.PacManL1;
            }
        }
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            Score();
            WallCollision();
            Teleport();
            Start();
            LoadPellets();
            Lives();
            Reset();
        }

        private void pictureBox132_Click(object sender, EventArgs e)
        {
            f2.ShowDialog();
            tmrUpdate.Stop();
            MoveAndAnimate.Stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.SScore = score;
            Properties.Settings.Default.Lives = life;
            if (pellets.Count > 0 && Bpellets.Count > 0)
            {
                Properties.Settings.Default.Start = false;
            }
            Properties.Settings.Default.Save();
        }

        private void pictureBox48_Click(object sender, EventArgs e)
        {
            f3.ShowDialog();
        }
    }
}
