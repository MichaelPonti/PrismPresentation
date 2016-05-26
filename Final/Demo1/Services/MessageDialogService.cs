using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Demo1.Services
{
	public class MessageDialogService : IMessageDialogService
	{
		public async Task<uint> ShowAsync(MessageDialogData d)
		{
			var messageDialog = new MessageDialog(d.Caption, d.Title);
			foreach(var b in d.Buttons)
			{
				messageDialog.Commands.Add(new UICommand(b.Label, null, b.CommandIndex));
			}

			messageDialog.DefaultCommandIndex = d.DefaultCommandIndex;
			messageDialog.CancelCommandIndex = d.CancelCommandIndex;

			var ret = await messageDialog.ShowAsync();
			return (uint) ret.Id;
		}
	}
}
