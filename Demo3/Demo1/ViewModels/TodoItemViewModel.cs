using Prism.Commands;
using Prism.Events;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.ViewModels
{
	public class TodoItemViewModel : AppItemViewModelBase
	{
		public TodoItemViewModel()
			: base()
		{
		}

		public TodoItemViewModel(IEventAggregator messageBus)
			: base(messageBus)
		{
		}

		public TodoItemViewModel(IEventAggregator messageBus, INavigationService navService, Models.TodoModel src)
			: base(messageBus)
		{
			Headline = src.Headline;
			IsComplete = src.IsComplete;
			DueDate = src.DueDate;
			NavService = navService;
			Source = src;
		}

		public TodoItemViewModel(IEventAggregator messageBus, Services.ITodoStorageService storageService, Models.TodoModel src)
			: this(messageBus, (INavigationService) null, src)
		{
			StorageService = storageService;
		}


		public Models.TodoModel Source { get; private set; }
		public INavigationService NavService { get; private set; }
		public Services.ITodoStorageService StorageService { get; private set; }



		#region properties

		private string _headline = null;
		public string Headline
		{
			get { return _headline; }
			set { SetProperty<string>(ref _headline, value); }
		}

		private bool _isComplete = false;
		public bool IsComplete
		{
			get { return _isComplete; }
			set { SetProperty<bool>(ref _isComplete, value); }
		}

		private DateTime? _dueDate = null;
		public DateTime? DueDate
		{
			get { return _dueDate; }
			set { SetProperty<DateTime?>(ref _dueDate, value); }
		}


		#endregion


		public void EditMe()
		{
			NavService.Navigate(PageTokens.UpdatePage, Source.Id);
		}


		#region CommandDeleteMe implementation

		private DelegateCommand _commandDeleteMe = null;
		public DelegateCommand CommandDeleteMe
		{
			get
			{
				return _commandDeleteMe ??
					(_commandDeleteMe = new DelegateCommand(
						async () => 
						{
							int id = Source.Id;
							await StorageService.DeleteAsync(id);

							var reloadMessage = MessageBus.GetEvent<Messages.ReloadListMessage>();
							reloadMessage.Publish(new Messages.ReloadListMessageArgs(PageTokens.DeletePage, Messages.OperationEnum.Delete));
						}));
			}
		}

		#endregion
	}
}
