using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace ReactionTracker
{

    public enum reqType {inputItems, outputItems};


    public partial class MainForm : Form
    {

        List<Reaction> dataGrid = new List<Reaction>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reaction reac = new Reaction(textBox3.Text, textBox1.Text, textBox2.Text);
            dgvMain.Rows.Add(reac.reactionName, reac.GetData(reqType.inputItems), reac.GetData(reqType.outputItems), reac.GetProfit());
            dataGrid.Add(reac);
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dgvMain.Rows.Clear();
            foreach (Reaction o in dataGrid)
            {
                string inputData = String.Format("{0:C}", o.GetData(reqType.inputItems));
                string outputData = String.Format("{0:C}", o.GetData(reqType.outputItems));
                dgvMain.Rows.Add(o.reactionName, inputData ,outputData , o.GetProfit());
            }

            MessageBox.Show("Refreshed!");
        }
    } 
}

