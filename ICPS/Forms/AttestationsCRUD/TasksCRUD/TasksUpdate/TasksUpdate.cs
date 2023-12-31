using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICPS;
using ICPS.DB.CRUD;

namespace ICPS.Forms
{
	public partial class TasksUpdate : Form
	{
		private TasksCRUD superForm;
		private int attestationId;
		private int id;

		public TasksUpdate(TasksCRUD superForm, int attestationId, int id)
		{
			this.superForm = superForm;
			this.attestationId = attestationId;
			this.id = id;
			InitializeComponent();
		}

		public void update_Click(object sender, EventArgs e)
		{
			if (IsCorrectInput()){
				Tasks.Update(
					id,
					attestationId, textBox2.Text, textBox1.Text, textBox3.Text);
				this.superForm.Update();
				this.Close();
			}
			else 
			{
				Log.Message("Неверный ввод!");
			}
		}


		public void TasksUpdate_Load(object sender, EventArgs e)
		{
			this.Update();
		}

		public void exit_Click(object sender, EventArgs e)
		{
			this.superForm.Update();
			this.Close();
		}

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

		public void Update()
		{
			object[] data = Tasks.Read(attestationId, id);

			textBox1.Text = data[1].ToString();
			textBox2.Text = data[0].ToString();
			textBox3.Text = data[2].ToString();
		}
	}
}
