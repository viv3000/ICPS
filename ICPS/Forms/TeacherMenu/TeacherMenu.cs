using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ICPS.Forms
{
	public partial class TeacherMenu : Form
	{
		public TeacherMenu()
		{
			InitializeComponent();

			ToolStripMenuItem tablesItem = new ToolStripMenuItem("Таблицы");

			ToolStripMenuItem classesItem      = new ToolStripMenuItem("Классы");
			ToolStripMenuItem attestationsItem = new ToolStripMenuItem("Работы");
			ToolStripMenuItem resultsItem      = new ToolStripMenuItem("Результаты");

			classesItem.Click      += classesItem_Click;
			attestationsItem.Click += attestationsItem_Click;
			resultsItem.Click      += resultsItem_Click;

			tablesItem.DropDownItems.Add(classesItem);
			tablesItem.DropDownItems.Add(attestationsItem);
			tablesItem.DropDownItems.Add(resultsItem);

			this.menuStrip1.Items.Add(tablesItem);
		}

		public void classesItem_Click(object sender, EventArgs e)
		{
			new Form().ShowDialog();
		}
		public void attestationsItem_Click(object sender, EventArgs e)
		{
			new AttestationsCRUD().Show();
		}
		public void resultsItem_Click(object sender, EventArgs e)
		{
			new Form().ShowDialog();
		}
	}
}
