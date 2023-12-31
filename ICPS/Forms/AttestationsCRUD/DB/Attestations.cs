using System;
using System.Data;
using MySqlConnector;

using ICPS;
using ICPS.DB;

namespace ICPS.DB.CRUD
{
	public static class Attestations
	{
		public static void Create(
				int teacherId, int subjectId, int classId, 
				string theme, string type, string date, 
				int minimum3, int minimum4, int minimum5)
		{
			try
			{
				DBConnection connection = new DBConnection();
				DBCommand command = new DBCommand(connection);
				command.ExecuteReaderToDataTable($@"INSERT INTO `intermediate_certification_processing_system`.`attestations` 
	(`attestation_id`, `teacher_id`, `subject_id`, `class_id`, `theme`, `type`, `date`, `minimum_percentage_for_5`, `minimum_percentage_for_4`, `minimum_percentage_for_3`) 
	VALUES (null, '{teacherId}', '{subjectId}', '{classId}', '{theme}', '{type}', '{date}', '{minimum5.ToString()}', '{minimum4.ToString()}', '{minimum3.ToString()}'); ");
				connection.Close();
				Log.Message("Запись добавленна!");
			}
			catch (Exception ex)
			{
				Log.Debug(ex, "Не удалось добавить запись!");
			}
		}

		public static DataTable Read()
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			DataTable dt = command.ExecuteReaderToDataTable(@"
					select attestations.theme as Тема, attestations.date as Дата, classes.title as Класс
					from attestations, classes
					where attestations.class_id = classes.class_id
			");
			connection.Close();
			return dt;
		}

		public static object[] Read(int id)
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			MySqlDataReader reader = command.ExecuteReader($@"
					select *
					from attestations
					where attestations.attestation_id = '{id}'
			");
			reader.Read();
			object[] data = new object[] {reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], reader[9]};
			connection.Close();
			return data;
		}

		public static DataTable ReadToList()
		{
			DBConnection connection = new DBConnection();
			DBCommand command = new DBCommand(connection);
			DataTable dt = command.ExecuteReaderToDataTable(@"
					select attestation_id as Id, CONCAT(theme, ' | ', date, ' | ', classes.title) as Title
					from attestations, classes where classes.class_id = attestations.class_id;
			");
			connection.Close();
			return dt;
		}

		public static void Update(
				int id, 
				int teacherId, int subjectId, int classId, 
				string theme, string type, string date, 
				int minimum3, int minimum4, int minimum5)
		{
			try
			{
				DBConnection connection = new DBConnection();
				DBCommand command = new DBCommand(connection);
					command.ExecuteReaderToDataTable($@"update intermediate_certification_processing_system.attestations
set `teacher_id` = '{teacherId}',  `subject_id` = '{subjectId}',  `class_id` = '{classId}', 
 `theme` = '{theme}',  `type` = '{type}',  `date` = '{date}', 
 `minimum_percentage_for_5` = '{minimum5.ToString()}', 
 `minimum_percentage_for_4` = '{minimum4.ToString()}', 
 `minimum_percentage_for_3` = '{minimum3.ToString()}'
where attestation_id = '{id}'
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
					command.ExecuteNonQuery($@"delete from attestations where attestation_id = '{id}'");
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
			bool isRelationInTasks = command.ExecuteScalar($"select * from tasks where attestation_id = '{id}'")!=null;

			bool isRelationInResults = command.ExecuteScalar($"select * from attestation_results where attestation_id = '{id}'")!=null;
			connection.Close();
			return isRelationInTasks||isRelationInResults;
		}

	}
}
