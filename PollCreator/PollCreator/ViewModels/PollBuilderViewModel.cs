using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PollCreator.CustomAttributes;
using PollCreator.Models;

namespace PollCreator.ViewModels
{
	public class PollBuilderViewModel
	{
		public string Title { get; set; }
		[OptionList(2, 10)]
		public List<PollOptionDTO> Options { get; set; }

		public bool? IsSingleOption { get; set; } = false;

		[HiddenInput]
		public string PollId { get; set; }
		[HiddenInput]
		public string EditorToken { get; set; }
		[HiddenInput]
		public bool IsPublished { get; set; }

	}
}
