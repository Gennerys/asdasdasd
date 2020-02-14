using Microsoft.AspNetCore.Mvc;

namespace PollCreator.ViewModels
{
	public class PollBuilderViewModel
	{
		[HiddenInput]
		public string PollId { get; set; }
		[HiddenInput]
		public string EditorToken { get; set; }
		[HiddenInput]
		public bool IsPublished { get; set; } = false;
	}
}
