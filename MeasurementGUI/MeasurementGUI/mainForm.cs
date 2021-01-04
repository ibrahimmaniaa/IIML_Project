using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeasurementGUI
{
    public partial class mainForm : Form
    {
        private string imgFilePath;
        private Image img;
        private bool zoomEnabled = false;
        //private bool saveEnabled = false;
        private double zoomFactor;


        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Text = "MeasurementGUI";
            pictureBox.AllowDrop = true;
        }


        /**************************************************************************************************************************************
         ************************************************       upload image        ***********************************************************
         *************************************************************************************************************************************/

        private void uploadImgBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog()
            {
                Filter = "Image Files(*.png; *.jpg; *.jpeg; *.bmp)|*.png; *.jpg; *.jpeg; *.bmp",
                Title = "Select image"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                img = Image.FromFile(open.FileName);
                pictureBox.Image = img;
                imgFilePath = open.FileName;
                this.Text = "";
                this.Text += "MeasurementGUI -- " + imgFilePath;
                pictureBox.BackColor = this.BackColor;
                zoomFactor = 1;
                zoomBtn.Enabled = true;
            }
        }

        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            zoomFactor = 1;
            zoomBtn.Enabled = true;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            pictureBox.Image = Image.FromFile(files[0]);
        }

        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap) &&
        (e.AllowedEffect & DragDropEffects.Copy) != 0)
            {
                // Allow this.
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                // Don't allow any other drop.
                e.Effect = DragDropEffects.None;
            }

            e.Effect = DragDropEffects.Copy;
        }

        /**************************************************************************************************************************************
         **********************************************         save button       *************************************************************
         *************************************************************************************************************************************/

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png",
                Title = "Save an Image File"
            };

            string[] subs = imgFilePath.Split('\\');
            sfd.FileName = subs[subs.Length - 1];

            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(sfd.FileName))
                {
                    pictureBox.Image.Save(sfd.FileName, format);
                }
            }
        }
            
        

        /**************************************************************************************************************************************
         *********************************************         zoom functions       ***********************************************************
         *************************************************************************************************************************************/
        

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (zoomEnabled)
            {
                zoomFactor += 0.2;
                if (zoomFactor <= 1.6) { pictureBox.Image = Zoom(pictureBox.Image); }
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            }         
        }

        private Image Zoom(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            Bitmap zoomed = new Bitmap(Convert.ToInt32(img.Width * zoomFactor), Convert.ToInt32(img.Height * zoomFactor));

            using (Graphics g = Graphics.FromImage(zoomed))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.SmoothingMode = SmoothingMode.None;
                g.DrawImage(bmp, new Rectangle(0, 0, zoomed.Width, zoomed.Height));
            }
            return zoomed;
        }


        /**************************************************************************************************************************************
         **************************************************         buttons      **************************************************************
         *************************************************************************************************************************************/

        private void homeBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image = img;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            zoomBtn.FlatStyle = FlatStyle.Popup;
            pictureBox.Cursor = Cursors.Default;
            pictureBox.Size = panel1.Size;
            zoomEnabled = false;
            zoomFactor = 1;
        }

        private void zoomBtn_Click(object sender, EventArgs e)
        {
            zoomBtn.FlatStyle = FlatStyle.Flat;
            pictureBox.Cursor = new Cursor("C:\\Users\\z00491jc\\Desktop\\private\\seminar\\MeasurementGUI\\zoom.cur");
            zoomEnabled = true;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                saveBtn.Enabled = true;
            }
            
        }

       
    }
}
