using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo1.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ListPage : SessionStateAwarePage, INotifyPropertyChanged
	{
		public ListPage()
		{
			this.InitializeComponent();

			DataContextChanged += (s, e) =>
			{
				var propertyChanged = PropertyChanged;
				if (propertyChanged != null)
					propertyChanged(this, new PropertyChangedEventArgs(nameof(ConcreteDataContext)));
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public ViewModels.ListPageViewModel ConcreteDataContext
		{
			get { return DataContext as ViewModels.ListPageViewModel; }
		}


		/// <summary>
		/// strictly speaking, not really a pure mvvm implementation. however, since the view
		/// always knows about the viewmodel, this is a quick technique you can use pretty safely.
		/// You will always know hte type of object that is contained in the list view, so you 
		/// can cast the SelectedItem property to that object type. Then you can just call the
		/// appropriate business logic method for the item viewmodel. In this case the EditMe function.
		/// All the main logic is still in the viewmodel.
		/// FOr another option on handling this, look at the delete page viewmodel.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListView_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView lv = sender as ListView;
			if (lv != null)
			{
				ViewModels.TodoItemViewModel item = lv.SelectedItem as ViewModels.TodoItemViewModel;
				if (item != null)
				{
					item.EditMe();
				}
			}
		}
	}
}
