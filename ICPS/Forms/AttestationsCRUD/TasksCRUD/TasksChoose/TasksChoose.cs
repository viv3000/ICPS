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
	public partial class TasksChoose : Form
	{
		private TasksCRUD superForm;
		private int attestationsId;
		public TasksChoose(TasksCRUD superForm, int attestationsId)
		{
			this.superForm = superForm;
			this.attestationsId = attestationsId;
			InitializeComponent();
		}
		public void choose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		public int ShowChooseDialog()
		{
			this.ShowDialog();
			return Int32.Parse(comboBox1.SelectedValue.ToString());
		}
		public void exit_Click(object sender, EventArgs e)
		{
			superForm.Update();
			this.Close();
		}
		public void AttestationsChoose_Load(object sender, EventArgs e)
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
