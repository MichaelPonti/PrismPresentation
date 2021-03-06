﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;

namespace Demo1.ViewModels
{
	public class AboutPageViewModel : AppViewModelBase
	{
		public AboutPageViewModel(IEventAggregator messageBus, IResourceLoader resourceLoader, INavigationService navigationService)
			: base(messageBus, resourceLoader, navigationService, null)
		{
			/// don't need the storage service in here so we just don't bother with
			/// it.
			/// 

			Title = "This is the About Page";
		}


		private string _title = null;
		public string Title
		{
			get { return _title; }
			set { SetProperty<string>(ref _title, value); }
		}
	}
}
