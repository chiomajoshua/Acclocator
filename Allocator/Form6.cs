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
    public partial class Form6 : MaterialForm
    {
        string folderPath = "http://reg.run.edu.ng/";
        string level;
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void generateroom()
        {
            try
            {
                string matric = textBox1.Text.Trim();
                string hostelid = Form1.Logininfo.hostelid.Trim();
                string url = "http://dssd.run.edu.ng/Allocator/cgi-bin/random_room_allocator.php?matric="+ matric +"&level="+ level +"&hostelid="+ hostelid +"";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 30000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                string access = NodeList[0].FirstChild.ChildNodes[0].InnerText;
                if (access == "1")
                {
                    materialLabel11.Text = "Block Number: " + NodeList[0].ChildNodes[2].InnerText;
                    materialLabel8.Text = "Room Number: " + NodeList[0].ChildNodes[3].InnerText;
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
                    string url = "http://reg.run.edu.ng/get_stud_profile.php?mat=" + matric + "&pwd=" + matric + "";
                    WebRequest requ = WebRequest.Create(url);
                    requ.Timeout = 30000;
                    WebResponse response = requ.GetResponse();
                    Stream stream = response.GetResponseStream();
                    XmlDocument CompSpecs = new XmlDocument();
                    CompSpecs.Load(stream);
                    XmlNodeList NodeList = CompSpecs.GetElementsByTagName("stud_profile"); // Create a list of the nodes in the xml file //
                    materialLabel6.Text = "Matric Number: " + NodeList[0].FirstChild.ChildNodes[0].InnerText;
                    materialLabel1.Text = "Surname: " + NodeList[0].ChildNodes[1].InnerText;
                    materialLabel2.Text = "Othernames: " + NodeList[0].ChildNodes[2].InnerText.Trim() + " " + NodeList[0].ChildNodes[3].InnerText.Trim();
                    materialLabel4.Text = "Sex: " + NodeList[0].ChildNodes[4].InnerText;
                    materialLabel5.Text = "Level: " + NodeList[0].ChildNodes[8].InnerText;
                    level = NodeList[0].ChildNodes[8].InnerText.Trim();
                    materialLabel7.Text = "Please Note That An Email Has Been Sent To: " + NodeList[0].ChildNodes[16].InnerText.Trim();
                   // pictureBox1.Load(folderPath + NodeList[0].ChildNodes[20].InnerText.Trim());
                    materialLabel3.Text = "Programme: " + NodeList[0].ChildNodes[22].InnerText;
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
                        }
                        else
                        {
                            pictureBox1.Load(folderPath + NodeList[0].ChildNodes[20].InnerText.Trim());
                            generateroom();
                        }
                                           
                    }
                }
                catch (Exception exec)
                {
                    InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                }
            }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel1.Visible = false;
            textBox1.Clear();
            this.ActiveControl = textBox1;
            textBox1.Focus();
        }
    }
}
