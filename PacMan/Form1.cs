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
        Form4 f4 = new Form4();
        Random rnd = new Random();
        int pink = 0;
        int orange = 0;
        int red = 0;
        int blue = 0;
        int green = 0;
        int p = 0;
        int Speed = 3;
        int pSpeed = 3;
        int oSpeed = 3;
        int rSpeed = 3;
        int bSpeed = 3;
        int gSpeed = 3;
        bool pellet = false;
        int gScore = 0;
        bool big = false;
        int score;
        int life;
        int x = 0;
        bool u = false; bool d = false; bool l = false; bool r = false;
        bool pStart = false; bool rStart = false; bool bStart = false; bool oStart = false; bool gStart = false;
        int plx = 0; int ply = 0; int px = 0; int py = 0; int rx = 0; int ry = 0; int ox = 0; int oy = 0; int bx = 0; int by=0; int gx = 0; int gy = 0;
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
            ghosts = new PictureBox[] { Pinky, Blinky, Clyde, Inky, Slimy };
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
            Clyde.Location = new Point(402, 302);
            Inky.Location = new Point(402, 302);
            Slimy.Location = new Point(402, 302);
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
                    gScore = 0;
                    x = 0;
                    Big_Pellet.Enabled = true;
                    Bpellets[i].Dispose();
                    Bpellets.RemoveAt(i);
                    big = true;
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
            ox = Clyde.Location.X;
            oy = Clyde.Location.Y;
            bx = Inky.Location.X;
            by = Inky.Location.Y;
            gx = Slimy.Location.X;
            gy = Slimy.Location.Y;

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
            OrangeGhost();
            BlueGhost();
            GreenGhost();
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
                    if (Player.Bounds.IntersectsWith(ghosts[i].Bounds) && big == false)
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
                    else if(Player.Bounds.IntersectsWith(ghosts[i].Bounds) && big == true && x<=10000)
                    {
                        gScore++;
                        ghosts[i].Location = new Point(402, 252);
                        if (i == 1)
                        {
                            Pinky.Location = new Point(402, 252);
                            Pinky.Image = Properties.Resources.PinkUp;                           
                            pink = 1;
                        }
                        else if (i == 2)
                        {
                            Blinky.Location = new Point(402, 252);
                            Blinky.Image = Properties.Resources.RedUp;
                            red = 1;                           
                        }
                        else if (i == 3)
                        {
                            Clyde.Location = new Point(402, 252);
                            Clyde.Image = Properties.Resources.Oup;
                            orange = 1;
                        }
                        else if (i == 4)
                        {
                            Inky.Location = new Point(402, 252);
                            Inky.Image = Properties.Resources.BlueUp;
                            blue = 1;
                        }
                        else if (i == 5)
                        {
                            Slimy.Location = new Point(402, 252);
                            Slimy.Image = Properties.Resources.GreenUp;
                            green = 1;
                        }
                        #region Ghost Reset
                        if (gScore ==1)
                        {
                            score += 200;                                                                               
                        }
                        else if (gScore == 2)
                        {
                            score += 400;
                        }
                        else if (gScore == 3)
                        {
                            score += 800;
                        }
                        else if (gScore == 4)
                        {
                            score += 1600;
                        }
                        else if (gScore == 5)
                        {
                            score += 3200;
                            big = false;
                        }
                        #endregion
                    }
                }
                #endregion
                #region Pink Ghost
                for (int i = 0; i < walls.Length; i++)
                {
                    if (Pinky.Bounds.IntersectsWith(walls[i].Bounds) && pStart == true)
                    {
                        pink = rnd.Next(1, 5);
                        Pinky.Location = new Point(px, py);
                    }
                }
                if (Pinky.Bounds.IntersectsWith(GhostWall.Bounds) && pStart==true)
                {
                    Pinky.Location = new Point(px, py);
                    pink = rnd.Next(3, 5);
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
                #region Orange Ghost
                for (int i = 0; i < walls.Length; i++)
                {
                    if (Clyde.Bounds.IntersectsWith(walls[i].Bounds) && oStart == true)
                    {
                        orange = rnd.Next(1, 5);
                        Clyde.Location = new Point(ox, oy);
                    }
                }
                if (Clyde.Bounds.IntersectsWith(GhostWall.Bounds) && oStart == true)
                {
                    Clyde.Location = new Point(ox, oy);
                    orange = rnd.Next(3, 5);
                }
                if (Clyde.Bounds.IntersectsWith(Corridor1.Bounds) || Clyde.Bounds.IntersectsWith(Corridor2.Bounds))
                {
                    oSpeed = 2;
                }
                else
                {
                    oSpeed = 3;
                }
                #endregion
                #region Blue Ghost
                for (int i = 0; i < walls.Length; i++)
                {
                    if (Inky.Bounds.IntersectsWith(walls[i].Bounds) && bStart == true)
                    {
                        blue = rnd.Next(1, 5);
                        Inky.Location = new Point(bx, by);
                    }
                }
                if (Inky.Bounds.IntersectsWith(GhostWall.Bounds) && bStart == true)
                {
                    Inky.Location = new Point(bx, by);
                    blue = rnd.Next(3, 5);
                }
                if (Inky.Bounds.IntersectsWith(Corridor1.Bounds) || Inky.Bounds.IntersectsWith(Corridor2.Bounds))
                {
                    bSpeed = 2;
                }
                else
                {
                    bSpeed = 3;
                }
                #endregion
                #region Green Ghost
                for (int i = 0; i < walls.Length; i++)
                {
                    if (Slimy.Bounds.IntersectsWith(walls[i].Bounds) && gStart == true)
                    {
                        green = rnd.Next(1, 5);
                        Slimy.Location = new Point(gx, gy);
                    }
                }
                if (Slimy.Bounds.IntersectsWith(GhostWall.Bounds) && gStart == true)
                {
                    Slimy.Location = new Point(gx, gy);
                    green = rnd.Next(3, 5);
                }
                if (Slimy.Bounds.IntersectsWith(Corridor1.Bounds) || Slimy.Bounds.IntersectsWith(Corridor2.Bounds))
                {
                    gSpeed = 2;
                }
                else
                {
                    gSpeed = 3;
                }
                #endregion
                #region Red Ghost
                for (int i = 0; i < walls.Length; i++)
                {
                    if (Blinky.Bounds.IntersectsWith(walls[i].Bounds) && rStart == true)
                    {
                        red = rnd.Next(1, 5);
                        Blinky.Location = new Point(rx, ry);
                    }
                }
                if (Blinky.Bounds.IntersectsWith(GhostWall.Bounds) && rStart == true)
                {
                    Blinky.Location = new Point(rx, ry);
                    red = rnd.Next(3, 5);
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
            if (rStart == true)
            {
                if (pStart == false)
                {
                    Pinky.Top -= pSpeed;
                    if (Pinky.Location.Y <= 252)
                    {
                        pStart = true;
                        pink = rnd.Next(1, 5);
                    }
                }
            }
            if (pStart == true)
            {
                if (big == false)
                {
                    if (pink == 1) { Pinky.Top -= pSpeed; Pinky.Image = Properties.Resources.PinkUp; }
                    else if (pink == 2) { Pinky.Top += pSpeed; Pinky.Image = Properties.Resources.PinkDown; }
                    else if (pink == 3) { Pinky.Left -= pSpeed; Pinky.Image = Properties.Resources.PinkLeft; }
                    else if (pink == 4) { Pinky.Left += pSpeed; Pinky.Image = Properties.Resources.PinkRight; }
                }
                else if (big)
                {
                    if (pink == 1) { Pinky.Top -= pSpeed; Pinky.Image = Properties.Resources.BigUp; }
                    else if (pink == 2) { Pinky.Top += pSpeed; Pinky.Image = Properties.Resources.BigDown; }
                    else if (pink == 3) { Pinky.Left -= pSpeed; Pinky.Image = Properties.Resources.BigLeft; }
                    else if (pink == 4) { Pinky.Left += pSpeed; Pinky.Image = Properties.Resources.BigRight; }
                }
            }
        }
        private void GreenGhost()
        {
            if (bStart == true)
            {
                if (gStart == false)
                {
                    Slimy.Top -= gSpeed;
                    if (Slimy.Location.Y <= 252)
                    {
                        gStart = true;
                        green = rnd.Next(1, 5);
                    }
                }
            }
            if (pStart == true)
            {
                if (big == false)
                {
                    if (green == 1) { Slimy.Top -= gSpeed; Slimy.Image = Properties.Resources.GreenUp; }
                    else if (green == 2) { Slimy.Top += gSpeed; Slimy.Image = Properties.Resources.GreenDown; }
                    else if (green == 3) { Slimy.Left -= gSpeed; Slimy.Image = Properties.Resources.GreenLeft; }
                    else if (green == 4) { Slimy.Left += gSpeed; Slimy.Image = Properties.Resources.GreenRight; }
                }
                else if(big)
                {
                    if (green == 1) { Slimy.Top -= gSpeed; Slimy.Image = Properties.Resources.BigUp; }
                    else if (green == 2) { Slimy.Top += gSpeed; Slimy.Image = Properties.Resources.BigDown; }
                    else if (green == 3) { Slimy.Left -= gSpeed; Slimy.Image = Properties.Resources.BigLeft; }
                    else if (green == 4) { Slimy.Left += gSpeed; Slimy.Image = Properties.Resources.BigRight; }
                }
            }
        }
        private void BlueGhost()
        {
            if (oStart == true)
            {
                if (bStart == false)
                {
                    Inky.Top -= bSpeed;
                    if (Inky.Location.Y <= 252)
                    {
                        bStart = true;
                        blue = rnd.Next(1, 5);
                    }
                }
            }
            if (bStart == true)
            {
                if (big == false)
                {
                    if (blue == 1) { Inky.Top -= bSpeed; Inky.Image = Properties.Resources.BlueUp; }
                    else if (blue == 2) { Inky.Top += bSpeed; Inky.Image = Properties.Resources.BlueDown; }
                    else if (blue == 3) { Inky.Left -= bSpeed; Inky.Image = Properties.Resources.BlueLeft; }
                    else if (blue == 4) { Inky.Left += bSpeed; Inky.Image = Properties.Resources.BlueRight; }
                }
                else if(big)
                {
                    if (blue == 1) { Inky.Top -= bSpeed; Inky.Image = Properties.Resources.BigUp; }
                    else if (blue == 2) { Inky.Top += bSpeed; Inky.Image = Properties.Resources.BigDown; }
                    else if (blue == 3) { Inky.Left -= bSpeed; Inky.Image = Properties.Resources.BigLeft; }
                    else if (blue == 4) { Inky.Left += bSpeed; Inky.Image = Properties.Resources.BigRight; }
                }
            }
        }
        private void OrangeGhost()
        {

            if (pStart == true)
            {
                if (oStart == false)
                {
                    Clyde.Top -= oSpeed;
                    if (Clyde.Location.Y <= 252)
                    {
                        oStart = true;
                        orange = rnd.Next(1, 5);
                    }
                }
            }
            if (oStart == true)
            {
                if (big == false)
                {
                    if (orange == 1) { Clyde.Top -= oSpeed; Clyde.Image = Properties.Resources.Oup; }
                    else if (orange == 2) { Clyde.Top += oSpeed; Clyde.Image = Properties.Resources.Odown; }
                    else if (orange == 3) { Clyde.Left -= oSpeed; Clyde.Image = Properties.Resources.Oleft; }
                    else if (orange == 4) { Clyde.Left += oSpeed; Clyde.Image = Properties.Resources.Oright; }
                }
                else if(big)
                {
                    if (orange == 1) { Clyde.Top -= oSpeed; Clyde.Image = Properties.Resources.BigUp; }
                    else if (orange == 2) { Clyde.Top += oSpeed; Clyde.Image = Properties.Resources.BigDown; }
                    else if (orange == 3) { Clyde.Left -= oSpeed; Clyde.Image = Properties.Resources.BigLeft; }
                    else if (orange == 4) { Clyde.Left += oSpeed; Clyde.Image = Properties.Resources.BigRight; }
                }
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
                    red = rnd.Next(1, 5);
                }
            }
            if (rStart == true)
            {
                if (big == false)
                {
                    if (red == 1) { Blinky.Top -= rSpeed; Blinky.Image = Properties.Resources.RedUp; }
                    else if (red == 2) { Blinky.Top += rSpeed; Blinky.Image = Properties.Resources.RedDown; }
                    else if (red == 3) { Blinky.Left -= rSpeed; Blinky.Image = Properties.Resources.RedLeft; }
                    else if (red == 4) { Blinky.Left += rSpeed; Blinky.Image = Properties.Resources.RedRight; }
                }
                else if(big)
                {
                    if (red == 1) { Blinky.Top -= rSpeed; Blinky.Image = Properties.Resources.BigUp; }
                    else if (red == 2) { Blinky.Top += rSpeed; Blinky.Image = Properties.Resources.BigDown; }
                    else if (red == 3) { Blinky.Left -= rSpeed; Blinky.Image = Properties.Resources.BigLeft; }
                    else if (red == 4) { Blinky.Left += rSpeed; Blinky.Image = Properties.Resources.BigRight; }
                }

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

        private void pictureBox47_Click(object sender, EventArgs e)
        {
            f4.ShowDialog();
        }

        private void Big_Pellet_Tick(object sender, EventArgs e)
        {
            x++;
            if(x >= 22)
            { big=false;
                Big_Pellet.Enabled = false;
            }
        }
    }
}