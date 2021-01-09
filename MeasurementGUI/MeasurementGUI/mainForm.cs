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
        private double zoomFactor;
        private int[] idx = new int[2];

        private List<Tuple<Rectangle, Rectangle, Color>> endpoints = new List<Tuple<Rectangle, Rectangle, Color>>();
        

        private bool isMouseDown = false;
        private bool started = false;


        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Text = "MeasurementGUI";
            pictureBox.AllowDrop = true;

            Rectangle rectJointLineL = new Rectangle(225, 456, 10, 10);
            Rectangle rectJointLineR = new Rectangle(360, 519, 10, 10);
            Rectangle rectL = new Rectangle(221, 546, 10, 10);
            Rectangle rectR = new Rectangle(361, 542, 10, 10);

            endpoints.Add(Tuple.Create(rectJointLineL, rectJointLineR, Color.Yellow));
            endpoints.Add(Tuple.Create(rectL, rectR, Color.Blue));
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

            img = Image.FromFile(files[0]);
            pictureBox.Image = img;
            imgFilePath = files[0];
            this.Text = "";
            this.Text += "MeasurementGUI -- " + imgFilePath;
            pictureBox.BackColor = this.BackColor;
            zoomFactor = 1;
            zoomBtn.Enabled = true;
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
            try
            {
                e.Effect = DragDropEffects.Copy;
            }
            catch { }
      
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

        /**************************************************************************************************************************************
         **************************************************      Start button    **************************************************************
         *************************************************************************************************************************************/

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                saveBtn.Enabled = true;
                started = true;
                pictureBox.Refresh();
            }
            
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (started)
            {
                foreach (var line in endpoints)
                {

                    e.Graphics.FillRectangle(new SolidBrush(line.Item3), line.Item1);
                    e.Graphics.FillRectangle(new SolidBrush(line.Item3), line.Item2);
                }

                for (int i=0; i<endpoints.Count; i++)
                {
                    Pen p = new Pen(endpoints[i].Item3);
                    e.Graphics.DrawLine(p, endpoints[i].Item1.Location, endpoints[i].Item2.Location);
                }
            }
            
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            idx = GetClickedEndPoint(e.Location);
        }


        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && idx[0] != 999)
            {
                Rectangle rect;
                if (idx[1] == 0) { rect = endpoints[idx[0]].Item1; }
                else { rect = endpoints[idx[0]].Item2; }
                
                rect.Location = e.Location;
                
                //Console.WriteLine("@@@@@@@@@  here " + rect.Location);

                if (rect.Right > pictureBox.Width)
                {
                    rect.X = pictureBox.Width - rect.Width;
                }
                if (rect.Top < 0)
                {
                    rect.Y = 0;
                }
                if (rect.Left < 0)
                {
                    rect.X = 0;
                }
                if (rect.Bottom > pictureBox.Height)
                {
                    rect.Y = pictureBox.Height - rect.Height;
                }

                if (idx[1] == 0) { endpoints[idx[0]] = Tuple.Create(rect, endpoints[idx[0]].Item2, endpoints[idx[0]].Item3); }
                else { endpoints[idx[0]] = Tuple.Create(endpoints[idx[0]].Item1, rect, endpoints[idx[0]].Item3); }

                Refresh();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }


        /**************************************************************************************************************************************
         ****************************************************      helpers    *****************************************************************
         *************************************************************************************************************************************/

        private int[] GetClickedEndPoint(Point location)
        {
            int[] idx = { 0, 0 };
            foreach (var line in endpoints)
            {
                if (location.X >= line.Item1.X && location.Y >= line.Item1.Y && location.X <= line.Item1.Right && location.Y <= line.Item1.Bottom)
                {
                    idx[1] = 0;
                    return idx;
                }

                if (location.X >= line.Item2.X && location.Y >= line.Item2.Y && location.X <= line.Item2.Right && location.Y <= line.Item2.Bottom)
                {
                    idx[1] = 1;
                    return idx;
                }

                idx[0] += 1;
            }

            idx = new int[]{ 999, 999};
            return idx;
        }

    }
}
