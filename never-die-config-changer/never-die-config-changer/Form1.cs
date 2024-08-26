using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.IO;

namespace never die config changer 
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Initialize_Click(object sender, EventArgs e)
        {
            // find config dir
            string myDocumentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            string targetFolderPath = Path.Combine(myDocumentsPath, "never die config changer from Fatality by EXU ");
            CFGDir.Text = targetFolderPath;
            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }
            // list all config
            string[] cfgFiles = Directory.GetFiles(targetFolderPath, "*", SearchOption.AllDirectories);
            ConfigList.Items.Clear();
            foreach (string cfgFile in cfgFiles)
            {
                string fileName = Path.GetFileName(cfgFile);
                ConfigList.Items.Add(fileName);
            }
            Refresh.Enabled = true;
            OpenDir.Enabled = true;
            Create.Enabled = true;
            // find cs dir
            string processName = "CS2.EXE";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE Name = '{processName}'");
            bool foundProcess = false;
            foreach (ManagementObject process in searcher.Get())
            {
                string processPath = process["ExecutablePath"]?.ToString();
                if (!string.IsNullOrEmpty(processPath))
                {
                    string directoryPath = System.IO.Path.GetDirectoryName(processPath);
                    Dir.Text = directoryPath;
                    foundProcess = true;
                    Load.Enabled = true;
                    Save.Enabled = true;
                }
            }
            if (!foundProcess)
            {
                Dir.Text = "Abra o CS2 primeiro";
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja salvar?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                string configFile = Path.Combine(Dir.Text, "fatal_settings.cfg");
                string content = File.ReadAllText(configFile);
                string ConfigName = CFGName.Text;
                string configPath = Path.Combine(CFGDir.Text, ConfigName);

                File.WriteAllText(configPath, content);

            }
            else
            {
                return;
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(CFGDir.Text))
            {
                Directory.CreateDirectory(CFGDir.Text);
            }
            // list all config
            string[] cfgFiles = Directory.GetFiles(CFGDir.Text, "*", SearchOption.AllDirectories);
            ConfigList.Items.Clear();
            foreach (string cfgFile in cfgFiles)
            {
                string fileName = Path.GetFileName(cfgFile);
                ConfigList.Items.Add(fileName);
            }
        }

        private void Load_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to load?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                string configFile = Path.Combine(Dir.Text, "fatal_settings.cfg");
                string ConfigName = CFGName.Text;
                string configPath = Path.Combine(CFGDir.Text, ConfigName);
                string fileContent = File.ReadAllText(configPath);

                File.WriteAllText(configFile, fileContent);

            }
            else
            {
                return;
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            string configFile = Path.Combine(CFGDir.Text, CFGName.Text);
            if (!File.Exists(configFile) && CFGName.Text != "Config existente")
            {
                // create file
                FileStream fs = File.Create(configFile);
                fs.Close(); // close file stream
                string contentFile = Path.Combine(Dir.Text, "fatal_settings.cfg");
                string content = File.ReadAllText(contentFile);

                File.WriteAllText(configFile, content);
            }
            else
            {
                CFGName.Text = "Config existente";
            }
            if (!Directory.Exists(CFGDir.Text))
            {
                Directory.CreateDirectory(CFGDir.Text);
            }
            // list all config
            string[] cfgFiles = Directory.GetFiles(CFGDir.Text, "*", SearchOption.AllDirectories);
            ConfigList.Items.Clear();
            foreach (string cfgFile in cfgFiles)
            {
                string fileName = Path.GetFileName(cfgFile);
                ConfigList.Items.Add(fileName);
            }
        }

        private void ConfigList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConfigList.SelectedItem != null)
            {
                CFGName.Text = ConfigList.SelectedItem.ToString();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.pcrf.net/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://help.unicef.org/donate-to-children");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://donate.unrwa.org/gaza/~my-donation");
        }

        private void OpenDir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", CFGDir.Text);
            // list all config
            string[] cfgFiles = Directory.GetFiles(CFGDir.Text, "*", SearchOption.AllDirectories);
            ConfigList.Items.Clear();
            foreach (string cfgFile in cfgFiles)
            {
                string fileName = Path.GetFileName(cfgFile);
                ConfigList.Items.Add(fileName);
            }
        }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void label3_Click(object sender, EventArgs e)
    {

    }

    private void label4_Click(object sender, EventArgs e)
    {

    }
}
}
