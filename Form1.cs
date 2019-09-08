using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hand_spoken_frontend
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        static List<Image> images = new List<Image>();
        public Form1()
        {
            InitializeComponent();
            InitializeOpenFileDialog();
        }

        private void InitializeOpenFileDialog()
        {
            // Set the file dialog to filter for graphics files.
            this.openFileDialog1.Filter =
                "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
                "All files (*.*)|*.*";

            // Allow the user to select multiple images.
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "My Image Browser";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Read the files
                foreach (String file in openFileDialog1.FileNames)
                {
                    // Create a PictureBox.
                    try
                    {
                        Image loadedImage = Image.FromFile(file);
                        
                        images.Add(loadedImage);
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }
            }
            flowLayoutPanel1.Controls.Clear();
            foreach(Image next in images)
            {
                PictureBox pb = new PictureBox();

                pb.Height = next.Height / 10;
                pb.Width = next.Width / 10;
                pb.Image = next;

                flowLayoutPanel1.Controls.Add(pb);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
