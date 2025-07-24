using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GooleMap.SDK.UI.Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            GMapsUtility map = new GMapsUtility();
            this.Controls.Add(map);
            map.CreateMarker(25.08042519244416, 121.52658741132015);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var place = textBox1.Text;

        }

    }
}
