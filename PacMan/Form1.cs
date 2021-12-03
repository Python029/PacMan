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
        bool pac = false;
        int p = 1;
        int Speed = 3;
        int pSpeed = 3;
        bool u = false; bool d = false;
        bool l = false; bool r = false;
        bool pu = false; bool pd = false;
        bool pl = false; bool pr = false;
        int px = 0; int py = 0;
        PictureBox[] walls;

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

        private void MoveAndAnimate_Tick(object sender, EventArgs e)
        {
            px = Player.Location.X;
            py = Player.Location.Y;
            #region Animation
            if(p==3)
            {
                Player.Image = Properties.Resources.PacMan2;
            }
            else if (p == 6)
            {
                Player.Image = Properties.Resources.PacMan1;
            }
            else if (p == 9)
            {
                Player.Image = Properties.Resources.PacMan3;
            }
            else if (p == 12)
            {
                Player.Image = Properties.Resources.PacMan1;
                p = 0;
            }
            p++;
            #endregion
            #region Movement
            if (u) { Player.Top -= Speed; }
            if (d) { Player.Top += Speed; }
            if (l) { Player.Left -= Speed; }
            if (r) { Player.Left += Speed; }
            #endregion
            #region Ghost Movement
            PinkGhost();
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
                        Player.Location = new Point(px, py);
                    }
                }
                if (Player.Bounds.IntersectsWith(GhostWall.Bounds))
                {
                    Player.Location = new Point(px, py);
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
                if (Player.Bounds.IntersectsWith(Corridor1.Bounds) || Player.Bounds.IntersectsWith(Corridor2.Bounds))
                {
                    pSpeed = 2;
                }
                else
                {
                    pSpeed = 3;
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
            if (pu) { Pinky.Top -= Speed; }
            if (pd) { Pinky.Top += Speed; }
            if (pl) { Pinky.Left -= Speed; }
            if (pr) { Pinky.Left += Speed; }
        }
        #endregion

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            WallCollision();
            Teleport();
        }
    }
}
