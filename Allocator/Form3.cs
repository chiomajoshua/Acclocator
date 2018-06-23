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
    public partial class Form3 : MaterialForm
    {
        string folderPath = "http://reg.run.edu.ng/";
        string fatherid = "1";
        string motherid = "2";
        string sponsorid = "3";
        string sessionid;
        string studentid, email1, email2;
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            materialLabel33.Text = DateTime.Now.ToShortDateString();
            getsessionid();
        }
        private void getsessionid()
        {
            try
            {
                string url = "http://localhost/Allocator/cgi-bin/get_session_id.php?";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
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
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                InformationBox.Show(exec.StackTrace);
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
                fetchstudentid();
            }
        }
        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            materialRadioButton1.Checked = false;
            materialRadioButton2.Checked = false;
            clearsponsorfield();
            sponsortextfieldsenabled();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // charCountOutput.Text = richTextBox1.Length.ToString();
            int countChar = FatherAddress.Text.ToString().Length;
            materialLabel16.Text = countChar.ToString() + "/150";
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            int countChar = MotherAddress.Text.ToString().Length;
            materialLabel17.Text = countChar.ToString() + "/150";
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            int countChar = SponsorAddress.Text.ToString().Length;
            materialLabel24.Text = countChar.ToString() + "/150";
        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fathertosponsor();
        }

        private void fathertosponsor()
        {
            SponsorSurname.Text = FatherSurname.Text;
            SponsorOthernames.Text = FatherOthernames.Text;
            SponsorAddress.Text = FatherAddress.Text;
            SponsorPhone.Text = FatherPhone.Text;
            SponsorEmail.Text = FatherEmail.Text;
            SponsorOccupation.Text = FatherOccupation.Text;
            sponsortextfieldsdisabled();
        }
        private void mothertosponsor()
        {
            SponsorSurname.Text = MotherSurname.Text;
            SponsorOthernames.Text = MotherOthernames.Text;
            SponsorAddress.Text = MotherAddress.Text;
            SponsorPhone.Text = MotherPhone.Text;
            SponsorEmail.Text = MotherEmail.Text;
            SponsorOccupation.Text = MotherOccupation.Text;
            sponsortextfieldsdisabled();
        }

        private void sponsortextfieldsdisabled()
        {
            SponsorSurname.Enabled = false;
            SponsorOthernames.Enabled = false;
            SponsorAddress.Enabled = false;
            SponsorPhone.Enabled = false;
            SponsorEmail.Enabled = false;
            SponsorOccupation.Enabled = false;
        }

        private void sponsortextfieldsenabled()
        {
            SponsorSurname.Enabled = true;
            SponsorOthernames.Enabled = true;
            SponsorAddress.Enabled = true;
            SponsorPhone.Enabled = true;
            SponsorEmail.Enabled = true;
            SponsorOccupation.Enabled = true;
        }
        private void clearsponsorfield()
        {
            SponsorSurname.Clear();
            SponsorOthernames.Clear();
            SponsorAddress.Clear();
            SponsorPhone.Clear();
            SponsorEmail.Clear();
            SponsorOccupation.Clear();
        }

        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mothertosponsor();
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialCheckBox1.Checked == true)
            {
                materialFlatButton1.Enabled = true;
            }
            else
                if (materialCheckBox1.Checked == false)
                {
                    materialFlatButton1.Enabled = false;
                }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {           
            if (SponsorSurname.Text == "")
            {
                InformationBox.Show("Sponsor Surname Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
            }
            else
                if (SponsorOthernames.Text == "")
                {
                    InformationBox.Show("Sponsor Othernames Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                }
                else
                    if (SponsorAddress.Text == "")
                    {
                        InformationBox.Show("Sponsor Address Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                    }
                    else
                        if (SponsorAddress.Text == "")
                        {
                            InformationBox.Show("Sponsor Address Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                        }
                        else
                            if (SponsorPhone.Text == "")
                            {
                                InformationBox.Show("Sponsor Phone Number Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                            }
                            else
                                if (SponsorEmail.Text == "")
                                {
                                    InformationBox.Show("Sponsor Email Address Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                                }
                                else
                                    if (SponsorOccupation.Text == "")
                                    {
                                        InformationBox.Show("Sponsor Occupation Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                                    }
                                    else
                                    {
                                        InformationBox.Show("Information Successfully Submitted. Please Proceed To The Hostel To Get Your Room.", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Success);
                                        panel4.Visible = true;
                                        metroPanel1.Visible = false;
                                        clearinfo();
                                        textBox1.Clear();
                                        this.ActiveControl = textBox1;
                                        textBox1.Focus();
                                    }
           
        }

        private void clearinfo()
        {
            FatherSurname.Text = "";
            FatherOthernames.Text = "";
            FatherAddress.Text = "";
            FatherEmail.Text = "";
            FatherPhone.Text = "";
            FatherOccupation.Text = "";
            MotherSurname.Text = "";
            MotherOthernames.Text = "";
            MotherAddress.Text = "";
            MotherEmail.Text = "";
            MotherPhone.Text = "";
            MotherOccupation.Text = "";
            SponsorSurname.Text = "";
            SponsorOthernames.Text = "";
            SponsorAddress.Text = "";
            SponsorPhone.Text = "";
            SponsorEmail.Text = "";
            SponsorOccupation.Text = "";
            materialRadioButton1.Checked = false;
            materialRadioButton2.Checked = false;
            txtpersonaleffect.Text = "";
            materialCheckBox1.Checked = false;
            materialFlatButton1.Enabled = false;
            materialTabControl2.SelectedTab = tabPage2;
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            if (FatherSurname.Text == "" || FatherOthernames.Text == "" || FatherAddress.Text == "" || FatherPhone.Text == "" || FatherEmail.Text == "" || FatherOccupation.Text == "")
            {
                if (InformationBox.Show("Father Information Fields Are Empty. Please Confirm To Continue.", "Project Administrator", InformationBoxButtons.YesNo, InformationBoxIcon.Question).ToString() == "No")
                {

                }
                else
                {
                    submitfatherinfo();
                }
            }
            else
                if ((FatherSurname.Text != "" && FatherOthernames.Text == "") || (FatherSurname.Text != "" && FatherAddress.Text == "") || (FatherSurname.Text != "" && FatherPhone.Text == "") || (FatherSurname.Text != "" && FatherEmail.Text == "") || (FatherSurname.Text != "" && FatherOccupation.Text == ""))
                {
                    InformationBox.Show("Father Information Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                }
                else
                    if ((FatherOthernames.Text != "" && FatherSurname.Text == "") || (FatherOthernames.Text != "" && FatherAddress.Text == "") || (FatherOthernames.Text != "" && FatherPhone.Text == "") || (FatherOthernames.Text != "" && FatherEmail.Text == "") || (FatherOthernames.Text != "" && FatherOccupation.Text == ""))
                    {
                        InformationBox.Show("Father Information Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                    }
                    else
                        if ((FatherAddress.Text != "" && FatherSurname.Text == "") || (FatherAddress.Text != "" && FatherOthernames.Text == "") || (FatherAddress.Text != "" && FatherPhone.Text == "") || (FatherAddress.Text != "" && FatherEmail.Text == "") || (FatherAddress.Text != "" && FatherOccupation.Text == ""))
                        {
                            InformationBox.Show("Father Information Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                        }
                        else
                            if ((FatherPhone.Text != "" && FatherSurname.Text == "") || (FatherPhone.Text != "" && FatherOthernames.Text == "") || (FatherPhone.Text != "" && FatherAddress.Text == "") || (FatherPhone.Text != "" && FatherEmail.Text == "") || (FatherPhone.Text != "" && FatherOccupation.Text == ""))
                            {
                                InformationBox.Show("Father Information Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                            }
                            else
                                if ((FatherEmail.Text != "" && FatherSurname.Text == "") || (FatherEmail.Text != "" && FatherOthernames.Text == "") || (FatherEmail.Text != "" && FatherAddress.Text == "") || (FatherEmail.Text != "" && FatherPhone.Text == "") || (FatherEmail.Text != "" && FatherOccupation.Text == ""))
                                {
                                    InformationBox.Show("Father Information Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                                }
                                else
                                {
                                    submitfatherinfo();
                                }
        

        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            if (MotherSurname.Text == "" || MotherOthernames.Text == "" || MotherAddress.Text == "" || MotherPhone.Text == "" || MotherEmail.Text == "" || MotherOccupation.Text == "")
            {
                if (InformationBox.Show("Mother Information Fields Are Empty. Please Confirm To Continue.", "Project Administrator", InformationBoxButtons.YesNo, InformationBoxIcon.Question).ToString() == "No")
                {

                }
                else
                {
                    submitmotherinfo();
                }
            }

            else
                if ((MotherSurname.Text != "" && MotherOthernames.Text == "") || (MotherSurname.Text != "" && MotherAddress.Text == "") || (MotherSurname.Text != "" && MotherPhone.Text == "") || (MotherSurname.Text != "" && MotherEmail.Text == "") || (MotherSurname.Text != "" && MotherOccupation.Text == ""))
                {
                    InformationBox.Show("Mother Information Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                }
                else
                    if ((MotherOthernames.Text != "" && MotherSurname.Text == "") || (MotherOthernames.Text != "" && MotherAddress.Text == "") || (MotherOthernames.Text != "" && MotherPhone.Text == "") || (MotherOthernames.Text != "" && MotherEmail.Text == "") || (MotherOthernames.Text != "" && MotherOccupation.Text == ""))
                    {
                        InformationBox.Show("Mother Information Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                    }
                    else
                        if ((MotherAddress.Text != "" && MotherSurname.Text == "") || (MotherAddress.Text != "" && MotherOthernames.Text == "") || (MotherAddress.Text != "" && MotherPhone.Text == "") || (MotherAddress.Text != "" && MotherEmail.Text == "") || (MotherAddress.Text != "" && MotherOccupation.Text == ""))
                        {
                            InformationBox.Show("Mother Information Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                        }
                        else
                            if ((MotherPhone.Text != "" && MotherSurname.Text == "") || (MotherPhone.Text != "" && MotherOthernames.Text == "") || (MotherPhone.Text != "" && MotherAddress.Text == "") || (MotherPhone.Text != "" && MotherEmail.Text == "") || (MotherPhone.Text != "" && MotherOccupation.Text == ""))
                            {
                                InformationBox.Show("Mother Information Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                            }
                            else
                                if ((MotherEmail.Text != "" && MotherSurname.Text == "") || (MotherEmail.Text != "" && MotherOthernames.Text == "") || (MotherEmail.Text != "" && MotherAddress.Text == "") || (MotherEmail.Text != "" && MotherPhone.Text == "") || (MotherEmail.Text != "" && MotherOccupation.Text == ""))
                                {
                                    InformationBox.Show("Mother Information Missing", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                                }
                                else
                                {
                                    submitmotherinfo();
                                }
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            if (materialRadioButton1.Checked == false || materialCheckBox2.Checked == false)
            {
                if (SponsorSurname.Text == "")
                {
                    InformationBox.Show("Sponsor Surname Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                }
                else
                    if (SponsorOthernames.Text == "")
                    {
                        InformationBox.Show("Sponsor Othernames Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                    }
                    else
                        if (SponsorAddress.Text == "")
                        {
                            InformationBox.Show("Sponsor Address Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                        }
                        else
                            if (SponsorAddress.Text == "")
                            {
                                InformationBox.Show("Sponsor Address Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                            }
                            else
                                if (SponsorPhone.Text == "")
                                {
                                    InformationBox.Show("Sponsor Phone Number Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                                }
                                else
                                    if (SponsorEmail.Text == "")
                                    {
                                        InformationBox.Show("Sponsor Email Address Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                                    }
                                    else
                                        if (SponsorOccupation.Text == "")
                                        {
                                            InformationBox.Show("Sponsor Occupation Is Missing", "Project Administrator", InformationBoxIcon.Information, InformationBoxButtons.OK);
                                        }
                                        else
                                        {
                                            if (InformationBox.Show("Please Confirm To Continue.", "Project Administrator", InformationBoxButtons.YesNo, InformationBoxIcon.Question).ToString() == "No")
                                            {

                                            }
                                            else
                                            {
                                                submitsponsorinfo();
                                            }
                                        }
            }
            else
            {
                submitsponsorinfo();
            }
        }

        private void materialFlatButton5_Click(object sender, EventArgs e)
        {
            if (materialCheckBox2.Checked == true && txtpersonaleffect.Text == "")
            {
                InformationBox.Show("Personal Effects Cannot Be Empty", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
            }
            else
            {
                submitpersonaleffects();
            }
        }

        private void materialFlatButton6_Click(object sender, EventArgs e)
        {
            materialTabControl2.SelectedTab = tabPage2;
        }

        private void materialFlatButton7_Click(object sender, EventArgs e)
        {
            materialTabControl2.SelectedTab = tabPage3;
        }

        private void materialFlatButton8_Click(object sender, EventArgs e)
        {
            materialTabControl2.SelectedTab = tabPage4;
        }

        private void materialFlatButton9_Click(object sender, EventArgs e)
        {
            materialTabControl2.SelectedTab = tabPage5;
        }

        private void fetchstudentrecord()
        {
            string matric = textBox1.Text.Trim();
            string urld = "http://localhost/get_stud_profile.php?mat=" + matric + "&pwd=" + matric + "";
            WebRequest request = WebRequest.Create(urld);
            request.Timeout = 40000;
            WebResponse responses = request.GetResponse();
            Stream streams = responses.GetResponseStream();
            XmlDocument CompsSpecs = new XmlDocument();
            CompsSpecs.Load(streams);
            XmlNodeList NodeLists = CompsSpecs.GetElementsByTagName("stud_profile"); // Create a list of the nodes in the xml file //
            materialLabel7.Text = NodeLists[0].FirstChild.ChildNodes[0].InnerText;
            materialLabel1.Text = "Surname: " + NodeLists[0].ChildNodes[1].InnerText;
            materialLabel2.Text = "Othernames: " + NodeLists[0].ChildNodes[2].InnerText.Trim() + " " + NodeLists[0].ChildNodes[3].InnerText.Trim();
            materialLabel34.Text = NodeLists[0].ChildNodes[4].InnerText;
            materialLabel35.Text = NodeLists[0].ChildNodes[8].InnerText;
            email1 = NodeLists[0].ChildNodes[16].InnerText.Trim();
            email2 = NodeLists[0].ChildNodes[17].InnerText.Trim();           
            materialLabel3.Text = "Programme: " + NodeLists[0].ChildNodes[22].InnerText;
            if (pictureBox1.Image == null && materialLabel7.Text == "")
            {
                InformationBox.Show("Sorry, The Matric Number Does Not Exist. Please Confirm.", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                panel4.Visible = true;
                metroPanel1.Visible = false;
                textBox1.Clear();
                this.ActiveControl = textBox1;
                textBox1.Focus();
            }
            else
            {
                InformationBox.Show("By going further, you accept that the information entered is correct and valid. If by any chance the information provided is false, you accept to be called to face the disciplinary panel and punished for impersonation and provision of wrong information.", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Exclamation);
                if (NodeLists[0].ChildNodes[20].InnerText.Trim() == "")
                {
                    fetch_father_info();
                    panel4.Visible = false;
                    metroPanel1.Visible = true;
                }
                else
                {
                    pictureBox1.Load(folderPath + NodeLists[0].ChildNodes[20].InnerText.Trim());
                    fetch_father_info();
                    panel4.Visible = false;
                    metroPanel1.Visible = true;
                }                              
           }
        }

        private void fetchstudentid()
        {
            try
            {
                string matricnumber = textBox1.Text.Trim();
                string url = "http://localhost/Allocator/cgi-bin/fetch_student_id.php?matric="+ matricnumber +"";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                string confirmation = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();
               studentid = NodeList[0].ChildNodes[1].InnerText.Trim(); 
               if (confirmation == "1")
                {
                    fetchstudentrecord();    
                }
                else
                {
                    InformationBox.Show("Sorry You Cannot Fill The Hostel Allocation Form At The Moment. Please Pay Your Student Association Fees.", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);

                }
               
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                InformationBox.Show(exec.StackTrace);
            }
        }

        private void submitfatherinfo()
        {
            try
            {
                string surname = FatherSurname.Text.Trim();
                string othernames = FatherOthernames.Text;
                string address = FatherAddress.Text;
                string phone = FatherPhone.Text;
                string email = FatherEmail.Text;
                string occupation = FatherOccupation.Text;
                string url = "http://localhost/Allocator/cgi-bin/insert_kin_info.php?occupantid="+ studentid+"&kintype="+ fatherid +"&firstname="+ othernames +"&lastname="+ surname +"&address="+ address +"&phone="+ phone +"&email="+ email +"&occupation="+ occupation+ "&sessionid="+ sessionid +"";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                fetch_mother_info();
                materialTabControl2.SelectedTab = tabPage3;
                
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                InformationBox.Show(exec.StackTrace);
            }
        }

        private void submitmotherinfo()
        {
            try
                {
                    string surname = MotherSurname.Text.Trim();
                    string othernames = MotherOthernames.Text;
                    string address = MotherAddress.Text;
                    string phone = MotherPhone.Text;
                    string email = MotherEmail.Text;
                    string occupation = MotherOccupation.Text;
                    string url = "http://localhost/Allocator/cgi-bin/insert_kin_info.php?occupantid=" + studentid + "&kintype=" + motherid + "&firstname=" + othernames + "&lastname=" + surname + "&address=" + address + "&phone=" + phone + "&email=" + email + "&occupation=" + occupation + "&sessionid=" + sessionid + "";
                    WebRequest requ = WebRequest.Create(url);
                    requ.Timeout = 40000;
                    WebResponse response = requ.GetResponse();
                    Stream stream = response.GetResponseStream();
                    XmlDocument CompSpecs = new XmlDocument();
                    CompSpecs.Load(stream);
                    //Load the data from the file into the XmlDocument (CompSpecs) //
                    XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                    // Create a list of the nodes in the xml file //
                    fetch_sponsor_info();
                    materialTabControl2.SelectedTab = tabPage4;
                    
                }
                catch (Exception exec)
                {
                    InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                    InformationBox.Show(exec.StackTrace);
                }
        
        }

        private void submitsponsorinfo()
        {
            try
            {
                string surname = SponsorSurname.Text.Trim();
                string othernames = SponsorOthernames.Text;
                string address = SponsorAddress.Text;
                string phone = SponsorPhone.Text;
                string email = SponsorEmail.Text;
                string occupation = SponsorOccupation.Text;
                string url = "http://localhost/Allocator/cgi-bin/insert_kin_info.php?occupantid=" + studentid + "&kintype=" + sponsorid + "&firstname=" + othernames + "&lastname=" + surname + "&address=" + address + "&phone=" + phone + "&email=" + email + "&occupation=" + occupation + "&sessionid=" + sessionid + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                fetch_personal_effects();
                materialTabControl2.SelectedTab = tabPage5;
                
               
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                InformationBox.Show(exec.StackTrace);
            }
        }

        private void submitpersonaleffects()
        {
            try
            {
                string description = txtpersonaleffect.Text;
                string url = "http://localhost/Allocator/cgi-bin/insert_personal_effects.php?occupantid="+ studentid +"&description="+ description +"&sessionid="+ sessionid +"";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                materialTabControl2.SelectedTab = tabPage6;
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                InformationBox.Show(exec.StackTrace);
            }
        }

        private void FatherPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void MotherPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SponsorPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void fetch_father_info()
        {
            try
            {
                string url = "http://localhost/Allocator/cgi-bin/fetch_father_info.php?occupantid="+ studentid +"&kintype="+ fatherid +"&sessionid="+ sessionid +"";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                string access = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();

                if (access == "1")
                {
                    FatherOthernames.Text = NodeList[0].ChildNodes[1].InnerText;
                    FatherSurname.Text = NodeList[0].ChildNodes[2].InnerText;
                    FatherAddress.Text = NodeList[0].ChildNodes[3].InnerText;
                    FatherPhone.Text = NodeList[0].ChildNodes[4].InnerText;
                    FatherEmail.Text = NodeList[0].ChildNodes[5].InnerText;
                    FatherOccupation.Text = NodeList[0].ChildNodes[6].InnerText;
                }
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                InformationBox.Show(exec.StackTrace);
            }
        }

        private void fetch_mother_info()
        {
            try
            {
                string url = "http://localhost/Allocator/cgi-bin/fetch_father_info.php?occupantid=" + studentid + "&kintype=" + motherid + "&sessionid=" + sessionid + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                string access = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();

                if (access == "1")
                {
                    MotherOthernames.Text = NodeList[0].ChildNodes[1].InnerText;
                    MotherSurname.Text = NodeList[0].ChildNodes[2].InnerText;
                    MotherAddress.Text = NodeList[0].ChildNodes[3].InnerText;
                    MotherPhone.Text = NodeList[0].ChildNodes[4].InnerText;
                    MotherEmail.Text = NodeList[0].ChildNodes[5].InnerText;
                    MotherOccupation.Text = NodeList[0].ChildNodes[6].InnerText;
                }
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                InformationBox.Show(exec.StackTrace);
            }
        }

        private void fetch_sponsor_info()
        {
            try
            {
                string url = "http://localhost/Allocator/cgi-bin/fetch_father_info.php?occupantid=" + studentid + "&kintype=" + sponsorid + "&sessionid=" + sessionid + "";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                string access = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();

                if (access == "1")
                {
                    SponsorOthernames.Text = NodeList[0].ChildNodes[1].InnerText;
                    SponsorSurname.Text = NodeList[0].ChildNodes[2].InnerText;
                    SponsorAddress.Text = NodeList[0].ChildNodes[3].InnerText;
                    SponsorPhone.Text = NodeList[0].ChildNodes[4].InnerText;
                    SponsorEmail.Text = NodeList[0].ChildNodes[5].InnerText;
                    SponsorOccupation.Text = NodeList[0].ChildNodes[6].InnerText;
                }
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                InformationBox.Show(exec.StackTrace);
            }
        }

        private void fetch_personal_effects()
        {
            try
            {
                string url = "http://localhost/Allocator/cgi-bin/fetch_personal_effects.php?occupantid="+ studentid +"&sessionid="+ sessionid+"";
                WebRequest requ = WebRequest.Create(url);
                requ.Timeout = 40000;
                WebResponse response = requ.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument CompSpecs = new XmlDocument();
                CompSpecs.Load(stream);
                //Load the data from the file into the XmlDocument (CompSpecs) //
                XmlNodeList NodeList = CompSpecs.GetElementsByTagName("response");
                // Create a list of the nodes in the xml file //
                string access = NodeList[0].FirstChild.ChildNodes[0].InnerText.Trim();

                if (access == "1")
                {
                    txtpersonaleffect.Text = NodeList[0].ChildNodes[1].InnerText;
                }
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                InformationBox.Show(exec.StackTrace);
            }
        }
        /*
        private void makepayment()
        {
            try
            {
                string matric = textBox1.Text.Trim();
                string amount = "3000";
                string officialid = "1";
                string url = "http://localhost/Allocator/cgi-bin/pay_runsa_fee.php?matric=" + matric + "&sessionid=" + sessionid + "&amount= " + amount + "&officialid=" + officialid + "&level=" + studentlevel + "";
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
                    fetchstudentid();
                }
                else
                {
                    InformationBox.Show("Clearance Was Not Successful. Please Try Again Shortly", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                    Form2 homepage = new Form2();
                    homepage.Show();
                    this.Hide();
                }
            }
            catch (Exception exec)
            {
                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
            }
        }
         */
    }
}
