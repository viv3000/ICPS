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
	public partial class AttestationsCRUD : Form
	{
		public AttestationsCRUD()
		{
			InitializeComponent();
		}
		public void create_Click(object sender, EventArgs e)
		{
			new AttestationsCreate(this).ShowDialog();
		}
		public void update_Click(object sender, EventArgs e)
		{
			new AttestationsUpdate(this, new AttestationsChoose(this).ShowChooseDialog()).ShowDialog();
		}
		public void task_Click(object sender, EventArgs e)
		{
			new TasksCRUD(new AttestationsChoose(this).ShowChooseDialog()).ShowDialog();
		}
		public void delete_Click(object sender, EventArgs e)
		{
			new AttestationsDelete(this).ShowDialog();
		}
		public void exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		public void AttestationsCRUD_Load(object sender, EventArgs e)
		{
			this.Update();
		}
		public void Update()
		{
			this.dataGrid1.DataSource = Attestations.Read();
		}
	}
}
