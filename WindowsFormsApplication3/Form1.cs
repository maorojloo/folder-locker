using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Threading;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        string path;
        
        string _LogFileName = "FolderHideLock.txt";
        string _LogFilePath = string.Empty;










        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            _LogFilePath = Path.Combine(Application.StartupPath, _LogFileName);
            LoadLockedFolders();
            base.OnLoad(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnBrowseFolder.Select();
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    label2.Text= fbd.SelectedPath;
                   path = fbd.SelectedPath;
                   textBox1.Text = string.Empty;
                   textBox1.ReadOnly = true;
                   textBox1.Text = path;
                }
            }

             
        }

        private void LoadLockedFolders()
        {
            dgvFolderLog.Rows.Clear();
            if (File.Exists(_LogFilePath))
            {
                string[] temp = File.ReadAllLines(_LogFilePath);
                for (int i = 0; i < temp.Length; i++)
                {
                    if (!string.IsNullOrEmpty(temp[i]))
                        dgvFolderLog.Rows.Add(temp[i]);
                }
            }
          // label2.ResetText();
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo("cmd");
                info.UseShellExecute = false;
                info.RedirectStandardError = true;
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.CreateNoWindow = true;
                info.ErrorDialog = false;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process P = Process.Start(info);
                StreamWriter W = P.StandardInput;
                if (W != null)
                    W.WriteLine("attrib  \"" + path + "\"");///("attrib +s +h \"" +label2.Text + "\"");باعث مخفی شدن نیز میشع

                Thread.Sleep(1000);
                P.Kill();

                string folderPath = path;
                string adminUserName = Environment.UserName;// getting your adminUserName
                DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                ds.AddAccessRule(fsa);
                Directory.SetAccessControl(folderPath, ds);

              //  MessageBox.Show( MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                MessageBox.Show("Locked", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                using (StreamWriter sw = (File.Exists(_LogFilePath)) ? File.AppendText(_LogFilePath) : File.CreateText(_LogFilePath))
                {
                    sw.WriteLine(path);
                }
               // label2.ResetText();
                LoadLockedFolders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath =path;
                string adminUserName = Environment.UserName;// getting your adminUserName
                DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);

                ds.RemoveAccessRule(fsa);
                Directory.SetAccessControl(folderPath, ds);

                ProcessStartInfo info = new ProcessStartInfo("cmd");
                info.UseShellExecute = false;
                info.RedirectStandardError = true;
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.CreateNoWindow = true;
                info.ErrorDialog = false;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process P = Process.Start(info);
                StreamWriter W = P.StandardInput;
                if (W != null)
                    W.WriteLine("attrib -s -h \"" + path + "\"");

                Thread.Sleep(1000);
                P.Kill();

                string[] temp = File.ReadAllLines(_LogFilePath);
                List tempnew = new List();

                for (int i = 0; i < temp.Length; i++)
                {
                    if (!temp[i].Contains(path) && !string.IsNullOrEmpty(temp[i]))
                        tempnew.Add(temp[i]);
                }

                File.WriteAllLines(_LogFilePath, tempnew.ToArray());
                MessageBox.Show("Unlocked", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLockedFolders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvFolderLog_Click(object sender, EventArgs e)
        {
            if (dgvFolderLog.CurrentRow != null)
            {
                path = Convert.ToString(dgvFolderLog.CurrentRow.Cells[0].Value);
                btnUnlock.Enabled = true;
            }
            else
                btnUnlock.Enabled = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo("cmd");
                info.UseShellExecute = false;
                info.RedirectStandardError = true;
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.CreateNoWindow = true;
                info.ErrorDialog = false;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process P = Process.Start(info);
                StreamWriter W = P.StandardInput;
                if (W != null)
                    W.WriteLine("attrib +s +h \"" +path + "\"");

                Thread.Sleep(1000);
                P.Kill();

                string folderPath = path;
                string adminUserName = Environment.UserName;// getting your adminUserName
                DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                ds.AddAccessRule(fsa);
                Directory.SetAccessControl(folderPath, ds);

                MessageBox.Show("Locked & hided", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                using (StreamWriter sw = (File.Exists(_LogFilePath)) ? File.AppendText(_LogFilePath) : File.CreateText(_LogFilePath))
                {
                    sw.WriteLine(path);
                }
                //label2.ResetText();
                LoadLockedFolders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
           path = op.FileName;
           label2.Text = op.FileName;
           textBox1.Text = string.Empty;
           textBox1.ReadOnly = true;
           textBox1.Text = path;
            if (path == string.Empty)
            {
                
                return;
            }

            //StreamReader sv = new StreamReader(s);
            //path = sv.ReadToEnd();
            //sv.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            string clear_Data;
           
           clear_Data = string.Empty;

           StreamWriter a = new StreamWriter(_LogFilePath);
           a.WriteLine(clear_Data);                             //پاک کردن داده ها


           Application.Restart();

          




              



        }

        private void dgvFolderLog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            path = textBox1.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();

            f2.Show();
            this.Hide();
           


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
