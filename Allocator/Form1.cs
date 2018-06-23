using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using MetroFramework;
using InfoBox;
using System.Net.Sockets;
using System.IO;
using System.Xml;

namespace Allocator
{
    public partial class Form1 : Form
    {
        string LBSYSNAME, access;
        public Form1()
        {
            InitializeComponent();
        }
        internal class Logininfo
        {
            public static string username
            {
                get;
                set;
            }
            public static string userid
            {
                get;
                set;
            }
            public static string hostelid
            {
                get;
                set;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LBSYSNAME = Dns.GetHostName();
            foreach (IPAddress adress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (adress.AddressFamily == AddressFamily.InterNetwork)
                {
                    toolStripStatusLabel1.Text = "IP Address: " + adress.ToString();
                }                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (InformationBox.Show("Please Confirm Application Exit", "Project Administrator", InformationBoxButtons.YesNo, InformationBoxIcon.Question).ToString() == "No")
            {

            }
            else
            {
                Application.Exit();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == "" || metroTextBox2.Text == "")
            {
                InformationBox.Show("Username/Password Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
            }
            else
                if(metroTextBox1.Text == "USER" && metroTextBox2.Text == "USER")
                {
                    Logininfo.username = metroTextBox1.Text;
                    InformationBox.Show("Welcome, "+ metroTextBox1.Text + "", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Success);
                    Form2 MDIOperations = new Form2();
                    MDIOperations.Show();
                    this.Hide();
                }
            else
            {
                checklogin();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (metroTextBox1.Text == "" || metroTextBox2.Text == "")
                {
                    InformationBox.Show("Username/Password Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                }
                else
                    if (metroTextBox1.Text == "USER" && metroTextBox2.Text == "USER")
                    {
                        Logininfo.username = metroTextBox1.Text;
                        InformationBox.Show("Welcome, " + metroTextBox1.Text + "", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Success);
                        Form2 MDIOperations = new Form2();
                        MDIOperations.Show();
                        this.Hide();
                    }
                    else
                    {

                        checklogin();
                    }
            }
        }

        private void checklogin()
        {
            try
            {
                string username = metroTextBox1.Text.Trim();
                string password = metroTextBox2.Text;
                string url = "http://localhost/Allocator/cgi-bin/porter_login.php?staffid=" + username + "&password=" + password + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                access = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();

                if (access == "1")
                {
                    Logininfo.userid = NodeList[0].ChildNodes[1].InnerText.Trim();
                    Logininfo.username = NodeList[0].ChildNodes[2].InnerText.Trim();
                    string greetings = NodeList[0].ChildNodes[2].InnerText.Trim();
                    Logininfo.hostelid = NodeList[0].ChildNodes[3].InnerText.Trim();
                    InformationBox.Show("Welcome, " + greetings + "", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Success);
                    Form2 homepage = new Form2();
                    homepage.Show();
                    this.Hide();
                }
                else
                {
                    InformationBox.Show("Sorry Your Username/Password Is Incorrect", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                    metroTextBox2.Clear();
                    this.ActiveControl = metroTextBox2;
                    metroTextBox2.Focus();
                }
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                //InformationBox.Show(exec.StackTrace);
            }
        }
    }
}
