using Demo1.Services;
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
	public class ListPageViewModel : AppViewModelBase
	{
		public ListPageViewModel(IEventAggregator messageBus, IResourceLoader resourceLoader, 
			INavigationService navigationService, ITodoStorageService storageService)
			: base(messageBus, resourceLoader, navigationService, storageService)
		{
		}

		#region properties

		private ObservableCollection<TodoItemViewModel> _todoItems = new ObservableCollection<TodoItemViewModel>();
		public ObservableCollection<TodoItemViewModel> TodoItems
		{
			get { return _todoItems; }
			set { SetProperty<ObservableCollection<TodoItemViewModel>>(ref _todoItems, value); }
		}


		private TodoItemViewModel _selectedItem = null;
		public TodoItemViewModel SelectedItem
		{
			get { return _selectedItem; }
			set { SetProperty<TodoItemViewModel>(ref _selectedItem, value); }
		}

		#endregion

		public async override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
		{
			base.OnNavigatedTo(e, viewModelState);
			await LoadItemsAsync();
		}


		public async Task LoadItemsAsync()
		{
			TodoItems.Clear();

			SelectedItem = null;
			var items = await TodoStorageService.GetListAsync();

			foreach(var item in items)
			{
				TodoItems.Add(new TodoItemViewModel(MessageBus, NavigationService, item));
			}
		}
	}
}
