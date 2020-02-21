using Microsoft.Data.SqlClient;
using PollCreator.Interfaces.Services;
using PollCreator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PollCreator.DataAccess
{
	public class PollOptionRepository : IPollOptionRepository
	{
		private readonly IDataConnection _dataConnection;

		public PollOptionRepository(IDataConnection dataConnection)
		{
			_dataConnection = dataConnection;
		}

		private async Task<SqlCommand> CreateCommand()
		{
			var connection = await _dataConnection.GetConnection();
			var command = connection.CreateCommand();
			command.Transaction = connection.BeginTransaction();

			return command;
		}

		public async Task Insert(List<PollOptionDTO> options, int pollId)
		{
			SqlCommand command = new SqlCommand();
		
			try
			{
				StringBuilder sqlText = new StringBuilder();
				foreach (var option in options)
				{
					sqlText.Clear();
					sqlText.Append("Insert Into ct_poll_option (serial_number, value, poll_id) ");
					sqlText.Append("Values (@serial_number, @value, @poll_id)");

					using (command = await CreateCommand())
					{
						command.CommandType = CommandType.Text;
						command.CommandText = sqlText.ToString();
						command.Parameters.AddWithValue("@serial_number", option.SerialNumber);
						command.Parameters.AddWithValue("@value", option.Value);
						command.Parameters.AddWithValue("@poll_id", pollId);
						await command.ExecuteNonQueryAsync();
						command.Transaction.Commit();
					}

				}
			}
			catch (SqlException)
			{
				command.Transaction.Rollback();
			}
		}

		public async Task<List<PollOptionDTO>> Select(int pollId)
		{
			List<PollOptionDTO> options = new List<PollOptionDTO>();
			using (var command = await CreateCommand())
			{
				try
				{
					StringBuilder sqlText = new StringBuilder();
					sqlText.Append(
						"Select serial_number, value From ct_poll_option ");
					sqlText.Append("Where poll_id = @poll_id");

					command.CommandType = CommandType.Text;
					command.CommandText = sqlText.ToString();
					command.Parameters.AddWithValue("@poll_id", pollId);
					using (var reader = await command.ExecuteReaderAsync())
					{

						if (reader.HasRows)
						{
							while (reader.Read())
							{
								options.Add(new PollOptionDTO
								{
									SerialNumber = reader.GetInt32(0),
									Value = reader.GetString(1)
								});
							}
						}

					}

					command.Transaction.Commit();
				}
				catch (SqlException)
				{
					command.Transaction.Rollback();
				}
			}

			return options;
		}
	}
}
