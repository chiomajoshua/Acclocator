using System;
using MaterialSkin.Controls;
using InfoBox;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace Allocator
{
    public partial class Form10 : MaterialForm
    {
        string occupant, gender, block_id, room_id;
        string folderPath = "http://localhost/";
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }
        private void generateroom()
        {
            try
            {
                string matric = textBox1.Text.Trim();
                string url = "http://localhost/Allocator/cgi-bin/fetch_accurate_accommodation_details.php?matric=" + matric + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 90000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                string access = NodeList[0].FirstChild.ChildNodes[0].InnerText;
                if (access == "1")
                {
                    occupant = NodeList[0].ChildNodes[1].InnerText.Trim();
                    materialLabel11.Text = "Block Number: " + NodeList[0].ChildNodes[3].InnerText;
                    materialLabel1.Text = "Room Number: " + NodeList[0].ChildNodes[4].InnerText;
                    panel4.Visible = false;
                    panel1.Visible = true;
                }
                else
                {
                    string message = NodeList[0].ChildNodes[1].InnerText;
                    InformationBox.Show(message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                    panel4.Visible = true;
                    panel1.Visible = false;
                    textBox1.Clear();
                    this.ActiveControl = textBox1;
                    textBox1.Focus();
                }

            }
            catch (Exception ce)
            {
                InformationBox.Show(ce.Message);
                InformationBox.Show(ce.StackTrace);
            }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "http://localhost/Allocator/cgi-bin/update_room_information.php?roomid=" + room_id + "&occupant=" + occupant +"";
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
                    InformationBox.Show("Room Information Has Been Changed", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);       
                }
                else
                {
                    InformationBox.Show("Room Information Was Not Changed Successfully", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                }
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                InformationBox.Show("Please Enter Matric/JAMB Number To Continue", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                this.ActiveControl = textBox1;
                textBox1.Focus();
            }
            else
            {
                try
                {
                    string matric = textBox1.Text.Trim();
                    string url = "http://localhost/get_stud_profile.php?mat=" + matric + "&pwd=" + matric + "";
                    WebRequest requ = WebRequest.Create(url);
                    requ.Timeout = 90000;
                    WebResponse response = requ.GetResponse();
                    Stream stream = response.GetResponseStream();
                    XmlDocument CompSpecs = new XmlDocument();
                    CompSpecs.Load(stream);
                    XmlNodeList NodeList = CompSpecs.GetElementsByTagName("stud_profile"); // Create a list of the nodes in the xml file //
                    Matric.Text = "Mat. Num.: " + NodeList[0].FirstChild.ChildNodes[0].InnerText;
                    Surname.Text = NodeList[0].ChildNodes[1].InnerText;
                    Othernames.Text = NodeList[0].ChildNodes[2].InnerText.Trim() + " " + NodeList[0].ChildNodes[3].InnerText.Trim();
                    Sex.Text = NodeList[0].ChildNodes[4].InnerText.Trim();
                    if(Sex.Text == "F")
                    {
                        gender = "Female";
                    }
                    else
                    {
                        gender = "Male";
                    }
                   
                    Level.Text = NodeList[0].ChildNodes[8].InnerText;
                    // pictureBox1.Load(folderPath + NodeList[0].ChildNodes[20].InnerText.Trim());
                    Programme.Text = "Prog.: " + NodeList[0].ChildNodes[22].InnerText;
                    if (pictureBox1.Image == null && NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim() == "")
                    {
                        InformationBox.Show("Sorry, The Matric Number Does Not Exist. Please Confirm.", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                        panel4.Visible = true;
                        panel1.Visible = false;
                        textBox1.Clear();
                        this.ActiveControl = textBox1;
                        textBox1.Focus();
                    }
                    else
                    {
                        
                        if (NodeList[0].ChildNodes[20].InnerText.Trim() == "")
                        {
                            generateroom();
                            Loadcombo();
                        }
                        else
                        {
                            pictureBox1.Load(folderPath + NodeList[0].ChildNodes[20].InnerText.Trim());
                            generateroom();     
                            Loadcombo();
                        }

                    }
                }
                catch (Exception exec)
                {
                    InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                    InformationBox.Show(exec.StackTrace);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                string actname = comboBox1.Text;
                string url = "http://localhost/Allocator/cgi-bin/fetch_block_with_condition.php?gender=" + gender + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 90000;
                WebResponse responses = requ.GetResponse();
                Stream stream = responses.GetResponseStream();
                XDocument xmlDoc = XDocument.Load(stream);

                var response = from status in xmlDoc.Descendants("status")
                               where status.Element("Block").Value == comboBox1.SelectedItem.ToString()
                               select new
                               {
                                   Points = status.Element("ID").Value,
                               };

                foreach (var status in response)
                {
                    block_id = status.Points;
                    Loadroomcombo();
                }
            }
            catch (Exception ce)
            {
                InformationBox.Show(ce.Message);
                InformationBox.Show(ce.StackTrace);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string actname = comboBox1.Text;
                string url = "http://localhost/Allocator/cgi-bin/fetch_room_with_condition.php?blockid=" + block_id + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 90000;
                WebResponse responses = requ.GetResponse();
                Stream stream = responses.GetResponseStream();
                XDocument xmlDoc = XDocument.Load(stream);

                var response = from status in xmlDoc.Descendants("status")
                               where status.Element("Room").Value == comboBox2.SelectedItem.ToString()
                               select new
                               {
                                   Points = status.Element("ID").Value,
                               };

                foreach (var status in response)
                {
                    room_id = status.Points;
                }
            }
            catch (Exception ce)
            {
                InformationBox.Show(ce.Message);
                InformationBox.Show(ce.StackTrace);
            }
        }

        private void Loadcombo()
        {
            comboBox1.Items.Clear();
            try
            {
                string url = "http://localhost/Allocator/cgi-bin/fetch_block_with_condition.php?gender="+ gender +"";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 90000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                XmlNodeList nodeList = doc.SelectNodes("response/status");

                foreach (XmlNode node in nodeList)
                {
                    if (!comboBox1.Items.Contains(node.SelectSingleNode("Block").InnerText))
                        comboBox1.Items.Add(node.SelectSingleNode("Block").InnerText);
                }
            }
            catch (Exception ce)
            {
                InformationBox.Show(ce.Message);
                InformationBox.Show(ce.StackTrace);
            }
        }

        private void Loadroomcombo()
        {
            comboBox2.Items.Clear();
            try
            {
                string url = "http://localhost/Allocator/cgi-bin/fetch_room_with_condition.php?blockid=" + block_id + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 90000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                XmlNodeList nodeList = doc.SelectNodes("response/status");

                foreach (XmlNode node in nodeList)
                {
                    if (!comboBox2.Items.Contains(node.SelectSingleNode("Room").InnerText))
                        comboBox2.Items.Add(node.SelectSingleNode("Room").InnerText);
                }
            }
            catch (Exception ce)
            {
                InformationBox.Show(ce.Message);
                InformationBox.Show(ce.StackTrace);
            }
        }
    }
}
