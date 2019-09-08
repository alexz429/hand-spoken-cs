using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hand_spoken_frontend
{
    public partial class Form1 : Form
    {
        private static string BACKEND_URL = "http://10.251.83.44:8008";
        private static readonly HttpClient client = new HttpClient();
        static List<KeyValuePair<string, Image>> images = new List<KeyValuePair<string,Image>>();
        public Form1()
        {
            InitializeComponent();
            InitializeOpenFileDialog();
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(flowLayoutPanel);
            panel1.Controls.Add(pictureBox1);
            panel2.Hide();
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button5);
        }

        private void InitializeOpenFileDialog()
        {
            // Set the file dialog to filter for graphics files.
            this.openFileDialog1.Filter =
                "Images (*.BMP;*.JEPG;*.GIF)|*.BMP;*.JPEG;*.GIF|" +
                "All files (*.*)|*.*";

            this.openFileDialog1.Title = "My Image Browser";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Read the files
                foreach (string file in openFileDialog1.FileNames)
                {
                    // Create a PictureBox.
                    try
                    {
                        Image loadedImage = Image.FromFile(file);
                        
                        images.Add(new KeyValuePair<string,Image>(file,loadedImage));
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
            refreshPhotos();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            startBuf1();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            images.Clear();
            refreshPhotos();
        }

        private void refreshPhotos()
        {
            flowLayoutPanel.Controls.Clear();
            foreach (KeyValuePair<string, Image> next in images)
            {
                PictureBox pb = new PictureBox();
                Label tb = new Label();
                tb.Font = new Font("Tw Cen MT", 10);
                tb.BackColor = Color.Transparent;
                tb.BorderStyle = BorderStyle.None;
                tb.ForeColor = Color.White;
                tb.Width = 220;
                tb.Text = next.Key.Split('\\')[next.Key.Split('\\').Length-1];//hahahahaha this is so bad
                pb.Height = 60;
                pb.Width = 100;
                pb.BackgroundImage = next.Value;
                pb.BackgroundImageLayout = ImageLayout.Stretch;
                flowLayoutPanel.Controls.Add(pb);
                flowLayoutPanel.Controls.Add(tb);
                flowLayoutPanel.SetFlowBreak(tb, true);
            }
        }

        private async void delay()
        {

        }
        private async void startBuf1()
        {
            Image image = images[0].Value;
            string b64;
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();
                b64 = Convert.ToBase64String(imageBytes);
            }

            Dictionary<string, string> values = new Dictionary<string, string>();

            var content = new FormUrlEncodedContent(values);
            panel1.Hide();
            panel1.Dispose();
            Refresh();
            pictureBox2.Visible = true;
            pictureBox2.Show();
            Refresh();
            //var response = await client.PostAsync(BACKEND_URL, content);
            //var responseString = await response.Content.ReadAsStringAsync();
            System.Threading.Thread.Sleep(2000);
            //Console.WriteLine(responseString);
            pictureBox2.Hide();
            pictureBox2.Dispose();
            start_layer2();
        }

            panel2.Show();
        private void start_layer2()
        {
        }

        private void Button5_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            File.Open(openFileDialog2.FileName, FileMode.Open);
        }
    }
}
