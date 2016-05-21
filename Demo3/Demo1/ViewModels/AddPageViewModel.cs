using Demo1.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.ViewModels
{
	public class AddPageViewModel : AppViewModelBase
	{
		public AddPageViewModel(IEventAggregator messageBus, IResourceLoader resourceLoader, 
			INavigationService navigationService, Services.ITodoStorageService storageService)
			: base(messageBus, resourceLoader, navigationService, storageService)
		{
		}


		private string _headline = null;
		[RestorableState]
		public string Headline
		{
			get { return _headline; }
			set
			{
				SetProperty<string>(ref _headline, value);
				UpdateCommandStatus();
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

		private DelegateCommand _commandAdd = null;
		public DelegateCommand CommandAdd
		{
			get
			{
				return _commandAdd ??
					(_commandAdd = new DelegateCommand(
						async () => 
						{
							string message = null;
							bool success = false;

							try
							{
								var model = CreateTodoModel();
								success = await TodoStorageService.AddAsync(model);
								if (!success)
									message = "error in storage service";
							}
							catch (Exception e)
							{
								message = e.ToString();
								success = false;
							}

							if (success)
							{
								ClearEditFields();
								NavigationService.Navigate(PageTokens.MainPage, null);
							}
							else
							{
								// notify user here
								Debug.WriteLine(message);
							}
						}, 
						() => !String.IsNullOrWhiteSpace(Headline)));
			}
		}

		private DelegateCommand _commandClear = null;
		public DelegateCommand CommandClear
		{
			get
			{
				return _commandClear ??
					(_commandClear = new DelegateCommand(() => ClearEditFields()));
			}
		}

		private void ClearEditFields()
		{
			Headline = null;
			AssignedTo = null;
			CreateDate = DateTime.Now;
			DueDate = null;
			IsComplete = false;
			Description = null;
		}

		private void UpdateCommandStatus()
		{
			CommandAdd.RaiseCanExecuteChanged();
			CommandClear.RaiseCanExecuteChanged();
		}

		private TodoModel CreateTodoModel()
		{
			DateTime? due = null;
			if (DueDate.HasValue)
				due = DueDate.Value.DateTime;
			return new TodoModel(
				0,
				Headline,
				Description,
				CreateDate.Value.DateTime,
				due,
				AssignedTo,
				IsComplete);
		}
	}
}
