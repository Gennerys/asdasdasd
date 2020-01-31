using System.Collections.Generic;

namespace PollCreator.Models
{
	public class Poll
	{
		public string Title { get; set; }
		public  List<string> Options = new List<string>();
		public bool IsSingleOption { get; set; }
		public bool IsPublished { get; set; } = false;
		public string PollId { get; set; }
		public string EditorToken { get; set; }
	}
}
