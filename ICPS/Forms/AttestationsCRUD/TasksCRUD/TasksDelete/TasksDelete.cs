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
	public partial class TasksDelete : Form
	{
		private TasksCRUD superForm;
		private int attestationsId;

		public TasksDelete(TasksCRUD superForm, int attestationsId)
		{
			this.superForm = superForm;
			this.attestationsId = attestationsId;
			InitializeComponent();
		}

		public void delete_Click(object sender, EventArgs e)
		{
			Tasks.Delete(Int32.Parse(comboBox1.SelectedValue.ToString()));
			superForm.Update();
			this.Close();
		}

		public void exit_Click(object sender, EventArgs e)
		{
			superForm.Update();
			this.Close();
		}
		public void TasksDelete_Load(object sender, EventArgs e)
		{
			this.Update();
		}
		public void Update()
		{
			comboBox1.DataSource = Tasks.ReadToList(attestationsId);
			comboBox1.DisplayMember = "Title";
			comboBox1.ValueMember = "Id";
		}
	}
}
