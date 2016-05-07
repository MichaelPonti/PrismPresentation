using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Demo1.ViewModels
{
	public class MenuItemViewModel : AppItemViewModelBase
	{
		public string DisplayName { get; set; }
		public string FontIcon { get; set; }
		public ICommand Command { get; set; }
		public string PageToken { get; set; }

		public override string ToString()
		{
			return DisplayName;
		}


		public MenuItemViewModel()
		{
		}

		public MenuItemViewModel(string pageToken, string displayName, string fontIcon, ICommand command)
		{
			PageToken = pageToken;
			DisplayName = displayName;
			FontIcon = fontIcon;
			Command = command;
		}
	}
}
