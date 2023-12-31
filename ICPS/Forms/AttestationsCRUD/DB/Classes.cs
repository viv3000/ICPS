using System;
using System.Data;

using ICPS;
using ICPS.DB;

namespace ICPS.DB.CRUD
{
	public static class Classes
	{
		public static int Create(int title)
		{
			return 0;
		}

		public static DataTable Read()
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			DataTable dt = command.ExecuteReaderToDataTable("select title as Класс from classes");
			connection.Close();
			return dt;
		}

		public static DataTable ReadToList()
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			DataTable dt = command.ExecuteReaderToDataTable("select class_id as Id, title from classes");
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
					command.ExecuteNonQuery($@"delete from classes where class_id = '{id}'");
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
			bool isRelationInClassesList = command.ExecuteScalar($"select * from classes_list where class_id = '{id}'")!=null;

			bool isRelationInAttestations = command.ExecuteScalar($"select * from attestations where class_id = '{id}'")!=null;
			bool isRelationInStudents = command.ExecuteScalar($"select * from students where class_id = '{id}'")!=null;
			connection.Close();
			return isRelationInClassesList||isRelationInAttestations||isRelationInStudents;
		}

	}
}
