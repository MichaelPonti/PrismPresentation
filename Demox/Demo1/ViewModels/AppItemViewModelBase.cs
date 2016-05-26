using Prism.Events;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.ViewModels
{
	public abstract class AppItemViewModelBase : ViewModelBase
	{
		public IEventAggregator MessageBus { get; private set; } = null;

		public AppItemViewModelBase()
		{
		}

		public AppItemViewModelBase(IEventAggregator messageBus)
		{
			MessageBus = messageBus;
		}



		protected virtual void OnIsChecked(bool newValue)
		{
		}

		protected virtual void OnIsExpanded(bool newValue)
		{
		}

		protected virtual void OnIsSelected(bool newValue)
		{
		}


		#region properties

		private bool _isChecked = false;
		public bool IsChecked
		{
			get { return _isChecked; }
			set
			{
				SetProperty<bool>(ref _isChecked, value);
				OnIsChecked(_isChecked);
			}
		}

		private bool _isExpanded = false;
		public bool IsExpanded
		{
			get { return _isExpanded; }
			set
			{
				SetProperty<bool>(ref _isExpanded, value);
				OnIsExpanded(_isExpanded);
			}
		}


		private bool _isSelected = false;
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				SetProperty<bool>(ref _isSelected, value);
				OnIsSelected(_isSelected);
			}
		}

		#endregion
	}
}
