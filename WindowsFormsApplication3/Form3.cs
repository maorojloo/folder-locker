using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form3 : Form
    {
        int leave_time;
        static int index = 0;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           // button1.Text = "skip";
           // button2.Text = "Exit";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string text = "Powered By \n                ma.orojloo                                     ";
            int t_l;
            t_l = text.Length;

            
            label1.Text = text.Substring(0, index) + "";
            index++;
            if (index == text.Length + 1)
            {
               
Form2 fs2 = new Form2();
            fs2.Show();
            this.Hide();
                timer2.Start();
                
                index = 0;
                timer1.Enabled = false;
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
         
           

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f_2 = new Form2();
            this.Hide();
            timer1.Stop();
            f_2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }
    }
}
