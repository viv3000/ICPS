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
	public partial class TasksCreate : Form
	{
		private TasksCRUD superForm;
		private int attestationId;

		public TasksCreate(TasksCRUD superForm, int attestationId)
		{
			this.superForm = superForm;
			this.attestationId = attestationId;
			InitializeComponent();
		} 

		public void create_Click(object sender, EventArgs e)
		{
			if (IsCorrectInput()){
				Tasks.Create(
					attestationId, textBox2.Text, textBox1.Text, textBox3.Text);
			}
			else 
			{
				Log.Message("Неверный ввод!");
			}
		}

		public void exit_Click(object sender, EventArgs e)
		{
			this.superForm.Update();
			this.Close();
		}

		public void TasksCreate_Load(object sender, EventArgs e){}

		private bool IsCorrectInput()
		{
			int b;
			return (
					Int32.TryParse(textBox3.Text, out b) &&
					b > 0 &&
					textBox1.Text!="" &&
					textBox2.Text!="" 
			);
		}
	}
}
