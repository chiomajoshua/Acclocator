using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Allocator
{
    public partial class Startpage : Form
    {
        string progresstext;
        public Startpage()
        {
            InitializeComponent();
        }

        private void Startpage_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(20);
            progresstext = progressBar1.Value.ToString();
            if (progresstext == "100")
            {
                timer1.Stop();
                Form1 homepage = new Form1();
                homepage.Show();
                this.Hide();
            }
        }
    }
}
