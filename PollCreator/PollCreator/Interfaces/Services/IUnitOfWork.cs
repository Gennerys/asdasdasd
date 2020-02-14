using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollCreator.Interfaces.Services
{
	public interface IUnitOfWork
	{
		//interface properties
		int SaveChanges();
	}
}
