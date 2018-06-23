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
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Controls;
using InfoBox;
using System.Net.Sockets;
using System.IO;
using System.Xml;

namespace Allocator
{
    public partial class Form2 : Form
    {
        string sessionid;
        string LBSYSNAME;
        public Form2()
        {
            InitializeComponent();
        }
        internal class Vitalinfo
        {
            public static string matricnumber
            {
                get;
                set;
            }
            public static string officialID
            {
                get;
                set;
            }
            public static string sessionid
            {
                get;
                set;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {           
            if (Form1.Logininfo.username == "USER")
            {
                roomClearanceToolStripMenuItem.Visible = false;
                fileToolStripMenuItem.Visible = false;
                changeRoomDetailsToolStripMenuItem.Visible = false;
                viewAllocationInformationToolStripMenuItem.Visible = false;
            }
            LBSYSNAME = Dns.GetHostName();
            foreach (IPAddress adress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (adress.AddressFamily == AddressFamily.InterNetwork)
                {
                    toolStripStatusLabel1.Text = "IP Address: " + adress.ToString();
                }
            }
            toolStripStatusLabel4.Text = "Welcome, " + Form1.Logininfo.username;
            getsessionid();
            getsession();
        }
        private void getsession()
        {
            try
            {
                string url = "http://localhost/Allocator/cgi-bin/get_session.php?";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 60000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("Session");
                // Create a list of the nodes in the xml file //
                toolStripStatusLabel2.Text = "Session: " + NodeList[0].ParentNode.ChildNodes[0].InnerText.Trim();
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Comida Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
            }
        }
        private void getsessionid()
        {
            try
            {
                string url = "http://localhost/Allocator/cgi-bin/get_session_id.php?";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 60000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("ID");
                // Create a list of the nodes in the xml file //
                sessionid = NodeList[0].ParentNode.ChildNodes[0].InnerText.Trim();
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Comida Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (InformationBox.Show("Please Confirm User Logout", "Project Administrator", InformationBoxButtons.YesNo, InformationBoxIcon.Question).ToString() == "No")
            {

            }
            else
            {
                Form1 homepage = new Form1();
                homepage.Show();
                this.Hide();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                InformationBox.Show("You Cannot Open Two Hostel Allocation Forms", "Project Administrator", InformationBoxButtons.OK);
            }
                
            else
            {
                Vitalinfo.sessionid = sessionid;
                Form3 hostelallocationform = new Form3();
                hostelallocationform.MdiParent = this;
                hostelallocationform.Show();
            }
                
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel5.Text = DateTime.Now.ToString();
        }

        private void roomClearanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form1.Logininfo.username == "USER")
            {
                InformationBox.Show("Sorry You Do Not Have Sufficient Permission To Perform This Task!!", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
            }
            else
            {
                Form5 Roomclearance = new Form5();
                Roomclearance.MdiParent = this;
                Roomclearance.Show();
            }
        }

        private void minimizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form1.Logininfo.username == "USER")
            {
                InformationBox.Show("Sorry You Do Not Have Sufficient Permission To Perform This Task!!", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
            }
            else
            {
                Form6 RoomAllocator = new Form6();
                RoomAllocator.MdiParent = this;
                RoomAllocator.Show();
            }
        }

        private void menuStrip1_Move(object sender, EventArgs e)
        {
            
        }

        private void Form2_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.ShowBalloonTip(1000, "Important Notice", "The Application Is Still Running. Click Here For More Information", ToolTipIcon.Info);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InformationBox.Show("Please Confirm Application Exit", "Project Administrator", InformationBoxButtons.YesNo, InformationBoxIcon.Question).ToString() == "No")
            {

            }
            else
            {
                Application.Exit();
            }
        }

        private void othersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 changepassword = new Form7();
            changepassword.ShowDialog();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form8 aboutpage = new Form8();
            aboutpage.ShowDialog();
        }

        private void viewAllocationInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformationBox.Show("Function Not Currently Available. Please Contact Administrator For Update", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
            //Form9 viewhostelinfo = new Form9();
            //viewhostelinfo.MdiParent = this;
            //viewhostelinfo.Show();
        }

        private void changeRoomDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form1.Logininfo.username == "USER")
            {
                InformationBox.Show("Sorry You Do Not Have Sufficient Permission To Perform This Task!!", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
            }
            else
            {
                Form10 changeroom = new Form10();
                changeroom.MdiParent = this;
                changeroom.Show();
            }
        }
    }
}
