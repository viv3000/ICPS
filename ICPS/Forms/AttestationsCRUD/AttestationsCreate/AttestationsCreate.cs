using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICPS;
using ICPS.DB;
using ICPS.DB.CRUD;

namespace ICPS.Forms
{
	public partial class AttestationsCreate : Form
	{
		private AttestationsCRUD superForm;
		public AttestationsCreate(AttestationsCRUD superForm)
		{
			this.superForm = superForm;
			InitializeComponent();
		}
		public void create_Click(object sender, EventArgs e)
		{
			if (IsCorrectInput()){
				Attestations.Create(
					Auth.TeacherId, Int32.Parse(comboBox1.SelectedValue.ToString()), Int32.Parse(comboBox2.SelectedValue.ToString()), 
					textBox1.Text, textBox2.Text, ToMySqlDate(dateTimePicker1.Value), 
					Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text), Int32.Parse(textBox5.Text));
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
		public void AttestationsCreate_Load(object sender, EventArgs e)
		{
			this.Update();
		}
		public void Update()
		{
			comboBox1.DataSource = Subjects.ReadToList();
			comboBox1.DisplayMember = "title";
			comboBox1.ValueMember = "Id";
			comboBox2.DataSource = Classes.ReadToList();
			comboBox2.DisplayMember = "title";
			comboBox2.ValueMember = "Id";
		}

		private bool IsCorrectInput()
		{
			int b3, b4, b5;
			return (
					Int32.TryParse(textBox3.Text, out b3) &&
					Int32.TryParse(textBox4.Text, out b4) &&
					Int32.TryParse(textBox5.Text, out b5) &&
					b3 < b4 &&
					b4 < b5 &&
					IsCorrectPercentage(b3) &&
					IsCorrectPercentage(b4) &&
					IsCorrectPercentage(b5) &&
					textBox1.Text!="" &&
					textBox2.Text!="" 
					);
		}

		private bool IsCorrectPercentage(int count)
		{
			return count >= 0 && count <= 100;
		}

		private string ToMySqlDate(DateTime date)
		{
			return $"{date.Year}-{date.Month}-{date.Day}";
		}
	}
}
