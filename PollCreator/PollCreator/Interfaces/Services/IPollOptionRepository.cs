using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PollCreator.Models;

namespace PollCreator.Interfaces.Services
{
	public interface IPollOptionRepository 
	{
		Task Insert(List<PollOptionDTO> options, int pollId);
		Task <List<PollOptionDTO>> Select(int pollId);
	}
}
