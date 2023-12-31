using System;
using System.Data;

using MySqlConnector;

namespace ICPS.DB
{
    class DBCommand
    {
        private DBConnection _connection;

        public DBCommand(DBConnection connection)
        {
            this._connection = connection;
        }

        public int ExecuteNonQuery(string query)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(query, _connection.Connection);
            return mySqlCommand.ExecuteNonQuery();
        }

        public MySqlDataReader ExecuteReader(string query)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(query, _connection.Connection);
            return mySqlCommand.ExecuteReader();
        }

        public object ExecuteScalar(string query)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(query, _connection.Connection);
            return mySqlCommand.ExecuteScalar();
        }

        public DataTable ExecuteReaderToDataTable(string query)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(query, _connection.Connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = mySqlCommand;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        ~DBCommand()
        {
            _connection.Close();
        }
    }
}
