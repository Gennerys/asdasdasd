using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PollCreator.Interfaces.Services;
using PollCreator.Models;
using PollCreator.ViewModels;

namespace PollCreator.DataAccess
{
	public class PollRepository : IPollRepository
	{
		private readonly IDataConnection _dataConnection;

		public PollRepository(IDataConnection dataConnection)
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
		public async Task<PollRenderViewModel> Select(string pollId)
		{
			PollRenderViewModel pollRenderViewModel = new PollRenderViewModel();
			using (var command = await CreateCommand())
			{
				try
				{
					StringBuilder sqlText = new StringBuilder();
					sqlText.Append(
						"Select title, is_single_option, is_published, poll_id, editor_token From ct_poll ");
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
								if (reader.GetValue(0).ToString() == "" || reader.GetValue(1).ToString() == "")
								{
									return null;
								}

								else
								{
									pollRenderViewModel.Title = reader.GetString(0);
									pollRenderViewModel.IsSingleOption = reader.GetBoolean(1);
									pollRenderViewModel.IsPublished = reader.GetBoolean(2);
								}
							}
						}
						else
						{
							return null;
						}

					}

					command.Transaction.Commit();
				}
				catch (SqlException)
				{
					command.Transaction.Rollback();
				}
			}
			return pollRenderViewModel;
		}


		public async Task Insert(PollPublishRequest poll)
		{
			using (var command = await CreateCommand())
			{
				try
				{
					StringBuilder sqlText = new StringBuilder();
					sqlText.Append(
						"Insert Into ct_poll (title, is_single_option, is_published, poll_id, editor_token) ");
					sqlText.Append(
						"Values (@title,@is_single_option,@is_published,@poll_id,@editor_token) ");

					command.CommandType = CommandType.Text;
					command.CommandText = sqlText.ToString();
					SqlParameter titleParameter = new SqlParameter
					{
						ParameterName = "@title",
						SqlDbType = SqlDbType.NVarChar,
						IsNullable = true,
						Direction = ParameterDirection.Input,
						Value = (object)poll.Title ?? DBNull.Value
					};
					SqlParameter isSingleOptionParameter = new SqlParameter
					{
						ParameterName = "@is_single_option",
						SqlDbType = SqlDbType.Bit,
						IsNullable = true,
						Direction = ParameterDirection.Input,
						Value = (object)poll.IsSingleOption ?? DBNull.Value
					};
					command.Parameters.Add(titleParameter);
					command.Parameters.Add(isSingleOptionParameter);
					command.Parameters.AddWithValue("@is_published", poll.IsPublished);
					command.Parameters.AddWithValue("@poll_id", poll.PollId);
					command.Parameters.AddWithValue("@editor_token", poll.EditorToken);
					await command.ExecuteNonQueryAsync();
					command.Transaction.Commit();
				}
				catch (SqlException)
				{
					command.Transaction.Rollback();
				}
			}

		}

		public async Task Update(PollPublishRequest poll)
		{
			using (var command = await CreateCommand())
			{
				try
				{
					StringBuilder sqlText = new StringBuilder();
					sqlText.Append(
						"Update ct_poll SET title = @title,  is_single_option = @is_single_option, is_published = @is_published, poll_id = @poll_id, editor_token = @editor_token ");
					sqlText.Append("Where poll_id = @poll_id");

					command.CommandType = CommandType.Text;
					command.CommandText = sqlText.ToString();
					command.Parameters.AddWithValue("@title", poll.Title);
					command.Parameters.AddWithValue("@is_single_option", poll.IsSingleOption);
					command.Parameters.AddWithValue("@is_published", poll.IsPublished);
					command.Parameters.AddWithValue("@poll_id", poll.PollId);
					command.Parameters.AddWithValue("@editor_token", poll.EditorToken);
					await	command.ExecuteNonQueryAsync();
					command.Transaction.Commit();
				}
				catch (SqlException)
				{
					command.Transaction.Rollback();
				}
			}
		}

		public async Task<int> SelectPollPk(string pollToken)
		{
			int primaryKey = 0;
			using (var command = await CreateCommand())
			{
				try
				{
					StringBuilder sqlText = new StringBuilder();
					sqlText.Append(
						"Select PK_poll_id From ct_poll ");
					sqlText.Append("Where poll_id = @poll_id");
					command.CommandType = CommandType.Text;
					command.CommandText = sqlText.ToString();
					command.Parameters.AddWithValue("@poll_id", pollToken);
					primaryKey = Convert.ToInt32(await command.ExecuteScalarAsync());

					command.Transaction.Commit();
				}
				catch (SqlException)
				{
					command.Transaction.Rollback();
				}
			}

			return primaryKey;
		}
	}
}
