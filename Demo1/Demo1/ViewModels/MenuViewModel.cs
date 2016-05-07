using Prism.Commands;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.ViewModels
{
	public class MenuViewModel : AppViewModelBase
	{
		public MenuViewModel(IEventAggregator messageBus, IResourceLoader resourceLoader, INavigationService navigationService)
			: base(messageBus, resourceLoader, navigationService, null)
		{
			InitializeCommands();
			InitializeSecondaryCommands();


			var msg = MessageBus.GetEvent<Messages.UpdateNavigationMessage>();
			msg.Subscribe((a) =>
			{
				UpdateButtonStatus(a);
			});
		}

		public ObservableCollection<MenuItemViewModel> Commands { get; private set; } = new ObservableCollection<MenuItemViewModel>();
		public ObservableCollection<MenuItemViewModel> SecondaryCommands { get; private set; } = new ObservableCollection<MenuItemViewModel>();


		private void InitializeCommands()
		{
			Commands.Add(new MenuItemViewModel(
				PageTokens.MainPage,
				ResourceLoader.GetString("HM_MainMenuDisplayName"),
				"\ue7c1",
				new DelegateCommand(
					() => NavigateInternal(PageTokens.MainPage),
					() => CanNavigateToInternal(PageTokens.MainPage))));

			Commands.Add(new MenuItemViewModel(
				PageTokens.ListPage,
				ResourceLoader.GetString("HM_ListMenuDisplayName"),
				"\ue133",
				new DelegateCommand(
					() => NavigateInternal(PageTokens.ListPage),
					() => CanNavigateToInternal(PageTokens.ListPage))));

			Commands.Add(new MenuItemViewModel(
				PageTokens.AddPage,
				ResourceLoader.GetString("HM_AddMenuDisplayName"),
				"\ue710",
				new DelegateCommand(
					() => NavigateInternal(PageTokens.AddPage),
					() => CanNavigateToInternal(PageTokens.AddPage))));

			Commands.Add(new MenuItemViewModel(
				PageTokens.DeletePage,
				ResourceLoader.GetString("HM_DeleteMenuDisplayName"),
				"\ue74d",
				new DelegateCommand(
					() => NavigateInternal(PageTokens.DeletePage),
					() => CanNavigateToInternal(PageTokens.DeletePage))));

			Commands.Add(new MenuItemViewModel(
				PageTokens.UpdatePage,
				ResourceLoader.GetString("HM_UpdateMenuDisplayName"),
				"\ue70f",
				new DelegateCommand(
					() => NavigateInternal(PageTokens.UpdatePage),
					() => CanNavigateToInternal(PageTokens.UpdatePage))));


		}


		private void InitializeSecondaryCommands()
		{
			SecondaryCommands.Add(new MenuItemViewModel(
				PageTokens.SettingsPage,
				ResourceLoader.GetString("HM_SettingsMenuDisplayName"),
				"\ue713",
				new DelegateCommand(
					() => NavigateInternal(PageTokens.SettingsPage),
					() => CanNavigateToInternal(PageTokens.SettingsPage))));

			SecondaryCommands.Add(new MenuItemViewModel(
				PageTokens.AboutPage,
				ResourceLoader.GetString("HM_AboutMenuDisplayName"),
				"\uE946",
				new DelegateCommand(
					() => NavigateInternal(PageTokens.AboutPage),
					() => CanNavigateToInternal(PageTokens.AboutPage))));
		}



		private void RaiseCanExecuteChanged()
		{
			foreach (var c in Commands)
				((DelegateCommand) c.Command).RaiseCanExecuteChanged();

			foreach (var c in SecondaryCommands)
				((DelegateCommand) c.Command).RaiseCanExecuteChanged();
		}


		private string _currentPageToken = PageTokens.MainPage;

		private void NavigateInternal(string pageToken, object navParameter = null)
		{
			NavigationService.Navigate(pageToken, navParameter);
			UpdateButtonStatus(pageToken);
		}

		private bool CanNavigateToInternal(string pageToken)
		{
			return (pageToken != _currentPageToken);
		}

		private void UpdateButtonStatus(string pageToken)
		{
			_currentPageToken = pageToken;
			RaiseCanExecuteChanged();
		}
	}
}
