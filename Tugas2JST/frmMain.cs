using JST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas2JST
{
    public partial class frmJst2 : Form
    {
        Perceptron pct;
        string[] filesDirectory;
        Label[] lblGambarFont1, lblGambarFont2, lblGambarFont3;
        bool print = true;
        System.Diagnostics.Process proc;

        public frmJst2()
        {
            pct = new Perceptron();
            InitializeComponent();
            modeInputPattern();
            filesDirectory = new string[21];
            addTargetLabel();

            proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;

            saveFileDialog1.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog1.FilterIndex = 1;

        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    filesDirectory = openFileDialog1.FileNames;

                    for (int i = 0; i < filesDirectory.Length; i++)
                    {
                        if (openFileDialog1.SafeFileNames[i].Substring(0, 8) == "pattern1")
                        {
                            pct.ListPattern1.Add(new Pattern(filesDirectory[i]));

                        }
                        else if (openFileDialog1.SafeFileNames[i].Substring(0, 8) == "pattern2")
                        {
                            pct.ListPattern2.Add(new Pattern(filesDirectory[i]));

                        }
                        else
                        {
                            pct.ListPattern3.Add(new Pattern(filesDirectory[i]));

                        }
                    }
                    for (int i = 0; i < 7; i++)
                    {
                        inputPattern(i, pct.ListPattern1, lblGambarFont1);
                        inputPattern(i, pct.ListPattern2, lblGambarFont2);
                        inputPattern(i, pct.ListPattern3, lblGambarFont3);
                    }
                    pct.ListFont.Add(pct.ListPattern1);
                    pct.ListFont.Add(pct.ListPattern2);
                    pct.ListFont.Add(pct.ListPattern3);
                    modeTestTrain();
                }
                catch (Exception ex)
                {
                    pct.Reset();
                    for (int i = 0; i < 7; i++)
                    {
                        lblGambarFont1[i].Text = "";
                        lblGambarFont2[i].Text = "";
                        lblGambarFont3[i].Text = "";
                    }
                    MessageBox.Show("Please insert all pattern directly", "Alert",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        private void addTargetLabel()
        {
            int x = new int();
            x = 0;
            lblGambarFont1 = new Label[7];
            lblGambarFont2 = new Label[7];
            lblGambarFont3 = new Label[7];

            for (int i = 0; i < 7; i++)
            {
                lblGambarFont1[i] = new Label();
                lblGambarFont1[i].Location = new Point(x, 20);
                lblGambarFont1[i].AutoSize = true;
                groupBox1.Controls.Add(lblGambarFont1[i]);
               

                lblGambarFont2[i] = new Label();
                lblGambarFont2[i].Location = new Point(x, 20);
                lblGambarFont2[i].AutoSize = true;
                groupBox2.Controls.Add(lblGambarFont2[i]);
               

                lblGambarFont3[i] = new Label();
                lblGambarFont3[i].Location = new Point(x, 20);
                lblGambarFont3[i].AutoSize = true;
                groupBox3.Controls.Add(lblGambarFont3[i]);
                x += 90;
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            rtbTrain.Text = pct.Training();
            btnTrain.Enabled = false;
            btnTest.Enabled = true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            rtbTest.Text = pct.Testing();
            btnTest.Enabled = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (rtbTest.Text != "" || rtbTrain.Text != "")
            {

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PrintToExcel(saveFileDialog1.FileName);
                    proc.StartInfo.FileName = saveFileDialog1.FileName;
                    proc.Start();
                }
            }
            else
            {
                MessageBox.Show("There are no results to be printed", "Alert",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            pct.Reset();
            rtbTest.Clear();
            rtbTrain.Clear();
            for (int i = 0; i < 7; i++)
            {
                lblGambarFont1[i].Text = "";
                lblGambarFont2[i].Text = "";
                lblGambarFont3[i].Text = "";
            }
            modeInputPattern();
            print = true;
        }

        private void inputPattern(int i,List<Pattern> listPattern,Label[] label)
        {
            string input;

            for (int j = 0; j < 63; j++)
            {
                if (listPattern[i].x[j].ToString() == "1")
                {
                    input = "#";
                }
                else
                {
                    input = "_";
                }
                if ((j + 1) % 7 == 0)
                {
                    label[i].Text = label[i].Text + input + "\n";
                }
                else
                {
                    label[i].Text = label[i].Text + input;
                }
            }

        }

        private void PrintToExcel(string name)
        {

            FileStream fs1 = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs1);
            writer.Write(pct.getPrintToExcel(print));
            writer.Close();
            print = false;
        }

        private void modeInputPattern()
        {
            btnTest.Enabled = false;
            btnTrain.Enabled = false;
            btnPrint.Enabled = false;
            btnReset.Enabled = false;
            btnInput.Enabled = true;
        }

        private void modeTestTrain()
        {
            btnTest.Enabled = false;
            btnTrain.Enabled = true;
            btnPrint.Enabled = true;
            btnReset.Enabled = true;
            btnInput.Enabled = false;
        }

       

        

    }
}
