using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InfoBox;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Net;
using System.IO;
using System.Xml;


namespace Allocator
{
    public partial class Form7 : MaterialForm
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            //materialLabel1.Text = Form1.Logininfo.userid;
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (currentpassword.Text == "" || newpassword.Text == "" || confirmpassword.Text == "")
            {
                InformationBox.Show("Password Field(s) Cannot Empty", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                this.ActiveControl = currentpassword;
                currentpassword.Focus();
            }
            else
                if (currentpassword.Text == newpassword.Text)
                {
                    InformationBox.Show("Current Password and New Password Cannot Be Same", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                    this.ActiveControl = currentpassword;
                    currentpassword.Focus();
                }
                else
                    if (newpassword.Text != confirmpassword.Text)
                    {
                        InformationBox.Show("New Password and Confirm Password Do Not Match", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                        this.ActiveControl = currentpassword;
                        currentpassword.Focus();
                    }
                    else
                    {

                        if (InformationBox.Show("Please Verify That The Room Is Either Habitable Or Not. You Will Be Held Responsible If The Information Provided Is False.", "Project Administrator", InformationBoxButtons.YesNo, InformationBoxIcon.Question).ToString() == "No")
                        {

                        }
                        else
                        {
                            try
                            {
                                string staffid = Form1.Logininfo.userid;
                                string oldpassword = currentpassword.Text;
                                string newpass = newpassword.Text;
                                string url = "http://localhost/Allocator/cgi-bin/update_porter_password.php?staffid=" + staffid + "&oldpassword=" + oldpassword + "&newpassword=" + newpass + "";
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
                                    InformationBox.Show("Password Successfully Changed.", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                                    this.Hide();
                                }
                                else
                                {
                                    InformationBox.Show("Password Change Not Successful. Please Try Again Shortly.", "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Information);
                                    this.Hide();
                                }
                            }
                            catch (Exception exec)
                            {
                                InformationBox.Show(exec.Message, "Project Administrator", InformationBoxButtons.OK, InformationBoxIcon.Error);
                            }
                        }
                    }
        }
    }
}
