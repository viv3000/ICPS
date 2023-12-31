using System;
using System.Data;

using System.Windows.Forms;

using MySqlConnector;

namespace ICPS.DB
{
	class DBConnection
	{
		//for home
		private const string _connectionString = "server=127.0.0.1;user=vi;password=dbrnjh;port=3306;database=intermediate_certification_processing_system;";
		//for college
		//public string ConnectionString{get;} = "server=localhost;user=root;database=intermediate_certification_processing_system;password=qwerty;";

		public MySqlConnection Connection { get; private set; }

		public DBConnection()
		{
			Connection = new MySqlConnection(_connectionString);
			Connection.Open();
		}

		public void Close() 
		{
			this.Connection.Close();
		}
	}
}
