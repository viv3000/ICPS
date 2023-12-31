using System;
using System.Data;

using ICPS;
using ICPS.DB;

namespace ICPS.DB.CRUD
{
	public static class Subjects
	{
		public static int Create(int title)
		{
			return 0;
		}

		public static DataTable Read()
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			DataTable dt = command.ExecuteReaderToDataTable("select title as Предмет from subjects");
			connection.Close();
			return dt;
		}

		public static DataTable ReadToList()
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			DataTable dt = command.ExecuteReaderToDataTable("select subject_id as Id, title from subjects");
			connection.Close();
			return dt;
		}

		public static void Update(int id, int title){}

		public static void Delete(int id)
		{
			try
			{
				if (!HaveRelations(id))
				{
					DBConnection connection = new DBConnection();
					DBCommand command = new DBCommand(connection);
					command.ExecuteNonQuery($@"delete from subjects where subject_id = '{id}'");
					connection.Close();
					Log.Message("Запись удалена!");
				}
				else
				{
					Log.Debug(new Exception(), "Есть связанные запись! Удаление невозможно!");
				}
			}
			catch (Exception ex)
			{
				Log.Debug(ex, "Не удалось удалить запись!");
			}
		}

		private static bool HaveRelations(int id)
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			bool isRelationInSubjectsList = command.ExecuteScalar($"select * from subjects_list where subject_id = '{id}'")!=null;

			bool isRelationInAttestations = command.ExecuteScalar($"select * from attestations where subject_id = '{id}'")!=null;
			connection.Close();
			return isRelationInSubjectsList||isRelationInAttestations;
		}

	}
}
