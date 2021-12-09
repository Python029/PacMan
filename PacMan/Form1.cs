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
        
        int p = 0;
        int Speed = 3;
        int pSpeed = 3;
        int rSpeed = 3;
        bool pellet = false;
        bool u = false; bool d = false; bool l = false; bool r = false;
        bool pu = false; bool pd = false; bool pl = false; bool pr = false;
        bool ru = false; bool rd = false; bool rl = false; bool rr = false;
        bool bu = false; bool bd = false; bool bl = false; bool br = false;
        bool ou = false; bool od = false; bool ol = false; bool or = false;
        bool pStart = false; bool rStart = false; bool bStart = false; bool oStart = false;
        bool pChange = false; bool rChange = false;
        int plx = 0; int ply = 0; int px = 0; int py = 0; int rx = 0; int ry = 0;
        PictureBox[] walls;
        List<PictureBox> pellets = new List<PictureBox>();        

        private void Form1_Load(object sender, EventArgs e)
        {
            //DoubleBuffer/Smoother Animation Code:this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            walls = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20, pictureBox21, pictureBox22, pictureBox23, pictureBox24, pictureBox25, pictureBox26, pictureBox27, pictureBox28, pictureBox29, pictureBox30, pictureBox31, pictureBox32, pictureBox33, pictureBox34, pictureBox35, pictureBox36, pictureBox37, pictureBox38, pictureBox39, pictureBox40, pictureBox41, pictureBox42, pictureBox43, pictureBox44, pictureBox45, pictureBox46 };
        }
        private void LoadPellets()
        {
            if (pellet == false)
            {
                for (int i = 49; i < 132; i++)
                {
                    pellets.Add("pictureBox" + i.ToString());
                }
                pellet = true;
            }
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
                #endregion
                #region Pink Ghost
                for (int i = 0; i < walls.Length; i++)
                {
                    if (Pinky.Bounds.IntersectsWith(walls[i].Bounds))
                    {
                        Pinky.Location = new Point(px, py);
                    }
                }
                if (Player.Bounds.IntersectsWith(GhostWall.Bounds))
                {
                    Pinky.Location = new Point(px, py);
                }
                if (Pinky.Bounds.IntersectsWith(Corridor1.Bounds) || Player.Bounds.IntersectsWith(Corridor2.Bounds))
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
                            rChange = false;
                        }
                        
                    }
                }
                if (Player.Bounds.IntersectsWith(GhostWall.Bounds))
                {
                    Blinky.Location = new Point(rx, ry);
                }
                if (Player.Bounds.IntersectsWith(Corridor1.Bounds) || Player.Bounds.IntersectsWith(Corridor2.Bounds))
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
        #region Ghost Movement
        private void PinkGhost()
        {
            #region AI
                
            #endregion
            if (pu) { Pinky.Top -= pSpeed; }
            else if (pd) { Pinky.Top += pSpeed; }
            if (pl) { Pinky.Left -= pSpeed; }
            else if (pr) { Pinky.Left += pSpeed; }
        }
        private void RedGhost()
        {
            if(rStart==false)
            {
                Blinky.Top -= rSpeed;
            }
            else if (rStart == true)
            {
                #region AI
                if (Player.Location.X < Blinky.Location.X)
                {
                    rl = true;
                    rr = false;
                }
                if (Player.Location.X > Blinky.Location.X)
                {
                    rl = false;
                    rr = true;
                }
                if (Player.Location.Y < Blinky.Location.Y)
                {
                    ru = true;
                    rd = false;
                }
                if (Player.Location.Y > Blinky.Location.Y)
                {
                    ru = false;
                    rd = true;
                }
                if (Player.Location.X == Blinky.Location.X)
                {
                    rl = false;
                    rr = false;
                }
                if (Player.Location.Y == Blinky.Location.X)
                {
                    ru = false;
                    rd = false;
                }
                #endregion
                #region Booleans
                if (rChange == false)
                {
                    if (ru) { Blinky.Top -= rSpeed; rChange = true; }
                    else if (rd) { Blinky.Top += rSpeed; rChange = true; }
                    if (rl) { Blinky.Left -= rSpeed; rChange = true; }
                    else if (rr) { Blinky.Left += rSpeed; rChange = true; }
                }
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
        
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            WallCollision();
            Teleport();
            Start();
            LoadPellets();
        }
    }
}
