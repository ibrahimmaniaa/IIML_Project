using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeasurementGUI
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Text = "MeasurementGUI";
            pictureBox.AllowDrop = true;
        }

        private void uploadImgBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog()
            {
                FileName = "Select a text file",
                Filter = "Image Files(*.png; *.jpg; *.jpeg; *.gif; *.bmp)|*.png; *.jpg; *.jpeg; *.gif; *.bmp",
                Title = "Select image"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(open.FileName);
                this.Text = "";
                this.Text += "MeasurementGUI -- " + open.FileName;
                pictureBox.BackColor = this.BackColor;
            }
        }

        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if(data != null)
            {
                var fileNames = data as string[];
                if (fileNames.Length > 0)
                {
                    pictureBox.Image = Image.FromFile(fileNames[0]);
                    this.Text = "";
                    this.Text += "MeasurementGUI -- " + fileNames[0];
                    pictureBox.BackColor = this.BackColor;

                }
            }
        }

        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(ImageFormat.Bmp.ToString()) &&
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
            Console.WriteLine(" @@@@@@@ ");
            Console.WriteLine(" @@@@@@@ " + e.Data.GetDataPresent(DataFormats.Bitmap));
            e.Effect = DragDropEffects.Copy;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png",
                    Title = "Save an Image File"
                };

                string[] subs = this.Text.Split('\\');
                sfd.FileName = subs[subs.Length-1];

                ImageFormat format = ImageFormat.Png;
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        pictureBox.Image.Save(sfd.FileName, format);
                    }
                }
            }
            
        }

        /**
        Point startPos;         // mouse-down position
        Point endPos;           // current mouse position
        Point currentPos;
        bool isDrawing = false;

        private void partPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (imgSwitch)
            {
                startPos = e.Location;
                isDrawing = true;
            }
        }

        private void partPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                currentPos = e.Location;
                partPictureBox.Paint += new PaintEventHandler(paintZoomRect);
                partPictureBox.Refresh();
            }
        }


        private bool isFirstZoom = false;
        private void partPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
                partPictureBox.Paint += new PaintEventHandler(paintZoomRect);
                partPictureBox.Refresh();

                if (startPos == e.Location) { return; }
                //else if (startPos.X - e.Location.X < 5 | startPos.Y - e.Location.Y < 5) { return; }
                else { endPos = e.Location; }

                //Console.WriteLine("?????  "+startPos.ToString() +"  " +currentPos.ToString() +"  " + endPos.ToString());

                Bitmap bmp;
                Rectangle cropRect = getZoomRect();

                int xRatio = Convert.ToInt32(originPartPictureImg.Width / partPictureBox.Width);
                int yRatio = Convert.ToInt32(originPartPictureImg.Height / partPictureBox.Height);

                int margin = 0;
                if (originPartPictureImg.Width > originPartPictureImg.Height) { margin = Convert.ToInt32((partPictureBox.Width - (originPartPictureImg.Width * (1 / xRatio))) / 2); }
                else { margin = Convert.ToInt32((partPictureBox.Height - (originPartPictureImg.Height * (1 / yRatio))) / 2); }
                Console.WriteLine("?????  " + margin);

                if (isFirstZoom)
                {
                    // cropRect parameters here have to be mapped to the originPartPictureImg dimensions
                    cropRect.X = cropRect.X * xRatio;
                    cropRect.Y = cropRect.Y * yRatio;
                    cropRect.Width = cropRect.Width * xRatio;
                    cropRect.Height = cropRect.Height * yRatio;
                    isFirstZoom = false;
                    bmp = new Bitmap(originPartPictureImg);
                }
                // cropRect parameters here are just the same with respect to partPictureBox dimensions 
                else { bmp = new Bitmap(partPictureBox.Image); }

                if (cropRect.Width > cropRect.Height) { cropRect.Height = cropRect.Width; }
                else { cropRect.Width = cropRect.Height; }

                Bitmap zoomed = new Bitmap(partPictureBox.Width, partPictureBox.Height);

                using (Graphics g = Graphics.FromImage(zoomed))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.DrawImage(bmp, new Rectangle(0, 0, zoomed.Width, zoomed.Height), cropRect, GraphicsUnit.Pixel);
                }
                partPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                partPictureBox.Image = zoomed;
            }
        }

        private void paintZoomRect(object sender, PaintEventArgs e)
        {
            Pen pen;
            if (isDrawing) { pen = new Pen(Color.White, 2); }
            else { pen = new Pen(Color.Transparent, 2); }
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawRectangle(pen, getZoomRect());
            e.Dispose();
        }

        private Rectangle getZoomRect()
        {
            return new Rectangle(
                Math.Min(startPos.X, currentPos.X),
                Math.Min(startPos.Y, currentPos.Y),
                Math.Abs(startPos.X - currentPos.X),
                Math.Abs(startPos.Y - currentPos.Y));
        }
    **/

    }
}
