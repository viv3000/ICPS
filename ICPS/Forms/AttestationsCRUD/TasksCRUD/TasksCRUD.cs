using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICPS.DB.CRUD;

namespace ICPS.Forms
{
	public partial class TasksCRUD : Form
	{
		private int attestationId;
		public TasksCRUD(int attestationId)
		{
			this.attestationId = attestationId;
			InitializeComponent();
		}
		public void create_Click(object sender, EventArgs e)
		{
			new TasksCreate(this, attestationId).ShowDialog();
		}
		public void update_Click(object sender, EventArgs e)
		{
			new TasksUpdate(this, attestationId, new TasksChoose(this, attestationId).ShowChooseDialog()).ShowDialog();
		}
		public void delete_Click(object sender, EventArgs e)
		{
			new TasksDelete(this, attestationId).ShowDialog();
		}
		public void exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		public void TasksCRUD_Load(object sender, EventArgs e)
		{
			this.Update();
		}
		public void Update()
		{
			this.dataGrid1.DataSource = Tasks.Read(attestationId);
		}
	}
}
