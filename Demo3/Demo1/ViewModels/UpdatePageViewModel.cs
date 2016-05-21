using Demo1.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.ViewModels
{
	public class UpdatePageViewModel : AppViewModelBase
	{
		private Services.IMessageDialogService _dialogService = null;

		public UpdatePageViewModel(IEventAggregator messageBus, IResourceLoader resourceLoader, 
			INavigationService navigationService, ITodoStorageService storageService,
			Services.IMessageDialogService dialogService)
			: base(messageBus, resourceLoader, navigationService, storageService)
		{
			_dialogService = dialogService;
		}


		#region properties

		private int? _id = null;
		public int? Id
		{
			get { return _id; }
			set
			{
				SetProperty<int?>(ref _id, value);
				UpdateButtonStatus();
			}
		}

		private string _headline = null;
		public string Headline
		{
			get { return _headline; }
			set
			{
				SetProperty<string>(ref _headline, value);
				UpdateButtonStatus();
			}
		}

		private string _assignedTo = null;
		public string AssignedTo
		{
			get { return _assignedTo; }
			set { SetProperty<string>(ref _assignedTo, value); }
		}

		private DateTimeOffset? _createDate = new DateTimeOffset(DateTime.Now);
		public DateTimeOffset? CreateDate
		{
			get { return _createDate; }
			set { SetProperty<DateTimeOffset?>(ref _createDate, value); }
		}

		private DateTimeOffset? _dueDate = new DateTimeOffset(DateTime.Now);
		public DateTimeOffset? DueDate
		{
			get { return _dueDate; }
			set { SetProperty<DateTimeOffset?>(ref _dueDate, value); }
		}

		private bool _isComplete = false;
		public bool IsComplete
		{
			get { return _isComplete; }
			set { SetProperty<bool>(ref _isComplete, value); }
		}

		private string _description = null;
		public string Description
		{
			get { return _description; }
			set { SetProperty<string>(ref _description, value); }
		}

		#endregion


		public async override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
		{
			base.OnNavigatedTo(e, viewModelState);
			if (e.Parameter == null)
				return;

			int id = (int) e.Parameter;
			await LoadItemDetails(id);
		}


		private async Task LoadItemDetails(int id)
		{
			var m = await TodoStorageService.GetAsync(id);
			if (m == null)
			{

			}
			else
			{
				Id = m.Id;
				Headline = m.Headline;
				AssignedTo = m.AssignedTo;
				CreateDate = m.CreateDate;
				DueDate = m.DueDate;
				Description = m.Description;
				IsComplete = m.IsComplete;
			}
		}

		#region CommandSave implementation

		private DelegateCommand _commandSave = null;
		public DelegateCommand CommandSave
		{
			get
			{
				return _commandSave ??
					(_commandSave = new DelegateCommand(
						async () => 
						{
							DateTime? dd;
							if (DueDate.HasValue)
								dd = DueDate.Value.DateTime;
							else
								dd = null;
							var m = new Models.TodoModel(
								Id.Value, 
								Headline, 
								Description, 
								CreateDate.Value.DateTime,
								dd, 
								AssignedTo, 
								IsComplete);

							if (!await TodoStorageService.UpdateAsync(m))
							{
								/// error notification here
								/// 
								var msgd = new Services.MessageDialogData
								{
									Title = "There was an error",
									Caption = "Failed to update the data in the list",
									CancelCommandIndex = 1,
									DefaultCommandIndex = 0,
								};
								msgd.Buttons.Add(new MessageDialogButton("OK", 0));
								msgd.Buttons.Add(new MessageDialogButton("Cancel", 1));
								var ret = await _dialogService.ShowAsync(msgd);
							}
							else
							{
								NavigationService.Navigate(PageTokens.ListPage, null);
							}
						},
						() => 
						{
							return (Id == null || String.IsNullOrWhiteSpace(Headline))
							? false
							: true;
						}));
			}
		}
		#endregion


		private void UpdateButtonStatus()
		{
			CommandSave.RaiseCanExecuteChanged();
		}
	}
}
