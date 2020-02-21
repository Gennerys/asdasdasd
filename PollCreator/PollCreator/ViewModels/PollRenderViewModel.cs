using System.Collections.Generic;
using PollCreator.Models;

namespace PollCreator.ViewModels
{
	public class PollRenderViewModel
	{
		public string Title { get; set; }
		public List<PollOptionDTO> Options { get; set; }
		public bool IsSingleOption { get; set; }
		public bool IsPublished { get; set; }
	}
}
