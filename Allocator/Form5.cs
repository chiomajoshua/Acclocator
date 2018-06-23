using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using InfoBox;
using System.Net;
using System.IO;
using System.Xml;
using System.Net.Mail;
using System.Web;
using System.Xml.Linq;

namespace Allocator
{
    public partial class Form5 : MetroForm
    {
        string habitable, access, hostelid, genderid, blockid;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            fetch_supervisor_Room_Clearance();
            metroload();
            loadcombo();
        }
        private void loadcombo()
        {
            comboBox1.Items.Clear();
            string porterid = Form1.Logininfo.userid;
            try
            {
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/fetch_supervisor_room_block.php?porterid="+ porterid +"";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                XmlNodeList nodeList = doc.SelectNodes("response/status");

                foreach (XmlNode node in nodeList)
                    if (!comboBox1.Items.Contains(node.SelectSingleNode("BlockNum").InnerText))
                        comboBox1.Items.Add(node.SelectSingleNode("BlockNum").InnerText);
            }
            catch (Exception ce)
            {
                InformationBox.Show(ce.Message);
            }
        }
        private void metroload()
        {
            metroTabControl1.SelectedTab = metroTabPage1;
            metroTile1.BackColor = Color.Maroon;
            metroTile2.BackColor = Color.Teal;
            metroTile3.BackColor = Color.Teal;
            habitable = "1";
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            metroTabControl1.SelectedTab = metroTabPage1;
            metroTile1.BackColor = Color.Maroon;
            metroTile2.BackColor = Color.Teal;
            metroTile3.BackColor = Color.Teal;
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            metroTabControl1.SelectedTab = metroTabPage2;
            metroTile2.BackColor = Color.Maroon;
            metroTile1.BackColor = Color.Teal;
            metroTile3.BackColor = Color.Teal;
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            metroTabControl1.SelectedTab = metroTabPage3;
            metroTile3.BackColor = Color.Maroon;
            metroTile1.BackColor = Color.Teal;
            metroTile2.BackColor = Color.Teal;
        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if(metroToggle1.CheckState == CheckState.Checked)
            {
                habitable = "1";
            }
            else
                 if (metroToggle1.CheckState == CheckState.Unchecked)
            {
                habitable = "0";
            }
        }

        private void fetch_supervisor_Room_Clearance()
        {
            try
            {
                string porterid = Form1.Logininfo.userid;
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/fetch_supervisor_Room_Clearance.php?porterid="+ porterid +"";
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
                    hostelid = NodeList[0].ChildNodes[1].InnerText.Trim();
                    genderid = NodeList[0].ChildNodes[2].InnerText.Trim();
                    if (genderid == "1")
                    {
                        materialLabel4.Text = "Hostel Type: Boy's Hostel";
                    }
                    else
                    {
                        materialLabel4.Text = "Hostel Type: Girl's Hostel";
                    }
                    materialLabel1.Text = "Hostel Name: " + NodeList[0].ChildNodes[3].InnerText.Trim();
                    materialLabel3.Text = "Supervisor: " + NodeList[0].ChildNodes[4].InnerText.Trim();
                }
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                //InformationBox.Show(exec.StackTrace);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string porterid = Form1.Logininfo.userid;
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/fetch_supervisor_room_block.php?porterid=" + porterid + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse responses = requ.GetResponse();
                Stream stream = responses.GetResponseStream();
                XDocument xmlDoc = XDocument.Load(stream);

                var response = from status in xmlDoc.Descendants("status")
                               where status.Element("BlockNum").Value == comboBox1.SelectedItem.ToString()
                               select new
                               {
                                   Points = status.Element("BlockID").Value,
                               };

                foreach (var status in response)
                {
                    blockid = status.Points;
                    loadroomcombo();
                }
            }
            catch (Exception ce)
            {
                InformationBox.Show(ce.Message);

            }
        }
        private void loadroomcombo()
        {
            comboBox2.Items.Clear();
            try
            {
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/fetch_room_details.php?blockid=" + blockid + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                XmlNodeList nodeList = doc.SelectNodes("response/status");

                foreach (XmlNode node in nodeList)
                    if (!comboBox2.Items.Contains(node.SelectSingleNode("Room").InnerText))
                        comboBox2.Items.Add(node.SelectSingleNode("Room").InnerText);
            }
            catch (Exception ce)
            {
                InformationBox.Show(ce.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                InformationBox.Show("Please Select Block Number.", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
            }
            else
                if (comboBox2.Text == "")
                {
                    InformationBox.Show("Please Select Room Number.", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                }
                else
                {
                    if (InformationBox.Show("Please Verify That The Room Is Either Habitable Or Not. You Will Be Held Responsible If The Information Provided Is False.", "Project Administrator", InformationBoxButtons.YesNo, InformationBoxIcon.Question).ToString() == "No")
                    {

                    }
                    else
                    {
                        submithabitableinformation();
                    }
                }
        }
        private void submithabitableinformation()
        {
            try
            {
                string roomnum = comboBox2.Text.Trim();
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/update_room_habitable.php?blockid="+ blockid +"&roomnum="+ roomnum +"&habitable="+ habitable +"";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                string confirmation = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim(); ;
                if (confirmation == "1")
                {
                    InformationBox.Show("The Information Has Been Successfully Submitted", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                    metroload();
                    loadcombo();
                    metroToggle1.CheckState = CheckState.Checked;
                }
                else
                {
                    InformationBox.Show("The Information Was Not Submitted Successfully", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                }
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
            }
        }
    }
}
