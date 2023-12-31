using System;
using System.Data;
using MySqlConnector;

using ICPS;
using ICPS.DB;

namespace ICPS.DB.CRUD
{
	public static class Tasks
	{
		public static void Create(
				int attestationId, string skill, string title, string maxPoint)
		{
			try
			{
				DBConnection connection = new DBConnection();
				DBCommand command = new DBCommand(connection);
				command.ExecuteReaderToDataTable($@"INSERT INTO `tasks` 
		   (`task_id`,        `attestation_id`,  `skill`,   `title`,   `max_point`) 
	VALUES (null,             '{attestationId}', '{skill}', '{title}', '{maxPoint}'); ");
				connection.Close();
				Log.Message("Запись добавленна!");
			}
			catch (Exception ex)
			{
				Log.Debug(ex, "Не удалось добавить запись!");
			}
		}

		public static DataTable Read(int attestationId)
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			DataTable dt = command.ExecuteReaderToDataTable($@"
					select title as Название, skill as Навык, max_point as Баллов
					from tasks
					where attestation_id = {attestationId}
			");
			connection.Close();
			return dt;
		}

		public static object[] Read(int attestationId, int id)
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			MySqlDataReader reader = command.ExecuteReader($@"
					select *
					from tasks
					where attestation_id = '{attestationId}'
					and task_id = '{id}'
			");
			reader.Read();
			object[] data = new object[] {reader[2], reader[3], reader[4]};
			connection.Close();
			return data;
		}

		public static DataTable ReadToList(int attestationId)
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			DataTable dt = command.ExecuteReaderToDataTable($@"
					select task_id as Id, CONCAT(title, ' | ', skill, ' | ', max_point) as Title
					from tasks 
					where attestation_id = '{attestationId}';
			");
			connection.Close();
			return dt;
		}

		public static void Update(
				int id, 
				int attestationId, string skill, string title, string maxPoint)

		{
			try
			{
				DBConnection connection = new DBConnection();
				DBCommand command = new DBCommand(connection);
					command.ExecuteReaderToDataTable($@"update tasks
set `attestation_id` = '{attestationId}',  `skill` = '{skill}',  `title` = '{title}', `max_point` = '{maxPoint}'
where task_id = '{id}'
");	
				connection.Close();
				Log.Message("Запись изменена!");
			}
			catch (Exception ex)
			{
				Log.Debug(ex, "Не удалось изменить запись!");
			}
			
		}

		public static void Delete(int id)
		{
			try
			{
				if (!HaveRelations(id))
				{
					DBConnection connection = new DBConnection();
					DBCommand command = new DBCommand(connection);
					command.ExecuteNonQuery($@"delete from tasks where task_id = '{id}'");
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
			bool isRelationInResults = command.ExecuteScalar($"select * from task_results where task_id = '{id}'")!=null;
			connection.Close();
			return isRelationInResults;
		}

	}
}
