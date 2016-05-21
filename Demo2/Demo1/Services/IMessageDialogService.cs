using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Demo1.Services
{
	public class MessageDialogButton
	{
		public string Label { get; set; }
		public uint CommandIndex { get; set; }


		public MessageDialogButton()
		{
		}

		public MessageDialogButton(string label, uint commandIndex)
		{
			Label = label;
			CommandIndex = commandIndex;

		}
	}

	public sealed class MessageDialogData
	{
		public string Title { get; set; }
		public string Caption { get; set; }
		public List<MessageDialogButton> Buttons { get; set; } = new List<MessageDialogButton>();
		public uint DefaultCommandIndex { get; set; }
		public uint CancelCommandIndex { get; set; }
	}



	public interface IMessageDialogService
	{
		Task<uint> ShowAsync(MessageDialogData d);
	}
}
