using Demo1.Services;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.ViewModels
{
	public abstract class AppViewModelBase : ViewModelBase
	{
		public IEventAggregator MessageBus { get; private set; } = null;
		public IResourceLoader ResourceLoader { get; private set; } = null;
		public INavigationService NavigationService { get; private set; }
		public ITodoStorageService TodoStorageService { get; private set; }


		public AppViewModelBase(IEventAggregator messageBus, IResourceLoader resourceLoader, INavigationService navigationService, ITodoStorageService storageService)
		{
			MessageBus = messageBus;
			ResourceLoader = resourceLoader;
			NavigationService = navigationService;
			TodoStorageService = storageService;
		}


		private Messages.NavigationMessage _navigateMessage = null;
		protected void NavigateToPage(string pageToken, object navParams = null)
		{
			if (_navigateMessage == null)
				_navigateMessage = MessageBus.GetEvent<Messages.NavigationMessage>();

			NavigationService.Navigate(pageToken, navParams);
			_navigateMessage.Publish(new Messages.NavigationMessageArgs(pageToken, navParams));
		}


		private bool _isActive = false;
		public bool IsActive
		{
			get { return _isActive; }
			set { SetProperty<bool>(ref _isActive, value); }
		}


		//private Messages.UpdateNavigationMessage _updateNavigationMessage = null;

		//public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
		//{
		//	base.OnNavigatedTo(e, viewModelState);
		//	string pageToken = e.SourcePageType.Name;
		//	pageToken = pageToken.Substring(0, pageToken.Length - ("Page").Length);
		//	if (_updateNavigationMessage == null)
		//		_updateNavigationMessage = MessageBus.GetEvent<Messages.UpdateNavigationMessage>();
		//	_updateNavigationMessage.Publish(pageToken);
		//}
	}
}
