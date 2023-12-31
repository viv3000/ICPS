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
	public partial class AttestationsDelete : Form
	{
		private AttestationsCRUD superForm;
		public AttestationsDelete(AttestationsCRUD superForm)
		{
			this.superForm = superForm;
			InitializeComponent();
		}
		public void delete_Click(object sender, EventArgs e)
		{
			Attestations.Delete(Int32.Parse(comboBox1.SelectedValue.ToString()));
			this.Update();
		}
		public void exit_Click(object sender, EventArgs e)
		{
			superForm.Update();
			this.Close();
		}
		public void AttestationsDelete_Load(object sender, EventArgs e)
		{
			this.Update();
		}
		public void Update()
		{
			comboBox1.DataSource = Attestations.ReadToList();
			comboBox1.DisplayMember = "Title";
			comboBox1.ValueMember = "Id";
		}
	}
}
