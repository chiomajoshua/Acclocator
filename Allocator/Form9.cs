using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Controls;
using InfoBox;
using System.Net;
using System.IO;
using System.Xml;
using System.Net.Mail;

namespace Allocator
{
    public partial class Form9 : MaterialForm
    {
        string hostelid, blockid;
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            hostelid = Form1.Logininfo.hostelid;
            loadcombo();
            loadbadrooms();
            loadpopulation();
            loadoccupiedrooms();
            loadunoccupiedrooms();
        }

        private void loadcombo()
        {
            comboBox1.Items.Clear();
            try
            {
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/fetch_block_details.php?hostelid=" + hostelid + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                XmlNodeList nodeList = doc.SelectNodes("response/status");

                foreach (XmlNode node in nodeList)
                    if (!comboBox1.Items.Contains(node.SelectSingleNode("Block").InnerText))
                        comboBox1.Items.Add(node.SelectSingleNode("Block").InnerText);
            }
            catch (Exception ce)
            {
                InformationBox.Show(ce.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string blocknum = comboBox1.Text.Trim();
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/fetch_block_id.php?hostelid=" + hostelid + "&blocknum=" + blocknum + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
               // materialLabel6.Text = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
                blockid = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                InformationBox.Show(exec.StackTrace);
            }     
        }
        private void loadbadrooms()
        {
            try
            {
                //string blocknum = comboBox1.Text.Trim();
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/count_habitable_rooms.php?hostelid=" + hostelid + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                // materialLabel6.Text = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
                materialLabel3.Text = "No. Of Bad Rooms: " + NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
            }    
        }
   
        private void loadpopulation()
        {
            try
            {
                //string blocknum = comboBox1.Text.Trim();
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/count_population_per_hostel.php?hostelid=" + hostelid + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                // materialLabel6.Text = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
                materialLabel2.Text = "Population: " + NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
            } 
        }
        //No. Of Occupied Rooms:
        private void loadoccupiedrooms()
        {
            try
            {
                //string blocknum = comboBox1.Text.Trim();
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/count_occupied_rooms.php?hostelid=" + hostelid + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                // materialLabel6.Text = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
                materialLabel1.Text = "No. Of Occupied Rooms: " + NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
            } 
        }
        private void loadunoccupiedrooms()
        {
            try
            {
                //string blocknum = comboBox1.Text.Trim();
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/count_occupied_rooms.php?hostelid=" + hostelid + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                // materialLabel6.Text = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
                materialLabel4.Text = "No. Of Unoccupied Rooms: " + NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
            } 
        }
    }
}
