using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace XlsxViewer
{
	public partial class frmXlsxViewer : Form
	{
		private DataSet _dsBook;
		private DataSet dsBook { get { return _dsBook; } set { _dsBook = value; } }
		private BindingList<string> SheetNames = new BindingList<string>();

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
			var result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				ChangeFilePath(openFileDialog1.FileName);
			}
		}
		private void ChangeFilePath(string filepath)
		{
			lblFilepath.Text = null;
			this.Text = Path.GetFileName(filepath) + " - XlsxViewer -";
			using(var fs = new FileStream(filepath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite ))
			using (Excel.IExcelDataReader datareader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs))
			{
				dsBook = datareader.AsDataSet();
			}
			lbSheetNames.DataSource = null;
			SheetNames.Clear();
			foreach(DataTable dt in dsBook.Tables)
				SheetNames.Add(dt.TableName);
			lbSheetNames.DataSource = SheetNames;
			lblFilepath.Text = filepath;
		}

		private void lbSheetNames_SelectedValueChanged(object sender, EventArgs e)
		{
			var lb = (ListBox)sender;
			dataGridView1.DataSource = dsBook.Tables[(string)lb.SelectedItem];
		}

		private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
		{
			var dgv = (DataGridView)sender;
			int i = 0;
			foreach (DataGridViewColumn col in dgv.Columns)
			{
				col.HeaderText = (i++).ToString();
			}
			dgv.ReadOnly = true;
		}

		private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;
			if (dgv.RowHeadersVisible)
			{
				//行番号を描画する範囲を決定する
				Rectangle rect = new Rectangle(
					e.RowBounds.Left, e.RowBounds.Top,
					dgv.RowHeadersWidth, e.RowBounds.Height);
				rect.Inflate(-2, -2);
				//行番号を描画する
				TextRenderer.DrawText(e.Graphics,
					(e.RowIndex).ToString(),
					e.InheritedRowStyle.Font,
					rect,
					e.InheritedRowStyle.ForeColor,
					TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
			}
		}
	}
}
