using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XlsxViewer
{
	public partial class frmXlsxViewer : Form
	{
		public frmXlsxViewer()
		{
			InitializeComponent();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			lblClock.Text = DateTime.Now.ToString();
		}

		private void frmXlsxViewer_FormClosed(object sender, FormClosedEventArgs e)
		{
			timer1.Enabled = false;
		}

		private void frmXlsxViewer_Load(object sender, EventArgs e)
		{
			timer1.Enabled = true;
		}

		private void mnOpen_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
		}
	}
}
