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
    public partial class Form2 : Form
    {

         Int32 w_pass;
        
            


        public Form2()
        {
            InitializeComponent();
        }


        
        private void button2_Click(object sender, EventArgs e)
        {
            if (textbox.Text == "ma.orojloo")
            {
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();

            }

            else
            {
                w_pass++;
                if(w_pass==3)
                {
                    Application.Exit();
                }
                 if(w_pass==1)
                 {
                textbox.Focus();
                textbox.Clear();

                DialogResult result;
                result = MessageBox.Show("Forget your password?! \n \n you can help website do you want to open web?? ", "error", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //press yes
                    System.Diagnostics.Process.Start("http://maorojloo.webs.com");


                }
                if (result == DialogResult.No)
                {
                    //You pressed the No button
                }
                
                     if(w_pass==2)
                     {
                         textbox.Focus();
                         textbox.Clear();


                     }
                

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
           
            // txtPassword.UseSystemPasswordChar = true;
        }
   
        private void Form2_Load(object sender, EventArgs e)
        {
            w_pass = 0;

            textbox.UseSystemPasswordChar = false;
            // Set to no text.
            textbox.Text = "";
            // The password character is an asterisk.
            textbox.PasswordChar = 'X';
            // The control will allow no more than 14 characters.
            textbox.MaxLength = 14;
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            textbox.UseSystemPasswordChar = true;

         else
                textbox.UseSystemPasswordChar = false;
                



           
                          
    

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
