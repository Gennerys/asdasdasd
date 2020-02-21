using System;
using System.Threading.Tasks;
using PollCreator.Models;
using PollCreator.ViewModels;

namespace PollCreator.Interfaces.Services
{
	public interface IPollRepository 
	{
		Task Insert(PollPublishRequest entity);
		Task Update(PollPublishRequest entity);
		Task<int> SelectPollPk(string pollToken);

		Task<PollRenderViewModel> Select(string pollId);
	}
}
