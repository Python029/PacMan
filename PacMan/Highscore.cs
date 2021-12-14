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
    public partial class Highscore : Form
    {
        Form2 f3 = new Form2();
        public Highscore()
        {
            InitializeComponent();
        }
        private void Highscore_Load(object sender, EventArgs e)
        {
            #region Update
            Properties.Settings.Default.Name5 = Properties.Settings.Default.Name4;
            Properties.Settings.Default.Name4 = Properties.Settings.Default.Name3;
            Properties.Settings.Default.Name3 = Properties.Settings.Default.Name2;
            Properties.Settings.Default.Name2 = Properties.Settings.Default.Name1;
            Properties.Settings.Default.High5 = Properties.Settings.Default.High4;
            Properties.Settings.Default.High4 = Properties.Settings.Default.High3;
            Properties.Settings.Default.High3 = Properties.Settings.Default.High2;
            Properties.Settings.Default.High2 = Properties.Settings.Default.High1;
            #endregion
            Properties.Settings.Default.High1 = Properties.Settings.Default.SScore;
            f3.ShowDialog();
            #region Names
            txtName1.Text = Properties.Settings.Default.Name1;
            txtName2.Text = Properties.Settings.Default.Name2;
            txtName3.Text = Properties.Settings.Default.Name3;
            txtName4.Text = Properties.Settings.Default.Name4;
            txtName5.Text = Properties.Settings.Default.Name5;
            if(Properties.Settings.Default.Name1=="")
            {
                txtName1.Text = "___";
            }
            if (Properties.Settings.Default.Name2 == "")
            {
                txtName2.Text = "___";
            }
            if (Properties.Settings.Default.Name3 == "")
            {
                txtName3.Text = "___";
            }
            if (Properties.Settings.Default.Name4 == "")
            {
                txtName4.Text = "___";
            }
            if (Properties.Settings.Default.Name5 == "")
            {
                txtName5.Text = "___";
            }
            #endregion
            #region Scores
            lblScore1.Text = Properties.Settings.Default.High1.ToString();
            lblScore2.Text = Properties.Settings.Default.High2.ToString();
            lblScore3.Text = Properties.Settings.Default.High3.ToString();
            lblScore4.Text = Properties.Settings.Default.High4.ToString();
            lblScore5.Text = Properties.Settings.Default.High5.ToString();
            #endregion
            #region Text Boxes
            #region High Score 1
            if (Properties.Settings.Default.High1 ==0)
            {
                lblScore1.Text = "000000";
            }
            if (Properties.Settings.Default.High1 >= 1 && Properties.Settings.Default.High1 < 10)
            {
                lblScore1.Text = "00000" + Properties.Settings.Default.High1.ToString();
            }
            if (Properties.Settings.Default.High1 >= 10 && Properties.Settings.Default.High1 < 100)
            {
                lblScore1.Text = "0000" + Properties.Settings.Default.High1.ToString();
            }
            if (Properties.Settings.Default.High1 >= 100 && Properties.Settings.Default.High1 < 1000)
            {
                lblScore1.Text = "000" + Properties.Settings.Default.High1.ToString();
            }
            if (Properties.Settings.Default.High1 >= 1000 && Properties.Settings.Default.High1 < 10000)
            {
                lblScore1.Text = "00" + Properties.Settings.Default.High1.ToString();
            }
            if (Properties.Settings.Default.High1 >= 10000 && Properties.Settings.Default.High1 < 100000)
            {
                lblScore1.Text = "0" + Properties.Settings.Default.High1.ToString();
            }
            #endregion
            #region High Score 2
            if (Properties.Settings.Default.High2 == 0)
            {
                lblScore2.Text = "000000";
            }
            if (Properties.Settings.Default.High2 >= 1 && Properties.Settings.Default.High2 < 10)
            {
                lblScore2.Text = "00000" + Properties.Settings.Default.High2.ToString();
            }
            if (Properties.Settings.Default.High2 >= 10 && Properties.Settings.Default.High2 < 100)
            {
                lblScore2.Text = "0000" + Properties.Settings.Default.High2.ToString();
            }
            if (Properties.Settings.Default.High2 >= 100 && Properties.Settings.Default.High2 < 1000)
            {
                lblScore2.Text = "000" + Properties.Settings.Default.High2.ToString();
            }
            if (Properties.Settings.Default.High2 >= 1000 && Properties.Settings.Default.High2 < 10000)
            {
                lblScore2.Text = "00" + Properties.Settings.Default.High2.ToString();
            }
            if (Properties.Settings.Default.High2 >= 10000 && Properties.Settings.Default.High2 < 100000)
            {
                lblScore2.Text = "0" + Properties.Settings.Default.High2.ToString();
            }
            #endregion
            #region High Score 3
            if (Properties.Settings.Default.High3 == 0)
            {
                lblScore3.Text = "000000";
            }
            if (Properties.Settings.Default.High3 >= 1 && Properties.Settings.Default.High3 < 10)
            {
                lblScore3.Text = "00000" + Properties.Settings.Default.High3.ToString();
            }
            if (Properties.Settings.Default.High3 >= 10 && Properties.Settings.Default.High3 < 100)
            {
                lblScore3.Text = "0000" + Properties.Settings.Default.High3.ToString();
            }
            if (Properties.Settings.Default.High3 >= 100 && Properties.Settings.Default.High3 < 1000)
            {
                lblScore3.Text = "000" + Properties.Settings.Default.High3.ToString();
            }
            if (Properties.Settings.Default.High3 >= 1000 && Properties.Settings.Default.High3 < 10000)
            {
                lblScore3.Text = "00" + Properties.Settings.Default.High3.ToString();
            }
            if (Properties.Settings.Default.High3 >= 10000 && Properties.Settings.Default.High3 < 100000)
            {
                lblScore3.Text = "0" + Properties.Settings.Default.High3.ToString();
            }
            #endregion
            #region High Score 4
            if (Properties.Settings.Default.High4 == 0)
            {
                lblScore4.Text = "000000";
            }
            if (Properties.Settings.Default.High4 >= 1 && Properties.Settings.Default.High4 < 10)
            {
                lblScore4.Text = "00000" + Properties.Settings.Default.High4.ToString();
            }
            if (Properties.Settings.Default.High4 >= 10 && Properties.Settings.Default.High4 < 100)
            {
                lblScore4.Text = "0000" + Properties.Settings.Default.High4.ToString();
            }
            if (Properties.Settings.Default.High4 >= 100 && Properties.Settings.Default.High4 < 1000)
            {
                lblScore4.Text = "000" + Properties.Settings.Default.High4.ToString();
            }
            if (Properties.Settings.Default.High4 >= 1000 && Properties.Settings.Default.High4 < 10000)
            {
                lblScore4.Text = "00" + Properties.Settings.Default.High4.ToString();
            }
            if (Properties.Settings.Default.High4 >= 10000 && Properties.Settings.Default.High4 < 100000)
            {
                lblScore4.Text = "0" + Properties.Settings.Default.High4.ToString();
            }
            #endregion
            #region High Score 5
            if (Properties.Settings.Default.High5 == 0)
            {
                lblScore5.Text = "000000";
            }
            if (Properties.Settings.Default.High5 >= 1 && Properties.Settings.Default.High5 < 10)
            {
                lblScore5.Text = "00000" + Properties.Settings.Default.High5.ToString();
            }
            if (Properties.Settings.Default.High5 >= 10 && Properties.Settings.Default.High5 < 100)
            {
                lblScore5.Text = "0000" + Properties.Settings.Default.High5.ToString();
            }
            if (Properties.Settings.Default.High5 >= 100 && Properties.Settings.Default.High5 < 1000)
            {
                lblScore5.Text = "000" + Properties.Settings.Default.High5.ToString();
            }
            if (Properties.Settings.Default.High5 >= 1000 && Properties.Settings.Default.High5 < 10000)
            {
                lblScore5.Text = "00" + Properties.Settings.Default.High5.ToString();
            }
            if (Properties.Settings.Default.High5 >= 10000 && Properties.Settings.Default.High5 < 100000)
            {
                lblScore5.Text = "0" + Properties.Settings.Default.High5.ToString();
            }
            #endregion
            #endregion           
        }
    }
}
