using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;

namespace Demo1.ViewModels
{
	public class MainPageViewModel : AppViewModelBase
	{
		public MainPageViewModel(IEventAggregator messageBus, IResourceLoader resourceLoader,
			INavigationService navigationService)
			: base(messageBus, resourceLoader, navigationService, null)
		{
		}

		private string _testText = "test text";
		public string TestText
		{
			get { return _testText; }
			set { SetProperty<string>(ref _testText, value); }
		}
	}
}
