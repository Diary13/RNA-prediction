using FontAwesome.Sharp;
using RNA_prediction.childForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RNA_prediction
{
    public partial class Form1 : Form
    {

        private IconButton CurrentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        private UserControl currentChildUserControl;

        public Form1()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);

            this.Text = string.Empty;
            this.ControlBox = false;
            //this.DoubleBuffered = false;

            //Affichage user control par défaut (Accueil)
            CurrentBtn = iconButton1;
            CurrentBtn.BackColor = Color.FromArgb(4, 54, 108);
            leftBorderBtn.Location = new Point(0, 140);
            leftBorderBtn.BackColor = Color.WhiteSmoke;
            leftBorderBtn.Visible = true;
            leftBorderBtn.BringToFront();

            currentChildUserControl = new AccueilUserControl();
            panelDesktop.Controls.Add(currentChildUserControl);
            currentChildUserControl.Dock = DockStyle.Fill;
            currentChildUserControl.BringToFront();
            currentChildUserControl.Show();
        }

        private void ActiveButton(object senderBtn)
        {
            if(senderBtn != null)
            {
                DisableButton();
                CurrentBtn = (IconButton)senderBtn;
                CurrentBtn.BackColor = Color.FromArgb(4, 54, 108);
                leftBorderBtn.BackColor = Color.WhiteSmoke;
                leftBorderBtn.Location = new Point(0, CurrentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                //icon current child form
                iconCurrentChildForm.IconChar = CurrentBtn.IconChar;
                lblCurrentChildForm.Text = CurrentBtn.Text;
            }
        }

        private void DisableButton()
        {
            if(CurrentBtn != null)
            {
                CurrentBtn.BackColor = Color.FromArgb(4, 24, 48);
            }
        }

        private void OpenChildForm(UserControl childForm)
        {
            
            if (currentChildUserControl != null)
            {
                currentChildUserControl.Hide();
                //currentChildForm.Close();
            }
            currentChildUserControl = childForm;
            panelDesktop.Controls.Add(childForm);
            childForm.Dock = DockStyle.Fill;
            childForm.BringToFront();
            childForm.Show();

            //currentChildForm = childForm;
            //childForm.TopLevel = false;
            //childForm.FormBorderStyle = FormBorderStyle.None;
            //childForm.Dock = DockStyle.Fill;
            //panelDesktop.Controls.Add(childForm);
            //panelDesktop.Tag = childForm;
            //childForm.BringToFront();
            //childForm.Show();
        }

        private void IconButton1_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            OpenChildForm(new AccueilUserControl());
        }

        private void IconButton2_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            OpenChildForm(new Henon());
        }

        private void IconButton3_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            OpenChildForm(new Apprentissage());
        }

        private void IconButton4_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            OpenChildForm(new PredictUnPas());
        }

        private void IconButton5_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);

            OpenChildForm(new PredictPlusieursPas());
        }

        private void IconButton6_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            OpenChildForm(new Result());
        }

        private void IconButton7_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            OpenChildForm(new Plus());
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParm, int lParam);
        private void PanelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void IconPictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void IconPictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
